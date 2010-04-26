using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;
using CarbonFitness.BusinessLogic.UnitHistory;

namespace CarbonFitness.App.Web.Models {
	public class ResultModel {
        public IEnumerable<SelectListItem> Nutrients { get; set; }
	    public ILine CalorieList { get; set; }

        [DisplayName("Ideal vikt")]
		public Decimal IdealWeight { get; set; }
	}

    
}