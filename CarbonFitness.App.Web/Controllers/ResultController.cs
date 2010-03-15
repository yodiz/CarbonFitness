using System.Web.Mvc;
using CarbonFitness.App.Web.Models;
using CarbonFitness.BusinessLogic;

namespace CarbonFitness.App.Web.Controllers {
	public class ResultController : Controller {
		private readonly IUserContext userContext;
		private readonly IUserIngredientBusinessLogic userIngredientBusinessLogic;

		public ResultController(IUserIngredientBusinessLogic userIngredientBusinessLogic, IUserContext userContext) {
			this.userIngredientBusinessLogic = userIngredientBusinessLogic;
			this.userContext = userContext;
		}

		public ActionResult Show() {
			var model = new ResultModel();
			model.CalorieHistoryList = userIngredientBusinessLogic.GetCalorieHistory(userContext.User);
			return View(model);
		}

		//[HttpPost]
		//public ActionResult Show(ResultModel model) {
		//   //var userIngredients = userIngredientBusinessLogic.GetUserIngredients(userContext.User, model.Date);
		//   //model.SumOfCalories = userIngredients.Sum(u => u.Ingredient.EnergyInKcal).ToString();
		//   model.CalorieHistoryList = userIngredientBusinessLogic.GetCalorieHistory(userContext.User);
		//   return View(model);
		//}
	}
}