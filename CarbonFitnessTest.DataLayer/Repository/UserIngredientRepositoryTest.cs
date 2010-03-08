using System;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;
using NUnit.Framework;
using SharpArch.Testing.NUnit.NHibernate;

namespace CarbonFitnessTest.DataLayer.Repository {
	[TestFixture]
	public class UserIngredientRepositoryTest : RepositoryTestsBase {
		private User user;
		private DateTime now = DateTime.Now;
		private Ingredient ingredient;

		protected override void LoadTestData() {
			var ingredientRepository = new IngredientRepository();
			ingredient = ingredientRepository.SaveOrUpdate(new Ingredient {Name = "Pannbiff"});

			user = new UserRepository().SaveOrUpdate(new User {Username = "test"});

			var userIngredientRepository = new UserIngredientRepository();
			userIngredientRepository.SaveOrUpdate(new UserIngredient {Ingredient = ingredient, Measure = 10, User = user, Date = now});
			userIngredientRepository.SaveOrUpdate(new UserIngredient {Ingredient = ingredient, Measure = 100, User = user, Date = now});
			userIngredientRepository.SaveOrUpdate(new UserIngredient {Ingredient = ingredient, Measure = 200, User = user, Date = now.AddDays(-1)});
		}

		[Test]
		public void shouldGetUserIngredientsFromUserId() {
			var repository = new UserIngredientRepository();

			var userIngredients = repository.GetUserIngredientsFromUserId(user.Id, now, now.AddDays(1));

			Assert.That(userIngredients, Is.Not.Null);
			Assert.That(userIngredients.Length, Is.EqualTo(2));
			Assert.That(userIngredients[0].Ingredient, Is.Not.Null);
			Assert.That(userIngredients[0].Ingredient.Name, Is.EqualTo("Pannbiff"));
			Assert.That(userIngredients[0].Date, Is.EqualTo(now));
		}

		[Test]
		public void shouldSaveUserIngredient() {
			var repository = new UserIngredientRepository();
			var userIngredient = repository.SaveOrUpdate(new UserIngredient {Ingredient = ingredient, Measure = 100, User = user, Date = now.AddDays(-1)});
			Assert.That(userIngredient.Id, Is.GreaterThan(0));
		}
	}
}