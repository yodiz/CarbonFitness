//Copyright 2009 - InRAD, LLC
//All Rights Reserved.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Web.Mvc;
using CarbonFitness.BusinessLogic;

namespace CarbonFitness.App.Web.FusionCharts {
	/// <summary>
	/// A builder for a 2D column chart.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <author>MBH</author>
	/// <dateAuthored>12/1/09</dateAuthored>
	public class FusionChartMSLineBuilder : FusionChartBuilder<IHistoryValues>
	{
		/// <summary>
		/// The filename of the chart.
		/// </summary>
		private const string CHART_NAME = "FCF_MSLine.swf";

		/// <summary>
		/// The label for the X axis.
		/// </summary>
		private string mXAxisLabel;

		/// <summary>
		/// The label for the Y axis.
		/// </summary>
		private string mYAxisLabel;

		/// <summary>
		/// Initializes the builder.
		/// </summary>
		/// <param name="helper"></param>
		/// <param name="baseUrl">The URL to the folder that contains the SWF files for Fusion Charts.</param>
		/// <param name="data"></param>
		/// <param name="valueExtractor"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		public FusionChartMSLineBuilder(HtmlHelper helper, string baseUrl, IEnumerable<IHistoryValues> multiHistoryValues, Func<IHistoryValues, double> valueExtractor, int width, int height) :
			base(helper, baseUrl + CHART_NAME, multiHistoryValues, valueExtractor, width, height)
		{
		}


		protected string getValue(HistoryValue item)
		{
			return item.Value.ToString(CultureInfo.InvariantCulture);
		}

		protected string getLabel(HistoryValue item)
		{
			return item.Date.ToShortDateString();
		}
		/// <summary>
		/// Writes the X and Y axis labels.
		/// </summary>
		/// <param name="xml"></param>
		internal override void WriteGraphProperties(StringBuilder xml)
		{
			if (mXAxisLabel != null) xml.AppendFormat(" xAxisName='{0}'", mXAxisLabel);

			if (mYAxisLabel != null) xml.AppendFormat(" yAxisName='{0}'", mYAxisLabel);
		}

		/// <summary>
		/// Sets the label for the X Axis.
		/// </summary>
		/// <param name="xAxisLabel"></param>
		/// <returns></returns>
		public FusionChartBuilder<IHistoryValues> XAxisLabel(string xAxisLabel) {
			mXAxisLabel = xAxisLabel;

			return this;
		}


		protected override void writeGraphContentData(StringBuilder xml)
		{
			xml.AppendLine("<categories>");

			foreach (var historyValues in Data) {
				foreach (var historyValue in historyValues) {
					xml.AppendFormat(CultureInfo.InvariantCulture, "<category name='{0}' />", getLabel(historyValue));
				}
				break;
			}
			xml.AppendLine("</categories>");

			foreach (var historyValues in Data) {
				xml.AppendFormat("<dataset seriesName='{0}' color='{1}' anchorBorderColor='{1}' anchorBgColor='{1}'>", historyValues.Title, GetNextColor());

				foreach (var item in historyValues)
				{
					xml.AppendFormat(CultureInfo.InvariantCulture, "<set value='{0}'", getValue(item));

					xml.AppendLine("/>");
				}
				xml.AppendLine("</dataset>");	
			}
		}

		/// <summary>
		/// Sets the label for the Y Axis.
		/// </summary>
		/// <param name="yAxisLabel"></param>
		/// <returns></returns>
		public FusionChartBuilder<IHistoryValues> YAxisLabel(string yAxisLabel)
		{
			mYAxisLabel = yAxisLabel;

			return this;
		}
	}
}