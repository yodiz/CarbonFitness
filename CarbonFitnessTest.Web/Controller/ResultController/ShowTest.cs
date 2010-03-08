using System;
using System.Web.Mvc;
using CarbonFitness.BusinessLogic;
using CarbonFitness.Data.Model;
using CarbonFitnessWeb;
using CarbonFitnessWeb.Models;
using Moq;
using NUnit.Framework;

namespace CarbonFitnessTest.Web.Controller.ResultController {
    [TestFixture]
    public class ShowTest {
        [Test]
        public void shouldShowSumOfCaloriesForADate() {
            var userIngredientBusinessLogicMock = new Mock<IUserIngredientBusinessLogic>(MockBehavior.Strict);
            var userContextMock = new Mock<IUserContext>();
            var userIngredient = new UserIngredient {Ingredient = new Ingredient {Calories = 2}};
            var userIngredient2 = new UserIngredient {Ingredient = new Ingredient {Calories = 3}};
            var userIngredients = new[] {userIngredient, userIngredient2};
            userIngredientBusinessLogicMock.Setup(x => x.GetUserIngredients(It.IsAny<User>(), It.IsAny<DateTime>())).Returns(userIngredients);

            var actionResult = (ViewResult) new CarbonFitnessWeb.Controllers.ResultController(userIngredientBusinessLogicMock.Object, userContextMock.Object).Show(new ResultModel {Date = DateTime.Now});
            var result = (ResultModel) actionResult.ViewData.Model;
            
            userIngredientBusinessLogicMock.VerifyAll();

            Assert.That(result.SumOfCalories, Is.EqualTo("5"));
        }
    }
}