using System.Collections.Generic;
using System.Linq;
using CarbonFitness.App.Importer;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;
using NUnit.Framework;

namespace CarbonFitnessTest.Integration {
	[TestFixture]
	public class ImportIngredientsTest {
		[Test]
		public void shouldImportIngredients() {
			new IngredientImporterEngine("Hibernate.cfg.xml", true).Import(@"TestData\Ingredients.csv");
			IList<Ingredient> ingredients = new IngredientRepository().GetAll().OrderBy(x => x.Name).ToList();
			Assert.That(ingredients.First().Name, Is.EqualTo("Abborre"));
			Assert.That(ingredients.Last().Name, Is.EqualTo("Örtte drickf"));
		}
	}
}