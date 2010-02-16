using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarbonFitness.BusinessLogic;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;
using Moq;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace CarbonFitnessTest.BusinessLogic
{
	[TestFixture]
	public class UserIngredientBusinessLogicTest
	{
		[Test]
		public void shouldAddUserIngredient() {
			var measure = 100;
			var ingredientName = "Pannbiff";
			var ingredientMock = new Mock<Ingredient>();
			ingredientMock.Setup(x => x.Id).Returns(1);
			ingredientMock.Setup(x => x.Name).Returns(ingredientName);

			var userIngredientRepositoryMock = new Mock<IUserIngredientRepository>();
			var ingredientRepositoryMock = new Mock<IIngredientRepository>();
			userIngredientRepositoryMock.Setup(x => x.SaveOrUpdate(It.Is<UserIngredient>(y => y.Ingredient.Name == ingredientName && y.Ingredient.Id > 0 && y.Measure == measure))).Returns(new UserIngredient());
			ingredientRepositoryMock.Setup(x => x.Get(ingredientName)).Returns(ingredientMock.Object);

			var userIngredientLogic = new UserIngredientBusinessLogic(userIngredientRepositoryMock.Object, ingredientRepositoryMock.Object);
			userIngredientLogic.AddUserIngredient(new User(), ingredientName, measure);

			userIngredientRepositoryMock.VerifyAll();
			ingredientRepositoryMock.VerifyAll();
		}

        [Test]
        public void shouldGetUserIngredients() {
            var userIngredients = new UserIngredient[2];
            userIngredients[0] = new UserIngredient {Ingredient = new Ingredient {Name = "Pannbiff"}, Measure = 100};
            userIngredients[1] = new UserIngredient {Ingredient = new Ingredient {Name = "Lök"}, Measure = 150};

            var userIngredientRepositoryMock = new Mock<IUserIngredientRepository>();
            userIngredientRepositoryMock.Setup(x => x.GetUserIngredientsFromUserId(It.IsAny<int>())).Returns(userIngredients);

            var userIngredientssBusinessLogic = new UserIngredientBusinessLogic(userIngredientRepositoryMock.Object, null);
            var returnedUserIngredients = userIngredientssBusinessLogic.GetUserIngredients(new User("myUser"));

            Assert.That(returnedUserIngredients[0].Ingredient.Name, Is.EqualTo("Pannbiff"));
            Assert.That(returnedUserIngredients[1].Ingredient.Name, Is.EqualTo("Lök"));
            Assert.That(returnedUserIngredients.Count(), Is.EqualTo(2));
        }
	}
}
