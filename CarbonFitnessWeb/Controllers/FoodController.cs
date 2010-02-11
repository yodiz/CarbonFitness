using System.Web.Mvc;
using CarbonFitness.BusinessLogic;
using CarbonFitness.Data.Model;
using CarbonFitnessWeb.Models;

namespace CarbonFitnessWeb.Controllers {
	public class FoodController : Controller {
		private IMealBusinessLogic mealBusinessLogic;
		public IUserBusinessLogic userBusinessLogic;
      
		public FoodController(IMealBusinessLogic mealBusinessLogic, IUserBusinessLogic userBusinessLogic) {
			this.userBusinessLogic = userBusinessLogic;
			this.mealBusinessLogic = mealBusinessLogic;
		}

		[HttpPost]
		public ActionResult Input(InputFoodModel model) {
			mealBusinessLogic.AddIngredient(new UserContext(userBusinessLogic).User, new Ingredient { Name = model.Ingredient }, model.Measure);

			return View(model);
		}

		public ActionResult Input(int mealId)
		{
			var mealIngredients = mealBusinessLogic.GetMealIngredients(mealId);
			return View(new InputFoodModel{MealIngredients = mealIngredients} );
		}
	}
}