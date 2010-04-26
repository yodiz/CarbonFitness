using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using System.Xml.Serialization;
using CarbonFitness.App.Web;
using CarbonFitness.App.Web.Models;
using CarbonFitness.BusinessLogic;
using CarbonFitness.BusinessLogic.UnitHistory;
using CarbonFitness.Data.Model;
using Moq;
using NUnit.Framework;

namespace CarbonFitnessTest.Web.Controller.ResultController {
	[TestFixture]
	public class ShowTest {
		[SetUp]
		public void SetUp() {
			userIngredientBusinessLogicMock = new Mock<IUserIngredientBusinessLogic>();
			userContextMock = new Mock<IUserContext>();
			userProfileBusinessLogic = new Mock<IUserProfileBusinessLogic>();
			graphBuilderMock = new Mock<IGraphBuilder>();
            nutrientBusinessLogicMock = new Mock<INutrientBusinessLogic>();
            userWeightBusinessLogicMock = new Mock<IUserWeightBusinessLogic>();
            
		}

		private Mock<IUserIngredientBusinessLogic> userIngredientBusinessLogicMock;
		private Mock<IUserContext> userContextMock;
		private Mock<IUserProfileBusinessLogic> userProfileBusinessLogic;
		private Mock<IGraphBuilder> graphBuilderMock;
	    private Mock<INutrientBusinessLogic> nutrientBusinessLogicMock;
        private Mock<IUserWeightBusinessLogic> userWeightBusinessLogicMock;

	    private ActionResult RunMethodUnderTest(Func<CarbonFitness.App.Web.Controllers.ResultController, ActionResult> methodUnderTest) {
            var resultController = new CarbonFitness.App.Web.Controllers.ResultController(userProfileBusinessLogic.Object, userIngredientBusinessLogicMock.Object, userContextMock.Object, graphBuilderMock.Object, userWeightBusinessLogicMock.Object, nutrientBusinessLogicMock.Object, null);
			return methodUnderTest(resultController);
		}

		private ResultModel GetModelFromActionResult(ActionResult result) {
			return (ResultModel) ((ViewResult) result).ViewData.Model;
		}

		[Test]
		public void shouldGetCalorieHistoryAsAmChartDataXml() {
			AutoMappingsBootStrapper.MapHistoryGraphToAmChartData();

			ILine line = new Line(new Dictionary<DateTime, decimal> {{DateTime.Now, 35M}});
			var graph = new Graph {Labels = new[] {new Label {Value = "val1", Index = "1"}}, LinesContainer = new LinesContainer {Lines = new[] {line}}};
			userIngredientBusinessLogicMock.Setup(x => x.GetNutrientHistory(NutrientEntity.EnergyInKcal, It.IsAny<User>())).Returns(line);
			graphBuilderMock.Setup(x => x.GetGraph(It.Is<ILine[]>(y => y[0] == line))).Returns(graph);

            var actionResult = (ContentResult)RunMethodUnderTest(x => x.ShowXml(NutrientEntity.EnergyInKcal.ToString()));

			var serializer = new XmlSerializer(typeof(AmChartData));
			var stringReader = new StringReader(actionResult.Content);
			var deserialized = (AmChartData) serializer.Deserialize(stringReader);

			graphBuilderMock.VerifyAll();
			userIngredientBusinessLogicMock.VerifyAll();
			Assert.That(deserialized.DataPoints.Length, Is.EqualTo(1));
			Assert.That(deserialized.DataPoints.Length, Is.EqualTo(graph.Labels.Length));
			Assert.That(deserialized.DataPoints[0].Value, Is.EqualTo(graph.Labels[0].Value));
			Assert.That(decimal.Parse(deserialized.GraphRoot.Graphs[0].values[0].Value), Is.EqualTo(graph.LinesContainer.Lines[0].GetValuePoints()[0].Value));
		}

        [Test] 
        public void shouldGetUserWeightHistoryForGraph(){
            ILine line = new Line(new Dictionary<DateTime, decimal> { { DateTime.Now, 35M } });
            var graph = new Graph { Labels = new[] { new Label { Value = "val1", Index = "1" } }, LinesContainer = new LinesContainer { Lines = new[] { line } } };
            userWeightBusinessLogicMock.Setup(x => x.GetHistoryLine(It.IsAny<User>())).Returns(line);
            graphBuilderMock.Setup(x => x.GetGraph(It.Is<ILine[]>(y => y[0] == line))).Returns(graph);

            var actionResult = (ContentResult)RunMethodUnderTest(x => x.ShowXml("Weight"));

            userWeightBusinessLogicMock.VerifyAll();
            userIngredientBusinessLogicMock.Verify(x => x.GetNutrientHistory(NutrientEntity.EnergyInKcal, It.IsAny<User>()), Times.Never());
        }

        [Test]
        public void shouldTellWhenGraphlineWeightIsIncluded() {
            var controller = new CarbonFitness.App.Web.Controllers.ResultController(null, null, null, null, null, null, null);
			
            Assert.That(controller.shouldShowWeight(new [] { "adfssaf", "Weight", "dsf"}));
            Assert.That(controller.shouldShowWeight(new[] { "adfssaf", "noewigh", "dsf" }), Is.EqualTo(false));
        }

	    [Test]
        public void shouldNotThrowWhenUnexpectedStringTransformsToNutrientEntity() {
            var resultController = new CarbonFitness.App.Web.Controllers.ResultController(userProfileBusinessLogic.Object, userIngredientBusinessLogicMock.Object, userContextMock.Object, graphBuilderMock.Object, null, nutrientBusinessLogicMock.Object, null);
            string[] nutrients = new[] { "No GOod Nutrient Entity", NutrientEntity.ZincInmG.ToString() };
            NutrientEntity[] nutrientEntitys = resultController.GetNutrientEntitys(nutrients);

            Assert.That(nutrientEntitys.Length, Is.EqualTo(1));
            Assert.That(nutrientEntitys[0], Is.EqualTo(NutrientEntity.ZincInmG));
            
        }

	    [Test]
        public void shouldGetNutrientEntitysFromStrings() {
            var resultController = new CarbonFitness.App.Web.Controllers.ResultController(userProfileBusinessLogic.Object, userIngredientBusinessLogicMock.Object, userContextMock.Object, graphBuilderMock.Object, null, nutrientBusinessLogicMock.Object, null);
            string[] nutrients = new[] { NutrientEntity.ZincInmG.ToString(), NutrientEntity.EVitaminInmG.ToString() };
            NutrientEntity[] nutrientEntitys = resultController.GetNutrientEntitys(nutrients);
            
            Assert.That(nutrientEntitys.Length, Is.EqualTo(2));
            Assert.That(nutrientEntitys[0], Is.EqualTo(NutrientEntity.ZincInmG));
            Assert.That(nutrientEntitys[1], Is.EqualTo(NutrientEntity.EVitaminInmG));
        }

	    [Test]
        public void shouldHaveMultipleLines() {
            userContextMock.Setup(x => x.User).Returns(new User());
            graphBuilderMock.Setup(x => x.GetGraph(It.Is<ILine[]>(y => y.Length == 2))).Returns(new Graph());
            userIngredientBusinessLogicMock.Setup(x => x.GetNutrientHistory(It.IsAny<NutrientEntity>(), It.IsAny<User>())).Returns(new Line( new Dictionary<DateTime, decimal>{{DateTime.Now, 123}}));
            var resultController = new CarbonFitness.App.Web.Controllers.ResultController(userProfileBusinessLogic.Object, userIngredientBusinessLogicMock.Object, userContextMock.Object, graphBuilderMock.Object, null, nutrientBusinessLogicMock.Object, null);
			NutrientEntity[] nutrients = new [] {NutrientEntity.ZincInmG, NutrientEntity.EVitaminInmG };
            var graph = resultController.GetGraph(nutrients, false);
            userIngredientBusinessLogicMock.VerifyAll();
            graphBuilderMock.VerifyAll();
        }


	    [Test]
		public void shouldShowIdealWeight() {
			const decimal userIdealWeight = 65;
			userProfileBusinessLogic.Setup(x => x.GetIdealWeight(It.IsAny<User>())).Returns(userIdealWeight);
			var result = RunMethodUnderTest(x => x.Show());
			var model = GetModelFromActionResult(result);
			Assert.That(model.IdealWeight, Is.EqualTo(userIdealWeight));
		}

        [Test]
        public void shouldShowNutrients() {
            IEnumerable<Nutrient> expectedNutrients = new[] {new Nutrient(), new Nutrient()};
            nutrientBusinessLogicMock.Setup(x => x.GetNutrients()).Returns(expectedNutrients);

            var result = RunMethodUnderTest(x => x.Show());
            var model = GetModelFromActionResult(result);

            Assert.That(model.Nutrients, Is.SameAs(expectedNutrients));
        }
	}
}