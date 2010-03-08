using System.Web.Mvc;
using CarbonFitness.BusinessLogic;
using CarbonFitness.Data.Model;
using CarbonFitnessWeb.Models;
using System.Linq;

namespace CarbonFitnessWeb.Controllers {
    public class ResultController : Controller {
        private readonly IUserIngredientBusinessLogic userIngredientBusinessLogic;
        private readonly IUserContext userContext;

        public ResultController(IUserIngredientBusinessLogic userIngredientBusinessLogic, IUserContext userContext) {
            this.userIngredientBusinessLogic = userIngredientBusinessLogic;
            this.userContext = userContext;
        }

        public ActionResult Show() {
            return View();
        }

        [HttpPost]
        public ActionResult Show(ResultModel model) {
            var userIngredients = userIngredientBusinessLogic.GetUserIngredients(userContext.User, model.Date);
            model.SumOfCalories = userIngredients.Sum(u => u.Ingredient.Calories).ToString();
            return View(model);
        }
    }
}