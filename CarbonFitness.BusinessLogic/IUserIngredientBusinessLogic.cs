using System;
using CarbonFitness.BusinessLogic.UnitHistory;
using CarbonFitness.Data.Model;

namespace CarbonFitness.BusinessLogic {
	public interface IUserIngredientBusinessLogic {
		UserIngredient AddUserIngredient(User user, string ingredientName, int measure, DateTime dateTime);
		UserIngredient[] GetUserIngredients(User user, DateTime dateTime);
		HistoryValuesContainer GetCalorieHistory(User user);
	}
}