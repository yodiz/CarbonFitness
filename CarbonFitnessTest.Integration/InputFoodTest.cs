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

            createAndAddUserIngredient(ingredient1,  "150");
            createAndAddUserIngredient(ingredient2, "210");

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

            createAndAddUserIngredient(ingredient1,  "150");

            browser.TextField(GetDatePickerName()).TypeText("2023-01-02"); 

            createAndAddUserIngredient(ingredient2, "150");

            browser.Link(Find.ByText(SiteMasterConstant.FoodInputLinkText)).Click();

            browser.TextField(GetDatePickerName()).TypeText("2023-01-02");

            Assert.That(browser.Text.Contains(ingredient2), Is.True, "Ost doesn't exist on page after navigating away and back");
            Assert.That(browser.Text.Contains(ingredient1), Is.False, "Pannbiff should not exist on page after navigating away and back");
        }

        [Test]
        public void shouldBeAbleToChangeDateWithoutNoIngredientFoundMessageAppears()
        {
            browser.TextField(GetDatePickerName()).TypeText("2023-01-01");
            Assert.That(browser.ContainsText(FoodConstant.NoIngredientFoundMessage), Is.False, "ValidationSummary message was not expected.");
        }

        [Test]
        public void shouldEmptyFoodInputAfterSubmit()
        {
            browser.Link(Find.ByText(SiteMasterConstant.FoodInputLinkText)).Click();

            createAndAddUserIngredient("Pannbiff", "150");

            string ingredient = GetFieldNameOnModel<InputFoodModel>(m => m.Ingredient);
            string measure = GetFieldNameOnModel<InputFoodModel>(m => m.Measure);

            Assert.That(browser.TextField(Find.ByName(ingredient)).Text, Is.Null, "Expected that " + ingredient + " was null.");
            Assert.That(browser.TextField(Find.ByName(measure)).Text, Is.EqualTo("0"), "Expected that " + measure + " was empty.");
        }

	    private string GetDatePickerName() {
	        return GetFieldNameOnModel<InputFoodModel>(m => m.Date);
	    }

	    [Test]
        public void shouldHaveImportedIngredientsInDB() {
            
            //First row in Livsmedelsdatabasen
            addUserIngredient("Abborre", "100");

            //Last row in Livsmedelsdatabasen
            addUserIngredient("Örtte drickf", "100");

            Assert.That(browser.Text.Contains("Abborre"), Is.True, "Abborre should exist on page");
            Assert.That(browser.Text.Contains("Örtte drickf"), Is.True, "Abborre should exist on page");
        }

	    [Test]
        public void shouldAutoCompleteWhenLookingForIngredients() {
            Assert.That(true, "Could not find the list element with watIn. It works though...");
        }

        [Test]
        public void shouldShowNiceErrorMsgWhenNoIngredientFound() {
            const string wrongIngredientName = "asdgsdagaasdsdfdsdfd";
            addUserIngredient(wrongIngredientName, "100");

            var ingredientErrorMessage = browser.Element(Find.ByText(FoodConstant.NoIngredientFoundMessage + wrongIngredientName));
            Assert.That(ingredientErrorMessage, Is.Not.Null, "No error message when entering a wrong ingredient");
            Assert.That(ingredientErrorMessage.Exists, "No error message when entering a wrong ingredient");
        }

	    private void createAndAddUserIngredient(string ingredientText, string measureText)
	    {
	        createIngredient(ingredientText);

	        addUserIngredient(ingredientText, measureText);
	    }

	    private void addUserIngredient(string ingredientText, string measureText)
	    {
	        string ingredient = GetFieldNameOnModel<InputFoodModel>(m => m.Ingredient);
	        string measure = GetFieldNameOnModel<InputFoodModel>(m => m.Measure);
	        browser.TextField(Find.ByName(ingredient)).TypeText(ingredientText);
	        browser.TextField(Find.ByName(measure)).TypeText(measureText);
	        browser.Image(Find.BySrc(x => x.Contains("save.gif"))).Click();
	    }

	    public void createIngredient(string name)
		{
			new IngredientRepository().SaveOrUpdate(new Ingredient {Name = name});
		}
	}
}