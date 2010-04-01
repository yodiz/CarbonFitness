using CarbonFitness.App.Web.Models;
using CarbonFitness.App.Web.ViewConstants;
using NUnit.Framework;
using WatiN.Core;

namespace CarbonFitnessTest.Integration {
	[TestFixture]
	public class AccountLogOnTest : IntegrationBaseTest {
		public AccountLogOnTest() {}

		public AccountLogOnTest(Browser browser) : base(browser) {}

		public override string Url {
			get { return getUrl("Account", "LogOn") ; }
		}

		public void LogOn(string userName, string password) {
			Browser.GoTo(Url);
			Browser.TextField(Find.ByName(AccountConstant.UsernameElement)).TypeText(userName);
			Browser.TextField(Find.ByName(AccountConstant.PasswordElement)).TypeText(password);
            Browser.CheckBox(Find.ByName(RememberMeFieldName)).Click();
			Browser.Button(Find.ByValue(AccountConstant.SubmitElement)).Click();
		}

        private string RememberMeFieldName { get { return GetFieldNameOnModel<LogOnModel, bool>(m => m.RememberMe); } }

		[Test]
		public void shouldGotoUserCreateWhenClickingRegister() {
			var userNameConstraint = Find.ByText("Register");
			Browser.Link(userNameConstraint).Click();

			Assert.IsTrue(Browser.Url.Contains(getUrl("User", "Create")));
		}

		[Test]
		public void shouldShowLoggedOnUserAfterLogon() {
			var createUserTest = new CreateUserTest(Browser);
			createUserTest.getUniqueUserId();

			LogOn(CreateUserTest.UserName, CreateUserTest.Password);

			var usernameExsistsOnPage = Browser.Text.Contains(CreateUserTest.UserName);

			Assert.That(usernameExsistsOnPage);
		}
	}
}