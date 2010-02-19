using System;
using CarbonFitness.Data.Model;

namespace CarbonFitness.DataLayer
{
	public interface IUserIngredientRepository
	{
		UserIngredient SaveOrUpdate(UserIngredient userIngredient);
		UserIngredient[] GetUserIngredientsFromUserId(int userId, DateTime dateTime);
	}
}