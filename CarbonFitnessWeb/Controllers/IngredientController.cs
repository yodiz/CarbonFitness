using System.Web.Mvc;
using CarbonFitness.BusinessLogic;

namespace CarbonFitnessWeb.Controllers
{
    public class IngredientController : Controller
    {
        private readonly IIngredientBusinessLogic _ingredientBusinessLogic;

        public IngredientController(IIngredientBusinessLogic ingredientBusinessLogic)
        {
            _ingredientBusinessLogic = ingredientBusinessLogic;
        }

        public ViewResult Search(string q)
        {
            return View(_ingredientBusinessLogic.Search(q));
        }
    }
}