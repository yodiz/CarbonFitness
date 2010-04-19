using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

namespace CarbonFitness.App.Web.Models {
	public class ProfileModel {

	    [DisplayName("Ålder")]
        public int Age { get; set; }

        [DisplayName("Kön")]
        public IEnumerable<SelectListItem> GenderViewTypes { get; set; }

        public string SelectedGender { get; set; }

        [DisplayName("Aktivitets nivå")]
        public IEnumerable<SelectListItem> ActivityLevelViewTypes { get; set; }

        public string SelectedActivityLevel { get; set; }

	    [DisplayName("BMI")]
        public decimal BMI { get; set; }

        [DisplayName("BMR")]
        public decimal BMR { get; set; }

        [DisplayName("Dagligt kalori behov")]
        public decimal DailyCalorieNeed { get; set; }

	    [DisplayName("Vikt")]
        public decimal Weight { get; set; }

	    [DisplayName("Längd")]
		public decimal Length { get; set; }

        [DisplayName("Ideal vikt")]
		public decimal IdealWeight { get; set; }
	}
}