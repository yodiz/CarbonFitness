using System;
using System.Linq;
using System.Web.Mvc;
using CarbonFitness.BusinessLogic;
using CarbonFitness.Data.Model;
using CarbonFitnessWeb;
using CarbonFitnessWeb.Models;
using Moq;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace CarbonFitnessTest.Web.Controller.FoodController
{
	[TestFixture]
	public class InputTest
	{
		[Test]
		public void shouldAddIngredientToCurrentUser() {
            var mockFactory = new MockFactory(MockBehavior.Strict);
            var userContextMock = GetSetuppedUserContextMock(mockFactory);

            var userIngredientBusinessLogicMock = mockFactory.Create<IUserIngredientBusinessLogic>();
		    var userIngredient = new UserIngredient {
		                                                User = new User { Username = "myUser" },
		                                                Ingredient = new Ingredient { Name = "Pannbiff" },
		                                                Measure = 100};

            userIngredientBusinessLogicMock.Setup(x => x.GetUserIngredients(It.IsAny<User>(), It.IsAny<DateTime>())).Returns(new[] { userIngredient });
            userIngredientBusinessLogicMock.Setup(x => x.AddUserIngredient(It.Is<User>(u => u.Username == "myUser"), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<DateTime>())).Returns(userIngredient);


            var inputFoodModel = new InputFoodModel { Ingredient = "Pannbiff", Measure = 100, Date = DateTime.Now };
            InputFoodModel model = testController(x => x.Input(inputFoodModel), userIngredientBusinessLogicMock, userContextMock);

		    Assert.That(model, Is.Not.Null);
			Assert.That(model.UserIngredients, Is.Not.Null);
			Assert.That(model.UserIngredients.Any(x => x.Ingredient.Name == "Pannbiff"));
			Assert.That(model.UserIngredients.Any(x => x.Measure == 100));

			userIngredientBusinessLogicMock.VerifyAll();
		}

	    [Test]
        public void shouldLoadUserIngredientsAfterAddedIngredients() {
            var mockFactory = new MockFactory(MockBehavior.Strict);
            var userContextMock = GetSetuppedUserContextMock(mockFactory);
            Mock<IUserIngredientBusinessLogic> userIngredientBusinessLogicMock = GetSetuppedUserIngredientBusinessLogicMock(mockFactory);

            userIngredientBusinessLogicMock.Setup(x => x.AddUserIngredient(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<int>(), It.Is<DateTime>(y => y.ToShortDateString() == DateTime.Now.ToShortDateString()))).Returns(new UserIngredient());

            var inputFoodModel = new InputFoodModel { Ingredient = "Pannbiff", Measure = 100, Date = DateTime.Now };
            InputFoodModel model = testController(x => x.Input(inputFoodModel), userIngredientBusinessLogicMock, userContextMock);

            AssertUserIngredientsExist(mockFactory, model);
        }

	    [Test]
        public void shouldGetTodaysUserIngredientsOnLoad() {
            var mockFactory = new MockFactory(MockBehavior.Strict);
            var userContextMock = GetSetuppedUserContextMock(mockFactory);

            Mock<IUserIngredientBusinessLogic> userIngredientBusinessLogicMock = GetSetuppedUserIngredientBusinessLogicMock(mockFactory);

            InputFoodModel model = testController(x => x.Input(), userIngredientBusinessLogicMock, userContextMock);

            AssertUserIngredientsExist(mockFactory, model);
        }

        private Mock<IUserIngredientBusinessLogic> GetSetuppedUserIngredientBusinessLogicMock(MockFactory mockFactory)
        {
            var userIngredientBusinessLogicMock = mockFactory.Create<IUserIngredientBusinessLogic>();
            var returnedUserIngredients = new[] { new UserIngredient { Date = DateTime.Now }, new UserIngredient { Date = DateTime.Now } };

            userIngredientBusinessLogicMock.Setup(x => x.GetUserIngredients(It.IsAny<User>(), It.Is<DateTime>(y => y.ToShortDateString() == DateTime.Now.ToShortDateString()))).Returns(returnedUserIngredients);
            return userIngredientBusinessLogicMock;
        }

        private void AssertUserIngredientsExist(MockFactory mockFactory, InputFoodModel model) {
            Assert.That(model.UserIngredients, Is.Not.Null);
            Assert.That(model.UserIngredients.Count(), Is.EqualTo(2));
            Assert.That(model.UserIngredients.First().Date.ToShortDateString(), Is.EqualTo(DateTime.Now.ToShortDateString()));
            mockFactory.VerifyAll();
        }

        private InputFoodModel testController(Func<CarbonFitnessWeb.Controllers.FoodController, object> callMethodUnderTest, Mock<IUserIngredientBusinessLogic> userIngredientBusinessLogicMock, Mock<IUserContext> userContextMock) {
            var foodController = new CarbonFitnessWeb.Controllers.FoodController(userIngredientBusinessLogicMock.Object, userContextMock.Object);
            var actionResult = (ViewResult)callMethodUnderTest(foodController);
            return (InputFoodModel)actionResult.ViewData.Model;
        }

	    private Mock<IUserContext> GetSetuppedUserContextMock(MockFactory mockFactory) {
	        var userContextMock = mockFactory.Create<IUserContext>();
	        userContextMock.Setup(x => x.User).Returns(new User{Username = "myUser"});
	        return userContextMock;
	    }
	}
}