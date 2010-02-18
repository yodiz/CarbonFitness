using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CarbonFitness.Data.Model;

namespace CarbonFitnessWeb.Models
{
	public class InputFoodModel {
	    public string Ingredient { get; set; }
        public int Measure{ get; set;}

        
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}" , ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public IEnumerable<UserIngredient> UserIngredients { get; set; }
	}
}