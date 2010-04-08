using System;
using System.Collections.Generic;
using CarbonFitness.Data.Model;
using NUnit.Framework;


namespace CarbonFitnessTest.BusinessLogic {
	[TestFixture]
	public class UserIngredientExtensionsTest  {


		[Test]
		public void shouldGetCaloriesForActualMeasure() {
			var expectedKcal = 99;

		    var ingredientSetup = new IngredientSetup();
		    var ingredient = ingredientSetup.GetNewIngredient("Ärtor", NutrientEntity.EnergyInKcal, 33, 100);
			var userIngredient = new UserIngredient {Measure = 300, Ingredient = ingredient};

			var kcal = userIngredient.GetActualCalorieCount(x => x.GetNutrient(NutrientEntity.EnergyInKcal).Value);

			Assert.That(kcal, Is.EqualTo(expectedKcal));
		}
	}
}