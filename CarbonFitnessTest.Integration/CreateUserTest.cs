using System;
using System.Text;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;
using CarbonFitnessWeb.ViewConstants;
using NUnit.Framework;
using WatiN.Core;
using WatiN.Core.Constraints;

namespace CarbonFitnessTest.Integration {
	[TestFixture]
	public class CreateUserTest : IntegrationBaseTest {
		public const string UserName = "myUser";
		public const string Password = "mittlösenord";

		public override string Url { get { return baseUrl + "/User/Create"; } }

		public CreateUserTest() {
		}

		public CreateUserTest(Browser browser) : base(browser) {
		}

        public int createIfNoUserExist() {
            var u = new UserRepository().Get(UserName);
            return u ==null ? createUser(UserName) : u.Id;
        }

	    private int createUser(string userName) {
			browser.GoTo(Url);
            browser.TextField(Find.ByName(UserConstant.UsernameElement)).TypeText(userName);
			browser.TextField(Find.ByName(UserConstant.PasswordElement)).TypeText(Password);
			browser.Button(Find.ByValue(UserConstant.SaveElement)).Click();

			int id = Int32.Parse(browser.Url.Substring(browser.Url.LastIndexOf("/") + 1));
			return id;
		}

		[Test]
		public void shouldCreateUser() {
		    string randomUsername = randomString(10, false);
            int id = createUser(randomUsername);
			User user = new UserRepository().Get(id);
			Assert.That(user, Is.Not.Null);
            Assert.That(user.Username, Is.EqualTo(randomUsername));
			Assert.That(user.Password, Is.Not.Empty);
		}

        private string randomString(int size, bool lowerCase) {
            var builder = new StringBuilder();
            var random = new Random();
            for (var i = 0; i < size; i++) {
                var ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }

		[Test]
		public void shouldGotoDetailsWhenSavingUserAndHaveUserNameOnPage() {
		    var userName = randomString(10, false);
		    browser.TextField(Find.ByName(UserConstant.UsernameElement)).TypeText(userName);

			browser.Button(Find.ByValue(UserConstant.SaveElement)).Click();

			Assert.IsTrue(browser.Url.Contains("User/Details"));
            Assert.That(browser.ContainsText(userName));
		}
	}
}