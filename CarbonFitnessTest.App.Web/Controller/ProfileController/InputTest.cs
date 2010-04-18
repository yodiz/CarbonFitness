using System;
using System.Collections.Generic;
using System.Linq;
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
            const string gender = "Kvinna";
            userProfileBusinessLogicMock.Setup(y => y.SaveProfile(It.IsAny<User>(), idealWeight, length, weight, gender));
            runMethodUnderTest(userProfileBusinessLogicMock.Object, x => x.Input(new ProfileModel { IdealWeight = idealWeight, Length = length, Weight = weight, SelectedGender = gender }));
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
            var genderTypeBusinessLogicMock = new Mock<IGenderTypeBusinessLogic>();
            var expectedGenders = new List<GenderType>();
            var userProfileBusinessLogicMock = new Mock<IUserProfileBusinessLogic>();

            userProfileBusinessLogicMock.Setup(x => x.GetGender(It.IsAny<User>())).Returns(new GenderType{Name= "Kvinna"});
            genderTypeBusinessLogicMock.Setup(x => x.GetGenderTypes()).Returns(expectedGenders);
            var profileController = GetProfileController(new Mock<IUserProfileBusinessLogic>().Object, genderTypeBusinessLogicMock.Object);
            var profileModel = getModel(x => x.Input(new ProfileModel()), profileController);

            genderTypeBusinessLogicMock.VerifyAll();
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
        public void shouldShowBMIForLoggedinUser() {
            const decimal expectedBMI = 32;

            var userProfileBusinessLogicMock = new Mock<IUserProfileBusinessLogic>();
            userProfileBusinessLogicMock.Setup(x => x.GetBMI(It.IsAny<User>())).Returns(expectedBMI);

            var profileModel = runMethodUnderTest(userProfileBusinessLogicMock.Object, x => x.Input());

            Assert.That(profileModel.BMI, Is.EqualTo(expectedBMI));
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

	    [Test]
        public void shouldShowStoredGenderForLoggedinUser() {
            var expectedGender = new GenderType{Name = "selected"};

            var userProfileBusinessLogicMock = new Mock<IUserProfileBusinessLogic>();
            userProfileBusinessLogicMock.Setup(x => x.GetGender(It.IsAny<User>())).Returns(expectedGender);

	        var genderBusinessLogicMock = new Mock<IGenderTypeBusinessLogic>();
	        genderBusinessLogicMock.Setup(x => x.GetGenderTypes()).Returns(new List<GenderType> {new GenderType {Name = "selected"}, new GenderType {Name = "notSelected"}});

            var profileController = GetProfileController(userProfileBusinessLogicMock.Object, genderBusinessLogicMock.Object);
            var profileModel = getModel(x => x.Input(), profileController);

	        var selectedGender = (from gen in profileModel.GenderViewTypes where gen.Selected select gen).FirstOrDefault();
            
            userProfileBusinessLogicMock.VerifyAll();
            Assert.That(selectedGender.Text, Is.SameAs(expectedGender.Name));
        }

        private ProfileModel runMethodUnderTest(IUserProfileBusinessLogic userProfileBusinessLogicMock, Func<CarbonFitness.App.Web.Controllers.ProfileController, ActionResult> methodUnderTest) {
            var profileController = GetProfileController(userProfileBusinessLogicMock, new Mock<IGenderTypeBusinessLogic>().Object);
            return getModel(methodUnderTest, profileController);
        }

	    private ProfileModel getModel(Func<CarbonFitness.App.Web.Controllers.ProfileController, ActionResult> methodUnderTest, CarbonFitness.App.Web.Controllers.ProfileController profileController) {
	        var actionResult = (ViewResult)methodUnderTest(profileController);
	        return (ProfileModel)actionResult.ViewData.Model;
	    }

        private CarbonFitness.App.Web.Controllers.ProfileController GetProfileController(IUserProfileBusinessLogic userProfileBusinessLogicMock, IGenderTypeBusinessLogic genderTypeBusinessLogicMock) {
            return new CarbonFitness.App.Web.Controllers.ProfileController(userProfileBusinessLogicMock, genderTypeBusinessLogicMock, getSetuppedUserContextMock().Object);
	    }

	    [Test]
        public void shouldNotSaveWeightWhenModelStateIsInvalid() {
            var userProfileBusinessLogicMock = new Mock<IUserProfileBusinessLogic>(MockBehavior.Strict);

            var profileController = new CarbonFitness.App.Web.Controllers.ProfileController(userProfileBusinessLogicMock.Object, null, null);
            profileController.ModelState.AddModelError("ff", "ll");
            profileController.Input(new ProfileModel { IdealWeight = 70 });

            userProfileBusinessLogicMock.Verify(x => x.SaveProfile(It.IsAny<User>(), It.IsAny<Decimal>(), It.IsAny<Decimal>(), It.IsAny<Decimal>(), It.IsAny<string>()), Times.Never());
        }
	}
}