using System;
using System.Collections.Generic;
using CarbonFitness.BusinessLogic.UnitHistory;
using CarbonFitness.Data.Model;

namespace CarbonFitness.App.Web.Models {
	public class ResultModel {
	    public IEnumerable<Nutrient> Nutrients{ get; set;}
	    public ILine CalorieList { get; set; }
		public Decimal IdealWeight { get; set; }
	}
}