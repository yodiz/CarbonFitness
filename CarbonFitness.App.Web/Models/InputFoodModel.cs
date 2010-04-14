using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CarbonFitness.Data.Model;

namespace CarbonFitness.App.Web.Models {
	public class InputFoodModel {

        [DisplayName("Livsmedel")]
		public string Ingredient { get; set; }

        [DisplayName("Vikt (g)")]
		public int Measure { get; set; }


		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Datum")]
        public DateTime Date { get; set; }

		public IEnumerable<UserIngredient> UserIngredients { get; set; }
        
	}
}