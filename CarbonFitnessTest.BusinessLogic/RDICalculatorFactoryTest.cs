using CarbonFitness.BusinessLogic;
using CarbonFitness.BusinessLogic.RDI.Calculators;
using CarbonFitness.Data.Model;
using Moq;
using NUnit.Framework;

namespace CarbonFitnessTest.BusinessLogic
{
    [TestFixture]
    public class RDICalculatorFactoryTest {
        [Test]
        public void shouldGetRDICalculator() {
            var mineralRDICalculatorMock = new Mock<IRDICalculator>();
            var carbonHydrateRDICalculatorMock = new Mock<IRDICalculator>();
            carbonHydrateRDICalculatorMock.Setup(x => x.DoesSupportNutrient(NutrientEntity.CarbonHydrateInG)).Returns(true);
            mineralRDICalculatorMock.Setup(x => x.DoesSupportNutrient(NutrientEntity.CarbonHydrateInG)).Returns(false);

            RDICalculatorFactory factory = new RDICalculatorFactory();
            factory.AddRDICalculator(mineralRDICalculatorMock.Object);
            factory.AddRDICalculator(carbonHydrateRDICalculatorMock.Object);

            IRDICalculator result = factory.GetRDICalculator(NutrientEntity.CarbonHydrateInG);
            Assert.That(result.DoesSupportNutrient(NutrientEntity.CarbonHydrateInG)); 
        }
    }
}
