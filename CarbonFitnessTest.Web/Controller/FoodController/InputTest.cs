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
		public void shouldAddIngredientToCurrentUser()
		{
			var userIngredientBusinessLogicMock = new Mock<IUserIngredientBusinessLogic>();
			userIngredientBusinessLogicMock.Setup(x => x.AddUserIngredient(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<int>())).Returns(new UserIngredient
			                                                                                                    	{
			                                                                                                    		User = new User { Username = "myUser" },
																																						Ingredient = new Ingredient { Name = "Pannbiff" },
																																						Measure = 100
			                                                                                                    	});
			var userContextMock = new Mock<IUserContext>();
			var userName = "myUser";
			userContextMock.Setup(x => x.User).Returns(new User(userName));

			var foodController = new CarbonFitnessWeb.Controllers.FoodController(userIngredientBusinessLogicMock.Object, userContextMock.Object);

			var inputFoodModel = new InputFoodModel { Ingredient = "Pannbiff", Measure = 100 };

			var actionResult = (ViewResult)foodController.Input(inputFoodModel);
			var model = (InputFoodModel)actionResult.ViewData.Model;

			Assert.That(model, Is.Not.Null);
			Assert.That(model.UserIngredients, Is.Not.Null);
			Assert.That(model.UserIngredients.Any(x => x.Ingredient.Name == "Pannbiff"));
			Assert.That(model.UserIngredients.Any(x => x.Measure == 100));

			userIngredientBusinessLogicMock.VerifyAll();
		}

		//[Test]
		//public void shouldAddIngredientToCurrentUser()
		//{
		//   var mockFactory = new MockFactory(MockBehavior.Strict);
		//   var userIngredientRepositoryMock = mockFactory.Create<IUserIngredientRepository>();

		//   userIngredientRepositoryMock.Setup(x => x.SaveOrUpdate(It.IsAny<UserIngredient>())).Returns(new UserIngredient());

		//   var ingredient = new Ingredient {Name = "MyIngredient"};
		//   var currentUser = new User {Password = "", Username = "user1"};
		//   var meal = new Meal {User = currentUser, CreatedDate = new DateTime(1980, 04, 07)};

		//   new MealBusinessLogic(userIngredientRepositoryMock.Object, meal).AddIngredient(currentUser, ingredient, 100);
		//   mockFactory.VerifyAll();
		//}

		//[Test]
		//public void shouldLoadIngredientsOnPage()
		//{
		//   var mealBusinessLogic = new Mock<IMealBusinessLogic>();
		//   var userBusinessLogic = new Mock<IUserBusinessLogic>();

		//   var mealIngredient = new MealIngredient {Ingredient = new Ingredient {Name = "Pannbiff"}, Measure = 100};
		//   mealBusinessLogic.Setup(x => x.GetMealIngredients(It.IsAny<int>())).Returns(new[] {mealIngredient});
		//      // Should go through aggregate or should have MealIngredientBL ?

		//   var foodController = new CarbonFitnessWeb.Controllers.FoodController(mealBusinessLogic.Object,
		//                                                                        userBusinessLogic.Object);

		//   int mealId = 1;
		//   var actionResult = (ViewResult) foodController.Input(mealId);
		//   var model = (InputFoodModel) actionResult.ViewData.Model;

		//   Assert.That(model.MealIngredients, Is.Not.Null);
		//   Assert.That(model.MealIngredients.Any(x => x.Ingredient.Name == "Pannbiff"));
		//   Assert.That(model.MealIngredients.Any(x => x.Measure == 100));
		//}

		//[Test]
		//public void shouldCreateMealIfMealIdIsNull(){}

		//[Test]
		//public void shouldReturnView()
		//{
		//   var mealBusinessLogic = new Mock<IMealBusinessLogic>();
		//   var foodController = new CarbonFitnessWeb.Controllers.FoodController(mealBusinessLogic.Object, null);

		//   var viewResult = (ViewResult) foodController.Input(1);
		//   Assert.That(viewResult, Is.Not.Null);
		//}

		/*
		[Test]
		public void shouldContainIngredientInModelWhenAddingIngredientOnPage() {
			var mealBusinessLogic = new Mock<IMealBusinessLogic>();
			var userBusinessLogic = new Mock<IUserBusinessLogic>();

			mealBusinessLogic.Setup(x => x.AddIngredient(It.IsAny<User>(), It.IsAny<Ingredient>(), It.IsAny<int>())).AtMost(1);
			var foodController = new CarbonFitnessWeb.Controllers.FoodController(mealBusinessLogic.Object, userBusinessLogic.Object);

			InputFoodModel inputFoodModel = new InputFoodModel { Ingredient = "Panbiff", Measure = 100 };

			var actionResult = (ViewResult)foodController.Input(inputFoodModel);
			var model = (InputFoodModel)actionResult.ViewData.Model;

			Assert.That(model, Is.Not.Null);
			Assert.That(model.MealIngredients, Is.Not.Null);
			Assert.That(model.MealIngredients.Any(x => x.Ingredient.Name == "Pannbiff"));
			Assert.That(model.MealIngredients.Any(x => x.Measure == 100));
			
			mealBusinessLogic.VerifyAll();
		}*/
	}
}