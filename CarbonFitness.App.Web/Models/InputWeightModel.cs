using System;
using System.ComponentModel.DataAnnotations;
using CarbonFitness.Data.Model;

namespace CarbonFitness.App.Web.Models {
	public class InputWeightModel {
		public InputWeightModel() {
			Date = DateTime.Now;
			Weight = 0M;
		}

		public InputWeightModel(UserWeight userWeight) : this() {
			if (userWeight == null) {
				return;
			}
			Date = userWeight.Date;
			Weight = userWeight.Weight;
		}

		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime Date { get; set; }

		public decimal Weight { get; set; }

	}
}