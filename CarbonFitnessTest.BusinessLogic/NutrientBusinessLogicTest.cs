using System.Collections.Generic;
using CarbonFitness.BusinessLogic.Implementation;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;
using Moq;
using NUnit.Framework;

namespace CarbonFitnessTest.BusinessLogic {
    [TestFixture]
    public class NutrientBusinessLogicTest {
        [Test]
        public void shouldGetNutrients() {
            var expectedNutrients = new[] {new Nutrient(), new Nutrient()};
            var nutrientRepositoryMock = new Mock<INutrientRepository>();
            nutrientRepositoryMock.Setup(x => x.GetAll()).Returns(expectedNutrients);
            IEnumerable<Nutrient> returnedNutrients = new NutrientBusinessLogic(nutrientRepositoryMock.Object).GetNutrients();

            Assert.That(returnedNutrients,Is.SameAs(expectedNutrients));
        }

        [Test]
        public void shouldNotExportInitialDataValuesIfAlreadyExported() {
            var nutrientRepositoryMock = new Mock<INutrientRepository>(MockBehavior.Strict);
            nutrientRepositoryMock.Setup(x => x.GetAll()).Returns(new[] { new Nutrient(), new Nutrient(), });
            new NutrientBusinessLogic(nutrientRepositoryMock.Object).Export();
            nutrientRepositoryMock.VerifyAll();
        }

        [Test]
        public void shouldExportInitialDataValues() {
            var nutrientRepositoryMock = new Mock<INutrientRepository>();
            nutrientRepositoryMock.Setup(x => x.Save(It.Is<Nutrient>(y => y.Name == "EnergyInKcal")));
            nutrientRepositoryMock.Setup(x => x.Save(It.Is<Nutrient>(y => y.Name == "FibresInG")));
            nutrientRepositoryMock.Setup(x => x.Save(It.Is<Nutrient>(y => y.Name == "CarbonHydrateInG")));

            new NutrientBusinessLogic(nutrientRepositoryMock.Object).Export();
            nutrientRepositoryMock.VerifyAll();
        }
        
    }
}