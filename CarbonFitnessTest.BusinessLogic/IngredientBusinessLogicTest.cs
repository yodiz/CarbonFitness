using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarbonFitness.BusinessLogic;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;
using Moq;
using NUnit.Framework;

namespace CarbonFitnessTest.BusinessLogic
{
    [TestFixture]
    public class IngredientBusinessLogicTest
    {
        [Test]
        public void shouldSearchIngredients()
        {
            var IngredientsRepositoryMock = new Mock<IIngredientRepository>();
            const string searchQuery = "abb";
            IngredientsRepositoryMock.Setup(x => x.Search(searchQuery)).Returns(new[] { new Ingredient() { Name = searchQuery }, new Ingredient() { Name = "abbo" } });

            IIngredientBusinessLogic ingredientBusinessLogic = new IngredientBusinessLogic(IngredientsRepositoryMock.Object);
            var ingredients = ingredientBusinessLogic.Search(searchQuery);

            Assert.That(ingredients.ToList().TrueForAll(x => x.Name.StartsWith(searchQuery)));

            IngredientsRepositoryMock.Verify();
        }
    }
}
