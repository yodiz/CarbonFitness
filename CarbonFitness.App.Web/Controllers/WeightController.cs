using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CarbonFitness.App.Web.Models;
using CarbonFitness.App.Web.ViewConstants;
using CarbonFitness.BusinessLogic;
using CarbonFitness.BusinessLogic.Exceptions;
using CarbonFitness.Data.Model;
using SharpArch.Web.NHibernate;

namespace CarbonFitness.App.Web.Controllers {
	public class WeightController : Controller {
		private readonly IUserWeightBusinessLogic userWeightBusinessLogic;
		private readonly IUserContext userContext;

		public WeightController(IUserWeightBusinessLogic userWeightBusinessLogic, IUserContext userContext) {
			this.userWeightBusinessLogic = userWeightBusinessLogic;
			this.userContext = userContext;
		}

		[Authorize]
		public ActionResult Input(DateTime? id) {
			if(!id.HasValue) {
				id = DateTime.Now.Date;
			}

			var userWeight = userWeightBusinessLogic.GetUserWeight(userContext.User, id.Value);
         var userWeightHistory = userWeightBusinessLogic.GetHistoryList(userContext.User);

			return View(initializeInputWeightModel(id.Value, userWeight, userWeightHistory));
		}
      
		[HttpPost]
		[Transaction]
		[Authorize]
		public ActionResult Input(InputWeightModel inputWeightModel) {
			UserWeight userWeight = null;
			try {
				userWeight = userWeightBusinessLogic.SaveWeight(userContext.User, inputWeightModel.Weight, inputWeightModel.Date);	
			} catch(InvalidWeightException e) {
				ModelState.AddModelError("Weight", WeightConstant.ZeroWeightErrorMessage);
			}
			var userWeightHistory = userWeightBusinessLogic.GetHistoryList(userContext.User);

			return View(initializeInputWeightModel(inputWeightModel.Date, userWeight, userWeightHistory));
		}

		private InputWeightModel initializeInputWeightModel(DateTime date, UserWeight userWeight, IEnumerable<UserWeight> userWeightHistoryList) {
			var model = new InputWeightModel();
			if (userWeight != null) {
				model.Weight = userWeight.Weight;
			}
			model.Date = date;
			model.UserWeightHistoryList = userWeightHistoryList;
			return model;
		}
	}
}