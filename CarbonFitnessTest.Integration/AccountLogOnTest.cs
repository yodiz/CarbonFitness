using CarbonFitnessWeb.ViewConstants;
using NUnit.Framework;
using WatiN.Core;
using WatiN.Core.Constraints;

namespace CarbonFitnessTest.Integration {
	[TestFixture]
	public class AccountLogOnTest : IntegrationBaseTest {
		public AccountLogOnTest() {}

		public AccountLogOnTest(Browser browser) :base(browser){}

		public override string Url { get { return baseUrl + "/Account/LogOn"; } }

		[Test]
		public void shouldGotoUserCreateWhenClickingRegister() {
			AttributeConstraint userNameConstraint = Find.ByText("Register");
			browser.Link(userNameConstraint).Click();

			Assert.IsTrue(browser.Url.Contains("User/Create"));
		}

		[Test]
		public void shouldShowLoggedOnUserAfterLogon() {
			var createUserTest = new CreateUserTest(browser);
            createUserTest.createIfNoUserExist();

			LogOn(CreateUserTest.UserName, CreateUserTest.Password);

			bool usernameExsistsOnPage = browser.Text.Contains(CreateUserTest.UserName);

			Assert.That(usernameExsistsOnPage);
		}

		public void LogOn(string userName, string password) {
			 browser.GoTo(Url);
			 browser.TextField(Find.ByName(AccountConstant.UsernameElement)).TypeText(userName);
			 browser.TextField(Find.ByName(AccountConstant.PasswordElement)).TypeText(password);
			 browser.Button(Find.ByValue(AccountConstant.SubmitElement)).Click();
		}
	}
}