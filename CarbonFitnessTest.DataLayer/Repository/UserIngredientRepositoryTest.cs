using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using SharpArch.Testing.NUnit.NHibernate;

namespace CarbonFitnessTest.DataLayer.Repository
{
	[TestFixture]
	public class UserIngredientRepositoryTest : RepositoryTestsBase
	{
		protected override void LoadTestData()
		{
		
		}

		[Test]
		public void shouldGetIngredientByName()
		{
			var repository = new UserIngredientRepository();

			UserIngredient userIngredient = repository.SaveOrUpdate(new UserIngredient { Measure = 10 });

			Assert.That(userIngredient.Id, Is.GreaterThan(0));
		}
	}
}