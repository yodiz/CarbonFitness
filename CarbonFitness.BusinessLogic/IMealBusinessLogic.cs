using CarbonFitness.Data.Model;

namespace CarbonFitness.BusinessLogic
{
	public interface IMealBusinessLogic
	{
		void AddIngredient(User user, Ingredient ingredient, int measure);
		MealIngredient[] GetMealIngredients(int mealId);
	}
}