using System.Web.Mvc;
using CarbonFitness.App.Web.Models;
using CarbonFitness.BusinessLogic;

namespace CarbonFitness.App.Web.Controllers {
	public class ResultController : Controller {
		private readonly IUserContext userContext;
		private readonly IUserIngredientBusinessLogic userIngredientBusinessLogic;
        private readonly IUserProfileBusinessLogic userProfileBusinessLogic;

		public ResultController(IUserProfileBusinessLogic userProfileBusinessLogic, IUserIngredientBusinessLogic userIngredientBusinessLogic, IUserContext userContext) {
			this.userIngredientBusinessLogic = userIngredientBusinessLogic;
			this.userContext = userContext;
		    this.userProfileBusinessLogic = userProfileBusinessLogic;
		}

		public ActionResult Show() {
			var model = new ResultModel();
			model.CalorieHistoryList = userIngredientBusinessLogic.GetCalorieHistory(userContext.User);
		    model.IdealWeight = userProfileBusinessLogic.GetIdealWeight(userContext.User);
			return View(model);
		}
	}
}