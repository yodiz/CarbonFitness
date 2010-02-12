using System.Security.Principal;
using System.Threading;
using System.Web.Mvc;
using CarbonFitness.Data.Model;
using CarbonFitnessWeb;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using CarbonFitness.BusinessLogic;
using NUnit.Framework.SyntaxHelpers;

namespace CarbonFitnessTest.Web.Controller.UserController
{
	[TestFixture]
	public class CreateTest {

		[Test]
		public void shouldBeLoggedInAfterUserCreation() {
			var userBusinessLogicMock = new Mock<IUserBusinessLogic>();
			var theUsersName = "kalle";
			var thePassword = "myPass";
			userBusinessLogicMock.Setup(x => x.SaveOrUpdate(It.IsAny<User>())).Returns(new User { Username = theUsersName });

			var userContextMock = new Mock<IUserContext>();
			userContextMock.Setup(x => x.LogIn(It.IsAny<User>(), It.IsAny<bool>()));

			var controller = new CarbonFitnessWeb.Controllers.UserController(userBusinessLogicMock.Object, userContextMock.Object);

			var viewResult = (RedirectToRouteResult)controller.Create(theUsersName, thePassword);
			userContextMock.VerifyAll();
		}

		[Test]
		public void shouldRedirectToView() {
			var userBusinessLogicMock = new Mock<IUserBusinessLogic>();
			var userContextMock = new Mock<IUserContext>();

			var theUsersName = "kalle";
			var thePassword = "myPass";
			userBusinessLogicMock.Setup(x => x.SaveOrUpdate(It.IsAny<User>())).Returns(new User { Username = theUsersName });

			var controller = new CarbonFitnessWeb.Controllers.UserController(userBusinessLogicMock.Object, userContextMock.Object);
            
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
			var userContextMock = new Mock<IUserContext>();
            
			mock.Setup(x => x.SaveOrUpdate(It.Is<User>(y => y.Username == userName))).Returns(new User());

			var controller = new CarbonFitnessWeb.Controllers.UserController(mock.Object, userContextMock.Object);
			controller.Create(userName, password);
			factory.VerifyAll();
		}

		[Test]
		public void shouldPassCreatedUserIdToDetails() {
			var userName = "UserName";
			var password = "password";

			var factory = new MockFactory(MockBehavior.Strict);
			var userContextMock = new Mock<IUserContext>();
			var mock = factory.Create<IUserBusinessLogic>();
			var createdUser = new User { Username = userName };
			mock.Setup(x => x.SaveOrUpdate(It.Is<User>(y => y.Username == userName))).Returns(createdUser);

			var controller = new CarbonFitnessWeb.Controllers.UserController(mock.Object, userContextMock.Object);
			var viewResult = (RedirectToRouteResult)controller.Create(userName, password);

			var idRouteValue = viewResult.RouteValues["id"];
			Assert.That(idRouteValue.ToString() == createdUser.Id.ToString(), "Expected id" + createdUser.Id + ", but was:" + idRouteValue);
		}
	}
}