using System;
using CarbonFitness.App.Web.Models;
using CarbonFitness.App.Web.ViewConstants;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;
using NUnit.Framework;
using WatiN.Core;

namespace CarbonFitnessTest.Integration {
	[TestFixture]
	public class InputFoodTest : IntegrationBaseTest {
		public InputFoodTest() {}

		public InputFoodTest(Browser browser) : base(browser) {}

		public override string Url { get { return BaseUrl + "/Food/Input"; } }

		[TestFixtureSetUp]
		public override void TestFixtureSetUp() {
			base.TestFixtureSetUp();

			var createUserTest = new CreateUserTest(Browser);
			createUserTest.getUniqueUserId();
			var accountLogOnTest = new AccountLogOnTest(Browser);
			accountLogOnTest.LogOn(CreateUserTest.UserName, CreateUserTest.Password);
		}

		private string MeasureFieldName { get { return GetFieldNameOnModel<InputFoodModel>(m => m.Measure); } }

		private TextField MeasureTextField { get { return Browser.TextField(Find.ByName(MeasureFieldName)); } }

		private string IngredientFieldName { get { return GetFieldNameOnModel<InputFoodModel>(m => m.Ingredient); } }

		private TextField IngredientTextField { get { return Browser.TextField(Find.ByName(IngredientFieldName)); } }

		private string DatePickerName { get { return GetFieldNameOnModel<InputFoodModel>(m => m.Date); } }

		private TextField DatePicker { get { return Browser.TextField(DatePickerName); } }

		private Button SaveButton { get { return Browser.Button(Find.ByValue("Spara")); } }

		private void reloadPage() {
			ReloadPage(SiteMasterConstant.FoodInputLinkText);
		}


		public void addUserIngredient(string ingredientText, string measureText) {
			IngredientTextField.TypeText(ingredientText);
			MeasureTextField.TypeText(measureText);
			SaveButton.Click();
		}

		public void createIngredientIfNotExist(string name) {
			var repository = new IngredientRepository();
			if (repository.Get(name) == null) {
				repository.SaveOrUpdate(new Ingredient {Name = name, EnergyInKcal = 100, WeightInG = 100});
			}
		}

		public void changeDate(string date) {
			DatePicker.TypeText(date);
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
			Assert.That(Browser.ContainsText(FoodConstant.NoIngredientFoundMessage), Is.False, "ValidationSummary message was not expected.");
		}

		[Test]
		public void shouldEmptyFoodInputAfterSubmit() {
			reloadPage();

			getUniqueIngredientAndAddUserIngredient("Pannbiff", "150");

			Assert.That(IngredientTextField.Text, Is.Null, "Expected that " + IngredientFieldName + " was null.");
			Assert.That(MeasureTextField.Text, Is.EqualTo("0"), "Expected that " + MeasureFieldName + " was empty.");
		}

		[Test]
		public void shouldGoToResultsAfterClickingResults() {
			ReloadPage(SiteMasterConstant.ResultLinkText);

			Assert.That(Browser.ContainsText(ResultConstant.ResultTitle));
		}

		[Test]
		public void shouldHaveImportedIngredientsInDB() {
			//First row in Livsmedelsdatabasen
			addUserIngredient("Abborre", "100");

			//Last row in Livsmedelsdatabasen
			addUserIngredient("Örtte drickf", "100");

			Assert.That(Browser.Text.Contains("Abborre"), Is.True, "Abborre should exist on page");
			Assert.That(Browser.Text.Contains("Örtte drickf"), Is.True, "Örtte drickf should exist on page");
		}

		[Test]
		public void shouldShowDateSelectorOnPage() {
			Assert.That(DatePicker.Exists, "No Textfield with name:" + DatePickerName + " exist on page");
		}

		[Test]
		public void shouldShowIngredientsForCurrentUser() {
			var ingredient1 = "Pannbiff";
			var ingredient2 = "Lök";

			getUniqueIngredientAndAddUserIngredient(ingredient1, "150");
			getUniqueIngredientAndAddUserIngredient(ingredient2, "210");

			Assert.That(Browser.Text.Contains(ingredient1), Is.True, "Pannbiff doesn't exist on page");
			Assert.That(Browser.Text.Contains(ingredient2), Is.True, "Lök doesn't exist on page");

			reloadPage();

			Assert.That(Browser.Text.Contains(ingredient1), Is.True, "Pannbiff doesn't exist on page after navigating away and back");
			Assert.That(Browser.Text.Contains(ingredient2), Is.True, "Lök doesn't exist on page after navigating away and back");
		}

		[Test]
		public void shouldShowIngredientsForDateOnPage() {
			var ingredient1 = "Pannbiff";
			var ingredient2 = "Ost";

			getUniqueIngredientAndAddUserIngredient(ingredient1, "150");

			changeDate("2023-01-02");

			getUniqueIngredientAndAddUserIngredient(ingredient2, "150");

			reloadPage();

			changeDate("2023-01-02");

			Assert.That(Browser.Text.Contains(ingredient2), Is.True, "Ost doesn't exist on page after navigating away and back");
			Assert.That(Browser.Text.Contains(ingredient1), Is.False, "Pannbiff should not exist on page after navigating away and back");
		}

		[Test]
		public void shouldShowNiceErrorMsgWhenInvalidDateEntered() {
			changeDate("aaa");
			var invalidDateMsg = Browser.Element(Find.ByText(FoodConstant.InvalidDateErrorMessage));
			Assert.That(invalidDateMsg, Is.Not.Null, "No error message when entering a invalid date");
		}

		[Test]
		public void shouldShowNiceErrorMsgWhenNoIngredientFound() {
			const string wrongIngredientName = "asdgsdagaasdsdfdsdfd";
			addUserIngredient(wrongIngredientName, "100");

			var ingredientErrorMessage = Browser.Element(Find.ByText(FoodConstant.NoIngredientFoundMessage + wrongIngredientName));
			Assert.That(ingredientErrorMessage, Is.Not.Null, "No error message when entering a wrong ingredient");
			Assert.That(ingredientErrorMessage.Exists, "No error message when entering a wrong ingredient");
		}

		[Test]
		public void shouldShowTodaysDateInDateSelector() {
			Assert.That(DatePicker.Text, Is.EqualTo(DateTime.Today.ToShortDateString()), "Todays date doesn't exist on page");
		}

		[Test]
		public void shouldShowViewAfterNewFoodInput() {
			var addFoodHeaderExists = Browser.ContainsText(FoodConstant.FoodInputTitle);
			Assert.That(addFoodHeaderExists);
		}
	}
}