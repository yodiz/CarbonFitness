using CarbonFitness.Data.Model;

namespace CarbonFitness.DataLayer.Repository
{
	public interface IIngredientRepository
	{
		Ingredient Get(string ingredientName);
	}
}