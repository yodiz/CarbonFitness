using System.Collections.Generic;
using CarbonFitness.Data.Model;

namespace CarbonFitnessWeb.Models
{
	public class InputFoodModel
	{
		public int Measure;
		public string Ingredient;
		public IList<MealIngredient> MealIngredients;
		
	}
}