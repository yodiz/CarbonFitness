using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;
using CarbonFitness.BusinessLogic;
using CarbonFitness.BusinessLogic.UnitHistory;

namespace CarbonFitness.App.Web.Models {
	public class ResultModel {
  	    public IEnumerable<SelectListItem> GraphLineOptions { get; set; }
	    public ILine CalorieList { get; set; }

        [DisplayName("Ideal vikt")]
		public Decimal IdealWeight { get; set; }
	}

    public class ResultListModel {
        public INutrientAverage NutrientAverage { get; set; }
        public IEnumerable<INutrientSum> NutrientSumList { get; set; }
    }
}