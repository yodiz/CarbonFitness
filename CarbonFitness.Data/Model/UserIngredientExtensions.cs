using System;

namespace CarbonFitness.Data.Model {
	public static class UserIngredientExtensions {
		public static decimal GetActualCalorieCount(this UserIngredient userIngredient, Func<Ingredient, decimal> valueProperty) {
			return (userIngredient.Measure / userIngredient.Ingredient.WeightInG) * valueProperty.Invoke(userIngredient.Ingredient);
		}
	}
}