using System;
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
		}

		private Mock<IUserIngredientBusinessLogic> userIngredientBusinessLogicMock;
		private Mock<IUserContext> userContextMock;
		private Mock<IUserProfileBusinessLogic> userProfileBusinessLogic;
		private Mock<IGraphBuilder> graphBuilderMock;

		private ActionResult RunMethodUnderTest(Func<CarbonFitness.App.Web.Controllers.ResultController, ActionResult> methodUnderTest) {
			var resultController = new CarbonFitness.App.Web.Controllers.ResultController(userProfileBusinessLogic.Object, userIngredientBusinessLogicMock.Object, userContextMock.Object, graphBuilderMock.Object);
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
			userIngredientBusinessLogicMock.Setup(x => x.GetCalorieHistory(It.IsAny<User>())).Returns(line);
			graphBuilderMock.Setup(x => x.GetGraph(It.Is<ILine[]>(y => y[0] == line))).Returns(graph);

			var actionResult = (ContentResult) RunMethodUnderTest(x => x.ShowXml());

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
		public void shouldShowIdealWeight() {
			const decimal userIdealWeight = 65;
			userProfileBusinessLogic.Setup(x => x.GetIdealWeight(It.IsAny<User>())).Returns(userIdealWeight);
			var result = RunMethodUnderTest(x => x.Show());
			var model = GetModelFromActionResult(result);
			Assert.That(model.IdealWeight, Is.EqualTo(userIdealWeight));
		}
	}
}