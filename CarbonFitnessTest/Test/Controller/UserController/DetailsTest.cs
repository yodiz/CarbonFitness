using System.Web.Mvc;
using CarbonFitness.Model;
using CarbonFitness.Repository;
using CarbonFitnessWeb.Controllers;
using Moq;
using NUnit.Framework;

namespace CarbonFitnessTest.Test.UserCotroller {
    [TestFixture]
    public class DetailsTest {
        [Test]
        public void detailsShouldReturnUser() {
            IUserRepository userRepository;
            var factory = new MockFactory(MockBehavior.Strict);
            var mock = factory.Create<IUserRepository>();
            mock.Setup(x => x.Get(1)).Returns(new User { Username = "kalle" });
            userRepository = mock.Object;

            var controller = new UserController(userRepository);
            var viewResult = (ViewResult) controller.Details(1);

            var user = (User) viewResult.ViewData.Model;

            Assert.IsInstanceOfType(typeof(User), user);
            Assert.AreEqual("kalle", user.Username);
        }
    }
}