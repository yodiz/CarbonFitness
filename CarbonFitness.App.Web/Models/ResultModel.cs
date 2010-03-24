using System;
using CarbonFitness.BusinessLogic;
using System.Collections.Generic;

namespace CarbonFitness.App.Web.Models {
	public class ResultModel {
		public List<HistoryValue> CalorieHistoryList { get; set; }
		public Decimal IdealWeight { get; set; }
	}
}