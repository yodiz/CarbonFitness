using System;
using System.Collections.Generic;
using System.ComponentModel;
using CarbonFitness.Data.Model;

namespace CarbonFitness.App.Web.Models {
	public class ProfileModel {
        [DisplayName("K�n")]
        public IEnumerable<GenderType> GenderTypes { get; set; }

        public GenderType Gender { get; set; }

	    [DisplayName("BMI")]
        public decimal BMI { get; set; }

	    [DisplayName("Vikt")]
        public decimal Weight { get; set; }

	    [DisplayName("L�ngd")]
		public decimal Length { get; set; }

        [DisplayName("Ideal vikt")]
		public decimal IdealWeight { get; set; }
	}
}