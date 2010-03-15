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
            var userRepositoryMock = new Mock<IUserBusinessLogic>(MockBehavior.Strict);
            userRepositoryMock.Setup(x => x.SaveOrUpdate(It.IsAny<User>())).Returns(new User());

            var userContextMock = getSetuppedUserContextMock();
            var profileController = new CarbonFitness.App.Web.Controllers.ProfileController(userRepositoryMock.Object, userContextMock.Object);
            var actionResult = (ViewResult)profileController.Input(new ProfileModel() {IdealWeight = 75, Length = DateTime.Now});

            Assert.That(((ProfileModel) actionResult.ViewData.Model).IdealWeight, Is.EqualTo(75));
        }
    }
}
