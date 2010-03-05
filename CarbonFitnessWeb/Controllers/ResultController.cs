using System;
using System.Linq;
using System.Web.Mvc;
using CarbonFitness.BusinessLogic;
using CarbonFitness.BusinessLogic.Exceptions;
using CarbonFitness.Data.Model;
using CarbonFitnessWeb.Models;
using CarbonFitnessWeb.ViewConstants;

namespace CarbonFitnessWeb.Controllers {
    public class ResultController : Controller {
        private readonly IUserContext userContext;
        private readonly IUserIngredientBusinessLogic userIngredientBusinessLogic;

        public ResultController(IUserIngredientBusinessLogic userIngredientBusinessLogic, IUserContext userContext) {
            this.userIngredientBusinessLogic = userIngredientBusinessLogic;
            this.userContext = userContext;
        }

        public ActionResult Show() {
            return View();
        }

        [HttpPost]
        public ActionResult Show(ResultModel model) {
            UserIngredient[] userIngredients = getUserIngredients(model.Date);
            model.SumOfCalories = userIngredients.Sum(u => u.Ingredient.Calories).ToString();
            return View(model);
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
}