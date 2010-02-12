using System.Collections.Generic;
using System.Web.Mvc;
using CarbonFitness.BusinessLogic;
using CarbonFitness.Data.Model;
using CarbonFitnessWeb.Models;

namespace CarbonFitnessWeb.Controllers {
	public class FoodController : Controller {
		private IUserIngredientBusinessLogic userIngredientBusinessLogic;
		private IUserContext userContext;

		public FoodController(IUserIngredientBusinessLogic userIngredientBusinessLogic, IUserContext userContext)
		{
			this.userIngredientBusinessLogic = userIngredientBusinessLogic;
			this.userContext = userContext;
		}

		[HttpPost]
		public ActionResult Input(InputFoodModel model) {
			var userIngredient = userIngredientBusinessLogic.AddUserIngredient(userContext.User, model.Ingredient, model.Measure);

			var userIngredients = new List<UserIngredient> {userIngredient};
			model.UserIngredients = userIngredients;
			
			return View(model);
		}

		public ActionResult Input()
		{
			return View(new InputFoodModel { UserIngredients = new List<UserIngredient>() });
		}

		//public ActionResult Input(int mealId)
		//{
		//   var mealIngredients = mealBusinessLogic.GetMealIngredients(mealId);
		//   return View(new InputFoodModel{MealIngredients = mealIngredients} );
		//}
	}
}