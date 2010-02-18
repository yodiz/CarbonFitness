using CarbonFitness;
using CarbonFitnessWeb.Models;
using Moq;
using NUnit.Framework;
using System.Web.Mvc;

namespace CarbonFitnessTest.Web.Controller.AccountController
{
	[TestFixture]
	public class LogOnTest
	{

		[Test]
		public void shouldRedirectToReturnUrlWhenLoginIsSuccessfull() {
			string returnUrl = "~/Home/Index";

			MockFactory mockFactory = new MockFactory(MockBehavior.Strict);
			var membershipService = mockFactory.Create<IMembershipBusinessLogic>();
			var formsAuthenticationService = mockFactory.Create<IFormsAuthenticationService>();

			membershipService.Setup(x => x.ValidateUser("username", "password")).Returns(true);
			formsAuthenticationService.Setup(x => x.SignIn("username", false));

			var accountController = new CarbonFitnessWeb.Controllers.AccountController(formsAuthenticationService.Object, membershipService.Object);
			var actionResult = accountController.LogOn(new LogOnModel() {UserName = "username", Password = "password"}, returnUrl);

			Assert.That(((RedirectResult) actionResult).Url.Contains(returnUrl));
		}

	}
}