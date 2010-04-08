using System;
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
            
            nutrientRepositoryMock.Setup(x => x.GetByName(NutrientEntity.ZincInmG.ToString())).Returns(null as Nutrient).Verifiable("Zink failed");
            nutrientRepositoryMock.Setup(x => x.Save(It.Is<Nutrient>(y => y.Name.Equals(NutrientEntity.ZincInmG.ToString())))).Returns(new Nutrient());

            foreach (var nutriant in Enum.GetNames(typeof(NutrientEntity))) {
                if (nutriant != NutrientEntity.ZincInmG.ToString()) {
                    string name = nutriant;
                    nutrientRepositoryMock.Setup(x => x.GetByName(name)).Returns(new Nutrient()).Verifiable(name + " failed");
                }
            }

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

        [Test] 
        public void shouldGetNutrient() {
            var nutrientRepositoryMock = new Mock<INutrientRepository>();
            nutrientRepositoryMock.Setup(x => x.GetByName(It.Is<string>(y => y == "WasteInPercent")));
            new NutrientBusinessLogic(nutrientRepositoryMock.Object).GetNutrient(NutrientEntity.WasteInPercent);
            nutrientRepositoryMock.VerifyAll();
        }
    }
}