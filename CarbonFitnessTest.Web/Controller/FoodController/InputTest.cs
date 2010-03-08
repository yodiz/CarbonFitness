using System;
using System.Linq;
using System.Web.Mvc;
using CarbonFitness.App.Web;
using CarbonFitness.App.Web.Models;
using CarbonFitness.App.Web.ViewConstants;
using CarbonFitness.BusinessLogic;
using CarbonFitness.BusinessLogic.Exceptions;
using CarbonFitness.Data.Model;
using Moq;
using NUnit.Framework;

namespace CarbonFitnessTest.Web.Controller.FoodController {
	[TestFixture]
	public class InputTest {
		private string getErrormessage(CarbonFitness.App.Web.Controllers.FoodController foodController, string key) {
			ModelState modelState;
			foodController.ModelState.TryGetValue(key, out modelState);
			Assert.That(modelState, Is.Not.Null);
			return modelState.Errors[0].ErrorMessage;
		}

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
			var foodController = new CarbonFitness.App.Web.Controllers.FoodController(userIngredientBusinessLogicMock.Object, userContextMock.Object);
			var actionResult = (ViewResult) callMethodUnderTest(foodController);
			return (InputFoodModel) actionResult.ViewData.Model;
		}

		private Mock<IUserContext> GetSetuppedUserContextMock(MockFactory mockFactory) {
			var userContextMock = mockFactory.Create<IUserContext>();
			userContextMock.Setup(x => x.User).Returns(new User {Username = "myUser"});
			return userContextMock;
		}

		[Test]
		public void shouldAddIngredientToCurrentUser() {
			var mockFactory = new MockFactory(MockBehavior.Strict);
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
			var mockFactory = new MockFactory(MockBehavior.Strict);
			var userContextMock = GetSetuppedUserContextMock(mockFactory);

			var userIngredientBusinessLogicMock = GetSetuppedUserIngredientBusinessLogicMock(mockFactory);

			var model = testController(x => x.Input(), userIngredientBusinessLogicMock, userContextMock);

			AssertUserIngredientsExist(mockFactory, model);
		}

		[Test]
		public void shouldLoadUserIngredientsAfterAddedIngredients() {
			var mockFactory = new MockFactory(MockBehavior.Strict);
			var userContextMock = GetSetuppedUserContextMock(mockFactory);
			var userIngredientBusinessLogicMock = GetSetuppedUserIngredientBusinessLogicMock(mockFactory);

			userIngredientBusinessLogicMock.Setup(x => x.AddUserIngredient(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<int>(), It.Is<DateTime>(y => y.ToShortDateString() == DateTime.Now.ToShortDateString()))).Returns(new UserIngredient());

			var inputFoodModel = new InputFoodModel {Ingredient = "Pannbiff", Measure = 100, Date = DateTime.Now};
			var model = testController(x => x.Input(inputFoodModel), userIngredientBusinessLogicMock, userContextMock);

			AssertUserIngredientsExist(mockFactory, model);
		}

		[Test]
		public void shouldReportErrorWhenInvalidDateEnteredOnPage() {
			var m = new InputFoodModel {Date = new DateTime(1234)};

			var userIngredientBusinessLogicMock = new Mock<IUserIngredientBusinessLogic>(MockBehavior.Strict);
			var userContextMock = new Mock<IUserContext>(MockBehavior.Strict);
			userIngredientBusinessLogicMock.Setup(x => x.GetUserIngredients(It.IsAny<User>(), It.IsAny<DateTime>())).Throws(new InvalidDateException());
			userContextMock.Setup(x => x.User).Returns(new User());

			var foodController = new CarbonFitness.App.Web.Controllers.FoodController(userIngredientBusinessLogicMock.Object, userContextMock.Object);
			foodController.Input(m);

			var errormessage = getErrormessage(foodController, "Date");
			Assert.That(errormessage, Is.EqualTo("Invalid date entered. Date should be in format YYYY-MM-DD"));
		}

		[Test]
		public void shouldReportErrorWhenNoIngredientFound() {
			var mockFactory = new MockFactory(MockBehavior.Strict);
			var userContextMock = GetSetuppedUserContextMock(mockFactory);

			var ingredientName = "afafafafafafafa";

			var userIngredientBusinessLogicMock = mockFactory.Create<IUserIngredientBusinessLogic>();

			userIngredientBusinessLogicMock.Setup(x => x.GetUserIngredients(It.IsAny<User>(), It.IsAny<DateTime>())).Returns(new[] {new UserIngredient()});
			userIngredientBusinessLogicMock.Setup(x => x.AddUserIngredient(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<DateTime>()))
				.Throws(new NoIngredientFoundException(ingredientName));

			var foodController = new CarbonFitness.App.Web.Controllers.FoodController(userIngredientBusinessLogicMock.Object, userContextMock.Object);
			foodController.Input(new InputFoodModel {Ingredient = ingredientName, Measure = 10});

			//var model = testController(x => x.Input(new InputFoodModel()), userIngredientBusinessLogicMock, userContextMock);

			var errormessage = getErrormessage(foodController, "Ingredient");

			Assert.That(errormessage.Contains(ingredientName));
			Assert.That(errormessage, Is.EqualTo(FoodConstant.NoIngredientFoundMessage + ingredientName));
		}
	}
}