﻿using System.Web.Mvc;
using CarbonFitness.BusinessLogic;
using CarbonFitness.Data.Model;
using Moq;
using NUnit.Framework;

namespace CarbonFitnessTest.Web.Controller.UserController {
	[TestFixture]
	public class DetailsTest {
		[Test]
		public void detailsShouldReturnUser() {
			var factory = new MockFactory(MockBehavior.Strict);
			var mock = factory.Create<IUserBusinessLogic>();
			mock.Setup(x => x.Get(1)).Returns(new User {Username = "kalle"});
			var userBusinessLogic = mock.Object;

			var controller = new CarbonFitness.App.Web.Controllers.UserController(userBusinessLogic, null);
			var viewResult = (ViewResult) controller.Details(1);

			var user = (User) viewResult.ViewData.Model;

			Assert.That(user, Is.TypeOf(typeof(User)));
			Assert.AreEqual("kalle", user.Username);
		}
	}
}