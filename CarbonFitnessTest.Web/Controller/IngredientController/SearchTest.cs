using System;
using System.Linq;
using CarbonFitness.BusinessLogic;
using CarbonFitness.Data.Model;
using Moq;
using NUnit.Framework;

namespace CarbonFitnessTest.Web.Controller.IngredientController {
	[TestFixture]
	public class SearchTest {
		[Test]
		public void shouldSearchIngredients() {
			Ingredient[] ingredients;
			var ingredientBusinessLogicMock = new Mock<IIngredientBusinessLogic>(MockBehavior.Strict);
			ingredientBusinessLogicMock.Setup(x => x.Search(It.IsAny<String>())).Returns(new[] {
				new Ingredient(),
				new Ingredient(),
				new Ingredient()
			});

			var ingredientController = new CarbonFitness.App.Web.Controllers.IngredientController(ingredientBusinessLogicMock.Object);

			var actionResult = ingredientController.Search("abb");
			ingredients = (Ingredient[]) actionResult.ViewData.Model;

			Assert.That(ingredients.Count(), Is.GreaterThan(2));
		}
	}
}