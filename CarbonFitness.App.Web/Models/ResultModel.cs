using System;
using CarbonFitness.BusinessLogic.UnitHistory;

namespace CarbonFitness.App.Web.Models {
	public class ResultModel {
		public IHistoryValues CalorieHistoryList { get; set; }
		public Decimal IdealWeight { get; set; }
	}
}