using System;
using CarbonFitness.Model;
using CarbonFitness.Repository;
using CarbonFitnessWeb.ViewConstants;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
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

		public int createUser() {
			browser.GoTo(Url);
			browser.TextField(Find.ByName(UserConstant.UsernameElement)).TypeText(UserName);
			browser.TextField(Find.ByName(UserConstant.PasswordElement)).TypeText(Password);
			browser.Button(Find.ByValue(UserConstant.SaveElement)).Click();

			int id = Int32.Parse(browser.Url.Substring(browser.Url.LastIndexOf("/") + 1));
			return id;
		}

		[Test]
		public void shouldCreateUser() {
			int id = createUser();
			User user = new UserRepository().Get(id);
			Assert.That(user, Is.Not.Null);
			Assert.That(user.Username, Is.EqualTo(UserName));
			Assert.That(user.Password, Is.Not.Empty);
		}

		[Test]
		public void shouldGotoDetailsWhenSavingUser() {
			AttributeConstraint a = Find.ByName(UserConstant.UsernameElement);
			browser.TextField(a).TypeText(UserName);

			browser.Button(Find.ByValue(UserConstant.SaveElement)).Click();

			Assert.IsTrue(browser.Url.Contains("User/Details"));
		}

		[Test]
		public void shouldHaveUserNameOnDetailsPageWhenSavingUser() {
			AttributeConstraint userNameConstraint = Find.ByName(UserConstant.UsernameElement);
			browser.TextField(userNameConstraint).TypeText(UserName);

			browser.Button(Find.ByValue(UserConstant.SaveElement)).Click();

			Assert.That(browser.ContainsText(UserName));
		}
	}
}