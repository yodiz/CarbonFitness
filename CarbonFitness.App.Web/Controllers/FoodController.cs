using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using CarbonFitness.App.Web.Models;
using CarbonFitness.App.Web.ViewConstants;
using CarbonFitness.BusinessLogic;
using CarbonFitness.BusinessLogic.Exceptions;
using CarbonFitness.Data.Model;
using SharpArch.Web.NHibernate;

namespace CarbonFitness.App.Web.Controllers {
	public class FoodController : Controller {
		private readonly IUserContext userContext;
		private readonly IUserIngredientBusinessLogic userIngredientBusinessLogic;

		public FoodController(IUserIngredientBusinessLogic userIngredientBusinessLogic, IUserContext userContext) {
			this.userIngredientBusinessLogic = userIngredientBusinessLogic;
			this.userContext = userContext;
		}

		[HttpPost]
		[Transaction]
		//When pressing submit, trying to add an ingredient to user
		public ActionResult Input(InputFoodModel model) {
			if (!string.IsNullOrEmpty(model.Ingredient) && model.Measure > 0) {
				AddUserIngredient(model);
			}

			model.UserIngredients = getUserIngredients(model.Date);

			return View(model);
		}

		private void AddUserIngredient(InputFoodModel model) {
			try {
				userIngredientBusinessLogic.AddUserIngredient(userContext.User, model.Ingredient, model.Measure, model.Date);
				RemoveFoodInputValues(model);
			}
			catch (NoIngredientFoundException e) {
				this.AddModelError<InputFoodModel>(x => x.Ingredient, FoodConstant.NoIngredientFoundMessage + e.IngredientName);
			}
		}

		private void RemoveFoodInputValues(InputFoodModel model) {
			model.Ingredient = "";
			model.Measure = 0;

			ModelState.Remove("Ingredient");
			ModelState.Remove("Measure");
		}

		public static string GetPropertyNameFromModel<T>(Controller controller, Expression<Func<T, string>> namedPropertyToGet) {
			return ExpressionHelper.GetExpressionText(namedPropertyToGet);
		}

		//First time when coming to the "kost" page
		[Transaction]
		public ActionResult Input() {
			var inputFoodModel = new InputFoodModel {UserIngredients = getUserIngredients(DateTime.Now), Date = DateTime.Now};

			return View(inputFoodModel);
		}

		private UserIngredient[] getUserIngredients(DateTime date) {
			try {
				return userIngredientBusinessLogic.GetUserIngredients(userContext.User, date);
			}
			catch (InvalidDateException) {
				this.AddModelError<InputFoodModel>(x => x.Date, FoodConstant.InvalidDateErrorMessage);
			}
			return null;
		}
	}

	public static class ControllerExtension {
		public static void AddModelError<T>(this Controller controller, Expression<Func<T, DateTime>> namedPropertyToGet, string message) {
			var name = ExpressionHelper.GetExpressionText(namedPropertyToGet);
			addModelError(controller, name, message);
		}

		public static void AddModelError<T>(this Controller controller, Expression<Func<T, string>> namedPropertyToGet, string message) {
			var name = ExpressionHelper.GetExpressionText(namedPropertyToGet);
			addModelError(controller, name, message);
		}

		private static void addModelError(Controller controller, string name, string message) {
			controller.ModelState.AddModelError(name, message);
		}
	}
}