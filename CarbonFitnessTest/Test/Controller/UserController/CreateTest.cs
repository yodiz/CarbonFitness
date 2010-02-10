using System.Web.Mvc;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;
using CarbonFitnessWeb.Controllers;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using CarbonFitness.BusinessLogic;

namespace CarbonFitnessTest.Test.UserCotroller {
    [TestFixture]
    public class CreateTest {

        [Test]
        public void shouldRedirectToView() {
            var userBusinessLogicMock = new Mock<IUserBusinessLogic>();
            var theUsersName = "kalle";
            var thePassword = "myPass";
            userBusinessLogicMock.Setup(x => x.SaveOrUpdate(It.IsAny<User>())).Returns(new User { Username = theUsersName });

            var controller = new UserController(userBusinessLogicMock.Object);
            
            var viewResult = (RedirectToRouteResult)controller.Create(theUsersName, thePassword);

            var actionRouteValue = viewResult.RouteValues["action"];
            Assert.That(actionRouteValue.ToString() == "Details", "Expected view, but was:" + actionRouteValue);
        }

        [Test]
        public  void shouldCallSaveOnRepository() {
            const string userName = "test";
            var password = "password";
            var factory = new MockFactory(MockBehavior.Strict);
            var mock = factory.Create<IUserBusinessLogic>();
            
            mock.Setup(x => x.SaveOrUpdate(It.Is<User>(y => y.Username == userName))).Returns(new User());

            var controller = new UserController(mock.Object);
            controller.Create(userName, password);
            factory.VerifyAll();
        }

        [Test]
        public void shouldPassCreatedUserIdToDetails() {
            var userName = "UserName";
            var password = "password";

            var factory = new MockFactory(MockBehavior.Strict);
            var mock = factory.Create<IUserBusinessLogic>();
            var createdUser = new User { Username = userName };
            mock.Setup(x => x.SaveOrUpdate(It.Is<User>(y => y.Username == userName))).Returns(createdUser);

            var controller = new UserController(mock.Object);
            var viewResult = (RedirectToRouteResult)controller.Create(userName, password);

            var idRouteValue = viewResult.RouteValues["id"];
            Assert.That(idRouteValue.ToString() == createdUser.Id.ToString(), "Expected id" + createdUser.Id + ", but was:" + idRouteValue);
        }
    }
}