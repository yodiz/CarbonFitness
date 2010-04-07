using CarbonFitness.AppLogic;
using CarbonFitness.BusinessLogic;
using Moq;
using NUnit.Framework;

namespace CarbonFitnessTest.Web.AppLogic {
	[TestFixture]
	public class SchemaExportEngineTest {
        [Test]
		public void shouldExportDatabase() {
			var bootstapperMock = new Mock<IBootStrapper>(MockBehavior.Strict);

			bootstapperMock.Setup(x => x.ExportDataBaseSchema());

			new SchemaExportEngine(bootstapperMock.Object).Export();

			bootstapperMock.VerifyAll();
		}
	}
}