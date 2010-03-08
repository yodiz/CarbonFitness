using System.Web.Mvc;
using CarbonFitness.App.Web.Models;
using CarbonFitness.BusinessLogic;
using Moq;
using NUnit.Framework;

namespace CarbonFitnessTest.Web.Controller.AccountController {
	[TestFixture]
	public class LogOnTest {
		[Test]
		public void shouldRedirectToReturnUrlWhenLoginIsSuccessfull() {
			var returnUrl = "~/Home/Index";

			var mockFactory = new MockFactory(MockBehavior.Strict);
			var membershipService = mockFactory.Create<IMembershipBusinessLogic>();
			var formsAuthenticationService = mockFactory.Create<IFormsAuthenticationService>();

			membershipService.Setup(x => x.ValidateUser("username", "password")).Returns(true);
			formsAuthenticationService.Setup(x => x.SignIn("username", false));

			var accountController = new CarbonFitness.App.Web.Controllers.AccountController(formsAuthenticationService.Object, membershipService.Object);
			var actionResult = accountController.LogOn(new LogOnModel {UserName = "username", Password = "password"}, returnUrl);

			Assert.That(((RedirectResult) actionResult).Url.Contains(returnUrl));
		}
	}
}