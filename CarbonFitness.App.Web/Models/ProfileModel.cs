using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

namespace CarbonFitness.App.Web.Models {
	public class ProfileModel {

	    [DisplayName("�lder")]
        public int Age { get; set; }

        [DisplayName("K�n")]
        public IEnumerable<SelectListItem> GenderViewTypes { get; set; }

        public string SelectedGender { get; set; }

        [DisplayName("Aktivitets niv�")]
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

	    [DisplayName("L�ngd")]
		public decimal Length { get; set; }

        [DisplayName("Ideal vikt")]
		public decimal IdealWeight { get; set; }
	}
}