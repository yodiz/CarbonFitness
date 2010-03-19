//Copyright 2009 - InRAD, LLC
//All Rights Reserved.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CarbonFitness.App.Web.FusionCharts {
	/// <summary>
	/// Builds a chart.
	/// </summary>
	/// <author>MBH</author>
	/// <dateAuthored>11/10/09</dateAuthored>
	public abstract class FusionChartBuilder<T> {
		#region Private Fields

		/// <summary>
		/// Url to the chart flash file.
		/// </summary>
		private readonly string mChartUrl;

		/// <summary>
		/// The HtmlHelper.
		/// </summary>
		private readonly HtmlHelper mHelper;

		/// <summary>
		/// Delegate used to get chart values for items.
		/// </summary>
		private readonly Func<T, double> mValueExtractor;

		private int labelIndex;


		/// <summary>
		/// The chart caption.
		/// </summary>
		private string mCaption;

		/// <summary>
		/// The ID given to the chart in the DOM.
		/// </summary>
		private string mChartId;

		/// <summary>
		/// An enumerable array of colors to use for coloring items in a chart.
		/// </summary>
		private IEnumerator<string> mColors;

		/// <summary>
		/// Flag to control debug mode in Fusion Charts.
		/// </summary>
		private bool mDebugEnabled;

		/// <summary>
		/// The decimal precision.
		/// </summary>
		private int mDecimalPrecision = int.MinValue;

		/// <summary>
		/// Builds the label that will be shown when the user hovers over a chart element.
		/// </summary>
		private Func<T, string> mHoverLabelBuilder;

		/// <summary>
		/// Delegate used to get labels for chart items.
		/// </summary>
		private Func<T, string> mLabeler;

		/// <summary>
		/// Delegate used to get drill-down links for chart items.
		/// </summary>
		private Func<T, string> mLinkBuilder;

		/// <summary>
		/// The number prefix.
		/// </summary>
		private string mPrefix;

		/// <summary>
		/// The sub-caption.
		/// </summary>
		private string mSubCaption;

		/// <summary>
		/// The number suffix.
		/// </summary>
		private string mSuffix;

		/// <summary>
		/// Corresponds to ChartFusion FormatNumberScale setting, controls
		/// the placement of dynamic 'M' and 'K' suffixes. 
		/// </summary>
		private bool mUseDynamicSuffixes;

		#endregion

		#region Properties

		/// <summary>
		/// The width of the chart.
		/// </summary>
		public int Width { get; private set; }

		/// <summary>
		/// Chart height.
		/// </summary>
		public int Height { get; private set; }

		/// <summary>
		/// The data the chart will be built from.
		/// </summary>
		public IEnumerable<T> Data { get; private set; }

		#endregion

		#region Constructors

		/// <summary>
		/// Creates the builder.
		/// </summary>
		/// <param name="data">The data items to build a chart from.</param>
		/// <param name="valueExtractor">Used to get the value from data items.</param>
		/// <param name="helper"></param>
		/// <param name="chartUrl">The URL to the chart.</param>
		/// <param name="width">Chart width.</param>
		/// <param name="height">Chart height.</param>
		public FusionChartBuilder(HtmlHelper helper, string chartUrl, IEnumerable<T> data, Func<T, double> valueExtractor, int width, int height) {
			mHelper = helper;
			Data = data;
			mChartUrl = chartUrl;
			Height = height;
			Width = width;
			mValueExtractor = valueExtractor;

			//Defaults.
			mChartId = Guid.NewGuid().ToString().TrimStart('{').TrimEnd('}');
			mDebugEnabled = false;

			Colors("AFD8F8", "F6BD0F", "8BBA00", "FF8E46", "008E8E");
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Gets the next available color.
		/// </summary>
		/// <returns></returns>
		protected string GetNextColor() {
			if (!mColors.MoveNext()) {
				mColors.Reset();
				mColors.MoveNext();
			}

			return mColors.Current;
		}

		#endregion

		#region Internal Methods

		/// <summary>
		/// Writes chart-specific XML settings. 
		/// </summary>
		/// <param name="xml"></param>
		/// <remarks>
		/// Derived classes should override this method to add any chart-specific markup to the
		/// &lt;graph&gt; element.  When called, the '&lt;graph ' markup will have been rendered already.  
		/// </remarks>
		internal abstract void WriteGraphProperties(StringBuilder xml);

		#endregion

		#region Public Methods

		/// <summary>
		/// Adds an action link to each item. 
		/// </summary>
		/// <param name="actionLink"></param>
		/// <returns></returns>
		public FusionChartBuilder<T> Action(Func<T, string> actionLink) {
			mLinkBuilder = actionLink;

			return this;
		}

		/// <summary>
		/// Sets the ID of the generated chart.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public FusionChartBuilder<T> Id(string id) {
			mChartId = id;

			return this;
		}

		public FusionChartBuilder<T> Colors(params string[] hexColors) {
			mColors = ((IEnumerable<string>) (hexColors)).GetEnumerator();
			return this;
		}

		/// <summary>
		/// Specify a callback that extracts a friendly label for each item.
		/// </summary>
		/// <param name="getLabel"></param>
		/// <param name="eachTimes"></param>
		/// <returns></returns>
		public FusionChartBuilder<T> Label(Func<T, string> getLabel, int eachTimes) {
			mLabeler = x => {
				var retVal = getLabel(x);
				if (eachTimes > 1 && labelIndex % eachTimes != 0) {
					retVal = "";
				}
				labelIndex++;
				return retVal;
			};
			return this;
		}

		/// <summary>
		/// Enables debug mode.
		/// </summary>
		/// <returns></returns>
		public FusionChartBuilder<T> EnableDebugMode() {
			mDebugEnabled = true;

			return this;
		}

		/// <summary>
		/// Sets the number of decimal places to show.
		/// </summary>
		/// <param name="precision"></param>
		/// <returns></returns>
		public FusionChartBuilder<T> DecimalPrecision(int precision) {
			mDecimalPrecision = precision;

			return this;
		}

		/// <summary>
		/// When enabled this will round numbers to millions or thousands and add the
		/// corresponding suffix (k for thousands and m for millions).
		/// </summary>
		/// <param name="enabled"></param>
		/// <returns></returns>
		/// <remarks>
		/// Setting this to true corresponds to setting FormatNumberScale='1' on the
		/// graph XML element in FusionCharts.
		/// </remarks>
		public FusionChartBuilder<T> UseDynamicSuffixes(bool enabled) {
			mUseDynamicSuffixes = enabled;

			return this;
		}

		/// <summary>
		/// Sets the value to prepend to all numeric values.
		/// </summary>
		/// <param name="prefix"></param>
		/// <returns></returns>
		public FusionChartBuilder<T> NumberPrefix(string prefix) {
			mPrefix = prefix;

			return this;
		}

		/// <summary>
		/// Sets the value to append to all numeric values.
		/// </summary>
		/// <param name="suffix"></param>
		/// <returns></returns>
		public FusionChartBuilder<T> NumberSuffix(string suffix) {
			mSuffix = suffix;

			return this;
		}

		/// <summary>
		/// Builds a string of text to show as a chart item's tooltip.
		/// </summary>
		/// <param name="hoverLabelBuilder"></param>
		/// <returns></returns>
		public FusionChartBuilder<T> Hover(Func<T, string> hoverLabelBuilder) {
			mHoverLabelBuilder = hoverLabelBuilder;

			return this;
		}

		protected virtual double getValue(T item) {
			return mValueExtractor(item);
		}

		protected virtual string getLabel(T item) {
			return mLabeler(item);
		}

		/// <summary>
		/// Renders the chart.
		/// </summary>
		/// <returns></returns>
		public override string ToString() {
			var xml = new StringBuilder();

			writeGraphHeader(xml);

			writeGraphContentData(xml);

			writeGraphEnd(xml);

			return GetMarkupFromXml(xml);
		}

		protected virtual void writeGraphContentData(StringBuilder xml) {
			foreach (var item in Data) {
				xml.AppendFormat(CultureInfo.InvariantCulture, "<set value='{0}' color='{1}'", getValue(item), GetNextColor());

				if (mLabeler != null) {
					xml.AppendFormat(" name='{0}'", HttpUtility.UrlEncode(getLabel(item)));
				}

				if (mLinkBuilder != null) {
					xml.AppendFormat(" link='{0}'", HttpUtility.UrlEncode(mLinkBuilder(item)));
				}

				if (mHoverLabelBuilder != null) {
					xml.AppendFormat(" hoverText='{0}'", HttpUtility.UrlEncode(mHoverLabelBuilder(item)));
				}

				xml.AppendLine("/>");
			}
		}

		private string GetMarkupFromXml(StringBuilder xml) {
			var markup = InfoSoftGlobal.FusionCharts.RenderChartHTML(mChartUrl, "", xml.ToString(), mChartId,
				Width.ToString(), Height.ToString(), mDebugEnabled);

			//We have to add another param to make sure the flash object doesn't shine through jQuery UI.
			markup = markup.Replace("<param name=\"quality\" value=\"high\" />",
				"<param name=\"quality\" value=\"high\" /><param value=\"opaque\" name=\"wmode\" />")
				.Replace("<embed", "<embed wmode=\"opaque\"");
			return markup;
		}

		private void writeGraphEnd(StringBuilder xml) {
			xml.AppendLine("</graph>");
		}

		private void writeGraphHeader(StringBuilder xml) {
			xml.Append("<graph");

			WriteGraphProperties(xml);

			if (mDecimalPrecision >= 0) {
				xml.AppendFormat(" decimalPrecision='{0}'", mDecimalPrecision);
			}

			if (mUseDynamicSuffixes) {
				xml.AppendFormat(" formatNumberScale='1'");
			}

			if (mPrefix != null) {
				xml.AppendFormat(" numberPrefix='{0}'", mPrefix);
			}

			if (mSuffix != null) {
				xml.AppendFormat(" numberSuffix='{0}'", mSuffix);
			}

			if (mCaption != null) {
				xml.AppendFormat(" caption='{0}'", mCaption);
			}

			if (mSubCaption != null) {
				xml.AppendFormat(" subCaption='{0}'", mSubCaption);
			}

			xml.AppendFormat(" showAnchors='0' rotateNames='1' ");

			xml.AppendLine(">");
		}

		/// <summary>
		/// Sets the chart caption.
		/// </summary>
		/// <param name="caption"></param>
		public FusionChartBuilder<T> Caption(string caption) {
			mCaption = caption;

			return this;
		}

		/// <summary>
		/// Sets the chart's subcaption.
		/// </summary>
		/// <param name="subCaption"></param>
		public FusionChartBuilder<T> SubCaption(string subCaption) {
			mSubCaption = subCaption;

			return this;
		}

		#endregion
	}
}