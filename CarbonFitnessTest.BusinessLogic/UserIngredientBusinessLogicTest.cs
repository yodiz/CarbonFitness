using System;
using System.Linq;
using CarbonFitness.BusinessLogic.Exceptions;
using CarbonFitness.BusinessLogic.Implementation;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;
using Moq;
using NUnit.Framework;

namespace CarbonFitnessTest.BusinessLogic
{
	[TestFixture]
	public class UserIngredientBusinessLogicTest
	{
        [Test]
        public void shouldNotBeAbleToAddAnIngredientThatDoesNotExistInTheDatabase() {
            const int measure = 100;
            const string ingredientName = "aaaabbbbbcccccddddd";
 
            var userIngredientRepositoryMock = new Mock<IUserIngredientRepository>(MockBehavior.Strict);
            var ingredientRepositoryMock = new Mock<IIngredientRepository>(MockBehavior.Strict);
            ingredientRepositoryMock.Setup(x => x.Get(ingredientName)).Returns((Ingredient) null);
            
            var userIngredientLogic = new UserIngredientBusinessLogic(userIngredientRepositoryMock.Object, ingredientRepositoryMock.Object);
            Assert.Throws<NoIngredientFoundException>(() => userIngredientLogic.AddUserIngredient(new User(),ingredientName, measure,DateTime.Now));

            userIngredientRepositoryMock.VerifyAll();
            ingredientRepositoryMock.VerifyAll();
        }

		[Test]
		public void shouldAddUserIngredient() {
			var measure = 100;
			var ingredientName = "Pannbiff";
		    var todaysDate = DateTime.Now;
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
        public void shouldGetUserIngredients() {
            var now = DateTime.Now;
            var userIngredients = new UserIngredient[2];
            userIngredients[0] = new UserIngredient { Ingredient = new Ingredient { Name = "Pannbiff" }, Measure = 100, Date = now };
            userIngredients[1] = new UserIngredient {Ingredient = new Ingredient {Name = "Lök"}, Measure = 150, Date = now};

            var userIngredientRepositoryMock = new Mock<IUserIngredientRepository>(MockBehavior.Strict);
            userIngredientRepositoryMock.Setup(x => x.GetUserIngredientsFromUserId(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(userIngredients);

            var userIngredientssBusinessLogic = new UserIngredientBusinessLogic(userIngredientRepositoryMock.Object, null);
            var returnedUserIngredients = userIngredientssBusinessLogic.GetUserIngredients(new User("myUser"), now);

            userIngredientRepositoryMock.Verify();
            Assert.That(returnedUserIngredients.Count(), Is.GreaterThan(1));
            Assert.That(returnedUserIngredients[0].Ingredient.Name, Is.EqualTo("Pannbiff"));
            Assert.That(returnedUserIngredients[1].Ingredient.Name, Is.EqualTo("Lök"));
            Assert.That(returnedUserIngredients[1].Date, Is.EqualTo(now));
            Assert.That(returnedUserIngredients.Count(), Is.EqualTo(2));
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
