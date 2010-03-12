using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CarbonFitness.Data.Model;

namespace CarbonFitness.App.Web.Models {
	public class InputWeightModel {
		public InputWeightModel() {
			Date = DateTime.Now.Date;
			Weight = 0M;
		}

		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime Date { get; set; }

		public decimal Weight { get; set; }

		public IEnumerable<UserWeight> UserWeightHistoryList { get; set; }
	}
}