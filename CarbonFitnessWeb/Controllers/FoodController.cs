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
			userIngredientBusinessLogic.AddUserIngredient(userContext.User, model.Ingredient, model.Measure);
            model.UserIngredients = userIngredientBusinessLogic.GetUserIngredients(userContext.User); 
			
			return View(model);
		}

		public ActionResult Input() {
            var userIngredients = userIngredientBusinessLogic.GetUserIngredients(userContext.User);
            var inputFoodModel = new InputFoodModel { UserIngredients = userIngredients };

		    return View(inputFoodModel);
		}
	}
}