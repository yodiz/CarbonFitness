using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;

namespace CarbonFitness.BusinessLogic
{
    public class IngredientBusinessLogic : IIngredientBusinessLogic
    {
        private readonly IIngredientRepository _ingredientRepository;

        public IngredientBusinessLogic(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }

        public Ingredient[] Search(string searchQuery)
        {
            return _ingredientRepository.Search(searchQuery);
        }
    }
}