using System;
using System.Collections.Generic;
using CarbonFitness.BusinessLogic.UnitHistory;
using CarbonFitness.Data.Model;

namespace CarbonFitness.BusinessLogic {
	public interface IUserIngredientBusinessLogic {
		UserIngredient AddUserIngredient(User user, string ingredientName, int measure, DateTime dateTime);
		UserIngredient[] GetUserIngredients(User user, DateTime dateTime);
        ILine GetNutrientHistory(NutrientEntity entity, User user);
	    decimal GetNutrientSumForDate(User user, NutrientEntity entity, DateTime date);
        IEnumerable<INutrientSum> GetNutrientSumList(IEnumerable<NutrientEntity> nutrients, User user);
	    INutrientAverage GetNutrientAverage(IEnumerable<NutrientEntity> nutrientEntities, User user);
	    void DeleteUserIngredient(User user, int userIngredientId, DateTime date);
	}
}