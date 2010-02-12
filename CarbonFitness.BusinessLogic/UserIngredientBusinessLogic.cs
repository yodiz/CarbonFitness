using CarbonFitness.Data.Model;
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

		public UserIngredient AddUserIngredient(User user, string ingredientName, int measure)
		{
			var userIngredient = new UserIngredient();
			userIngredient.User = user;
			userIngredient.Ingredient = ingredientRepository.Get(ingredientName);
			userIngredient.Measure = measure;
			return userIngredientRepository.SaveOrUpdate(userIngredient);
		}
	}
}