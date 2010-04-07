using System;
using System.Collections.Generic;
using System.Linq;
using CarbonFitness.App.Web;
using CarbonFitness.App.Web.Models;
using CarbonFitness.BusinessLogic;
using CarbonFitness.BusinessLogic.Implementation;
using CarbonFitness.BusinessLogic.UnitHistory;
using CarbonFitness.Data.Model;
using Moq;
using NUnit.Framework;

namespace CarbonFitnessTest.Web.Controller.ResultController {
	[TestFixture]
	public class ShowWeightPrognosisXmlTest {
		[Test]
		public void shouldReturnWeightPrognosisXml() {
			AutoMappingsBootStrapper.MapHistoryGraphToAmChartData();

			var user = new User("Min user");
			var userContextMock = new Mock<IUserContext>(MockBehavior.Strict);
			userContextMock.Setup(x => x.User).Returns(user);

			var graphBuilderMock = new Mock<IGraphBuilder>(MockBehavior.Strict);

			ILine line = new Line(new Dictionary<DateTime, decimal> { { DateTime.Now.AddDays(1), 35M } }, "MyTitle");
			var graph = new Graph { Labels = new[] { new Label { Value = "val1", Index = "1" } }, LinesContainer = new LinesContainer { Lines = new[] { line } } };
			var userWeightBusinessLogicMock = new Mock<IUserWeightBusinessLogic>();
			userWeightBusinessLogicMock.Setup(x => x.GetProjectionList(It.Is<User>(y => y == user))).Returns(line);
			graphBuilderMock.Setup(x => x.GetGraph(It.Is<ILine[]>(y => y[0] == line))).Returns(graph);

			var resultController = new CarbonFitness.App.Web.Controllers.ResultController(null, null, userContextMock.Object, graphBuilderMock.Object, userWeightBusinessLogicMock.Object, null);

			var result = resultController.ShowWeightPrognosisXml();
			Assert.That(result.ObjectToSerialize, Is.AssignableTo(typeof(AmChartData)));

			var amChartData = (AmChartData) result.ObjectToSerialize;
			var dataPoints = amChartData.DataPoints;
			Assert.That(dataPoints, Is.Not.Null);

			var firstPoint = dataPoints.First();
			Assert.That(firstPoint.Value, Is.EqualTo("val1"));

			Assert.That(amChartData.GraphRoot.Graphs[0].values[0].Value, Is.EqualTo("35"));
			userWeightBusinessLogicMock.Verify();
		}

		//[Test]
		//public void shouldReturnWeightPrognosisForSpecifiedDate()
		//{

		//}
	}
}