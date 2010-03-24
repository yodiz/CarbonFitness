using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WatiN.Core;
using CarbonFitness.Repository;
using CarbonFitness.Model;

namespace CarbonFitnessTest
{
    [TestClass]
    public class CreateUserTest
    {

        //[TestMethod]
        //public void testCreateUserPage()
        //{
        //    using (var browser = new IE("http://localhost:56193/User/Create.aspx"))
        //    {
        //        var a = Find.ByName("username");
        //        browser.TextField(a).TypeText("myUser");

        //        browser.Button(Find.ByValue("Save")).Click();
        //        //ie.Close();

        //        Assert.IsTrue(browser.Url.Contains("User/View.aspx"));
        //    }
        //}

        [TestMethod]
        public void testCreateUser() {
            var userName = "myUser";
            var repository = new UserRepository();
            int userId = repository.Create(new User(userName));

            var user = repository.Get(userId);
            Assert.AreEqual(user.Username,userName);
        }

        //[TestMethod]
        //public void test

    }
}
