using System;
using System.Web.Mvc;
using CarbonFitness;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;
using Moq;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using MvcContrib.TestHelper;

namespace CarbonFitnessTest.Test.Controller.FoodController {
    [TestFixture]
    public class InputTest {
        [Test]
        public void shouldAddIngredientToMealForCurrentUser() {
            var mockFactory = new MockFactory(MockBehavior.Strict);
            var mealIngredientRepositoryMock = mockFactory.Create<IMealIngredientRepository>();

            mealIngredientRepositoryMock.Setup(x => x.SaveOrUpdate(It.IsAny<MealIngredient>())).Returns(new MealIngredient());

            var ingredient = new Ingredient {Name = "MyIngredient"};
            var currentUser = new User {Password = "", Username = "user1"};
            var meal = new Meal {User = currentUser, CreatedDate = new DateTime(1980, 04, 07)};
            
            new MealBusinessLogic(mealIngredientRepositoryMock.Object, meal).AddIngredient(currentUser, ingredient);
            mockFactory.VerifyAll();
        }

        [Test]
        public void shouldReturnView() {
            var foodController = new CarbonFitnessWeb.Controllers.FoodController();

            var viewResult = (ViewResult) foodController.Input();
            Assert.That(viewResult, Is.Not.Null);
        }


        //[Test]
        //public void shouldContainIngredientInModelWhenAddingIngredientOnPage() {
        //    var foodController = new CarbonFitnessWeb.Controllers.FoodController();

        //    InputFoodModell inputFoodModell = new InputFoodModell() { Ingredient="Panbiff", Meassure=100 };

        //    var actionResult = (ViewResult)foodController.Input(inputFoodModell);
        //    var model = (InputFoodModell) actionResult.ViewData.Model;

        //    Assert.That(model.Ingredients.Find(x=> x.Name == "Pannbiff"));
        //}
    }
}