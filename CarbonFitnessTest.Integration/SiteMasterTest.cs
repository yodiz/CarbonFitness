using CarbonFitnessWeb.ViewConstants;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using WatiN.Core;

namespace CarbonFitnessTest.Integration {
	[TestFixture]
	public class SiteMasterTest : IntegrationBaseTest {
		public override string Url { get { return baseUrl; } }

		[Test]
		public void shouldNotShowAddFoodLinkIfNotLoggedIn() {
			browser.GoTo(baseUrl);
			
			Link logOffButton = browser.Link(Find.ByText("Log Off"));
			if (logOffButton.Exists) {
				 logOffButton.Click();
			}
			
			Link link = browser.Link(Find.ByText(SiteMasterConstant.AddFoodLinkId));

			Assert.That(link.Exists, Is.False);
		}

		[Test]
		public void shouldShowAddFoodLinkAfterLoggedOn() {
			var createUserTest = new CreateUserTest(browser);
			createUserTest.createUser();
			var loggonTest = new AccountLogOnTest(browser);
			loggonTest.LogOn(CreateUserTest.UserName, CreateUserTest.Password);
			Link link = browser.Link(Find.ByText(SiteMasterConstant.AddFoodLinkId));

			Assert.That(link.Exists);
		}
	}
}