using System;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;
using NUnit.Framework;
using SharpArch.Data.NHibernate;
using SharpArch.Testing.NUnit.NHibernate;

namespace CarbonFitnessTest.DataLayer.Repository {
	[TestFixture]
	public class UserIngredientRepositoryTest : RepositoryTestsBase {
		private User user; 
		private User user2;
		private DateTime now = DateTime.Now.Date;
		private Ingredient ingredient;

		protected override void LoadTestData() {
			var ingredientRepository = new IngredientRepository();
			ingredient = ingredientRepository.SaveOrUpdate(new Ingredient {Name = "Pannbiff"});

			user = new UserRepository().Save(new User { Username = "test" });
			user2 = new UserRepository().Save(new User { Username = "test2" });
			var user3 = new UserRepository().Save(new User { Username = "test3" });
			NHibernateSession.Current.Flush();

			var userIngredientRepository = new UserIngredientRepository();

			userIngredientRepository.SaveOrUpdate(new UserIngredient {Ingredient = ingredient, Measure = 10, User = user, Date = now});
			userIngredientRepository.SaveOrUpdate(new UserIngredient {Ingredient = ingredient, Measure = 100, User = user, Date = now});
			userIngredientRepository.SaveOrUpdate(new UserIngredient {Ingredient = ingredient, Measure = 200, User = user, Date = now.AddDays(-1)});

			userIngredientRepository.SaveOrUpdate(new UserIngredient { Ingredient = ingredient, Measure = 10, User = user2, Date = now });
			userIngredientRepository.SaveOrUpdate(new UserIngredient { Ingredient = ingredient, Measure = 100, User = user3, Date = now });
			userIngredientRepository.SaveOrUpdate(new UserIngredient { Ingredient = ingredient, Measure = 200, User = user2, Date = now.AddDays(-1) });

			NHibernateSession.Current.Flush();
		}

		[Test]
		public void shouldGetUserIngredientsFromUserId() {
			var repository = new UserIngredientRepository();

			var userIngredients = repository.GetUserIngredientsByUser(user.Id, now, now.AddDays(1));

			Assert.That(userIngredients, Is.Not.Null);
			Assert.That(userIngredients.Length, Is.EqualTo(2));
			Assert.That(userIngredients[0].Ingredient, Is.Not.Null);
			Assert.That(userIngredients[0].Ingredient.Name, Is.EqualTo("Pannbiff"));
			Assert.That(userIngredients[0].Date, Is.EqualTo(now));
			Assert.That(userIngredients[0].User.Id, Is.EqualTo(user.Id));
		}

		[Test]
		public void shouldSaveUserIngredient() {
			var repository = new UserIngredientRepository();
			var userIngredient = repository.SaveOrUpdate(new UserIngredient {Ingredient = ingredient, Measure = 100, User = user, Date = now.AddDays(-1)});
			Assert.That(userIngredient.Id, Is.GreaterThan(0));
		}
	}
}