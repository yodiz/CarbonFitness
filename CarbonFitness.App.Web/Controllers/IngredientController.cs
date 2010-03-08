using System.Web.Mvc;
using CarbonFitness.BusinessLogic;

namespace CarbonFitness.App.Web.Controllers {
	public class IngredientController : Controller {
		private readonly IIngredientBusinessLogic ingredientBusinessLogic;

		public IngredientController(IIngredientBusinessLogic ingredientBusinessLogic) {
			this.ingredientBusinessLogic = ingredientBusinessLogic;
		}

		public ViewResult Search(string q) {
			return View(ingredientBusinessLogic.Search(q));
		}
	}
}