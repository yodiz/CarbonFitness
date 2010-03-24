using System;
using System.Collections.Generic;
using CarbonFitness.BusinessLogic;

namespace CarbonFitness.App.Web.Models {
	public class ResultModel {
		public string SumOfCalories { get; set; }
		public DateTime Date { get; set; }
		public List<HistoryValue> CalorieHistoryList { get; set; }

        public Decimal IdealWeight { get; set; }
	}
}