using System;
using System.Web.Mvc;
using CarbonFitness.App.Web;
using CarbonFitness.App.Web.Models;
using CarbonFitness.BusinessLogic;
using CarbonFitness.Data.Model;
using Moq;
using NUnit.Framework;

namespace CarbonFitnessTest.Web.Controller.ProfileController {
	[TestFixture]
	public class InputTest {
		private static Mock<IUserContext> getSetuppedUserContextMock() {
			var userContextMock = new Mock<IUserContext>();
			userContextMock.Setup(x => x.User).Returns(new User {Username = "myUser"});
			return userContextMock;
		}

		[Test]
		public void shouldNotSaveWeightWhenModelStateIsInvalid() {
			var userProfileBusinessLogicMock = new Mock<IUserProfileBusinessLogic>(MockBehavior.Strict);

			var profileController = new CarbonFitness.App.Web.Controllers.ProfileController(userProfileBusinessLogicMock.Object, null);
			profileController.ModelState.AddModelError("ff", "ll");

			profileController.Input(new ProfileModel { IdealWeight = 70});

			userProfileBusinessLogicMock.Verify(x => x.SaveIdealWeight(It.IsAny<User>(), It.IsAny<Decimal>()), Times.Never());
		}

		[Test]
		public void shouldSaveIdealWeightValue() {
			const int idealWeight = 75;
			var userProfileBusinessLogicMock = new Mock<IUserProfileBusinessLogic>(MockBehavior.Strict);
			userProfileBusinessLogicMock.Setup(x => x.SaveIdealWeight(It.IsAny<User>(), idealWeight));

			var userContextMock = getSetuppedUserContextMock();
			var profileController = new CarbonFitness.App.Web.Controllers.ProfileController(userProfileBusinessLogicMock.Object, userContextMock.Object);
			var actionResult = (ViewResult) profileController.Input(new ProfileModel {IdealWeight = idealWeight, Length = 1.80M});

			Assert.That(((ProfileModel) actionResult.ViewData.Model).IdealWeight, Is.EqualTo(idealWeight));
		}

		[Test]
		public void shouldShowStoredIdealWeightValueForLoggedinUser() {
			const decimal expectedIdealWeight = 65;

			var userProfileBusinessLogicMock = new Mock<IUserProfileBusinessLogic>(MockBehavior.Strict);
			userProfileBusinessLogicMock.Setup(x => x.GetIdealWeight(It.IsAny<User>())).Returns(expectedIdealWeight);
			var userContextMock = getSetuppedUserContextMock();

			var profileController = new CarbonFitness.App.Web.Controllers.ProfileController(userProfileBusinessLogicMock.Object, userContextMock.Object);
			var actionResult = (ViewResult) profileController.Input();

			Assert.That(((ProfileModel) actionResult.ViewData.Model).IdealWeight, Is.EqualTo(expectedIdealWeight));
		}
	}
}