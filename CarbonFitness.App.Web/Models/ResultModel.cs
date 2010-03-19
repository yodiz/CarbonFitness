using System;
using CarbonFitness.BusinessLogic;

namespace CarbonFitness.App.Web.Models {
	public class ResultModel {
		public IHistoryValues CalorieHistoryList { get; set; }
		public Decimal IdealWeight { get; set; }
	}
}