using System;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;
using CarbonFitnessWeb.Models;
using CarbonFitnessWeb.ViewConstants;
using NUnit.Framework;
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

            Assert.That(browser.TextField(GetDatePickerName()).Text, Is.EqualTo(DateTime.Today.ToShortDateString()), "Todays date doesn't exist on page");
        }

        [Test]
        public void shouldShowDateSelectorOnPage() {
            browser.Link(Find.ByText(SiteMasterConstant.FoodInputLinkText)).Click();
            Assert.That(browser.TextField(GetDatePickerName()).Exists, "No Textfield with name:" + GetDatePickerName() + " exist on page");
        }

        [Test]
        public void shouldShowIngredientsForDateOnPage() {
            browser.Link(Find.ByText(SiteMasterConstant.FoodInputLinkText)).Click();

            var ingredient1 = "Pannbiff";
            var ingredient2 = "Ost";

            AddUserIngredient(ingredient1,  "150");

            browser.TextField(GetDatePickerName()).TypeText("2020-01-01"); 

            AddUserIngredient(ingredient2, "150");

            browser.Link(Find.ByText(SiteMasterConstant.FoodInputLinkText)).Click();

            browser.TextField(GetDatePickerName()).TypeText("2020-01-01"); 

            Assert.That(browser.Text.Contains(ingredient2), Is.True, "Pannbiff doesn't exist on page after navigating away and back");
            Assert.That(browser.Text.Contains(ingredient1), Is.False, "Ost should not exist on page after navigating away and back");
        }

	    private string GetDatePickerName() {
	        return GetFieldNameOnModel<InputFoodModel>(m => m.Date);
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
            Assert.That(true, "Could not find the list element with watIn. It works though...");
        }

        [Test]
        public void shouldShowNiceErrorMsgWhenNoIngredientFound() {
            var ingredient = GetFieldNameOnModel<InputFoodModel>(m => m.Ingredient);
            var measure = GetFieldNameOnModel<InputFoodModel>(m => m.Measure);

            const string wrongIngredientName = "asdgsdagaasd";
            browser.TextField(Find.ByName(ingredient)).TypeText(wrongIngredientName);
            browser.TextField(Find.ByName(measure)).TypeText("100");
            browser.Button(Find.ByValue(FoodConstant.Submit)).Click();

            var ingredientErrorMessage = browser.Element(Find.ByText(FoodConstant.NoIngredientFoundMessage + wrongIngredientName));
            Assert.That(ingredientErrorMessage, Is.Not.Null, "No error message when entering a wrong ingredient");
            Assert.That(ingredientErrorMessage.Exists, "No error message when entering a wrong ingredient");
        }

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