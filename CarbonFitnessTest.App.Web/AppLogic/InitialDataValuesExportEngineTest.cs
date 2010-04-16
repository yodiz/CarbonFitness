using CarbonFitness.AppLogic;
using CarbonFitness.BusinessLogic;
using CarbonFitness.BusinessLogic.Implementation;
using Moq;
using NUnit.Framework;

namespace CarbonFitnessTest.Web.AppLogic {
    [TestFixture]
    public class InitialDataValuesExportEngineTest {
        [Test]
        public void shouldExportInitialNutrientDataValues() {
            var nutrientBusinessLogicMock = new Mock<INutrientBusinessLogic>();
            nutrientBusinessLogicMock.Setup(x => x.ExportInitialValues());

            new InitialDataValuesExportEngine(nutrientBusinessLogicMock.Object, new Mock<IGenderTypeBusinessLogic>().Object).Export();
            nutrientBusinessLogicMock.VerifyAll(); 
        }

        [Test]
        public void shouldExportInitialGenderTypeDataValues() {
            var genderTypeBusinessLogicMock = new Mock<IGenderTypeBusinessLogic>();
            genderTypeBusinessLogicMock.Setup(x => x.ExportInitialValues());
            new InitialDataValuesExportEngine(new Mock<INutrientBusinessLogic>().Object, genderTypeBusinessLogicMock.Object).Export();
            genderTypeBusinessLogicMock.VerifyAll();
        }
    }
}