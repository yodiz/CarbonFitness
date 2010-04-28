using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using System.Xml.Serialization;
using CarbonFitness.App.Web;
using CarbonFitness.App.Web.Controllers.ViewTypeConverters;
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
            userWeightBusinessLogicMock = new Mock<IUserWeightBusinessLogic>();
            graphLineOptionViewTypeConverterMock = new Mock<IGraphLineOptionViewTypeConverter>();
            
		}

		private Mock<IUserIngredientBusinessLogic> userIngredientBusinessLogicMock;
		private Mock<IUserContext> userContextMock;
		private Mock<IUserProfileBusinessLogic> userProfileBusinessLogic;
		private Mock<IGraphBuilder> graphBuilderMock;
        private Mock<IUserWeightBusinessLogic> userWeightBusinessLogicMock;
	    private Mock<IGraphLineOptionViewTypeConverter> graphLineOptionViewTypeConverterMock;

	    private ActionResult RunMethodUnderTest(Func<CarbonFitness.App.Web.Controllers.ResultController, ActionResult> methodUnderTest) {
	        var resultController = GetResultController();
	        return methodUnderTest(resultController);
	    }

	    private CarbonFitness.App.Web.Controllers.ResultController GetResultController() {
	        return new CarbonFitness.App.Web.Controllers.ResultController(userProfileBusinessLogic.Object, userIngredientBusinessLogicMock.Object, userContextMock.Object, graphBuilderMock.Object, userWeightBusinessLogicMock.Object, graphLineOptionViewTypeConverterMock.Object);
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
		    graphLineOptionViewTypeConverterMock.Setup(x => x.GetNutrientEntitys(It.IsAny<string[]>())).Returns(new[] {NutrientEntity.EnergyInKcal});
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
            graphLineOptionViewTypeConverterMock.Setup(x => x.GetNutrientEntitys(It.IsAny<string[]>())).Returns(null as NutrientEntity[]);
            graphLineOptionViewTypeConverterMock.Setup(x => x.shouldShowWeight(It.IsAny<string[]>())).Returns(true);
            RunMethodUnderTest(x => x.ShowXml("Weight"));

            userWeightBusinessLogicMock.VerifyAll();
            userIngredientBusinessLogicMock.Verify(x => x.GetNutrientHistory(NutrientEntity.EnergyInKcal, It.IsAny<User>()), Times.Never());
        }

	    [Test]
        public void shouldHaveMultipleLines() {
            userContextMock.Setup(x => x.User).Returns(new User());
            graphBuilderMock.Setup(x => x.GetGraph(It.Is<ILine[]>(y => y.Length == 2))).Returns(new Graph());
            userIngredientBusinessLogicMock.Setup(x => x.GetNutrientHistory(It.IsAny<NutrientEntity>(), It.IsAny<User>())).Returns(new Line( new Dictionary<DateTime, decimal>{{DateTime.Now, 123}}));
            var resultController = new CarbonFitness.App.Web.Controllers.ResultController(userProfileBusinessLogic.Object, userIngredientBusinessLogicMock.Object, userContextMock.Object, graphBuilderMock.Object, null,  null);
			NutrientEntity[] nutrients = new [] {NutrientEntity.ZincInmG, NutrientEntity.EVitaminInmG };
            resultController.GetGraph(nutrients, false);
            userIngredientBusinessLogicMock.VerifyAll();
            graphBuilderMock.VerifyAll();
        }

	    [Test]
		public void shouldShowIdealWeight() {
			const decimal userIdealWeight = 65;
			userProfileBusinessLogic.Setup(x => x.GetIdealWeight(It.IsAny<User>())).Returns(userIdealWeight);
			var result = RunMethodUnderTest(x => x.ShowAdvanced());
			var model = GetModelFromActionResult(result);
			Assert.That(model.IdealWeight, Is.EqualTo(userIdealWeight));
		}

        [Test]
        public void shouldShowGraphlineOptions() {
            var expectedGraphLineOptions = new List<SelectListItem>{new SelectListItem()};
            graphLineOptionViewTypeConverterMock.Setup(x => x.GetViewTypes(It.IsAny<User>())).Returns(expectedGraphLineOptions);
            var result = RunMethodUnderTest(x => x.ShowAdvanced());
            var model = GetModelFromActionResult(result);
            Assert.That(model.GraphLineOptions, Is.EqualTo(expectedGraphLineOptions));
        }

        [Test]
        public void shouldShowWeightEnergy() {
            var result = RunMethodUnderTest(x => x.Show());
            var model = GetModelFromActionResult(result);
            Assert.That(model, Is.Null);
        }

        [Test]
        public void shouldGetResultListModel() {
            var expected = new List<INutrientSum>{ new NutrientSum() };
            userIngredientBusinessLogicMock.Setup(x => x.GetNutrientSumList(It.IsAny<IEnumerable<NutrientEntity>>(), It.IsAny<User>())).Returns(expected);

            var resultController = GetResultController();
            var result = (ViewResult)resultController.ShowResultList();
           
            userIngredientBusinessLogicMock.VerifyAll();
            Assert.That(result.ViewData.Model, Is.TypeOf(typeof(ResultListModel)));
            var model = (ResultListModel)result.ViewData.Model;
            Assert.That(model.NutrientSumList, Is.SameAs(expected));
        }

        [Test]
        public void shouldGetNutrientAvreage() {
            var expected =  new NutrientAverage() ;
            userIngredientBusinessLogicMock.Setup(x => x.GetNutrientAverage(It.IsAny<IEnumerable<NutrientEntity>>(), It.IsAny<User>())).Returns(expected);

            var resultController = GetResultController();
            var result = (ViewResult)resultController.ShowResultList();
            var model = (ResultListModel)result.ViewData.Model;
            
            userIngredientBusinessLogicMock.VerifyAll();
            Assert.That(model.NutrientAverage, Is.SameAs(expected));
        }
	}
}