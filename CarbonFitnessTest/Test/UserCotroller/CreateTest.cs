using System.Web.Mvc;
using CarbonFitness.Model;
using CarbonFitnessWeb.Controllers;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace CarbonFitnessTest.Test.UserCotroller {
    [TestFixture]
    public class CreateTest {

        [Test]
        public void shouldRedirectToView() {
            var controller = new UserController(RepositoryMock.GetUserRepository());
            var viewResult = (RedirectToRouteResult)controller.Create("userName");

            var actionRouteValue = viewResult.RouteValues["action"];
            Assert.That(actionRouteValue.ToString() == "Details", "Expected view, but was:" + actionRouteValue);
        }
    }
}