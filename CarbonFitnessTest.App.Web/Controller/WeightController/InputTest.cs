using System;
using System.Web.Mvc;
using CarbonFitness.App.Web;
using CarbonFitness.App.Web.Models;
using CarbonFitness.BusinessLogic;
using CarbonFitness.Data.Model;
using Moq;
using NUnit.Framework;

namespace CarbonFitnessTest.Web.Controller.WeightController {
	[TestFixture]
	public class InputTest {
		[Test]
		public void shouldSaveWeight() {
			var expectedWeight = 80M;
			var expectedDate = DateTime.Now;
			var userContextMock = new Mock<IUserContext>();
			var userWeightMock = new Mock<IUserWeightBusinessLogic>();
			var user = new User {Username = "arne"};
			userContextMock.Setup(x => x.User).Returns(user);
			userWeightMock.Setup(x => x.SaveWeight(It.Is<User>(u => u.Username == "arne"), expectedWeight, expectedDate)).Returns(new UserWeight {Weight = expectedWeight});

			var actionResult = (ViewResult) new CarbonFitness.App.Web.Controllers.WeightController(userWeightMock.Object, userContextMock.Object).Input(new InputWeightModel {Weight = expectedWeight, Date = expectedDate});
			var inputWeightModel = (InputWeightModel) actionResult.ViewData.Model;

			userWeightMock.VerifyAll();
			Assert.That(inputWeightModel.Weight, Is.EqualTo(expectedWeight));
			Assert.That(inputWeightModel.Date, Is.EqualTo(expectedDate));
		}
	}
}