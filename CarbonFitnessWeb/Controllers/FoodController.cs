using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using CarbonFitness.BusinessLogic;
using CarbonFitness.Data.Model;
using CarbonFitnessWeb.Models;
using CarbonFitnessWeb.ViewConstants;

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
        //When pressing submit, trying to add an ingredient to user
		public ActionResult Input(InputFoodModel model) {
            try {
                userIngredientBusinessLogic.AddUserIngredient(userContext.User, model.Ingredient, model.Measure, model.Date);
            } catch(NoIngredientFoundException e) {
                this.AddModelError<InputFoodModel>(x => x.Ingredient, FoodConstant.NoIngredientFoundMessage + e.IngredientName);
            }

		    model.UserIngredients = userIngredientBusinessLogic.GetUserIngredients(userContext.User, model.Date); 
			
			return View(model);
		}
        public static string GetPropertyNameFromModel<T>(Controller controller, Expression<Func<T, string>> namedPropertyToGet)
        {
            return ExpressionHelper.GetExpressionText(namedPropertyToGet);
        }

	    //First time when coming to the "kost" page
		public ActionResult Input() {
            var userIngredients = userIngredientBusinessLogic.GetUserIngredients(userContext.User, DateTime.Now);
            var inputFoodModel = new InputFoodModel { UserIngredients = userIngredients, Date = DateTime.Now };

		    return View(inputFoodModel);
		}
	}
    //public static class ModelStateExtension
    //{
    //    public static void AddModelError<T>(this ModelState modelState, Expression<Func<T, string>> namedPropertyToGet, string message)
    //    {
    //        string name = ExpressionHelper.GetExpressionText(namedPropertyToGet);
    //        modelState.AddModelError(name, message);
    //        //return ExpressionHelper.GetExpressionText(namedPropertyToGet);
    //    }
    //}

    public static class ControllerExtension {
        public static void AddModelError<T>(this Controller controller, Expression<Func<T, string>> namedPropertyToGet, string message) {
            string name = ExpressionHelper.GetExpressionText(namedPropertyToGet);
            controller.ModelState.AddModelError(name, message);
        }
    }
}