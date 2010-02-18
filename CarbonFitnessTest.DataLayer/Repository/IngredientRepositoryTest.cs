using System.Linq;
using System.Threading;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;
using NUnit.Framework;
using SharpArch.Testing.NUnit.NHibernate;

namespace CarbonFitnessTest.DataLayer.Repository
{
	[TestFixture]
	public class IngredientRepositoryTest : RepositoryTestsBase
	{
		protected override void LoadTestData()
		{
			var repository = new IngredientRepository();

			repository.SaveOrUpdate(new Ingredient {Name = "Pannbiff"});
            repository.SaveOrUpdate(new Ingredient { Name = "Abborre" });
            repository.SaveOrUpdate(new Ingredient { Name = "abborgine" });
		}

		[Test]
		public void shouldGetIngredientByName()
		{
			var repository = new IngredientRepository() as IIngredientRepository;

			var ingredient = repository.Get("Pannbiff");

			Assert.That(ingredient.Name, Is.EqualTo("Pannbiff"));
		}

        [Test]
        public void shouldMatchBothUpperAndLowerCaseOnName()
        {
            var repository = new IngredientRepository() as IIngredientRepository;

            var ingredient = repository.Get("pannbiff");

            Assert.That(ingredient.Name, Is.EqualTo("Pannbiff"));
        }

        [Test]
        public void shouldReturnmatchingItemsForSearchParamter() {
            var repository = new IngredientRepository();
            var ingredients = repository.Search("abb");

            Assert.That(ingredients.Count(), Is.GreaterThan(0));
            Assert.That(ingredients.ToList().TrueForAll(x => x.Name.StartsWith("Abb",true, Thread.CurrentThread.CurrentCulture)));
        }
	}
}