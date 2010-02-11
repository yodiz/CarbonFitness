using System;
using System.Security.Principal;
using System.Threading;
using CarbonFitness.BusinessLogic;
using CarbonFitness.Data.Model;
using CarbonFitnessWeb;
using Moq;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace CarbonFitnessTest.Web
{
	[TestFixture]
	public class UserContextTest
	{
		[Test]
		public void shouldGetUserFromThreadIdentity() {
			var userBusinessLogicMock = new Mock<IUserBusinessLogic>();
			userBusinessLogicMock.Setup(x => x.Get(It.IsAny<string>())).Returns(new User { Username = "myUser" });
			var user = new User("myUser");
			Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity("myUser"), null);
			Assert.That(new UserContext(userBusinessLogicMock.Object).User.Username, Is.EqualTo(user.Username));

			userBusinessLogicMock.Setup(x => x.Get(It.IsAny<string>())).Returns(new User { Username = "arne" });

			var newUserName = "arne";
			Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(newUserName), null);
			Assert.That(new UserContext(userBusinessLogicMock.Object).User.Username, Is.EqualTo(newUserName));
			
		}

		[Test]
		public void shouldLookUpUserFromBackend() {
			var userBusinessLogicMock = new Mock<IUserBusinessLogic>();
			userBusinessLogicMock.Setup(x => x.Get(It.IsAny<string>())).Returns(new User { Username = "myUser" });

			var userName = "myUser";
			Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(userName), null);
			Assert.That(new UserContext(userBusinessLogicMock.Object).User.Username, Is.EqualTo(userName));

			userBusinessLogicMock.VerifyAll();
		}
	}
}