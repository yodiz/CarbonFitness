using CarbonFitness.DataLayer.Repository;
using CarbonFitness.Data.Model;

namespace CarbonFitness {
    public class MealBusinessLogic {

        protected IMealIngredientRepository MealIngredientRepository { get; private set; }
        protected Meal Meal { get; private set; }

        public MealBusinessLogic(IMealIngredientRepository mealIngredientRepository, Meal meal) {
            MealIngredientRepository = mealIngredientRepository;
            Meal = meal;
        }

        public void AddIngredient(User user, Ingredient ingredient) {
            var item = new MealIngredient();

            MealIngredientRepository.SaveOrUpdate(item);
        }
    }
}