using CarbonFitness.BusinessLogic;
using CarbonFitness.BusinessLogic.Implementation;
using CarbonFitness.BusinessLogic.RDI.Importers;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;
using Moq;
using NUnit.Framework;

namespace CarbonFitnessTest.BusinessLogic
{
    [TestFixture]
    public class NutrientRecommendationBusinessLogicTest {
        [Test]
        public void shouldGetNutrientRecommendation() {
            var expected = new NutrientRecommendation();
            var nutrientRecommendationRepositoryMock = new Mock<INutrientRecommendationRepository>();
            var nutrientBusinessLogicMock = new Mock<INutrientBusinessLogic>();
            var nutrientMock = new Mock<Nutrient>();
            var nutrientId = 2;
            nutrientMock.Setup(x => x.Id).Returns(nutrientId);
            nutrientBusinessLogicMock.Setup(x => x.GetNutrient(It.IsAny<NutrientEntity>())).Returns(nutrientMock.Object);
            nutrientRecommendationRepositoryMock.Setup(x => x.GetByNutrient(nutrientMock.Object)).Returns(expected);

            var result = new NutrientRecommendationBusinessLogic(nutrientBusinessLogicMock.Object, nutrientRecommendationRepositoryMock.Object).GetNutrientRecommendation(NutrientEntity.ZincInmG);
            Assert.That(result, Is.SameAs(expected));
        }

        [Test]
        public void shouldExportNutrientRecommendations() {
            var nutrientRDIImporterMock = new Mock<INutrientRDIImporter>();

            nutrientRDIImporterMock.Setup(x => x.Import(It.IsAny<INutrientRecommendationRepository>()));
            new NutrientRecommendationBusinessLogic(null, new Mock<INutrientRecommendationRepository>().Object).ImportNutrientRecommendations(nutrientRDIImporterMock.Object);

            nutrientRDIImporterMock.VerifyAll();
        }
    }
}
