//Copyright 2009 - InRAD, LLC
//All Rights Reserved.

using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CarbonFitness.App.Web.FusionCharts {
	/// <summary>
	/// Container for the actual extension method.
	/// </summary>
	public static class FusionChartsHtmlHelper
	{
		/// <summary>
		/// Gets a helper for building a fusion chart.
		/// </summary>
		/// <param name="helper"></param>
		/// <returns></returns>
		public static FusionChartsHelper FusionCharts(this HtmlHelper helper)
		{
			return new FusionChartsHelper(helper);
		}
	}

	/// <summary>
	/// An HTML helper for FusionCharts. 
	/// </summary>
	/// <author>MBH</author>
	/// <dateAuthored>11/09/09</dateAuthored>
	public class FusionChartsHelper
	{
		/// <summary>
		/// The HTML helper.
		/// </summary>
		private readonly HtmlHelper mHtmlHelper;

		/// <summary>
		/// The resolved path to the Fusion Charts SWF files.
		/// </summary>
		private readonly string mChartsFolderBase;

		/// <summary>
		/// Initializes the helper. 
		/// </summary>
		/// <param name="helper"></param>
		public FusionChartsHelper(HtmlHelper helper)
		{
			mHtmlHelper = helper;
			UrlHelper urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

			mChartsFolderBase = urlHelper.Content("~/FusionCharts/");
		}

		/// <summary>
		/// Gets a fusion chart builder that will create a 2D bar chart.
		/// </summary>
		/// <typeparam name="T">The type of the data items.</typeparam>
		/// <param name="data">The items to bind to the chart.</param>
		/// <param name="width">Width in pixels.</param>
		/// <param name="height">Height in pixels.</param>
		/// <param name="getValue">Delegate that extracts the numerical value from a data item.</param>
		/// <returns>A 2D chart builder.</returns>
		public FusionChartColumn2DBuilder<T> Column2D<T>(
			IEnumerable<T> data, 
			int width, 
			int height,  
			Func<T, double> getValue)
		{
			return new FusionChartColumn2DBuilder<T>(mHtmlHelper, mChartsFolderBase, data, getValue, width, height);
		}

		public FusionChartLine2DBuilder<T> Line2D<T>(IEnumerable<T> data,int width,int height,Func<T, double> getValue)
		{
			return new FusionChartLine2DBuilder<T>(mHtmlHelper, mChartsFolderBase, data, getValue, width, height);
		}

		/// <summary>
		/// Creates a builder for 2D pie chart.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="data"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="getValue"></param>
		/// <returns></returns>
		public FusionChartPie2DChartBuilder<T> Pie2D<T>(
			IEnumerable<T> data, 
			int width, 
			int height,  
			Func<T, double> getValue)
		{
			return new FusionChartPie2DChartBuilder<T>(mHtmlHelper, mChartsFolderBase, data, getValue, width, height);
		}

		
	}
}