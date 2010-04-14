using System;
using System.ComponentModel;

namespace CarbonFitness.App.Web.Models {
	public class ProfileModel {
        [DisplayName("Längd")]
		public decimal Length { get; set; }

        [DisplayName("Ideal vikt")]
		public decimal IdealWeight { get; set; }
	}
}