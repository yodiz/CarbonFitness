using System;
using CarbonFitness.BusinessLogic;
using CarbonFitness.Data.Model;
using NUnit.Framework;

namespace CarbonFitnessTest.BusinessLogic
{
    [TestFixture]
    public class CalorieCalculatorTest {

        [Test]
        public void shouldGetAgeFactor() {
            var ageFactor = new CalorieCalculator().GetAgeFactor(29);
            Assert.That(ageFactor, Is.EqualTo(142.68m));
        }

        [Test]
        public void shouldGetWeightFactor() {
            var weightFactor = new CalorieCalculator().GetWeightFactor(43);
            Assert.That(weightFactor, Is.EqualTo(429.57m));
        }

        [Test]
        public void shouldGetHeightFactor() {
            var height = new CalorieCalculator().GetHeightFactor(6.25M);
            Assert.That(height, Is.EqualTo(39.0625m));
        }

        [Test]
        public void shouldGetBMR() {
            const decimal weight = 59;
            const decimal height = 168;
            const int age = 55;
            var female = new GenderType {GenderBMRFactor = -161};
            var bmr = new CalorieCalculator().GetBMR(weight, height, age, female);
            Assert.That(bmr, Is.EqualTo(1207.81m));
        }

        [Test]
        public void shouldGetDailyCalorieNeed() {
            const decimal weight = 59;
            const decimal height = 168;
            const int age = 55;
            var female = new GenderType {GenderBMRFactor = -161};
            var activityLevel = new ActivityLevelType { EnergyFactor = 1.2M };
            var dailyCalorieNeed = new CalorieCalculator().GetDailyCalorieNeed(weight, height, age, female, activityLevel);
            Assert.That(dailyCalorieNeed, Is.EqualTo(1449.372m));
        }
    }
}
