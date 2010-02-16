using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using SharpArch.Testing.NUnit.NHibernate;

namespace CarbonFitnessTest.DataLayer.Repository
{
	[TestFixture]
	public class UserIngredientRepositoryTest : RepositoryTestsBase
	{
	    private User _user;

	    protected override void LoadTestData()
		{
		    var ingredientRepository = new IngredientRepository();
		    var ingredient = ingredientRepository.SaveOrUpdate(new Ingredient {Name = "Pannbiff"});

            _user = new UserRepository().SaveOrUpdate(new User{Username = "test"});
            
            var userIngredientRepository = new UserIngredientRepository();
            userIngredientRepository.SaveOrUpdate(new UserIngredient { Ingredient = ingredient, Measure = 10, User = _user});
            userIngredientRepository.SaveOrUpdate(new UserIngredient { Ingredient = ingredient, Measure = 100, User = _user });
		}

		[Test]
		public void shouldGetIngredientByName() {
			var repository = new UserIngredientRepository();
			UserIngredient userIngredient = repository.SaveOrUpdate(new UserIngredient { Measure = 10 });
			Assert.That(userIngredient.Id, Is.GreaterThan(0));
		}

        [Test]
        public void shouldGetUserIngredientsFromUserId() {
            var repository = new UserIngredientRepository();

            UserIngredient[] userIngredients = repository.GetUserIngredientsFromUserId(_user.Id);

            Assert.That(userIngredients, Is.Not.Null);
            Assert.That(userIngredients.Length, Is.EqualTo(2));
            Assert.That(userIngredients[0].Ingredient, Is.Not.Null);
            Assert.That(userIngredients[0].Ingredient.Name, Is.EqualTo("Pannbiff"));
        }
	}
}