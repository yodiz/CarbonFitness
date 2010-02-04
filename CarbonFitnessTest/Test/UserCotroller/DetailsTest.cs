using System.Web.Mvc;
using CarbonFitness.Model;
using CarbonFitness.Repository;
using CarbonFitnessWeb.Controllers;
using NUnit.Framework;

namespace CarbonFitnessTest.Test.UserCotroller {
    [TestFixture]
    public class DetailsTest {
        [Test]
        public void detailsShouldReturnUser() {
            var userRepository = RepositoryMock.GetUserRepository();

            var controller = new UserController(userRepository);
            var viewResult = (ViewResult) controller.Details("username");

            var user = (User)viewResult.ViewData["User"];

            Assert.IsInstanceOfType(typeof(User), user);
            Assert.AreEqual("username", user.Username);
        }
    }
}