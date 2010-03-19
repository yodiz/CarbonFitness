using CarbonFitness.Data.Model;
using NUnit.Framework;

namespace CarbonFitnessTest.BusinessLogic {
	[TestFixture]
	public class UserIngredientExtensionsTest {
		[Test]
		public void shouldGetCaloriesForActualMeasure() {
			var expectedKcal = 99;

			var ingredient = new Ingredient {WeightInG = 100, EnergyInKcal = 33};
			var userIngredient = new UserIngredient {Measure = 300, Ingredient = ingredient};

			var kcal = userIngredient.GetActualCalorieCount(x => x.EnergyInKcal);

			Assert.That(kcal, Is.EqualTo(expectedKcal));
		}
	}
}