using CarbonFitnessWeb.ViewConstants;
using NUnit.Framework;
using WatiN.Core;

namespace CarbonFitnessTest.Integration {
    [TestFixture]
    public class SiteMasterTest : IntegrationBaseTest {
        public override string Url {
            get { return baseUrl; }
        }

        [Test]
        public void shouldGoToResultsAfterClickingResultsAfterLoggedOn() {
            createUserAndLogOn();

            browser.Link(Find.ByText(SiteMasterConstant.ResultLinkText)).Click();

            Assert.That(browser.ContainsText(ResultConstant.ResultTitle));
        }

        [Test]
        public void shouldGoToAddFoodPageAfterClickingAddFoodLinkAfterLoggedOn() {
            createUserAndLogOn();
            browser.Link(Find.ByText(SiteMasterConstant.FoodInputLinkText)).Click();

            Assert.That(browser.ContainsText(FoodConstant.FoodInputTitle));
        }

        private void createUserAndLogOn() {
            var createUserTest = new CreateUserTest(browser);
            createUserTest.createIfNoUserExist();
            var loggonTest = new AccountLogOnTest(browser);
            loggonTest.LogOn(CreateUserTest.UserName, CreateUserTest.Password);
        }

        [Test]
        public void shouldGoToLogOnPageAfterClickingAddFoodLinkIfNotLoggedOn() {
            browser.GoTo(baseUrl);

            var logOffButton = browser.Link(Find.ByText("Log Off"));
            if (logOffButton.Exists) {
                logOffButton.Click();
            }

            var link = browser.Link(Find.ByText(SiteMasterConstant.FoodInputLinkText));
            link.Click();

            Assert.That(browser.ContainsText(AccountConstant.LoginTitle));
        }
    }
}