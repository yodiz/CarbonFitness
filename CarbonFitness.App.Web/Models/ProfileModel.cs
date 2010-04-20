using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
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

	    [DisplayName("Vikt (kg)")]
        public decimal Weight { get; set; }

	    [DisplayName("Längd (cm)")]
        [ValidateLengthIsInCentimeterAttribute]
		public decimal Length { get; set; }

        [DisplayName("Ideal vikt")]
		public decimal IdealWeight { get; set; }
	}

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class ValidateLengthIsInCentimeterAttribute : ValidationAttribute {
        private const string defaultErrorMessage = "'{0}' måste vara i centimeter.";

        public ValidateLengthIsInCentimeterAttribute() : base(defaultErrorMessage) { }

        public override string FormatErrorMessage(string name) {
            return String.Format(CultureInfo.CurrentUICulture, ErrorMessageString, name);
        }

        public override bool IsValid(object value) {
            return ((decimal) value) > 20;
        }
    }
}