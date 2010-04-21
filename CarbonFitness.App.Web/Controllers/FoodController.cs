using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using CarbonFitness.App.Web.Models;
using CarbonFitness.App.Web.ViewConstants;
using CarbonFitness.BusinessLogic;
using CarbonFitness.BusinessLogic.Exceptions;
using CarbonFitness.Data.Model;
using SharpArch.Web.NHibernate;

namespace CarbonFitness.App.Web.Controllers
{
	[HandleError]
	public class FoodController : Controller {
		private readonly IUserContext userContext;
		private readonly IUserIngredientBusinessLogic userIngredientBusinessLogic;
        private readonly IRDICalculator rdiCalculator;

        public FoodController(IUserIngredientBusinessLogic userIngredientBusinessLogic, IRDICalculator rdiCalculator, IUserContext userContext)
        {
			this.userIngredientBusinessLogic = userIngredientBusinessLogic;
            this.rdiCalculator = rdiCalculator;
            this.userContext = userContext;
		}

        //First time when coming to the "kost" page
        [Transaction]
        [Authorize]
        public ActionResult Input() {
            return View( GetInputFoodModel(DateTime.Now));
        }

	    private InputFoodModel GetInputFoodModel(DateTime date) {
	        return new InputFoodModel {
	            UserIngredients = getUserIngredients(date),
	            Date = date,
	            SumOfProtein = userIngredientBusinessLogic.GetNutrientSumForDate(userContext.User, NutrientEntity.ProteinInG, date),
	            SumOfFat = userIngredientBusinessLogic.GetNutrientSumForDate(userContext.User, NutrientEntity.FatInG, date),
	            SumOfFiber = userIngredientBusinessLogic.GetNutrientSumForDate(userContext.User, NutrientEntity.FibresInG, date),
	            SumOfCarbonHydrates = userIngredientBusinessLogic.GetNutrientSumForDate(userContext.User, NutrientEntity.CarbonHydrateInG, date),
	            SumOfIron = userIngredientBusinessLogic.GetNutrientSumForDate(userContext.User, NutrientEntity.IronInmG, date),
                RDIOfProtein = rdiCalculator.GetRDI(userContext.User, NutrientEntity.ProteinInG),
                RDIOfFat = rdiCalculator.GetRDI(userContext.User, NutrientEntity.FatInG),
                RDIOfFiber = rdiCalculator.GetRDI(userContext.User, NutrientEntity.FibresInG),
                RDIOfCarbonHydrates = rdiCalculator.GetRDI(userContext.User, NutrientEntity.CarbonHydrateInG),
                RDIOfIron = rdiCalculator.GetRDI(userContext.User, NutrientEntity.IronInmG),
	        };
	    }

	    [HttpPost]
		[Transaction]
		[Authorize]
		//When pressing submit, trying to add an ingredient to user
		public ActionResult Input(InputFoodModel model) {
			if (!string.IsNullOrEmpty(model.Ingredient) && model.Measure > 0) {
				AddUserIngredient(model);
			}

	        return View(GetInputFoodModel(model.Date));
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

        private void AddUserIngredient(InputFoodModel model)
        {
            try
            {
                userIngredientBusinessLogic.AddUserIngredient(userContext.User, model.Ingredient, model.Measure, model.Date);
                RemoveFoodInputValues(model);
            }
            catch (NoIngredientFoundException e)
            {
                this.AddModelError<InputFoodModel>(x => x.Ingredient, FoodConstant.NoIngredientFoundMessage + e.IngredientName);
            }
        }

        private void RemoveFoodInputValues(InputFoodModel model)
        {
            model.Ingredient = "";
            model.Measure = 0;

            ModelState.Remove("Ingredient");
            ModelState.Remove("Measure");
        }

        public static string GetPropertyNameFromModel<T>(Controller controller, Expression<Func<T, string>> namedPropertyToGet)
        {
            return ExpressionHelper.GetExpressionText(namedPropertyToGet);
        }
	}
}