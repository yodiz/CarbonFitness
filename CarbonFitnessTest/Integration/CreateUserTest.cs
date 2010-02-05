using NUnit.Framework;
using WatiN.Core;
using WatiN.Core.Constraints;

namespace CarbonFitnessTest.Integration {
    [TestFixture]
    public class CreateUserTest {
        private const string url = "http://localhost:49639/User/Create";
        private const string userName = "myUser";

        [Test]
        public void shouldGotoDetailsWhenSavingUser() {
            using (var browser = new IE(url)) {
                AttributeConstraint a = Find.ByName("username");
                browser.TextField(a).TypeText(userName);

                browser.Button(Find.ByValue("Save")).Click();

                Assert.IsTrue(browser.Url.Contains("User/Details"));
            }
        }

        [Test]
        public void shouldHaveUserNameOnDetailsPageWhenSavingUser() {
            using (var browser = new IE(url)) {
                var userNameConstraint = Find.ByName("username");
                browser.TextField(userNameConstraint).TypeText(userName);

                browser.Button(Find.ByValue("Save")).Click();

                var userNameElement = browser.Element(Find.ByLabelText(userName));
                Assert.That(userNameElement.Exists);
            }
        }

        //[Test]
        //public void testCreateUser()
        //{
        //    var userName = "myUser";
        //    var repository = new UserRepository();
        //    int userId = repository.Create(new User(userName));

        //    var user = repository.Get(userId);
        //    Assert.AreEqual(user.Username, userName);
        //}

        //[TestMethod]
        //public void test
    }
}