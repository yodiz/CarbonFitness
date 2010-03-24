using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using CarbonFitness.App.Web.Models;
using CarbonFitness.BusinessLogic;
using MvcContrib.ActionResults;

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

		public ActionResult ShowXml()
		{
			var historyValueContainer = userIngredientBusinessLogic.GetCalorieHistory(userContext.User);

			var amChartData = new AmChartData();

			Mapper.Map(historyValueContainer, amChartData);

			return new XmlResult(amChartData);
		}
	}

	public class HistoryValueWrapper {

		public HistoryValueWrapper() {}

		public HistoryValueWrapper(DateTime date, decimal value) {
			Date = date;
			Value = value;
		}

		public decimal Value { get; set; }
		public DateTime Date { get; set; }
	}
}