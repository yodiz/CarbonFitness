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
        public void shouldSaveProfile() {
            var userProfileBusinessLogicMock = new Mock<IUserProfileBusinessLogic>();
            const decimal length = 1.80M;
            const int idealWeight = 75;
            const int weight = 83;
            userProfileBusinessLogicMock.Setup(y => y.SaveProfile(It.IsAny<User>(), idealWeight, length, weight));
            runMethodUnderTest(userProfileBusinessLogicMock, x => x.Input(new ProfileModel { IdealWeight = idealWeight, Length = length, Weight = weight }));
            userProfileBusinessLogicMock.VerifyAll();
        }

        [Test]
        public void shouldShowBMIAfterSaveProfile() {
            var userProfileBusinessLogicMock = new Mock<IUserProfileBusinessLogic>();
            var expectedBMI = 23;
            userProfileBusinessLogicMock.Setup(x => x.GetBMI(It.IsAny<User>())).Returns(expectedBMI);
            var model =  runMethodUnderTest(userProfileBusinessLogicMock, x => x.Input(new ProfileModel()));
            userProfileBusinessLogicMock.VerifyAll();
            Assert.That(model.BMI, Is.EqualTo(expectedBMI));
        }

	    [Test]
        public void shouldShowStoredWeightValueForLoggedinUser() {
            const decimal expectedWeight = 85;

            var userProfileBusinessLogicMock = new Mock<IUserProfileBusinessLogic>();
            userProfileBusinessLogicMock.Setup(x => x.GetWeight(It.IsAny<User>())).Returns(expectedWeight);

            var profileModel = runMethodUnderTest(userProfileBusinessLogicMock, x => x.Input());

            Assert.That(profileModel.Weight, Is.EqualTo(expectedWeight));
        }

        [Test]
        public void shouldShowBMIForLoggedinUser() {
            const decimal expectedBMI = 32;

            var userProfileBusinessLogicMock = new Mock<IUserProfileBusinessLogic>();
            userProfileBusinessLogicMock.Setup(x => x.GetBMI(It.IsAny<User>())).Returns(expectedBMI);

            var profileModel = runMethodUnderTest(userProfileBusinessLogicMock, x => x.Input());

            Assert.That(profileModel.BMI, Is.EqualTo(expectedBMI));
        }

	    [Test]
		public void shouldShowStoredIdealWeightValueForLoggedinUser() {
			const decimal expectedIdealWeight = 65;

			var userProfileBusinessLogicMock = new Mock<IUserProfileBusinessLogic>();
			userProfileBusinessLogicMock.Setup(x => x.GetIdealWeight(It.IsAny<User>())).Returns(expectedIdealWeight);
            
            var profileModel = runMethodUnderTest(userProfileBusinessLogicMock, x => x.Input());

            Assert.That(profileModel.IdealWeight, Is.EqualTo(expectedIdealWeight));
		}

        [Test]
        public void shouldShowStoredLengthForLoggedinUser() {
            const decimal expectedLenght = 1.82M;

            var userProfileBusinessLogicMock = new Mock<IUserProfileBusinessLogic>();
            userProfileBusinessLogicMock.Setup(x => x.GetLength(It.IsAny<User>())).Returns(expectedLenght);

            var profileModel = runMethodUnderTest(userProfileBusinessLogicMock, x => x.Input());

            Assert.That(profileModel.Length, Is.EqualTo(expectedLenght));
        }

        private ProfileModel runMethodUnderTest(Mock<IUserProfileBusinessLogic> userProfileBusinessLogicMock, Func<CarbonFitness.App.Web.Controllers.ProfileController, ActionResult> methodUnderTest) {
            var profileController = new CarbonFitness.App.Web.Controllers.ProfileController(userProfileBusinessLogicMock.Object, getSetuppedUserContextMock().Object);
            var actionResult = (ViewResult)methodUnderTest(profileController);
            return (ProfileModel)actionResult.ViewData.Model;
        }

        [Test]
        public void shouldNotSaveWeightWhenModelStateIsInvalid() {
            var userProfileBusinessLogicMock = new Mock<IUserProfileBusinessLogic>(MockBehavior.Strict);

            var profileController = new CarbonFitness.App.Web.Controllers.ProfileController(userProfileBusinessLogicMock.Object, null);
            profileController.ModelState.AddModelError("ff", "ll");
            profileController.Input(new ProfileModel { IdealWeight = 70 });

            userProfileBusinessLogicMock.Verify(x => x.SaveProfile(It.IsAny<User>(), It.IsAny<Decimal>(), It.IsAny<Decimal>(), It.IsAny<Decimal>()), Times.Never());
        }
	}
}