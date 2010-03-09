using System;
using System.ComponentModel.DataAnnotations;

namespace CarbonFitness.App.Web.Models {
	public class InputWeightModel
	{
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime Date { get; set; }

		public decimal Weight { get; set; }
	}
}