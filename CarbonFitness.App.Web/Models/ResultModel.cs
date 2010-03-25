using System;
using CarbonFitness.BusinessLogic.UnitHistory;

namespace CarbonFitness.App.Web.Models {
	public class ResultModel {
		public ILine CalorieList { get; set; }
		public Decimal IdealWeight { get; set; }
	}
}