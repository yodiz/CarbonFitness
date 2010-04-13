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

            var ingredientSetup = new IngredientSetup();

			var userIngredients = new UserIngredient[4];
            userIngredients[0] = new UserIngredient { Ingredient = ingredientSetup.GetNewIngredient("Pannbiff", NutrientEntity.EnergyInKcal, 28, 100), Measure = 100, Date = date };
			userIngredients[1] = new UserIngredient {Ingredient = ingredientSetup.GetNewIngredient("Lök", NutrientEntity.EnergyInKcal, 25, 100), Measure = 150, Date = date};

			userIngredients[2] = new UserIngredient {Ingredient = ingredientSetup.GetNewIngredient("Lök", NutrientEntity.EnergyInKcal, 25, 100), Measure = 150, Date = date.AddDays(-100)};
			userIngredients[3] = new UserIngredient {Ingredient =  ingredientSetup.GetNewIngredient("Lök", NutrientEntity.EnergyInKcal, 27, 100), Measure = 150, Date = date.AddDays(-100)};
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

			var userIngredientLogic = new UserIngredientBusinessLogic(userIngredientRepositoryMock.Object, ingredientRepositoryMock.Object, null);
			userIngredientLogic.AddUserIngredient(new User(), ingredientName, measure, todaysDate);

			userIngredientRepositoryMock.VerifyAll();
			ingredientRepositoryMock.VerifyAll();
		}

		[Test]
		public void shouldGetCalorieHistory() {
			var expectedUserIngredients = GetExpectedUserIngredients(todaysDate);
			var repositoryMock = new Mock<IUserIngredientRepository>();
            var nutrientRepositoryMock = new Mock<INutrientRepository>();
            var nutrientMock = new Mock<Nutrient>();
		    var expectedNutrientId = 3;
		    nutrientMock.Setup(x => x.Id).Returns(expectedNutrientId);

			var firstDate = todaysDate.AddDays(-100).Date;
			var lastDate = todaysDate;
            nutrientRepositoryMock.Setup(x => x.GetByName(NutrientEntity.EnergyInKcal.ToString())).Returns(nutrientMock.Object);
			repositoryMock.Setup(x => x.GetUserIngredientsByUser(It.IsAny<int>(), firstDate, lastDate.AddDays(1))).Returns(expectedUserIngredients);
            var returnedValues = new UserIngredientBusinessLogic(repositoryMock.Object, null, nutrientRepositoryMock.Object).GetNutrientHistory(NutrientEntity.EnergyInKcal, new User());

			repositoryMock.VerifyAll();
            nutrientRepositoryMock.VerifyAll();

			var todaysUserIngredients = (from ui in expectedUserIngredients where ui.Date == todaysDate select ui);
			var oldUserIngredients = (from ui in expectedUserIngredients where ui.Date == todaysDate.AddDays(-100) select ui);

            Assert.That(returnedValues.Id, Is.EqualTo(expectedNutrientId));

			Assert.That(returnedValues.GetValue(firstDate).Value, Is.EqualTo(oldUserIngredients.Sum(x => x.GetActualCalorieCount(y => y.GetNutrient(NutrientEntity.EnergyInKcal).Value))));
			Assert.That(returnedValues.GetValue(lastDate).Value, Is.EqualTo(todaysUserIngredients.Sum(x => x.GetActualCalorieCount(y => y.GetNutrient(NutrientEntity.EnergyInKcal).Value))));
		}

        [Test]
        public void shouldGetZeroInNutrientIngredientValueWhenNoNutrientIngredient() {
            decimal nutrientIngredientValue = new UserIngredientBusinessLogic(null, null, null).getNutrientIngredientValue(null as IngredientNutrient);
            Assert.That(nutrientIngredientValue, Is.EqualTo(0));
        }

	    [Test]
		public void shouldGetUserIngredients() {
			var userIngredients = GetExpectedUserIngredients(todaysDate);

			var userIngredientRepositoryMock = new Mock<IUserIngredientRepository>(MockBehavior.Strict);
			userIngredientRepositoryMock.Setup(x => x.GetUserIngredientsByUser(It.IsAny<int>(), todaysDate, todaysDate.AddDays(1))).Returns(userIngredients);

            var userIngredientssBusinessLogic = new UserIngredientBusinessLogic(userIngredientRepositoryMock.Object, null, null);
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

            var userIngredientLogic = new UserIngredientBusinessLogic(userIngredientRepositoryMock.Object, ingredientRepositoryMock.Object, null);
			Assert.Throws<NoIngredientFoundException>(() => userIngredientLogic.AddUserIngredient(new User(), ingredientName, measure, DateTime.Now));

			userIngredientRepositoryMock.VerifyAll();
			ingredientRepositoryMock.VerifyAll();
		}

		[Test]
		public void shouldThrowWhenDateNotInSqlRange() {
			var userRepositoryMock = new Mock<IUserIngredientRepository>(MockBehavior.Strict);
			var ingredientRepositoryMock = new Mock<IIngredientRepository>(MockBehavior.Strict);
            Assert.Throws<InvalidDateException>(() => new UserIngredientBusinessLogic(userRepositoryMock.Object, ingredientRepositoryMock.Object, null).GetUserIngredients(new User("myUser"), new DateTime(1234)));
            Assert.Throws<InvalidDateException>(() => new UserIngredientBusinessLogic(userRepositoryMock.Object, ingredientRepositoryMock.Object, null).GetUserIngredients(new User("myUser"), DateTime.MaxValue));
		}
	}
}