using System;
using System.Linq.Expressions;
using System.Web.Mvc;
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
		public void shouldBeAbleToAddFood(){
		    
            //InputFoodModel m = new InputFoodModel();
            string ingredient = GetFieldNameOnModel<InputFoodModel>(m => m.Ingredient);
            string measure = GetFieldNameOnModel<InputFoodModel>(m => m.Measure);
			var pannbiff = "Pannbiff";
			createIngredient(pannbiff);

			browser.GoTo(Url);

            browser.TextField(Find.ByName(ingredient)).TypeText(pannbiff);
			browser.TextField(Find.ByName(measure)).TypeText("110");
			browser.Button(Find.ByValue(FoodConstant.Submit)).Click();

			bool foodNameExsistsOnPage = browser.Text.Contains(pannbiff);
			bool foodMessaureExsistsOnPage = browser.Text.Contains("110");

			Assert.That(foodNameExsistsOnPage, Is.True);
			Assert.That(foodMessaureExsistsOnPage, Is.True);
		}

	    public void createIngredient(string name)
		{
			new IngredientRepository().SaveOrUpdate(new Ingredient {Name = name});
		}
	}
}