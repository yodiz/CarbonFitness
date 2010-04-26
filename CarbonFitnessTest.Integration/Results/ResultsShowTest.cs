using System;
using System.Linq;
using CarbonFitness.DataLayer.Repository;
using NUnit.Framework;
using WatiN.Core;

namespace CarbonFitnessTest.Integration.Results {
	[TestFixture]
	public class ResultsShowTest : ResultsTestBase {
		public override string Url { get { return getUrl("Result", "Show"); } }
		private Element caloriHistoryFusionGraphElement { get { return Browser.Element(Find.By("classid", "clsid:d27cdb6e-ae6d-11cf-96b8-444553540000")); } }

		private void reloadPage() {
			Browser.GoTo(Url + "?e=" + Guid.NewGuid());
		}

		//[Test]
		//public void shouldHaveCalorieHistory() {
		//   reloadPage();

		//   var userIngredients = new UserIngredientRepository().GetUserIngredientsByUser(UserId, Now, Now);
		//   var sum = userIngredients.Sum(u => u.Ingredient.EnergyInKcal * (u.Measure / u.Ingredient.WeightInG)); //u.GetMeasureMultiplier()

		//   var fusionChartSumOfCalorieValue = "<set value='" + ((int) sum);

		//   Assert.That(caloriHistoryFusionGraphElement.InnerHtml.Contains(fusionChartSumOfCalorieValue), "No calorieValueWith (" + fusionChartSumOfCalorieValue + ") found in graph");
		//}

		//[Test]
		//public void shouldHaveCalorieHistoryInDailyDataPoints() {
		//   addOneUserIngredient(Now.AddDays(-2).ToString(), 200); // adds the second user ingredient
		//   reloadPage();

		//   var fusionChartSumOfCalorieValue = "<set value='";

		//   var matches = caloriHistoryFusionGraphElement.InnerHtml.Split(new[] {fusionChartSumOfCalorieValue}, StringSplitOptions.None);
		//   Assert.That(matches.Length, Is.EqualTo(3 + 1), "No calorieValueWith (" + fusionChartSumOfCalorieValue + ") found in graph");
		//}


		//[Test]
		//public void shouldHaveMultipleSeries() {
		//   const string seriesname = "seriesName";
		//   var matches = caloriHistoryFusionGraphElement.InnerHtml.Split(new[] {seriesname}, StringSplitOptions.None);

		//   Assert.That(matches.Length, Is.EqualTo(3), "Should be more than one series on page");
		//}

		[Test]
		public void shouldShowLoggedInUsersIdealWeight() {
			var userProfileRepository = new UserProfileRepository();

			var userProfile = userProfileRepository.GetByUserId(UserId);
			var userIdealWeight = userProfile.IdealWeight;

			var idealWeightString = userIdealWeight.ToString("N1") + "kg";
			Assert.That(Browser.ContainsText(idealWeightString), "Page did not contain: " + idealWeightString);
		}

        [Test]
        public void shouldHaveNutrientResultDropDown() {
            var nutrientDropDown = Browser.SelectList("Nutrients");

            Assert.That(nutrientDropDown.Option("Energi (Kcal)").Exists, "Page did not contain: Nutrients dropdown");
        }
	}
}