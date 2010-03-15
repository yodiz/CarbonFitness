using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        [Test]
        public void shouldSaveIdealWeightValue() {


            var userRepositoryMock = new Mock<IUserBusinessLogic>(MockBehavior.Strict);
            userRepositoryMock.Setup(x => x.SaveOrUpdate(It.IsAny<User>())).Returns(new User());

            var profileController = new CarbonFitness.App.Web.Controllers.ProfileController(); 
            profileController.Input()

        }
    }
}
