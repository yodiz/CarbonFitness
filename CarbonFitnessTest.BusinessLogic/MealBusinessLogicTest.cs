using System.Linq;
using CarbonFitness.BusinessLogic;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;
using Moq;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace CarbonFitnessTest.BusinessLogic
{
	[TestFixture]
	public class MealBusinessLogicTest
	{
		[Test]
		public void shouldGetMealIngredientsFromRepository() {
			var mealIngredientRepository = new Mock<IMealIngredientRepository>();
			mealIngredientRepository.Setup(x => x.GetByMealId(1)).Returns(new[] { new MealIngredient(), new MealIngredient() });
			var mealBusinessLogic = new MealBusinessLogic(mealIngredientRepository.Object);
			
			var mealIngredients = mealBusinessLogic.GetMealIngredients(1);

			Assert.That(mealIngredients, Is.Not.Null);
			Assert.That(mealIngredients.Count(), Is.EqualTo(2));
         mealIngredientRepository.VerifyAll();
		}
	}
}
