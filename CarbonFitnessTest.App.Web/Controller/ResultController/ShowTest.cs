using System;
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
		    userProfileBusinessLogic = new Mock<IUserProfileBusinessLogic>();
		}

		private Mock<IUserIngredientBusinessLogic> userIngredientBusinessLogicMock;
		private Mock<IUserContext> userContextMock;
        private Mock<IUserProfileBusinessLogic> userProfileBusinessLogic;

		private ResultModel RunMethodUnderTest() {
            var resultController = new CarbonFitness.App.Web.Controllers.ResultController(userProfileBusinessLogic.Object, userIngredientBusinessLogicMock.Object, userContextMock.Object);
			var actionResult = (ViewResult) resultController.Show();
			return (ResultModel) actionResult.ViewData.Model;
		}

		[Test]
		public void shouldShowCalorieHistory() {
			var expectedCalorieHistory = new HistoryValues(new Dictionary<DateTime, double> { { DateTime.Now.Date.AddDays(-1), 2000 }, { DateTime.Now.Date, 2150 } });
            userIngredientBusinessLogicMock.Setup(x => x.GetCalorieHistory(It.IsAny<User>())).Returns(expectedCalorieHistory);

			var model = RunMethodUnderTest();

			userIngredientBusinessLogicMock.VerifyAll();
			Assert.That(model.CalorieHistoryList, Is.SameAs(expectedCalorieHistory));
		}

        [Test]
        public void shouldShowIdealWeight() {
            decimal userIdealWeight = 65;
            userProfileBusinessLogic.Setup(x => x.GetIdealWeight(It.IsAny<User>())).Returns(userIdealWeight);
            var model = RunMethodUnderTest();
            Assert.That(model.IdealWeight, Is.EqualTo(userIdealWeight));
        }
	}
}