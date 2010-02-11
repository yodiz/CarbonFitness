using CarbonFitnessWeb.ViewConstants;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using WatiN.Core;

namespace CarbonFitnessTest.Integration {
	[TestFixture]
	public class InputFoodTest : IntegrationBaseTest {
		public override string Url { get { return baseUrl + "/Food/Input/1"; } }

		[Test]
		public void shouldShowViewAfterNewFoodInput() {
			bool addFoodHeaderExists = browser.ContainsText("Enter food consumed");
			Assert.That(addFoodHeaderExists);
		}
		
		[Test]
		public void shouldBeAbleToAddFood() {
			 browser.TextField(Find.ByName(FoodConstant.FoodNameElement)).TypeText("Pannbiff");
			 browser.TextField(Find.ByName(FoodConstant.FoodMessaureElement)).TypeText("110"); 
			 browser.Button(Find.ByValue(FoodConstant.Submit)).Click();

			 bool foodNameExsistsOnPage = browser.Text.Contains("Pannbiff");
			 bool foodMessaureExsistsOnPage = browser.Text.Contains("110");

			 Assert.That(foodNameExsistsOnPage, Is.True);
			 Assert.That(foodMessaureExsistsOnPage, Is.True);
		}
      
		//[Test]
		//public void shouldNotBeAbleToAddFoodIfNotLoggedIn() {}
		
		//public void shouldBeAbleToAddIngredient()
		
		//public void shouldBeAbleToLinkIngredientToUser() 
	}
}