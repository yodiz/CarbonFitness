//Copyright 2009 - InRAD, LLC
//All Rights Reserved.

using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace CarbonFitness.App.Web.FusionCharts {
	/// <summary>
	/// A builder for a 2D column chart.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <author>MBH</author>
	/// <dateAuthored>12/1/09</dateAuthored>
	public class FusionChartLine2DBuilder<T> : FusionChartBuilder<T>
	{
		/// <summary>
		/// The filename of the chart.
		/// </summary>
		private const string CHART_NAME = "FCF_Line.swf";

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
		public FusionChartLine2DBuilder(HtmlHelper helper, string baseUrl, IEnumerable<T> data, Func<T, double> valueExtractor, int width, int height) :
			base(helper, baseUrl + CHART_NAME, data, valueExtractor, width, height)
		{
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
		public FusionChartBuilder<T> XAxisLabel(string xAxisLabel)
		{
			mXAxisLabel = xAxisLabel;

			return this;
		}

		/// <summary>
		/// Sets the label for the Y Axis.
		/// </summary>
		/// <param name="yAxisLabel"></param>
		/// <returns></returns>
		public FusionChartBuilder<T> YAxisLabel(string yAxisLabel)
		{
			mYAxisLabel = yAxisLabel;

			return this;
		}
	}
}