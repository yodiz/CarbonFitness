using System;
using CarbonFitness.Maps;
using CarbonFitness.Repository;
using CarbonFitnessWeb.ViewConstants;
using Moq;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using SharpArch.Data.NHibernate;
using SharpArch.Testing.NUnit.NHibernate;
using WatiN.Core;
using WatiN.Core.Constraints;

namespace CarbonFitnessTest.Integration {
    [TestFixture]
    public class CreateUserTest
    {
        public const string UserName = "myUser";
        public const string Password = "mittlösenord";
        private const string url = "http://localhost:49639/User/Create";
        

        [SetUp]
        public virtual void SetUp()
        {
            string[] mappingAssemblies = RepositoryTestsHelper.GetMappingAssemblies();
            NHibernateSession.Init(new SimpleSessionStorage(), mappingAssemblies,
                new AutoPersistenceModelGenerator().Generate(),
                "../../../CarbonFitnessWeb/NHibernate.config");
        }

        [Test]
        public void shouldCreateUser() {
            var id = createUser();
            var user = new UserRepository().Get(id);
            Assert.That(user, Is.Not.Null);
            Assert.That(user.Username, Is.EqualTo(UserName));
            Assert.That(user.Password, Is.Not.Empty);
        }

        public int createUser() {
            using (var browser = new IE(url)) {
                browser.TextField(Find.ByName(UserConstant.UsernameElement)).TypeText(UserName);
                browser.TextField(Find.ByName(UserConstant.PasswordElement)).TypeText(Password);
                browser.Button(Find.ByValue(UserConstant.SaveElement)).Click();

                var id = Int32.Parse(browser.Url.Substring(browser.Url.LastIndexOf("/")+1));
                return id;
            }
        }

        [Test]
        public void shouldGotoDetailsWhenSavingUser() {
            using (var browser = new IE(url)) {
                AttributeConstraint a = Find.ByName(UserConstant.UsernameElement);
                browser.TextField(a).TypeText(UserName);

                browser.Button(Find.ByValue(UserConstant.SaveElement)).Click();

                Assert.IsTrue(browser.Url.Contains("User/Details"));
            }
        }

        [Test]
        public void shouldHaveUserNameOnDetailsPageWhenSavingUser() {
            using (var browser = new IE(url)) {
                var userNameConstraint = Find.ByName(UserConstant.UsernameElement);
                browser.TextField(userNameConstraint).TypeText(UserName);

                browser.Button(Find.ByValue(UserConstant.SaveElement)).Click();

                Assert.That(browser.ContainsText(UserName));
            }
        }



        //[Test]
        //public void testCreateUser()
        //{
        //    var UserName = "myUser";
        //    var repository = new UserRepository();
        //    int userId = repository.Create(new User(UserName));

        //    var user = repository.Get(userId);
        //    Assert.AreEqual(user.Username, UserName);
        //}

        //[TestMethod]
        //public void test

    }
}