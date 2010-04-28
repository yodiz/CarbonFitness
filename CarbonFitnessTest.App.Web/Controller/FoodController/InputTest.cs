using System;
using System.Linq;
using System.Web.Mvc;
using CarbonFitness.App.Web;
using CarbonFitness.App.Web.Controllers.RDI;
using CarbonFitness.App.Web.Models;
using CarbonFitness.App.Web.ViewConstants;
using CarbonFitness.BusinessLogic;
using CarbonFitness.BusinessLogic.Exceptions;
using CarbonFitness.Data.Model;
using Moq;
using NUnit.Framework;

namespace CarbonFitnessTest.Web.Controller.FoodController {
	[TestFixture]
	public class InputTest : BaseTestController {

		private Mock<IUserIngredientBusinessLogic> GetSetuppedUserIngredientBusinessLogicMock(MockFactory mockFactory) {
			var userIngredientBusinessLogicMock = mockFactory.Create<IUserIngredientBusinessLogic>();
			var returnedUserIngredients = new[] {new UserIngredient {Date = DateTime.Now}, new UserIngredient {Date = DateTime.Now}};

			userIngredientBusinessLogicMock.Setup(x => x.GetUserIngredients(It.IsAny<User>(), It.Is<DateTime>(y => y.ToShortDateString() == DateTime.Now.ToShortDateString()))).Returns(returnedUserIngredients);
			return userIngredientBusinessLogicMock;
		}

		private void AssertUserIngredientsExist(MockFactory mockFactory, InputFoodModel model) {
			Assert.That(model.UserIngredients, Is.Not.Null);
			Assert.That(model.UserIngredients.Count(), Is.EqualTo(2));
			Assert.That(model.UserIngredients.First().Date.ToShortDateString(), Is.EqualTo(DateTime.Now.ToShortDateString()));
			mockFactory.VerifyAll();
		}

		private InputFoodModel testController(Func<CarbonFitness.App.Web.Controllers.FoodController, object> callMethodUnderTest, Mock<IUserIngredientBusinessLogic> userIngredientBusinessLogicMock, Mock<IUserContext> userContextMock) {
		    CarbonFitness.App.Web.Controllers.FoodController foodController = getFoodController(userIngredientBusinessLogicMock, userContextMock);
		    return runMethodUnderTest(callMethodUnderTest, foodController);
		}

	    private InputFoodModel runMethodUnderTest(Func<CarbonFitness.App.Web.Controllers.FoodController, object> callMethodUnderTest, CarbonFitness.App.Web.Controllers.FoodController foodController) {
	        var actionResult = (ViewResult) callMethodUnderTest(foodController);
	        return (InputFoodModel) actionResult.ViewData.Model;
	    }

	    private CarbonFitness.App.Web.Controllers.FoodController getFoodController(Mock<IUserIngredientBusinessLogic> userIngredientBusinessLogicMock, Mock<IUserContext> userContextMock) {
            return new CarbonFitness.App.Web.Controllers.FoodController(userIngredientBusinessLogicMock.Object, new Mock<IRDIProxy>().Object, userContextMock.Object);
	    }

	    private Mock<IUserContext> GetSetuppedUserContextMock(MockFactory mockFactory) {
			var userContextMock = mockFactory.Create<IUserContext>();
			userContextMock.Setup(x => x.User).Returns(new User {Username = "myUser"});
			return userContextMock;
		}

		[Test]
		public void shouldAddIngredientToCurrentUser() {
			var mockFactory = new MockFactory(MockBehavior.Loose);
			var userContextMock = GetSetuppedUserContextMock(mockFactory);

			var userIngredientBusinessLogicMock = mockFactory.Create<IUserIngredientBusinessLogic>();
			var userIngredient = new UserIngredient {
				User = new User {Username = "myUser"},
				Ingredient = new Ingredient {Name = "Pannbiff"},
				Measure = 100
			};

			userIngredientBusinessLogicMock.Setup(x => x.GetUserIngredients(It.IsAny<User>(), It.IsAny<DateTime>())).Returns(new[] {userIngredient});
			userIngredientBusinessLogicMock.Setup(x => x.AddUserIngredient(It.Is<User>(u => u.Username == "myUser"), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<DateTime>())).Returns(userIngredient);

			var inputFoodModel = new InputFoodModel {Ingredient = "Pannbiff", Measure = 100, Date = DateTime.Now};
			var model = testController(x => x.Input(inputFoodModel), userIngredientBusinessLogicMock, userContextMock);

			Assert.That(model, Is.Not.Null);
			Assert.That(model.UserIngredients, Is.Not.Null);
			Assert.That(model.UserIngredients.Any(x => x.Ingredient.Name == "Pannbiff"));
			Assert.That(model.UserIngredients.Any(x => x.Measure == 100));

			userIngredientBusinessLogicMock.VerifyAll();
		}

		[Test]
		public void shouldGetTodaysUserIngredientsOnLoad() {
			var mockFactory = new MockFactory(MockBehavior.Loose);
			var userContextMock = GetSetuppedUserContextMock(mockFactory);

			var userIngredientBusinessLogicMock = GetSetuppedUserIngredientBusinessLogicMock(mockFactory);

			var model = testController(x => x.Input(), userIngredientBusinessLogicMock, userContextMock);

			AssertUserIngredientsExist(mockFactory, model);
		}

		[Test]
		public void shouldLoadUserIngredientsAfterAddedIngredients() {
            var mockFactory = new MockFactory(MockBehavior.Loose);
			var userContextMock = GetSetuppedUserContextMock(mockFactory);
			var userIngredientBusinessLogicMock = GetSetuppedUserIngredientBusinessLogicMock(mockFactory);

			userIngredientBusinessLogicMock.Setup(x => x.AddUserIngredient(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<int>(), It.Is<DateTime>(y => y.ToShortDateString() == DateTime.Now.ToShortDateString()))).Returns(new UserIngredient());

			var inputFoodModel = new InputFoodModel {Ingredient = "Pannbiff", Measure = 100, Date = DateTime.Now};
			var model = testController(x => x.Input(inputFoodModel), userIngredientBusinessLogicMock, userContextMock);

			AssertUserIngredientsExist(mockFactory, model);
		}

        [Test]
        public void shouldGetSumOfNutrients() {
            var userContextMock = new Mock<IUserContext>();
            userContextMock.Setup(x => x.User).Returns(new User {Username = "myUser"});

            var userIngredientBusinessLogicMock = new Mock<IUserIngredientBusinessLogic>();
            userIngredientBusinessLogicMock.Setup(x => x.GetNutrientSumForDate(It.IsAny<User>(), It.Is<NutrientEntity>(y=>y == NutrientEntity.ProteinInG), It.IsAny<DateTime>())).Returns(12M);
            userIngredientBusinessLogicMock.Setup(x => x.GetNutrientSumForDate(It.IsAny<User>(), It.Is<NutrientEntity>(y => y == NutrientEntity.FatInG), It.IsAny<DateTime>())).Returns(12M);
            userIngredientBusinessLogicMock.Setup(x => x.GetNutrientSumForDate(It.IsAny<User>(), It.Is<NutrientEntity>(y => y == NutrientEntity.CarbonHydrateInG), It.IsAny<DateTime>())).Returns(12M);
            userIngredientBusinessLogicMock.Setup(x => x.GetNutrientSumForDate(It.IsAny<User>(), It.Is<NutrientEntity>(y => y == NutrientEntity.FibresInG), It.IsAny<DateTime>())).Returns(12M);
            userIngredientBusinessLogicMock.Setup(x => x.GetNutrientSumForDate(It.IsAny<User>(), It.Is<NutrientEntity>(y => y == NutrientEntity.IronInmG), It.IsAny<DateTime>())).Returns(12M);
            var model = testController(x => x.Input(), userIngredientBusinessLogicMock, userContextMock);

            userIngredientBusinessLogicMock.VerifyAll();
            Assert.That(model.SumOfProtein, Is.EqualTo(12M));
            Assert.That(model.SumOfFat, Is.EqualTo(12M));
            Assert.That(model.SumOfCarbonHydrates, Is.EqualTo(12M));
            Assert.That(model.SumOfFiber, Is.EqualTo(12M));
            Assert.That(model.SumOfIron, Is.EqualTo(12M));
        }


        [Test]
        public void shouldGetRDIOfNutrients() {
            var userContextMock = new Mock<IUserContext>();
            userContextMock.Setup(x => x.User).Returns(new User { Username = "myUser" });

            var rdiProxy = new Mock<IRDIProxy>();
            rdiProxy.Setup(x => x.getRDI(It.IsAny<User>(), It.IsAny<DateTime>(), It.Is<NutrientEntity>(y => y == NutrientEntity.ProteinInG))).Returns(12M);
            rdiProxy.Setup(x => x.getRDI(It.IsAny<User>(), It.IsAny<DateTime>(), It.Is<NutrientEntity>(y => y == NutrientEntity.FatInG))).Returns(12M);
            rdiProxy.Setup(x => x.getRDI(It.IsAny<User>(), It.IsAny<DateTime>(), It.Is<NutrientEntity>(y => y == NutrientEntity.CarbonHydrateInG))).Returns(12M);
            rdiProxy.Setup(x => x.getRDI(It.IsAny<User>(), It.IsAny<DateTime>(), It.Is<NutrientEntity>(y => y == NutrientEntity.FibresInG))).Returns(12M);
            rdiProxy.Setup(x => x.getRDI(It.IsAny<User>(), It.IsAny<DateTime>(), It.Is<NutrientEntity>(y => y == NutrientEntity.IronInmG))).Returns(12M);

            var controller = new CarbonFitness.App.Web.Controllers.FoodController(new Mock<IUserIngredientBusinessLogic>().Object, rdiProxy.Object, userContextMock.Object);
            var model = runMethodUnderTest(x => x.Input(), controller);

            rdiProxy.VerifyAll();
            Assert.That(model.RDIOfProtein, Is.EqualTo(12M));
            Assert.That(model.RDIOfFat, Is.EqualTo(12M));
            Assert.That(model.RDIOfCarbonHydrates, Is.EqualTo(12M));
            Assert.That(model.RDIOfFiber, Is.EqualTo(12M));
            Assert.That(model.RDIOfIron, Is.EqualTo(12M));
        }


		[Test]
		public void shouldReportErrorWhenInvalidDateEnteredOnPage() {
			var m = new InputFoodModel {Date = new DateTime(1234)};

            var userIngredientBusinessLogicMock = new Mock<IUserIngredientBusinessLogic>(MockBehavior.Loose);
			var userContextMock = new Mock<IUserContext>(MockBehavior.Strict);
			userIngredientBusinessLogicMock.Setup(x => x.GetUserIngredients(It.IsAny<User>(), It.IsAny<DateTime>())).Throws(new InvalidDateException());
			userContextMock.Setup(x => x.User).Returns(new User());

            var foodController = new CarbonFitness.App.Web.Controllers.FoodController(userIngredientBusinessLogicMock.Object, new Mock<IRDIProxy>().Object, userContextMock.Object);
			foodController.Input(m);

			var errormessage = getErrormessage(foodController, "Date");
			Assert.That(errormessage, Is.EqualTo("Invalid date entered. Date should be in format YYYY-MM-DD"));
		}

		[Test]
		public void shouldReportErrorWhenNoIngredientFound() {
            var mockFactory = new MockFactory(MockBehavior.Loose);
			var userContextMock = GetSetuppedUserContextMock(mockFactory);

			var ingredientName = "afafafafafafafa";

			var userIngredientBusinessLogicMock = mockFactory.Create<IUserIngredientBusinessLogic>();

			userIngredientBusinessLogicMock.Setup(x => x.GetUserIngredients(It.IsAny<User>(), It.IsAny<DateTime>())).Returns(new[] {new UserIngredient()});
			userIngredientBusinessLogicMock.Setup(x => x.AddUserIngredient(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<DateTime>()))
				.Throws(new NoIngredientFoundException(ingredientName));

            var foodController = new CarbonFitness.App.Web.Controllers.FoodController(userIngredientBusinessLogicMock.Object, new Mock<IRDIProxy>().Object, userContextMock.Object);
			foodController.Input(new InputFoodModel {Ingredient = ingredientName, Measure = 10});

			//var model = testController(x => x.Input(new InputFoodModel()), userIngredientBusinessLogicMock, userContextMock);

			var errormessage = getErrormessage(foodController, "Ingredient");

			Assert.That(errormessage.Contains(ingredientName));
			Assert.That(errormessage, Is.EqualTo(FoodConstant.NoIngredientFoundMessage + ingredientName));
		}

        [Test]
        public void shouldRemoveUserIngredient() {
            var mockFactory = new MockFactory(MockBehavior.Loose);
            var userContextMock = GetSetuppedUserContextMock(mockFactory);
            var userIngredientBusinessLogicMock = mockFactory.Create<IUserIngredientBusinessLogic>();
            userIngredientBusinessLogicMock.Setup(x => x.DeleteUserIngredient(It.IsAny<User>(), It.IsAny<int>(), It.IsAny<DateTime>()));
            var foodController = new CarbonFitness.App.Web.Controllers.FoodController(userIngredientBusinessLogicMock.Object, new Mock<IRDIProxy>().Object, userContextMock.Object);
            foodController.DeleteUserIngredient(3, DateTime.Now);

            userIngredientBusinessLogicMock.VerifyAll();
        }


	}
}