using CarbonFitnessWeb.ViewConstants;
using NUnit.Framework;
using WatiN.Core;

namespace CarbonFitnessTest.Integration
{
    [TestFixture]
    public class AccountLogOnTest
    {
        private const string url = "http://localhost:49639/Account/LogOn";
        [Test]
        public void shouldGotoUserCreateWhenClickingRegister()
        {
            using (var browser = new IE(url))
            {
                var userNameConstraint = Find.ByText("Register");
                browser.Link(userNameConstraint).Click();
                
                Assert.IsTrue(browser.Url.Contains("User/Create"));
            }
        }
        
        [Test]
        public void shouldShowLoggedOnUserAfterLogon() {
            const string userName = CreateUserTest.UserName;
            const string password = CreateUserTest.Password;

            var createUserTest = new CreateUserTest();
            createUserTest.createUser();

            using (var browser = new IE(url)) {
                browser.TextField(Find.ByName(AccountConstant.UsernameElement)).TypeText(userName);
                browser.TextField(Find.ByName(AccountConstant.PasswordElement)).TypeText(password);
                browser.Button(Find.ByValue(AccountConstant.SubmitElement)).Click();

                var usernameExsistsOnPage = browser.Text.Contains(userName);

                Assert.That(usernameExsistsOnPage);
            }   
        }
    }
}
