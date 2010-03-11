using System;
using System.Web.Mvc;
using CarbonFitness.App.Web;
using CarbonFitness.App.Web.Models;
using CarbonFitness.App.Web.ViewConstants;
using CarbonFitness.BusinessLogic;
using CarbonFitness.BusinessLogic.Exceptions;
using CarbonFitness.Data.Model;
using Moq;
using NUnit.Framework;

namespace CarbonFitnessTest.Web.Controller.WeightController {
	[TestFixture]
	public class InputTest : BaseTestController {
		private Mock<IUserContext> userContextMock;
		private Mock<IUserWeightBusinessLogic> userWeightBusinessLogicMock;
		private DateTime expectedDate;
		private decimal expectedWeight;
		private User expectedUser;

		[SetUp]
		public void SetUp() {
			expectedDate = DateTime.Now.AddDays(1).Date;
			expectedWeight = 80M;
			expectedUser = new User { Username = "arne" };

			userWeightBusinessLogicMock = new Mock<IUserWeightBusinessLogic>();
			userContextMock = new Mock<IUserContext>();
			userContextMock.Setup(x => x.User).Returns(expectedUser);
		}

		[Test]
		public void shouldSaveWeight() {
			userWeightBusinessLogicMock.Setup(x => x.SaveWeight(It.Is<User>(u => u.Username == "arne"), expectedWeight, expectedDate)).Returns(new UserWeight {Weight = expectedWeight, Date = expectedDate});

			InputWeightModel model = runMethodUnderTest(x => x.Input(new InputWeightModel { Weight = expectedWeight, Date = expectedDate }));

			userWeightBusinessLogicMock.VerifyAll();
			Assert.That(model.Weight, Is.EqualTo(expectedWeight));
			Assert.That(model.Date, Is.EqualTo(expectedDate));
		}

		[Test]
		public void shouldNotSaveWhenWeightIsZero() {
			userWeightBusinessLogicMock.Setup(x => x.SaveWeight(It.IsAny<User>(), It.IsAny<decimal>(), It.IsAny<DateTime>())).Throws(new InvalidWeightException("Unable to save zero as weight"));

			var controller = GetWeightController();
			controller.Input(new InputWeightModel());

			var errormessage = getErrormessage(controller, "Weight");

			Assert.That(errormessage, Is.EqualTo(WeightConstant.ZeroWeightErrorMessage));
		}

		[Test] 
		public void shouldShowWeightForDateOnPage() {
			userWeightBusinessLogicMock.Setup(x => x.GetWeight(userContextMock.Object.User, expectedDate)).Returns(new UserWeight { Date = expectedDate, User = userContextMock.Object.User, Weight = expectedWeight });
			var model = runMethodUnderTest(x => x.Input(expectedDate));
			userWeightBusinessLogicMock.VerifyAll();
			Assert.That(model.Weight, Is.EqualTo(expectedWeight));
		}

		[Test]
		public void shouldShowWeight() {
			userWeightBusinessLogicMock.Setup(x => x.GetWeight(userContextMock.Object.User, DateTime.Now.Date)).Returns(new UserWeight {Date = expectedDate, User = userContextMock.Object.User, Weight = 80M});
         
			var model = runMethodUnderTest(x => x.Input(null as DateTime?));

			Assert.That(model.Weight, Is.EqualTo(80M));
			userWeightBusinessLogicMock.VerifyAll();
		}

		[Test]
		public void shouldShowEmptyValuesWhenNoExistingWeightFound() {
			var model = runMethodUnderTest(x => x.Input(null as DateTime?));
			Assert.That(model.Weight, Is.EqualTo(0M));
		}

		private InputWeightModel runMethodUnderTest(Func<CarbonFitness.App.Web.Controllers.WeightController, ActionResult> func) {
			var result = (ViewResult) func(GetWeightController());
			return (InputWeightModel) result.ViewData.Model;
		}

		private CarbonFitness.App.Web.Controllers.WeightController GetWeightController() {
			return new CarbonFitness.App.Web.Controllers.WeightController(userWeightBusinessLogicMock.Object, userContextMock.Object);
		}
	}
}