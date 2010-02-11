using System;
using CarbonFitness.DataLayer.Repository;
using CarbonFitness.Data.Model;

namespace CarbonFitness.BusinessLogic
{
	public class MealBusinessLogic : IMealBusinessLogic {

		protected IMealIngredientRepository MealIngredientRepository { get; private set; }
		protected Meal Meal { get; private set; }

		public MealBusinessLogic(IMealIngredientRepository mealIngredientRepository) {
			MealIngredientRepository = mealIngredientRepository;
		}

		public MealBusinessLogic(IMealIngredientRepository mealIngredientRepository, Meal meal) : this(mealIngredientRepository){
			MealIngredientRepository = mealIngredientRepository;
			Meal = meal;
		}

		public void AddIngredient(User user, Ingredient ingredient, int any) {
			var item = new MealIngredient();

			MealIngredientRepository.SaveOrUpdate(item);
		}

		public MealIngredient[] GetMealIngredients(int mealId)
		{
			throw new NotImplementedException();
		}
	}
}