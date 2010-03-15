using System;
using System.Linq;
using CarbonFitness.BusinessLogic.Exceptions;
using CarbonFitness.BusinessLogic.Implementation;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;
using Moq;
using NUnit.Framework;

namespace CarbonFitnessTest.BusinessLogic {
	[TestFixture]
	public class UserIngredientBusinessLogicTest {
		private DateTime todaysDate = DateTime.Now.Date;

		private UserIngredient[] GetExpectedUserIngredients(DateTime date) {
			var userIngredients = new UserIngredient[4];
			userIngredients[0] = new UserIngredient {Ingredient = new Ingredient {Name = "Pannbiff", EnergyInKcal = 28}, Measure = 100, Date = date};
			userIngredients[1] = new UserIngredient {Ingredient = new Ingredient {Name = "Lök", EnergyInKcal = 25}, Measure = 150, Date = date};

			userIngredients[2] = new UserIngredient { Ingredient = new Ingredient { Name = "Lök", EnergyInKcal = 25 }, Measure = 150, Date = date.AddDays(1) };
			userIngredients[3] = new UserIngredient { Ingredient = new Ingredient { Name = "Lök", EnergyInKcal = 27 }, Measure = 150, Date = date.AddDays(1) };
			return userIngredients;
		}

		[Test]
		public void shouldAddUserIngredient() {
			var measure = 100;
			var ingredientName = "Pannbiff";
			var ingredientMock = new Mock<Ingredient>();

			ingredientMock.Setup(x => x.Id).Returns(1);
			ingredientMock.Setup(x => x.Name).Returns(ingredientName);

			var userIngredientRepositoryMock = new Mock<IUserIngredientRepository>();
			var ingredientRepositoryMock = new Mock<IIngredientRepository>();
			userIngredientRepositoryMock.Setup(x => x.SaveOrUpdate(It.Is<UserIngredient>(y => y.Ingredient.Name == ingredientName && y.Ingredient.Id > 0 && y.Measure == measure && y.Date == todaysDate))).Returns(new UserIngredient());
			ingredientRepositoryMock.Setup(x => x.Get(ingredientName)).Returns(ingredientMock.Object);

			var userIngredientLogic = new UserIngredientBusinessLogic(userIngredientRepositoryMock.Object, ingredientRepositoryMock.Object);
			userIngredientLogic.AddUserIngredient(new User(), ingredientName, measure, todaysDate);

			userIngredientRepositoryMock.VerifyAll();
			ingredientRepositoryMock.VerifyAll();
		}

		[Test]
		public void shouldGetCalorieHistory() {
			var expectedUserIngredients = GetExpectedUserIngredients(todaysDate);
			var repositoryMock = new Mock<IUserIngredientRepository>();

			repositoryMock.Setup(x => x.GetUserIngredientsByUser(It.IsAny<int>(), todaysDate.AddMonths(-3).Date, todaysDate.Date.AddDays(1))).Returns(expectedUserIngredients);
			var returnedValues = new UserIngredientBusinessLogic(repositoryMock.Object, null).GetCalorieHistory(new User());

			repositoryMock.VerifyAll();
			Assert.That(returnedValues.Count, Is.EqualTo(2));
			Assert.That(returnedValues.ElementAt(0).Key, Is.EqualTo(todaysDate));

			var todaysUserIngredients = (from ui in expectedUserIngredients where ui.Date == todaysDate select ui);
			var tomorrowsUserIngredients = (from ui in expectedUserIngredients where ui.Date == todaysDate.AddDays(1) select ui);

			Assert.That(returnedValues.ElementAt(0).Value, Is.EqualTo(todaysUserIngredients.Sum(x => x.Ingredient.EnergyInKcal)));
			Assert.That(returnedValues.ElementAt(1).Value, Is.EqualTo(tomorrowsUserIngredients.Sum(x => x.Ingredient.EnergyInKcal)));
		}

		[Test]
		public void shouldGetUserIngredients() {
			var userIngredients = GetExpectedUserIngredients(todaysDate);

			var userIngredientRepositoryMock = new Mock<IUserIngredientRepository>(MockBehavior.Strict);
			userIngredientRepositoryMock.Setup(x => x.GetUserIngredientsByUser(It.IsAny<int>(), todaysDate, todaysDate.AddDays(1))).Returns(userIngredients);

			var userIngredientssBusinessLogic = new UserIngredientBusinessLogic(userIngredientRepositoryMock.Object, null);
			var returnedUserIngredients = userIngredientssBusinessLogic.GetUserIngredients(new User("myUser"), todaysDate);

			userIngredientRepositoryMock.VerifyAll();

			Assert.That(returnedUserIngredients, Is.SameAs(userIngredients));
		}

		[Test]
		public void shouldNotBeAbleToAddAnIngredientThatDoesNotExistInTheDatabase() {
			const int measure = 100;
			const string ingredientName = "aaaabbbbbcccccddddd";

			var userIngredientRepositoryMock = new Mock<IUserIngredientRepository>(MockBehavior.Strict);
			var ingredientRepositoryMock = new Mock<IIngredientRepository>(MockBehavior.Strict);
			ingredientRepositoryMock.Setup(x => x.Get(ingredientName)).Returns((Ingredient) null);

			var userIngredientLogic = new UserIngredientBusinessLogic(userIngredientRepositoryMock.Object, ingredientRepositoryMock.Object);
			Assert.Throws<NoIngredientFoundException>(() => userIngredientLogic.AddUserIngredient(new User(), ingredientName, measure, DateTime.Now));

			userIngredientRepositoryMock.VerifyAll();
			ingredientRepositoryMock.VerifyAll();
		}

		[Test]
		public void shouldThrowWhenDateNotInSqlRange() {
			var userRepositoryMock = new Mock<IUserIngredientRepository>(MockBehavior.Strict);
			var ingredientRepositoryMock = new Mock<IIngredientRepository>(MockBehavior.Strict);
			Assert.Throws<InvalidDateException>(() => new UserIngredientBusinessLogic(userRepositoryMock.Object, ingredientRepositoryMock.Object).GetUserIngredients(new User("myUser"), new DateTime(1234)));
			Assert.Throws<InvalidDateException>(() => new UserIngredientBusinessLogic(userRepositoryMock.Object, ingredientRepositoryMock.Object).GetUserIngredients(new User("myUser"), DateTime.MaxValue));
		}
	}
}