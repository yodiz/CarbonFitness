﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CarbonFitness.App.Web;
using CarbonFitness.App.Web.Models;
using CarbonFitness.BusinessLogic;
using CarbonFitness.Data.Model;
using Moq;
using NUnit.Framework;

namespace CarbonFitnessTest.Web.Controller.ResultController {
	[TestFixture]
	public class ShowTest {
		[SetUp]
		public void SetUp() {
			userIngredientBusinessLogicMock = new Mock<IUserIngredientBusinessLogic>();
			userContextMock = new Mock<IUserContext>();
		}

		private Mock<IUserIngredientBusinessLogic> userIngredientBusinessLogicMock;
		private Mock<IUserContext> userContextMock;

		private ResultModel RunMethodUnderTest() {
			var resultController = new CarbonFitness.App.Web.Controllers.ResultController(userIngredientBusinessLogicMock.Object, userContextMock.Object);
			var actionResult = (ViewResult) resultController.Show();
			return (ResultModel) actionResult.ViewData.Model;
		}

		[Test]
		public void shouldShowCalorieHistory() {
			IDictionary<DateTime, double> expectedCalorieHistory = new Dictionary<DateTime, double> { { DateTime.Now.Date.AddDays(-1), 2000 }, { DateTime.Now.Date, 2150 } };

			userIngredientBusinessLogicMock.Setup(x => x.GetCalorieHistory(It.IsAny<User>())).Returns(expectedCalorieHistory);

			var model = RunMethodUnderTest();

			userIngredientBusinessLogicMock.VerifyAll();
			Assert.That(model.CalorieHistoryList, Is.SameAs(expectedCalorieHistory));
		}
	}
}