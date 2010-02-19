using CarbonFitness.Data.Model;

namespace CarbonFitness.DataLayer
{
	public interface IIngredientRepository
	{
		Ingredient Get(string ingredientName);
		Ingredient [] Search(string queryText);
	}
}