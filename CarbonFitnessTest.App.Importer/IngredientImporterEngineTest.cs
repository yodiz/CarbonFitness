using CarbonFitness.App.Importer;
using CarbonFitness.BusinessLogic.IngredientImporter;
using Moq;
using NUnit.Framework;

namespace CarbonFitnessTest.App.Importer {
	[TestFixture]
	public class IngredientImporterEngineTest {
		[Test]
		public void shouldCallImporter() {
			var filePath = "myfile.csv";
			var importer = new Mock<IIngredientImporter>();

			importer.Setup(x => x.Import(filePath));

			new IngredientImporterEngine(importer.Object).Import(filePath);

			importer.VerifyAll();
		}
	}
}