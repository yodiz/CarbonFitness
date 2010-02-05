using System.Web.Mvc;
using CarbonFitness.Model;
using CarbonFitness.Repository;
using CarbonFitnessWeb.Controllers;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace CarbonFitnessTest.Test.UserCotroller {
    [TestFixture]
    public class CreateTest {

        [Test]
        public void shouldRedirectToView() {
            var userRepositoryMock = new Mock<IUserRepository>();
            var theUsersName = "kalle";
            userRepositoryMock.Setup(x => x.SaveOrUpdate(It.IsAny<User>())).Returns(new User { Username = theUsersName });

            var controller = new UserController(userRepositoryMock.Object);
            
            var viewResult = (RedirectToRouteResult)controller.Create(theUsersName);

            var actionRouteValue = viewResult.RouteValues["action"];
            Assert.That(actionRouteValue.ToString() == "Details", "Expected view, but was:" + actionRouteValue);
        }

        [Test]
        public  void shouldCallSaveOnRepository() {
            const string userName = "test";
            var factory = new MockFactory(MockBehavior.Strict);
            var mock = factory.Create<IUserRepository>();
            
            mock.Setup(x => x.SaveOrUpdate(It.Is<User>(y => y.Username == userName))).Returns(new User());

            var controller = new UserController(mock.Object);
            controller.Create(userName);
            factory.VerifyAll();
        }

        [Test]
        public void shouldPassCreatedUserIdToDetails() {
            var userName = "userName";

            var factory = new MockFactory(MockBehavior.Strict);
            var mock = factory.Create<IUserRepository>();
            var createdUser = new User { Username = userName };
            mock.Setup(x => x.SaveOrUpdate(It.Is<User>(y => y.Username == userName))).Returns(createdUser);

            var controller = new UserController(mock.Object);
            var viewResult = (RedirectToRouteResult)controller.Create(userName);

            var idRouteValue = viewResult.RouteValues["id"];
            Assert.That(idRouteValue.ToString() == createdUser.Id.ToString(), "Expected id" + createdUser.Id + ", but was:" + idRouteValue);
        }
    }
}