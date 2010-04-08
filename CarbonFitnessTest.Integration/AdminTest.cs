using System;
using CarbonFitness.App.Web.Models;
using CarbonFitness.DataLayer.Repository;
using NUnit.Framework;
using SharpArch.Data.NHibernate;
using WatiN.Core;

namespace CarbonFitnessTest.Integration {
	[TestFixture]
	public class AdminTest : IntegrationBaseTest {
		private IngredientRepository ingredientRepository;
		private TextField FilePathTextField { get { return Browser.TextField(GetFieldNameOnModel<InputDbInitModel>(m => m.ImportFilePath));  } }
		public override string Url { get { return getUrl("Admin", "DBInit"); } }

		private void clearIngredients() {
			var userIngredientRepository = new UserIngredientRepository();
			var userIngredients = new UserIngredientRepository().GetAll();
			foreach (var userIngredient in userIngredients) {
				userIngredientRepository.Delete(userIngredient);
			}

			ingredientRepository = new IngredientRepository();
			var ingredients = ingredientRepository.GetAll();
			foreach (var ingredient in ingredients) {
				ingredientRepository.Delete(ingredient);
			}

			NHibernateSession.Current.Flush();
		}

		[Test]
        [Ignore] // Super slow.
		public void shouldUpdateDB() {
            Browser.GoTo(Url);
		    try {
                clearIngredients(); // Might throw if no database exists...
            }catch (NHibernate.ADOException) { }
			
			
            Assert.That(FilePathTextField.Text, Is.EqualTo("~/App_data/Ingredients.csv"));
			
			Browser.Button(AdminConstant.RefreshDatabase).Click();

			Assert.That(ingredientRepository.Get("Abborre"), Is.Not.Null);
            Assert.That(new NutrientRepository().Get(2), Is.Not.Null, "Should have nutritients in db...");
		}
	}
}