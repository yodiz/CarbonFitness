using System;
using System.Threading;
using CarbonFitness.Data;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer;
using CarbonFitness.DataLayer.Repository;

namespace CarbonFitness.BusinessLogic
{
	public class UserIngredientBusinessLogic : IUserIngredientBusinessLogic
	{
		private readonly IUserIngredientRepository userIngredientRepository;
		private readonly IIngredientRepository ingredientRepository;

		public UserIngredientBusinessLogic(IUserIngredientRepository userIngredientRepository, IIngredientRepository ingredientRepository)
		{
			this.userIngredientRepository = userIngredientRepository;
			this.ingredientRepository = ingredientRepository;
		}

		public UserIngredient AddUserIngredient(User user, string ingredientName, int measure, DateTime dateTime)
		{
			var userIngredient = new UserIngredient();
			userIngredient.User = user;
		    userIngredient.Ingredient = GetExistingIngredient(ingredientName);
		    userIngredient.Measure = measure;
		    userIngredient.Date = dateTime;
			return userIngredientRepository.SaveOrUpdate(userIngredient);
		}

	    private Ingredient GetExistingIngredient(string ingredientName) {
	        var ingredient = ingredientRepository.Get(ingredientName);
	        if(ingredient == null) {
	            throw new NoIngredientFoundException(ingredientName);
	        }
	        return ingredient;
	    }

	    public UserIngredient[] GetUserIngredients(User user, DateTime dateTime) {
            return userIngredientRepository.GetUserIngredientsFromUserId(user.Id, dateTime);
	    }
	}
}