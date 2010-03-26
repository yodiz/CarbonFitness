using System.Linq;
using CarbonFitness.BusinessLogic;
using CarbonFitness.BusinessLogic.Implementation;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;
using Moq;
using NUnit.Framework;

namespace CarbonFitnessTest.BusinessLogic {
	[TestFixture]
	public class IngredientBusinessLogicTest {
		[Test]
		public void shouldSearchIngredients() {
			var ingredientsRepositoryMock = new Mock<IIngredientRepository>();
			const string searchQuery = "abb";
			ingredientsRepositoryMock.Setup(x => x.Search(searchQuery)).Returns(new[] {new Ingredient {Name = searchQuery}, new Ingredient {Name = "abbo"}});

			IIngredientBusinessLogic ingredientBusinessLogic = new IngredientBusinessLogic(ingredientsRepositoryMock.Object);
			var ingredients = ingredientBusinessLogic.Search(searchQuery);

			Assert.That(ingredients.ToList().TrueForAll(x => x.Name.StartsWith(searchQuery)));

			ingredientsRepositoryMock.Verify();
		}

	}
}