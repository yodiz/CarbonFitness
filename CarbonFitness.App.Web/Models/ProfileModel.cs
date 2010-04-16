using System;
using System.Collections.Generic;
using System.ComponentModel;
using CarbonFitness.Data.Model;

namespace CarbonFitness.App.Web.Models {
	public class ProfileModel {
        [DisplayName("Kön")]
        public IEnumerable<GenderType> GenderTypes { get; set; }

        public GenderType Gender { get; set; }

	    [DisplayName("BMI")]
        public decimal BMI { get; set; }

	    [DisplayName("Vikt")]
        public decimal Weight { get; set; }

	    [DisplayName("Längd")]
		public decimal Length { get; set; }

        [DisplayName("Ideal vikt")]
		public decimal IdealWeight { get; set; }
	}
}