using CarbonFitness.Data.Model;

namespace CarbonFitness.BusinessLogic
{
    public interface IIngredientBusinessLogic  
    {
        Ingredient[] Search(string searchQuery);
    }
}