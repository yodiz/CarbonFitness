using CarbonFitness.App.Web.ViewConstants;
using NUnit.Framework;
using WatiN.Core;

namespace CarbonFitnessTest.Integration {
	[TestFixture]
	public class SiteMasterTest : IntegrationBaseTest {
		public override string Url { get { return BaseUrl; } }

		private void createUserAndLogOn() {
			var createUserTest = new CreateUserTest(Browser);
			createUserTest.getUniqueUserId();
			var loggonTest = new AccountLogOnTest(Browser);
			loggonTest.LogOn(CreateUserTest.UserName, CreateUserTest.Password);
		}

		[Test]
		public void shouldGoToAddFoodPageAfterClickingAddFoodLinkAfterLoggedOn() {
			createUserAndLogOn();

			ReloadPage(SiteMasterConstant.FoodInputLinkText);

			Assert.That(Browser.ContainsText(FoodConstant.FoodInputTitle));
		}

		[Test]
		public void shouldGoToEnergyPageAfterClickingAddProfileLinkAfterLoggedOn() {
			createUserAndLogOn();
			ReloadPage(SiteMasterConstant.ProfileInputLinkText);

			Assert.That(Browser.ContainsText(ProfileConstant.ProfileInputTitle));
		}

		[Test]
		public void shouldGoToLogOnPageAfterClickingAddFoodLinkIfNotLoggedOn() {
			Browser.GoTo(BaseUrl);

			var logOffButton = Browser.Link(Find.ByText("Log Off"));
			if (logOffButton.Exists) {
				logOffButton.Click();
			}

			ReloadPage(SiteMasterConstant.FoodInputLinkText);

			Assert.That(Browser.ContainsText(AccountConstant.LoginTitle));
		}

		[Test]
		public void shouldGoToResultsAfterClickingResultsAfterLoggedOn() {
			createUserAndLogOn();

			ReloadPage(SiteMasterConstant.ResultLinkText);

			Assert.That(Browser.ContainsText(ResultConstant.ResultTitle));
		}

		[Test]
		public void shouldGoToWeightPageAfterClickingWeightAfterLoggedOn() {
			createUserAndLogOn();

			ReloadPage(SiteMasterConstant.WeightLinkText);

			Assert.That(Browser.ContainsText(WeightConstant.WeightTitle));
		}
	}
}