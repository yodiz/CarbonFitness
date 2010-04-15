using System;
using System.ComponentModel;

namespace CarbonFitness.App.Web.Models {
	public class ProfileModel {
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