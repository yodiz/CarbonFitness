using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;

namespace CarbonFitness.BusinessLogic.Implementation {
	public class IngredientBusinessLogic : IIngredientBusinessLogic {
		private readonly IIngredientRepository ingredientRepository;

		public IngredientBusinessLogic(IIngredientRepository ingredientRepository) {
			this.ingredientRepository = ingredientRepository;
		}

		public Ingredient[] Search(string searchQuery) {
			return ingredientRepository.Search(searchQuery);
		}
	}
}