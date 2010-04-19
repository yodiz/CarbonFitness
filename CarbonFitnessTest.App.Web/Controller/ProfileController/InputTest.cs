using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CarbonFitness.App.Web;
using CarbonFitness.App.Web.Controllers.ViewTypeConverters;
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
            const decimal idealWeight = 75;
            const decimal weight = 83;
            const int age = 25;
            const string gender = "Kvinna";
            const string activityLevel = "Medel";
            userProfileBusinessLogicMock.Setup(y => y.SaveProfile(It.IsAny<User>(), idealWeight, length, weight, age, gender, activityLevel));
            runMethodUnderTest(userProfileBusinessLogicMock.Object, x => x.Input(new ProfileModel { IdealWeight = idealWeight, Length = length, Weight = weight, Age = age, SelectedGender = gender, SelectedActivityLevel = activityLevel }));
            userProfileBusinessLogicMock.VerifyAll();
        }

        [Test]
        public void shouldShowBMIAfterSaveProfile() {
            var userProfileBusinessLogicMock = new Mock<IUserProfileBusinessLogic>();
            var expectedBMI = 23;
            userProfileBusinessLogicMock.Setup(x => x.GetBMI(It.IsAny<User>())).Returns(expectedBMI);
            var model = runMethodUnderTest(userProfileBusinessLogicMock.Object, x => x.Input(new ProfileModel()));
            userProfileBusinessLogicMock.VerifyAll();
            Assert.That(model.BMI, Is.EqualTo(expectedBMI));
        }

        [Test]
        public void shouldShowGendersAfterSaveProfile() {
            var genderViewTypeConverter = new Mock<IGenderViewTypeConverter>();
            var expectedGenders = new List<SelectListItem>();

            genderViewTypeConverter.Setup(x => x.GetViewTypes(It.IsAny<User>())).Returns(expectedGenders);
            var profileController = GetProfileController(new Mock<IUserProfileBusinessLogic>().Object, genderViewTypeConverter.Object, new Mock<IActivityLevelViewTypeConverter>().Object);
            var profileModel = getModel(x => x.Input(new ProfileModel()), profileController);

            genderViewTypeConverter.VerifyAll();
            Assert.That(profileModel.GenderViewTypes.Count(), Is.EqualTo(expectedGenders.Count));
        }

	    [Test]
        public void shouldShowStoredWeightValueForLoggedinUser() {
            const decimal expectedWeight = 85;

            var userProfileBusinessLogicMock = new Mock<IUserProfileBusinessLogic>();
            userProfileBusinessLogicMock.Setup(x => x.GetWeight(It.IsAny<User>())).Returns(expectedWeight);

            var profileModel = runMethodUnderTest(userProfileBusinessLogicMock.Object, x => x.Input());

            Assert.That(profileModel.Weight, Is.EqualTo(expectedWeight));
        }

        [Test]
        public void shouldShowStoredAgeForLoggedinUser() {
            const int expectedAge = 25;

            var userProfileBusinessLogicMock = new Mock<IUserProfileBusinessLogic>();
            userProfileBusinessLogicMock.Setup(x => x.GetAge(It.IsAny<User>())).Returns(expectedAge);

            var profileModel = runMethodUnderTest(userProfileBusinessLogicMock.Object, x => x.Input());

            Assert.That(profileModel.Age, Is.EqualTo(expectedAge));
        }

        [Test]
        public void shouldShowBMIForLoggedinUser() {
            const decimal expectedBMI = 32;

            var userProfileBusinessLogicMock = new Mock<IUserProfileBusinessLogic>();
            userProfileBusinessLogicMock.Setup(x => x.GetBMI(It.IsAny<User>())).Returns(expectedBMI);

            var profileModel = runMethodUnderTest(userProfileBusinessLogicMock.Object, x => x.Input());

            Assert.That(profileModel.BMI, Is.EqualTo(expectedBMI));
        }

        [Test]
        public void shouldShowDailyCalorieNeedForLoggedinUser() {
            const decimal expectedDailyCalorieNeed = 32;

            var userProfileBusinessLogicMock = new Mock<IUserProfileBusinessLogic>();
            userProfileBusinessLogicMock.Setup(x => x.GetDailyCalorieNeed(It.IsAny<User>())).Returns(expectedDailyCalorieNeed);

            var profileModel = runMethodUnderTest(userProfileBusinessLogicMock.Object, x => x.Input());

            Assert.That(profileModel.DailyCalorieNeed, Is.EqualTo(expectedDailyCalorieNeed));
        }


        [Test]
        public void shouldShowBMRForLoggedinUser() {
            const decimal expectedBMR = 32;

            var userProfileBusinessLogicMock = new Mock<IUserProfileBusinessLogic>();
            userProfileBusinessLogicMock.Setup(x => x.GetBMR(It.IsAny<User>())).Returns(expectedBMR);

            var profileModel = runMethodUnderTest(userProfileBusinessLogicMock.Object, x => x.Input());

            Assert.That(profileModel.BMR, Is.EqualTo(expectedBMR));
        }


        [Test]
		public void shouldShowStoredIdealWeightValueForLoggedinUser() {
			const decimal expectedIdealWeight = 65;

			var userProfileBusinessLogicMock = new Mock<IUserProfileBusinessLogic>();
			userProfileBusinessLogicMock.Setup(x => x.GetIdealWeight(It.IsAny<User>())).Returns(expectedIdealWeight);

            var profileModel = runMethodUnderTest(userProfileBusinessLogicMock.Object, x => x.Input());

            Assert.That(profileModel.IdealWeight, Is.EqualTo(expectedIdealWeight));
		}

        [Test]
        public void shouldShowStoredLengthForLoggedinUser() {
            const decimal expectedLenght = 1.82M;

            var userProfileBusinessLogicMock = new Mock<IUserProfileBusinessLogic>();
            userProfileBusinessLogicMock.Setup(x => x.GetLength(It.IsAny<User>())).Returns(expectedLenght);

            var profileModel = runMethodUnderTest(userProfileBusinessLogicMock.Object, x => x.Input());

            Assert.That(profileModel.Length, Is.EqualTo(expectedLenght));
        }

        private ProfileModel runMethodUnderTest(IUserProfileBusinessLogic userProfileBusinessLogicMock, Func<CarbonFitness.App.Web.Controllers.ProfileController, ActionResult> methodUnderTest) {
            var profileController = GetProfileController(userProfileBusinessLogicMock, new Mock<IGenderViewTypeConverter>().Object, new Mock<IActivityLevelViewTypeConverter>().Object);
            return getModel(methodUnderTest, profileController);
        }

	    private ProfileModel getModel(Func<CarbonFitness.App.Web.Controllers.ProfileController, ActionResult> methodUnderTest, CarbonFitness.App.Web.Controllers.ProfileController profileController) {
	        var actionResult = (ViewResult)methodUnderTest(profileController);
	        return (ProfileModel)actionResult.ViewData.Model;
	    }

        private CarbonFitness.App.Web.Controllers.ProfileController GetProfileController(IUserProfileBusinessLogic userProfileBusinessLogicMock, IGenderViewTypeConverter genderViewTypeConverter, IActivityLevelViewTypeConverter activityLevelViewTypeConverter) {
            return new CarbonFitness.App.Web.Controllers.ProfileController(userProfileBusinessLogicMock, genderViewTypeConverter, activityLevelViewTypeConverter, getSetuppedUserContextMock().Object);
	    }

	    [Test]
        public void shouldNotSaveWeightWhenModelStateIsInvalid() {
            var userProfileBusinessLogicMock = new Mock<IUserProfileBusinessLogic>();

            var profileController = new CarbonFitness.App.Web.Controllers.ProfileController(userProfileBusinessLogicMock.Object, new Mock<IGenderViewTypeConverter>().Object, new Mock<IActivityLevelViewTypeConverter>().Object, getSetuppedUserContextMock().Object);
            profileController.ModelState.AddModelError("ff", "ll");
            profileController.Input(new ProfileModel { IdealWeight = 70 });

            userProfileBusinessLogicMock.Verify(x => x.SaveProfile(It.IsAny<User>(), It.IsAny<Decimal>(), It.IsAny<Decimal>(), It.IsAny<Decimal>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        }
	}
}