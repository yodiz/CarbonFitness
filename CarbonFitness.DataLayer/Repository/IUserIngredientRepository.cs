using CarbonFitness.Data.Model;

namespace CarbonFitness.DataLayer.Repository
{
	public interface IUserIngredientRepository
	{
		UserIngredient SaveOrUpdate(UserIngredient userIngredient);
	    UserIngredient[] GetUserIngredientsFromUserId(int userId);
	}
}