using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;
using CarbonFitness.Data.Model;

namespace CarbonFitness.App.Web.Models {
	public class ProfileModel {
        [DisplayName("Kön")]
        public IEnumerable<SelectListItem> GenderViewTypes { get; set; }

        public string SelectedGender { get; set; }

	    [DisplayName("BMI")]
        public decimal BMI { get; set; }

	    [DisplayName("Vikt")]
        public decimal Weight { get; set; }

	    [DisplayName("Längd")]
		public decimal Length { get; set; }

        [DisplayName("Ideal vikt")]
		public decimal IdealWeight { get; set; }
	}

    //public class GenderViewType {
    //    public GenderType Gender { get; set; }
    //    public bool isSelected { get; set; }
    //}
}