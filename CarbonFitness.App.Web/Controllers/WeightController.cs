using System;
using System.Web.Mvc;
using CarbonFitness.App.Web.Models;
using CarbonFitness.App.Web.ViewConstants;
using CarbonFitness.BusinessLogic;
using CarbonFitness.BusinessLogic.Exceptions;
using CarbonFitness.Data.Model;

namespace CarbonFitness.App.Web.Controllers {
	public class WeightController : Controller {
		private readonly IUserWeightBusinessLogic userWeightBusinessLogic;
		private readonly IUserContext userContext;

		public WeightController(IUserWeightBusinessLogic userWeightBusinessLogic, IUserContext userContext) {
			this.userWeightBusinessLogic = userWeightBusinessLogic;
			this.userContext = userContext;
		}

		public ActionResult Input(DateTime? id) {
			if(!id.HasValue) {
				id = DateTime.Now.Date;
			}

			var userWeight = userWeightBusinessLogic.GetWeight(userContext.User, id.Value);
			return View(new InputWeightModel(userWeight));
		}

		[HttpPost]
		public ActionResult Input(InputWeightModel inputWeightModel) {
			UserWeight userWeight = null;
			try {
				userWeight = userWeightBusinessLogic.SaveWeight(userContext.User, inputWeightModel.Weight, inputWeightModel.Date);	
			} catch(InvalidWeightException e) {
				ModelState.AddModelError("Weight", WeightConstant.ZeroWeightErrorMessage);
			}

			return View(new InputWeightModel(userWeight));
		}
	}
}