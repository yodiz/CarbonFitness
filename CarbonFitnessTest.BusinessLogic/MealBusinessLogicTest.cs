using CarbonFitness.BusinessLogic;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;
using Moq;
using NUnit.Framework;

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
			
			mealBusinessLogic.GetMealIngredients(1);

         mealIngredientRepository.Verify();
		}
	}
}
