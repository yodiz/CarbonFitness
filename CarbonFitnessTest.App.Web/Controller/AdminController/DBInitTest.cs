using System;
using System.Web.Mvc;
using CarbonFitness.App.Web.Controllers;
using CarbonFitness.App.Web.Models;
using CarbonFitness.AppLogic;
using CarbonFitnessTest.Web.Controller.AdminController;
using Moq;
using NUnit.Framework;

namespace CarbonFitnessTest.Web.Controller.AdminController {
	[TestFixture]
	public class DBInitTest {
		[Test]
		public void shouldInitDatabase() {

			var expectedPath = "~/App_Data/Ingredients.csv";

			var schemaExportEngineMock = new Mock<ISchemaExportEngine>();
			schemaExportEngineMock.Setup(x => x.Export());

			var pathfinderMock = new Mock<IPathFinder>();
			pathfinderMock.Setup(x => x.GetPath(expectedPath)).Returns(@"c:\/App_Data/Ingredients.csv");

            var initialDataValuesExportEngine = new Mock<IInitialDataValuesExportEngine>();
            initialDataValuesExportEngine.Setup(x => x.Export());

			var ingredientImporterEngineMock = new Mock<IIngredientImporterEngine>();
			ingredientImporterEngineMock.Setup(x => x.Import(It.Is<String>(y=>y.EndsWith("/App_Data/Ingredients.csv") && y.StartsWith("c:\\") )));

			var controller = new CarbonFitness.App.Web.Controllers.AdminController(schemaExportEngineMock.Object, ingredientImporterEngineMock.Object, initialDataValuesExportEngine.Object, pathfinderMock.Object);
			
			InputDbInitModel model = new InputDbInitModel { ImportFilePath = expectedPath };

			ActionResult result = controller.DBInit(model);

			pathfinderMock.VerifyAll();
			schemaExportEngineMock.VerifyAll();
			ingredientImporterEngineMock.VerifyAll();
            initialDataValuesExportEngine.VerifyAll();
		}

		[Test]
		public void shouldBeAbleToViewPage() {
			var controller = new CarbonFitness.App.Web.Controllers.AdminController(null, null, null);

			ViewResult result = (ViewResult)controller.DBInit();
			Assert.That(result, Is.Not.Null);

			var model = (InputDbInitModel) result.ViewData.Model;
			Assert.That(model.ImportFilePath, Is.EqualTo("~/App_data/Ingredients.csv"));
			
		}

	}
}