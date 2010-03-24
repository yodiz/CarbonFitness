using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using System.Xml.Serialization;
using CarbonFitness.App.Web;
using CarbonFitness.App.Web.Models;
using CarbonFitness.BusinessLogic;
using CarbonFitness.Data.Model;
using Moq;
using MvcContrib.ActionResults;
using NUnit.Framework;

namespace CarbonFitnessTest.Web.Controller.ResultController {
	[TestFixture]
	public class ShowTest {
		[SetUp]
		public void SetUp() {
			userIngredientBusinessLogicMock = new Mock<IUserIngredientBusinessLogic>();
			userContextMock = new Mock<IUserContext>();
			userProfileBusinessLogic = new Mock<IUserProfileBusinessLogic>();
		}

		private Mock<IUserIngredientBusinessLogic> userIngredientBusinessLogicMock;
		private Mock<IUserContext> userContextMock;
		private Mock<IUserProfileBusinessLogic> userProfileBusinessLogic;

		private ResultModel RunMethodUnderTest(Func<CarbonFitness.App.Web.Controllers.ResultController, ActionResult> methodUnderTest) {
			var resultController = new CarbonFitness.App.Web.Controllers.ResultController(userProfileBusinessLogic.Object, userIngredientBusinessLogicMock.Object, userContextMock.Object);
			var actionResult = (ViewResult) methodUnderTest(resultController);
			return (ResultModel) actionResult.ViewData.Model;
		}

		[Test]
		public void shouldGetAmChartDataXml() {
			AutoMappingsBootStrapper.MapHistoryValuesContainerToAmChartData();

			var userIngredientBusinessLogicMock = new Mock<IUserIngredientBusinessLogic>();
			var historyValuesContainer = new HistoryValuesContainer();
			historyValuesContainer.labels = new[] {new strangeObject {value = "val1", xid = "1"}};
			historyValuesContainer.unnecessaryContainer = new UnnecessaryContainer {HistoryValueses = new IHistoryValues[] {new HistoryValues(new Dictionary<DateTime, decimal> {{DateTime.Now, 35M}})}};
			userIngredientBusinessLogicMock.Setup(x => x.GetCalorieHistory(It.IsAny<User>())).Returns(historyValuesContainer);

			var resultController = new CarbonFitness.App.Web.Controllers.ResultController(userProfileBusinessLogic.Object, userIngredientBusinessLogicMock.Object, userContextMock.Object);
			var actionResult = (XmlResult) resultController.ShowXml();
			var unserialized = (AmChartData) actionResult.ObjectToSerialize;

			var serializer = new XmlSerializer(typeof(AmChartData));
			var stringWriter = new StringWriter();
			serializer.Serialize(stringWriter, unserialized);
			var stringReader = new StringReader(stringWriter.ToString());
			var deserialized = (AmChartData) serializer.Deserialize(stringReader);

			userIngredientBusinessLogicMock.VerifyAll();
			Assert.That(deserialized.DataPoints.Length, Is.EqualTo(unserialized.DataPoints.Length));
			Assert.That(deserialized.DataPoints[0].Value, Is.EqualTo(unserialized.DataPoints[0].Value));
			Assert.That(deserialized.GraphRoot.Graphs[0].values[0].Value, Is.EqualTo(unserialized.GraphRoot.Graphs[0].values[0].Value));
		}

		[Test]
		public void shouldShowCalorieHistory() {
			var expectedCalorieHistory = new HistoryValues(new Dictionary<DateTime, decimal> {{DateTime.Now.Date.AddDays(-1), 2000}, {DateTime.Now.Date, 2150}});
			userIngredientBusinessLogicMock.Setup(x => x.GetCalorieHistory(It.IsAny<User>())).Returns(null as HistoryValuesContainer); //.Returns(expectedCalorieHistory);
			Assert.Fail("DO EEET!");
			ResultModel model = RunMethodUnderTest(x => x.Show());

			userIngredientBusinessLogicMock.VerifyAll();
			Assert.That(model.CalorieHistoryList, Is.SameAs(expectedCalorieHistory));
		}

		[Test]
		public void shouldShowIdealWeight() {
			decimal userIdealWeight = 65;
			userProfileBusinessLogic.Setup(x => x.GetIdealWeight(It.IsAny<User>())).Returns(userIdealWeight);
			ResultModel model = RunMethodUnderTest(x => x.Show());
			Assert.That(model.IdealWeight, Is.EqualTo(userIdealWeight));
		}
	}
}