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

			var historyValuesContainer = new HistoryValuesContainer();
			historyValuesContainer.labels = new[] {new Label {Value = "val1", Xid = "1"}};
			historyValuesContainer.unnecessaryContainer = new UnnecessaryContainer {HistoryValuesCollection = new IHistoryValuePoints[] {new HistoryValuePoints(new Dictionary<DateTime, decimal> {{DateTime.Now, 35M}})}};
			userIngredientBusinessLogicMock.Setup(x => x.GetCalorieHistory(It.IsAny<User>())).Returns(historyValuesContainer);

			var resultController = new CarbonFitness.App.Web.Controllers.ResultController(userProfileBusinessLogic.Object, userIngredientBusinessLogicMock.Object, userContextMock.Object);
			var actionResult = (ContentResult) resultController.ShowXml();

			var serializer = new XmlSerializer(typeof(AmChartData));
			var stringReader = new StringReader(actionResult.Content);
			var deserialized = (AmChartData) serializer.Deserialize(stringReader);

			userIngredientBusinessLogicMock.VerifyAll();
			Assert.That(deserialized.DataPoints.Length, Is.EqualTo(historyValuesContainer.labels.Length));
			Assert.That(deserialized.DataPoints[0].Value, Is.EqualTo(historyValuesContainer.labels[0].Value));
			Assert.That(decimal.Parse(deserialized.GraphRoot.Graphs[0].values[0].Value), Is.EqualTo(historyValuesContainer.unnecessaryContainer.HistoryValuesCollection[0].ValuesPoint[0].Value));
		}

		//[Test]
		//public void shouldShowCalorieHistory() {
		//   var 
		//   var expectedCalorieHistory = new HistoryValues(new Dictionary<DateTime, decimal> {{DateTime.Now.Date.AddDays(-1), 2000}, {DateTime.Now.Date, 2150}});
		//   userIngredientBusinessLogicMock.Setup(x => x.GetCalorieHistory(It.IsAny<User>())).Returns(expectedCalorieHistory);
		//   Assert.Fail("DO EEET!");
		//   var model = RunMethodUnderTest(x => x.Show());

		//   userIngredientBusinessLogicMock.VerifyAll();
		//   Assert.That(model.CalorieHistoryList, Is.SameAs(expectedCalorieHistory));
		//}

		[Test]
		public void shouldShowIdealWeight() {
			const decimal userIdealWeight = 65;
			userProfileBusinessLogic.Setup(x => x.GetIdealWeight(It.IsAny<User>())).Returns(userIdealWeight);
			var model = RunMethodUnderTest(x => x.Show());
			Assert.That(model.IdealWeight, Is.EqualTo(userIdealWeight));
		}
	}
}