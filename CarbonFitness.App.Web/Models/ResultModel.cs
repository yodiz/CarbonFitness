using System;
using System.Collections.Generic;
using System.ComponentModel;
using CarbonFitness.BusinessLogic.UnitHistory;
using CarbonFitness.Data.Model;

namespace CarbonFitness.App.Web.Models {
	public class ResultModel {
	    public IEnumerable<Nutrient> Nutrients{ get; set;}
	    public ILine CalorieList { get; set; }

        [DisplayName("Ideal vikt")]
		public Decimal IdealWeight { get; set; }
	}
}