using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
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
		}

		[Test]
		public void shouldGetIngredientByName()
		{
			var repository = new IngredientRepository() as IIngredientRepository;

			var ingredient = repository.Get("Pannbiff");

			Assert.That(ingredient.Name, Is.EqualTo("Pannbiff"));
		}
	}
}