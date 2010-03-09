using System;
using System.Web.Mvc;
using CarbonFitness.App.Web.Models;
using CarbonFitness.BusinessLogic;

namespace CarbonFitness.App.Web.Controllers {
	public class WeightController : Controller {
		private readonly IUserWeightBusinessLogic userWeightBusinessLogic;
		private readonly IUserContext userContext;

		public WeightController(IUserWeightBusinessLogic userWeightBusinessLogic, IUserContext userContext) {
			this.userWeightBusinessLogic = userWeightBusinessLogic;
			this.userContext = userContext;
		}

		public ActionResult Input() {
			return View(new InputWeightModel {Date = DateTime.Now});
		}

		public ActionResult Input(InputWeightModel inputWeightModel)
		{
			userWeightBusinessLogic.SaveWeight(userContext.User, inputWeightModel.Weight, inputWeightModel.Date);
			return View(inputWeightModel);
		}
	}
}