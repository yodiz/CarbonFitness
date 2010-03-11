//Copyright 2009 - InRAD, LLC
//All Rights Reserved.

using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CarbonFitness.App.Web.FusionCharts {
	/// <summary>
	/// Builds a chart.
	/// </summary>
	/// <author>MBH</author>
	/// <dateAuthored>11/10/09</dateAuthored>
	public abstract class FusionChartBuilder<T>
	{
		#region Private Fields

		/// <summary>
		/// Url to the chart flash file.
		/// </summary>
		private readonly string mChartUrl;

		/// <summary>
		/// An enumerable array of colors to use for coloring items in a chart.
		/// </summary>
		private readonly IEnumerator<string> mColors = ((IEnumerable<string>)(new[] { "AFD8F8", "F6BD0F", "8BBA00", "FF8E46", "008E8E" })).GetEnumerator();

		/// <summary>
		/// Delegate used to get chart values for items.
		/// </summary>
		private readonly Func<T, double> mValueExtractor;

		/// <summary>
		/// The HtmlHelper.
		/// </summary>
		private readonly HtmlHelper mHelper;

		/// <summary>
		/// The ID given to the chart in the DOM.
		/// </summary>
		private string mChartId;

		/// <summary>
		/// Flag to control debug mode in Fusion Charts.
		/// </summary>
		private bool mDebugEnabled;

		/// <summary>
		/// The decimal precision.
		/// </summary>
		private int mDecimalPrecision = int.MinValue;

		/// <summary>
		/// Corresponds to ChartFusion FormatNumberScale setting, controls
		/// the placement of dynamic 'M' and 'K' suffixes. 
		/// </summary>
		private bool mUseDynamicSuffixes;

		/// <summary>
		/// The number prefix.
		/// </summary>
		private string mPrefix;

		/// <summary>
		/// The number suffix.
		/// </summary>
		private string mSuffix;

		/// <summary>
		/// Delegate used to get labels for chart items.
		/// </summary>
		private Func<T, string> mLabeler;

		/// <summary>
		/// Delegate used to get drill-down links for chart items.
		/// </summary>
		private Func<T, string> mLinkBuilder;

		/// <summary>
		/// Builds the label that will be shown when the user hovers over a chart element.
		/// </summary>
		private Func<T, string> mHoverLabelBuilder;

		/// <summary>
		/// The chart caption.
		/// </summary>
		private string mCaption;

		/// <summary>
		/// The sub-caption.
		/// </summary>
		private string mSubCaption;

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
		public FusionChartBuilder(HtmlHelper helper, string chartUrl, IEnumerable<T> data, Func<T,double> valueExtractor, int width, int height)
		{
			mHelper = helper;
			Data = data;
			mChartUrl = chartUrl;
			Height = height;
			Width = width;
			mValueExtractor = valueExtractor;

			//Defaults.
			mChartId = Guid.NewGuid().ToString().TrimStart('{').TrimEnd('}');
			mDebugEnabled = false;
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Gets the next available color.
		/// </summary>
		/// <returns></returns>
		private string GetNextColor()
		{
			if (!mColors.MoveNext())
			{
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
		public FusionChartBuilder<T> Action(Func<T, string> actionLink)
		{
			mLinkBuilder = actionLink;

			return this;
		}

		/// <summary>
		/// Sets the ID of the generated chart.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public FusionChartBuilder<T> Id(string id)
		{
			mChartId = id;

			return this;
		}

		/// <summary>
		/// Specify a callback that extracts a friendly label for each item.
		/// </summary>
		/// <param name="getLabel"></param>
		/// <returns></returns>
		public FusionChartBuilder<T> Label(Func<T, string> getLabel)
		{
			mLabeler = getLabel;

			return this;
		}

		/// <summary>
		/// Enables debug mode.
		/// </summary>
		/// <returns></returns>
		public FusionChartBuilder<T> EnableDebugMode()
		{
			mDebugEnabled = true;

			return this;
		}

		/// <summary>
		/// Sets the number of decimal places to show.
		/// </summary>
		/// <param name="precision"></param>
		/// <returns></returns>
		public FusionChartBuilder<T> DecimalPrecision(int precision)
		{
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
		public FusionChartBuilder<T> UseDynamicSuffixes(bool enabled)
		{
			mUseDynamicSuffixes = enabled;

			return this;
		}

		/// <summary>
		/// Sets the value to prepend to all numeric values.
		/// </summary>
		/// <param name="prefix"></param>
		/// <returns></returns>
		public FusionChartBuilder<T> NumberPrefix(string prefix)
		{
			mPrefix = prefix;

			return this;
		}

		/// <summary>
		/// Sets the value to append to all numeric values.
		/// </summary>
		/// <param name="suffix"></param>
		/// <returns></returns>
		public FusionChartBuilder<T> NumberSuffix(string suffix)
		{
			mSuffix = suffix;

			return this;
		}

		/// <summary>
		/// Builds a string of text to show as a chart item's tooltip.
		/// </summary>
		/// <param name="hoverLabelBuilder"></param>
		/// <returns></returns>
		public FusionChartBuilder<T> Hover(Func<T, string> hoverLabelBuilder)
		{
			mHoverLabelBuilder = hoverLabelBuilder;

			return this;
		}

		/// <summary>
		/// Renders the chart.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			StringBuilder xml = new StringBuilder();

			xml.Append("<graph");

			WriteGraphProperties(xml);

			if (mDecimalPrecision >= 0) xml.AppendFormat(" decimalPrecision='{0}'", mDecimalPrecision);

			if (mUseDynamicSuffixes) xml.AppendFormat(" formatNumberScale='1'");

			if (mPrefix != null) xml.AppendFormat(" numberPrefix='{0}'", mPrefix);

			if (mSuffix != null) xml.AppendFormat(" numberSuffix='{0}'", mSuffix);

			if (mCaption != null) xml.AppendFormat(" caption='{0}'", mCaption);

			if (mSubCaption != null) xml.AppendFormat(" subCaption='{0}'", mSubCaption);

			xml.AppendLine(">");

			foreach (T item in Data)
			{
				xml.AppendFormat("<set value='{0}' color='{1}'", mValueExtractor(item), GetNextColor());

				if (mLabeler != null)
				{
					xml.AppendFormat(" name='{0}'", HttpUtility.UrlEncode(mLabeler(item)));
				}

				if (mLinkBuilder != null)
				{
					xml.AppendFormat(" link='{0}'", HttpUtility.UrlEncode(mLinkBuilder(item)));
				}

				if (mHoverLabelBuilder != null)
				{
					xml.AppendFormat(" hoverText='{0}'", HttpUtility.UrlEncode(mHoverLabelBuilder(item)));
				}

				xml.AppendLine("/>");
			}

			xml.AppendLine("</graph>");

			string markup = InfoSoftGlobal.FusionCharts.RenderChartHTML(mChartUrl, "", xml.ToString(), mChartId, 
				Width.ToString(), Height.ToString(), mDebugEnabled);

			//We have to add another param to make sure the flash object doesn't shine through jQuery UI.
			markup = markup.Replace("<param name=\"quality\" value=\"high\" />",
				"<param name=\"quality\" value=\"high\" /><param value=\"opaque\" name=\"wmode\" />")
				.Replace("<embed", "<embed wmode=\"opaque\"");

			return markup;
		}

		/// <summary>
		/// Sets the chart caption.
		/// </summary>
		/// <param name="caption"></param>
		public FusionChartBuilder<T> Caption(string caption)
		{
			mCaption = caption;

			return this;
		}

		/// <summary>
		/// Sets the chart's subcaption.
		/// </summary>
		/// <param name="subCaption"></param>
		public FusionChartBuilder<T> SubCaption(string subCaption)
		{
			mSubCaption = subCaption;

			return this;
		}

		#endregion
	}
}