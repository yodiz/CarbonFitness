using System;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;
using NUnit.Framework;
using SharpArch.Testing.NUnit.NHibernate;

namespace CarbonFitnessTest.DataLayer.Repository
{
	[TestFixture]
	public class UserIngredientRepositoryTest : RepositoryTestsBase
	{
	    private User _user;
	    private DateTime _now = DateTime.Now;
	    private Ingredient _ingredient;

	    protected override void LoadTestData()
		{
		    var ingredientRepository = new IngredientRepository();
		    _ingredient = ingredientRepository.SaveOrUpdate(new Ingredient {Name = "Pannbiff"});

            _user = new UserRepository().SaveOrUpdate(new User{Username = "test"});
            
            var userIngredientRepository = new UserIngredientRepository();
            userIngredientRepository.SaveOrUpdate(new UserIngredient { Ingredient = _ingredient, Measure = 10, User = _user, Date = _now});
            userIngredientRepository.SaveOrUpdate(new UserIngredient { Ingredient = _ingredient, Measure = 100, User = _user, Date = _now});
            userIngredientRepository.SaveOrUpdate(new UserIngredient { Ingredient = _ingredient, Measure = 200, User = _user, Date = _now.AddDays(-1) });
		}

		[Test]
		public void shouldSaveUserIngredient() {
			var repository = new UserIngredientRepository();
            UserIngredient userIngredient = repository.SaveOrUpdate(new UserIngredient { Ingredient = _ingredient, Measure = 100, User = _user, Date = _now.AddDays(-1) });
			Assert.That(userIngredient.Id, Is.GreaterThan(0));
		}

        [Test]
        public void shouldGetUserIngredientsFromUserId() {
            var repository = new UserIngredientRepository();

            UserIngredient[] userIngredients = repository.GetUserIngredientsFromUserId(_user.Id, _now, _now.AddDays(1));

            Assert.That(userIngredients, Is.Not.Null);
            Assert.That(userIngredients.Length, Is.EqualTo(2));
            Assert.That(userIngredients[0].Ingredient, Is.Not.Null);
            Assert.That(userIngredients[0].Ingredient.Name, Is.EqualTo("Pannbiff"));
            Assert.That(userIngredients[0].Date, Is.EqualTo(_now));
        }
	}
}