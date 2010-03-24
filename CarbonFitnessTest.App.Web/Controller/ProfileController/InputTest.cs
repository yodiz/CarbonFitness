using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web.Mvc;
using CarbonFitness.App.Web;
using CarbonFitness.App.Web.Models;
using CarbonFitness.BusinessLogic;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace CarbonFitnessTest.Web.Controller.ProfileController
{
    [TestFixture]
    public class InputTest
    {
        private Mock<IUserContext> getSetuppedUserContextMock()
        {
            var userContextMock = new Mock<IUserContext>();
            userContextMock.Setup(x => x.User).Returns(new User { Username = "myUser" });
           return userContextMock;
        }

        [Test]
        public void shouldSaveIdealWeightValue() {
            int idealWeight = 75;
            var userProfileBusinessLogicMock = new Mock<IUserProfileBusinessLogic>(MockBehavior.Strict);
            userProfileBusinessLogicMock.Setup(x => x.SaveIdealWeight(It.IsAny<User>(),idealWeight));

            var userContextMock = getSetuppedUserContextMock();
            var profileController = new CarbonFitness.App.Web.Controllers.ProfileController(userProfileBusinessLogicMock.Object, userContextMock.Object);

            var actionResult = (ViewResult)profileController.Input(new ProfileModel {IdealWeight = idealWeight, Length = DateTime.Now});

            Assert.That(((ProfileModel) actionResult.ViewData.Model).IdealWeight, Is.EqualTo(idealWeight));
        }

        [Test]
        public void shouldShowStoredIdealWeightValueForLoggedinUser() {
            decimal expectedIdealWeight = 65;

            var userProfileBusinessLogicMock = new Mock<IUserProfileBusinessLogic>(MockBehavior.Strict);
            userProfileBusinessLogicMock.Setup(x => x.GetIdealWeight(It.IsAny<User>())).Returns(expectedIdealWeight);
            var userContextMock = getSetuppedUserContextMock();

            var profileController = new CarbonFitness.App.Web.Controllers.ProfileController(userProfileBusinessLogicMock.Object, userContextMock.Object);
            var actionResult = (ViewResult)profileController.Input();

            Assert.That(((ProfileModel)actionResult.ViewData.Model).IdealWeight, Is.EqualTo(expectedIdealWeight));
        }

        //[Test]
        //public void shouldValidateIdealWeightToBeNumberAnd
    }
}
