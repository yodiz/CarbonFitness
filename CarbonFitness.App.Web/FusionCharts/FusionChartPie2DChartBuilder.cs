//Copyright 2009 - InRAD, LLC
//All Rights Reserved.

using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace CarbonFitness.App.Web.FusionCharts {
	/// <summary>
	/// A chart builder for 2D pie charts.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <author>MBH</author>
	/// <dateAuthored>12/1/09</dateAuthored>
	public class FusionChartPie2DChartBuilder<T> : FusionChartBuilder<T>
	{
		/// <summary>
		/// The name of the Pie Chart SWF file.
		/// </summary>
		private const string CHART_NAME = "FCF_Pie2D.swf";

		/// <summary>
		/// Flag that controls whether or not labels are shown by pie slices.
		/// </summary>
		private bool mShowLables = true;

		/// <summary>
		/// Initializes the builder.
		/// </summary>
		/// <param name="helper"></param>
		/// <param name="chartUrl"></param>
		/// <param name="data"></param>
		/// <param name="valueExtractor"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		public FusionChartPie2DChartBuilder(HtmlHelper helper, string chartUrl, IEnumerable<T> data, Func<T, double> valueExtractor, int width, int height) 
			: base(helper, chartUrl + CHART_NAME, data, valueExtractor, width, height)
		{
		}

		/// <summary>
		/// Writes chart-specific XML settings. 
		/// </summary>
		/// <param name="xml"></param>
		/// <remarks>
		/// Derived classes should override this method to add any chart-specific markup to the
		/// &lt;graph&gt; element.  When called, the '&lt;graph ' markup will have been rendered already.  
		/// </remarks>
		internal override void WriteGraphProperties(StringBuilder xml)
		{
			if (mShowLables) xml.Append(" shownames='1'");
		}

		/// <summary>
		/// Hides the labels from the pie chart.
		/// </summary>
		/// <returns></returns>
		public FusionChartPie2DChartBuilder<T> HideLabels()
		{
			mShowLables = false;

			return this;
		}
	}
}