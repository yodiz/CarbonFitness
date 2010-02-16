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
        public void shouldShowTodaysDateOnPage() {
            browser.Link(Find.ByText(SiteMasterConstant.FoodInputLinkText)).Click();

            Assert.That(browser.Text.Contains(DateTime.Today.ToShortDateString()), Is.True, "Todays date doesn't exist on page");
        }
        //[Test]
        //public void shouldShowIngredientsForDateOnPage() {


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