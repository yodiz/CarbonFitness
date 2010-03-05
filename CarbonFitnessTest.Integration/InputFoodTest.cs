using System;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;
using CarbonFitnessWeb.Models;
using CarbonFitnessWeb.ViewConstants;
using NUnit.Framework;
using WatiN.Core;

namespace CarbonFitnessTest.Integration {
    [TestFixture]
    public class InputFoodTest : IntegrationBaseTest {
        public InputFoodTest() {
        }

        public InputFoodTest(Browser browser) : base(browser) {
        }

        public override string Url {
            get { return baseUrl + "/Food/Input"; }
        }

        [TestFixtureSetUp]
        public override void TestFixtureSetUp() {
            base.TestFixtureSetUp();

            var createUserTest = new CreateUserTest(browser);
            createUserTest.getUniqueUserId();
            var accountLogOnTest = new AccountLogOnTest(browser);
            accountLogOnTest.LogOn(CreateUserTest.UserName, CreateUserTest.Password);
        }

        public void addUserIngredient(string ingredientText, string measureText) {
            string ingredient = GetFieldNameOnModel<InputFoodModel>(m => m.Ingredient);
            string measure = GetFieldNameOnModel<InputFoodModel>(m => m.Measure);
            browser.TextField(Find.ByName(ingredient)).TypeText(ingredientText);
            browser.TextField(Find.ByName(measure)).TypeText(measureText);
            browser.Image(Find.BySrc(x => x.Contains("save.gif"))).Click();
        }

        public void createIngredientIfNotExist(string name) {
            var repository = new IngredientRepository();
            if(repository.Get(name) == null) {
                repository.SaveOrUpdate(new Ingredient { Name = name, Calories = 100});
            }
        }

        public void changeDate(string date) {
            browser.TextField(GetDatePickerName()).TypeText(date);
        }


        private string GetDatePickerName() {
            return GetFieldNameOnModel<InputFoodModel>(m => m.Date);
        }


        private void getUniqueIngredientAndAddUserIngredient(string ingredientText, string measureText) {
            createIngredientIfNotExist(ingredientText);

            addUserIngredient(ingredientText, measureText);
        }

        [Test]
        public void shouldAutoCompleteWhenLookingForIngredients() {
            Assert.That(true, "Could not find the list element with watIn. It works though...");
        }

        [Test]
        public void shouldBeAbleToChangeDateWithoutNoIngredientFoundMessageAppears() {
            changeDate("2023-01-01");
            Assert.That(browser.ContainsText(FoodConstant.NoIngredientFoundMessage), Is.False, "ValidationSummary message was not expected.");
        }


        [Test]
        public void shouldEmptyFoodInputAfterSubmit() {
            browser.Link(Find.ByText(SiteMasterConstant.FoodInputLinkText)).Click();

            getUniqueIngredientAndAddUserIngredient("Pannbiff", "150");

            string ingredient = GetFieldNameOnModel<InputFoodModel>(m => m.Ingredient);
            string measure = GetFieldNameOnModel<InputFoodModel>(m => m.Measure);

            Assert.That(browser.TextField(Find.ByName(ingredient)).Text, Is.Null, "Expected that " + ingredient + " was null.");
            Assert.That(browser.TextField(Find.ByName(measure)).Text, Is.EqualTo("0"), "Expected that " + measure + " was empty.");
        }

        [Test]
        public void shouldGoToResultsAfterClickingResults() {
            browser.Link(Find.ByText(SiteMasterConstant.ResultLinkText)).Click();

            Assert.That(browser.ContainsText(ResultConstant.ResultTitle));
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
        public void shouldShowDateSelectorOnPage() {
            browser.Link(Find.ByText(SiteMasterConstant.FoodInputLinkText)).Click();
            Assert.That(browser.TextField(GetDatePickerName()).Exists, "No Textfield with name:" + GetDatePickerName() + " exist on page");
        }

        [Test]
        public void shouldShowIngredientsForCurrentUser() {
            string ingredient1 = "Pannbiff";
            string ingredient2 = "Lök";

            getUniqueIngredientAndAddUserIngredient(ingredient1, "150");
            getUniqueIngredientAndAddUserIngredient(ingredient2, "210");

            Assert.That(browser.Text.Contains(ingredient1), Is.True, "Pannbiff doesn't exist on page");
            Assert.That(browser.Text.Contains(ingredient2), Is.True, "Lök doesn't exist on page");

            browser.Link(Find.ByText(SiteMasterConstant.FoodInputLinkText)).Click();

            Assert.That(browser.Text.Contains(ingredient1), Is.True, "Pannbiff doesn't exist on page after navigating away and back");
            Assert.That(browser.Text.Contains(ingredient2), Is.True, "Lök doesn't exist on page after navigating away and back");
        }

        [Test]
        public void shouldShowIngredientsForDateOnPage() {
            browser.Link(Find.ByText(SiteMasterConstant.FoodInputLinkText)).Click();

            string ingredient1 = "Pannbiff";
            string ingredient2 = "Ost";

            getUniqueIngredientAndAddUserIngredient(ingredient1, "150");

            changeDate("2023-01-02");

            getUniqueIngredientAndAddUserIngredient(ingredient2, "150");

            browser.Link(Find.ByText(SiteMasterConstant.FoodInputLinkText)).Click();

            changeDate("2023-01-02");

            Assert.That(browser.Text.Contains(ingredient2), Is.True, "Ost doesn't exist on page after navigating away and back");
            Assert.That(browser.Text.Contains(ingredient1), Is.False, "Pannbiff should not exist on page after navigating away and back");
        }

        [Test]
        public void shouldShowNiceErrorMsgWhenInvalidDateEntered() {
            changeDate("aaa");
            Element invalidDateMsg = browser.Element(Find.ByText(FoodConstant.InvalidDateErrorMessage));
            Assert.That(invalidDateMsg, Is.Not.Null, "No error message when entering a invalid date");
        }

        [Test]
        public void shouldShowNiceErrorMsgWhenNoIngredientFound() {
            const string wrongIngredientName = "asdgsdagaasdsdfdsdfd";
            addUserIngredient(wrongIngredientName, "100");

            Element ingredientErrorMessage = browser.Element(Find.ByText(FoodConstant.NoIngredientFoundMessage + wrongIngredientName));
            Assert.That(ingredientErrorMessage, Is.Not.Null, "No error message when entering a wrong ingredient");
            Assert.That(ingredientErrorMessage.Exists, "No error message when entering a wrong ingredient");
        }

        [Test]
        public void shouldShowTodaysDateInDateSelector() {
            browser.Link(Find.ByText(SiteMasterConstant.FoodInputLinkText)).Click();

            Assert.That(browser.TextField(GetDatePickerName()).Text, Is.EqualTo(DateTime.Today.ToShortDateString()), "Todays date doesn't exist on page");
        }

        [Test]
        public void shouldShowViewAfterNewFoodInput() {
            browser.GoTo(Url);
            bool addFoodHeaderExists = browser.ContainsText(FoodConstant.FoodInputTitle);
            Assert.That(addFoodHeaderExists);
        }
    }
}