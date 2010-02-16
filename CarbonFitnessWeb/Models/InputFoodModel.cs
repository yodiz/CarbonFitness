using System.Collections.Generic;
using CarbonFitness.Data.Model;

namespace CarbonFitnessWeb.Models
{
	public class InputFoodModel {
		public int Measure{ get; set;}
        public string Ingredient { get; set; }
        public IEnumerable<UserIngredient> UserIngredients { get; set; }
	}
}