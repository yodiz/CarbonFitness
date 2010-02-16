using System.Collections.Generic;
using CarbonFitness.Data.Model;

namespace CarbonFitness.BusinessLogic
{
	public interface IUserIngredientBusinessLogic	
	{
		UserIngredient AddUserIngredient(User user, string ingredientName, int measure);
        UserIngredient[] GetUserIngredients(User user);
	}
}