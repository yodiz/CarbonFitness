using System;
using System.Collections.Generic;
using CarbonFitness.Data.Model;

namespace CarbonFitness.BusinessLogic {
	public interface IUserIngredientBusinessLogic {
		UserIngredient AddUserIngredient(User user, string ingredientName, int measure, DateTime dateTime);
		UserIngredient[] GetUserIngredients(User user, DateTime dateTime);
		IDictionary<DateTime, double> GetCalorieHistory(User user);
	}
}