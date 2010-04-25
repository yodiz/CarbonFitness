using System;
using CarbonFitness.BusinessLogic;
using CarbonFitness.BusinessLogic.RDI.Calculators;
using CarbonFitness.Data.Model;
using Moq;
using NUnit.Framework;

namespace CarbonFitnessTest.BusinessLogic.RDICalculators {
    [TestFixture]
    public class CarbonHydrateRDICalculatorTest {
        [Test]
        public void shouldGetCarbonHydratesRDI() {
            int dailyCalorieNeed = 300;
            var userProfileBusinessLogicMock = new Mock<IUserProfileBusinessLogic>();
            userProfileBusinessLogicMock.Setup(x => x.GetDailyCalorieNeed(It.IsAny<User>())).Returns(dailyCalorieNeed);

            decimal result = new CarbonHydrateRDICalculator(userProfileBusinessLogicMock.Object).GetRDI(new User(), DateTime.Now, NutrientEntity.CarbonHydrateInG);
            Assert.That(result, Is.EqualTo((dailyCalorieNeed * 0.55M) / 4));
        }

        [Test]
        public void shouldSupportCarbonHydratesOnly() {
            Assert.That(new CarbonHydrateRDICalculator(null).DoesSupportNutrient(NutrientEntity.CarbonHydrateInG));
            Assert.That(new CarbonHydrateRDICalculator(null).DoesSupportNutrient(NutrientEntity.ZincInmG), Is.False);
        }
    }
}