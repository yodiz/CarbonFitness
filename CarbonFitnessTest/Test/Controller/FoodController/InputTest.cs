using System;
using System.Web.Mvc;
using CarbonFitness.Model;
using Moq;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using MvcContrib.TestHelper;

namespace CarbonFitnessTest.Test.Controller.FoodController {
	[TestFixture]
	public class InputTest {
		[Test]
		public void shouldReturnView() {
			var foodController = new CarbonFitnessWeb.Controllers.FoodController();

			var viewResult = (ViewResult) foodController.Input();
			Assert.That(viewResult, Is.Not.Null);
		}
		
		
		[Test]
		public void shouldAddIngredientToMealForCurrentUser() {
		  MockFactory mockFactory = new MockFactory(MockBehavior.Strict);
		  var mealIngredientRepositoryMock = mockFactory.Create<IMealIngredientRepository>();

		  mealIngredientRepositoryMock.Setup(x => x.SaveOrUpdate(mealIngredient)).Returns(new MealIngredient() { });

			Ingredient ingredient = new Ingredient() { Id=1, Name="MyIngredietn" };
			User currentUser = new User() {Id = 1, Password="", Username="user1"};
			Meal meal = new Meal() { Id=1, User=currentUser, CreatedDate=new DateTime(1980, 04, 07) };

			meal.AddIngredient(currentUser, ingredient);
		}
	}
}