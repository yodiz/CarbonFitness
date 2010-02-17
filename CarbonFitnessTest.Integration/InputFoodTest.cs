using System;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;
using CarbonFitnessWeb.Models;
using CarbonFitnessWeb.ViewConstants;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using WatiN.Core;

namespace CarbonFitnessTest.Integration
{
	[TestFixture]
	public class InputFoodTest : IntegrationBaseTest
	{
		public override string Url { get { return baseUrl + "/Food/Input"; } }

		[TestFixtureSetUp]
		public override void TestFixtureSetUp()
		{
			base.TestFixtureSetUp();

			var createUserTest = new CreateUserTest(browser);
			createUserTest.createUser();
		}

		[Test]
		public void shouldShowViewAfterNewFoodInput()
		{
			browser.GoTo(Url);
			bool addFoodHeaderExists = browser.ContainsText(FoodConstant.FoodInputTitle);
			Assert.That(addFoodHeaderExists);
		}

	    [Test]
        public void shouldShowIngredientsForCurrentUser() {
            var ingredient1 = "Pannbiff";
            var ingredient2 = "Lök";

            AddUserIngredient(ingredient1,  "150");
            AddUserIngredient(ingredient2, "210");

            Assert.That(browser.Text.Contains(ingredient1), Is.True, "Pannbiff doesn't exist on page");
            Assert.That(browser.Text.Contains(ingredient2), Is.True, "Lök doesn't exist on page");

            browser.Link(Find.ByText(SiteMasterConstant.FoodInputLinkText)).Click();

            Assert.That(browser.Text.Contains(ingredient1), Is.True, "Pannbiff doesn't exist on page after navigating away and back");
            Assert.That(browser.Text.Contains(ingredient2), Is.True, "Lök doesn't exist on page after navigating away and back");
        }

        [Test]
        public void shouldShowTodaysDateInDateSelector() {
            browser.Link(Find.ByText(SiteMasterConstant.FoodInputLinkText)).Click();

            Assert.That(browser.TextField(FoodConstant.UserIngredientDate).Text, Is.EqualTo(DateTime.Today.ToShortDateString()), "Todays date doesn't exist on page");
        }

        [Test]
        public void shouldShowDateSelectorOnPage() {
            browser.Link(Find.ByText(SiteMasterConstant.FoodInputLinkText)).Click();
            Assert.That(browser.TextField(FoodConstant.UserIngredientDate).Exists, "No Textfield with name:" + FoodConstant.UserIngredientDate + " exist on page");
        }

        [Test]
        public void shouldShowIngredientsForDateOnPage() {
            string datePickerName = GetFieldNameOnModel<InputFoodModel>(m => m.Date);
            browser.Link(Find.ByText(SiteMasterConstant.FoodInputLinkText)).Click();

            var ingredient1 = "Pannbiff";
            var ingredient2 = "Ost";

            AddUserIngredient(ingredient1,  "150");
            browser.TextField(datePickerName).TypeText("2020-01-01"); 

            AddUserIngredient(ingredient2, "150");

            browser.Link(Find.ByText(SiteMasterConstant.FoodInputLinkText)).Click();

            Assert.That(browser.Text.Contains(ingredient1), Is.True, "Pannbiff doesn't exist on page after navigating away and back");
            Assert.That(browser.Text.Contains(ingredient2), Is.False, "Ost should not exist on page after navigating away and back");
        }

        [Test]
        public void shouldHaveImportedIngredientsInDB() {
            string ingredient = GetFieldNameOnModel<InputFoodModel>(m => m.Ingredient);
            string measure = GetFieldNameOnModel<InputFoodModel>(m => m.Measure);
            //First row in Livsmedelsdatabasen
            browser.TextField(Find.ByName(ingredient)).TypeText("Abborre");
            browser.TextField(Find.ByName(measure)).TypeText("100");
            browser.Button(Find.ByValue(FoodConstant.Submit)).Click();

            //Last row in Livsmedelsdatabasen
            browser.TextField(Find.ByName(ingredient)).TypeText("Örtte drickf");
            browser.TextField(Find.ByName(measure)).TypeText("100");
            browser.Button(Find.ByValue(FoodConstant.Submit)).Click();

            Assert.That(browser.Text.Contains("Abborre"), Is.True, "Abborre should exist on page");
            Assert.That(browser.Text.Contains("Örtte drickf"), Is.True, "Abborre should exist on page");
        }

        [Test]
        public void shouldAutoCompleteWhenLookingForIngredients() {
            string ingredient = GetFieldNameOnModel<InputFoodModel>(m => m.Ingredient);
            string measure = GetFieldNameOnModel<InputFoodModel>(m => m.Measure);
            
            browser.TextField(Find.ByName(ingredient)).TypeText("Äggak");
            browser.TextField(Find.ByName(measure)).TypeText("100");
            browser.Button(Find.ByValue(FoodConstant.Submit)).Click();

            Assert.That(browser.Text.Contains("Äggakaka"), Is.True, "Abborre should exist on page");

        }

        //[Test]
        //public void shouldThrowIfNoIngredientFoundInDb() {
        //    string ingredient = GetFieldNameOnModel<InputFoodModel>(m => m.Ingredient);
        //    string measure = GetFieldNameOnModel<InputFoodModel>(m => m.Measure);
        //    //First row in Livsmedelsdatabasen
        //    browser.TextField(Find.ByName(ingredient)).TypeText("asdgsdagaasd");
        //    browser.TextField(Find.ByName(measure)).TypeText("100");
        //    browser.Button(Find.ByValue(FoodConstant.Submit)).Click();

        //}



	    private void AddUserIngredient(string ingredientText, string measureText)
	    {
            createIngredient(ingredientText);

            string ingredient = GetFieldNameOnModel<InputFoodModel>(m => m.Ingredient);
            string measure = GetFieldNameOnModel<InputFoodModel>(m => m.Measure);
            browser.TextField(Find.ByName(ingredient)).TypeText(ingredientText);
            browser.TextField(Find.ByName(measure)).TypeText(measureText);
	        browser.Button(Find.ByValue(FoodConstant.Submit)).Click();
	    }

	    public void createIngredient(string name)
		{
			new IngredientRepository().SaveOrUpdate(new Ingredient {Name = name});
		}
	}
}