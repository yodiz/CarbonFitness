using CarbonFitness.App.Web.ViewConstants;
using NUnit.Framework;
using WatiN.Core;

namespace CarbonFitnessTest.Integration {
	[TestFixture]
	public class SiteMasterTest : IntegrationBaseTest {
		public override string Url {
			get { return BaseUrl; }
		}

		private void createUserAndLogOn() {
			var createUserTest = new CreateUserTest(Browser);
			createUserTest.getUniqueUserId();
			var loggonTest = new AccountLogOnTest(Browser);
			loggonTest.LogOn(CreateUserTest.UserName, CreateUserTest.Password);
		}

		[Test]
		public void shouldGoToAddFoodPageAfterClickingAddFoodLinkAfterLoggedOn() {
			createUserAndLogOn();
			Browser.Link(Find.ByText(SiteMasterConstant.FoodInputLinkText)).Click();

			Assert.That(Browser.ContainsText(FoodConstant.FoodInputTitle));
		}

		[Test]
		public void shouldGoToLogOnPageAfterClickingAddFoodLinkIfNotLoggedOn() {
			Browser.GoTo(BaseUrl);

			var logOffButton = Browser.Link(Find.ByText("Log Off"));
			if (logOffButton.Exists) {
				logOffButton.Click();
			}

			var link = Browser.Link(Find.ByText(SiteMasterConstant.FoodInputLinkText));
			link.Click();

			Assert.That(Browser.ContainsText(AccountConstant.LoginTitle));
		}

		[Test]
		public void shouldGoToResultsAfterClickingResultsAfterLoggedOn() {
			createUserAndLogOn();

			Browser.Link(Find.ByText(SiteMasterConstant.ResultLinkText)).Click();

			Assert.That(Browser.ContainsText(ResultConstant.ResultTitle));
		}
	}
}