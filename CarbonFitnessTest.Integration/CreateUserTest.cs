using System;
using System.Text;
using CarbonFitness.App.Web.ViewConstants;
using CarbonFitness.DataLayer.Repository;
using NUnit.Framework;
using WatiN.Core;

namespace CarbonFitnessTest.Integration {
	[TestFixture]
	public class CreateUserTest : IntegrationBaseTest {
		public const string UserName = "myUser";
		public const string Password = "mittlösenord";

		public override string Url {
			get { return getUrl("User", "Create"); }
		}

		public CreateUserTest() {}

		public CreateUserTest(Browser browser) : base(browser) {}

		public int getUniqueUserId() {
			var u = new UserRepository().Get(UserName);
			return u == null ? createUser(UserName) : u.Id ;
		}

		private int createUser(string userName) {
			Browser.GoTo(Url);
			Browser.TextField(Find.ByName(UserConstant.UsernameElement)).TypeText(userName);
			Browser.TextField(Find.ByName(UserConstant.PasswordElement)).TypeText(Password);
			Browser.Button(Find.ByValue(UserConstant.SaveElement)).Click();

			var id = Int32.Parse(Browser.Url.Substring(Browser.Url.LastIndexOf("/") + 1));
			return id;
		}

		private string randomString(int size, bool lowerCase) {
			var builder = new StringBuilder();
			var random = new Random();
			for (var i = 0; i < size; i++) {
				var ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26*random.NextDouble() + 65)));
				builder.Append(ch);
			}
			return lowerCase ? builder.ToString().ToLower() : builder.ToString();
		}

		[Test]
		public void shouldCreateUser() {
			var randomUsername = randomString(10, false);
			var id = createUser(randomUsername);
			var user = new UserRepository().Get(id);
			Assert.That(user, Is.Not.Null);
			Assert.That(user.Username, Is.EqualTo(randomUsername));
			Assert.That(user.Password, Is.Not.Empty);
		}

		[Test]
		public void shouldGotoDetailsWhenSavingUserAndHaveUserNameOnPage() {
			var userName = randomString(10, false);
			Browser.TextField(Find.ByName(UserConstant.UsernameElement)).TypeText(userName);

			Browser.Button(Find.ByValue(UserConstant.SaveElement)).Click();

			Assert.IsTrue(Browser.Url.Contains(getUrl("User", "Details")));
			Assert.That(Browser.ContainsText(userName));
		}
	}
}