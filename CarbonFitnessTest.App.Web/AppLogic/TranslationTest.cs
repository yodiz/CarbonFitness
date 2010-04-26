using CarbonFitness.Translation;
using NUnit.Framework;

namespace CarbonFitnessTest.Web.AppLogic
{
    [TestFixture]
    public class TranslationTest
    {
        [Test]
        public void shouldFetchTranlsationForNutrient() {
            var result = new NutrientTranslator().GetString("EnergyInKcal");
            Assert.That(result, Is.EqualTo("Energi i Kcal"));
        }
    }
}
