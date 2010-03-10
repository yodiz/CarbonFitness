using System;
using System.Web.Mvc;
using CarbonFitness.App.Web;
using CarbonFitness.App.Web.Models;
using CarbonFitness.BusinessLogic;
using CarbonFitness.Data.Model;
using Moq;
using NUnit.Framework;

namespace CarbonFitnessTest.Web.Controller.WeightController {
	[TestFixture]
	public class InputTest {
		private Mock<IUserContext> userContextMock;
		private Mock<IUserWeightBusinessLogic> userWeightBusinessLogicMock;
		private DateTime expectedDate;
		private decimal expectedWeight;
		private User expectedUser;

		[SetUp]
		public void SetUp() {
			expectedDate = DateTime.Now.Date;
			expectedWeight = 80M;
			expectedUser = new User { Username = "arne" };

			userWeightBusinessLogicMock = new Mock<IUserWeightBusinessLogic>();
			userContextMock = new Mock<IUserContext>();
			userContextMock.Setup(x => x.User).Returns(expectedUser);
		}

		[Test]
		public void shouldSaveWeight() {
			userWeightBusinessLogicMock.Setup(x => x.SaveWeight(It.Is<User>(u => u.Username == "arne"), expectedWeight, expectedDate)).Returns(new UserWeight {Weight = expectedWeight});

			InputWeightModel model = runMethodUnderTest(x => x.Input(new InputWeightModel { Weight = expectedWeight, Date = expectedDate }));

			userWeightBusinessLogicMock.VerifyAll();
			Assert.That(model.Weight, Is.EqualTo(expectedWeight));
			Assert.That(model.Date, Is.EqualTo(expectedDate));
		}

		[Test]
		public void shouldShowWeight() {
			userWeightBusinessLogicMock.Setup(x => x.GetWeight(userContextMock.Object.User, expectedDate)).Returns(new UserWeight {Date = expectedDate, User = userContextMock.Object.User, Weight = 80M});
         
			InputWeightModel model = runMethodUnderTest(x => x.Input());

			Assert.That(model.Weight, Is.EqualTo(80M));
			userWeightBusinessLogicMock.VerifyAll();
		}

		private InputWeightModel runMethodUnderTest(Func<CarbonFitness.App.Web.Controllers.WeightController, ActionResult> func) {
			var weightController = new CarbonFitness.App.Web.Controllers.WeightController(userWeightBusinessLogicMock.Object, userContextMock.Object);
			var result = (ViewResult) func(weightController);
			return (InputWeightModel) result.ViewData.Model;
		}
	}
}