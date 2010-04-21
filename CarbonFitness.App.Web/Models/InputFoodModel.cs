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

        public decimal SumOfProtein { get; set; }
        public decimal SumOfFat { get; set; }
        public decimal SumOfCarbonHydrates { get; set; }
        public decimal SumOfFiber { get; set; }
        public decimal SumOfIron { get; set; }

        public decimal RDIOfProtein { get; set; }
        public decimal RDIOfFat { get; set; }
	    public decimal RDIOfCarbonHydrates { get; set; }
	    public decimal RDIOfFiber { get; set; }
        public decimal RDIOfIron { get; set; }
    }
}