using System;
using System.Collections.Generic;

namespace CarbonFitness.App.Web.Models {
	public class ResultModel {
		public string SumOfCalories { get; set; }
		public DateTime Date { get; set; }
		public IDictionary<DateTime, double> CalorieHistoryList { get; set; }
	}
}