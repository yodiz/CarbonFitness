using System;
using System.Linq;
using CarbonFitness.DataLayer.Repository;
using NUnit.Framework;
using SharpArch.Data.NHibernate;
using WatiN.Core;

namespace CarbonFitnessTest.Integration {
	[TestFixture]
	public class ResultsTest : IntegrationBaseTest {
		[TestFixtureSetUp]
		public override void TestFixtureSetUp() {
			base.TestFixtureSetUp();
			userId = new CreateUserTest(Browser).getUniqueUserId();
			new AccountLogOnTest(Browser).LogOn(CreateUserTest.UserName, CreateUserTest.Password);

			clearUserIngredients();
			addOneUserIngredient(now.ToString(), 200);
		}

		private void addOneUserIngredient(string date, int weightMeasure) {
			var inputFoodTest = new InputFoodTest(Browser);

			inputFoodTest.changeDate(date);
			inputFoodTest.createIngredientIfNotExist("Arne anka");
			inputFoodTest.addUserIngredient("Arne anka", weightMeasure.ToString());
		}

		private int userId;

		public override string Url { get { return BaseUrl + "/Result/Show"; } }
		private DateTime now = DateTime.Now.Date;
		private Element caloriHistoryFusionGraphElement { get { return Browser.Element(Find.By("classid", "clsid:d27cdb6e-ae6d-11cf-96b8-444553540000")); } }

		private void reloadPage() {
			Browser.GoTo(Url + "?e=" + Guid.NewGuid());
		}

		private void clearUserIngredients() {
			var repository = new UserIngredientRepository();
			var userIngredients = new UserIngredientRepository().GetAll();
			foreach (var userIngredient in userIngredients) {
				repository.Delete(userIngredient);
			}
			NHibernateSession.Current.Flush();
		}

		[Test]
		public void shouldHaveCalorieHistory() {
			reloadPage();

			var userIngredients = new UserIngredientRepository().GetUserIngredientsByUser(userId, now, now);
			var sum = userIngredients.Sum(u => u.Ingredient.EnergyInKcal * (u.Measure / u.Ingredient.WeightInG));//u.GetMeasureMultiplier()

			var fusionChartSumOfCalorieValue = "<set value='" + ((int) sum);

			Assert.That(caloriHistoryFusionGraphElement.InnerHtml.Contains(fusionChartSumOfCalorieValue), "No calorieValueWith (" + fusionChartSumOfCalorieValue + ") found in graph");
		}

		[Test]
		public void shouldHaveCalorieHistoryInDailyDataPoints() {
			addOneUserIngredient(now.AddDays(-2).ToString(), 200); // adds the second user ingredient
			reloadPage();

			var fusionChartSumOfCalorieValue = "<set value='";

			var matches = caloriHistoryFusionGraphElement.InnerHtml.Split(new[] {fusionChartSumOfCalorieValue}, StringSplitOptions.None);
			Assert.That(matches.Length, Is.EqualTo(3 + 1), "No calorieValueWith (" + fusionChartSumOfCalorieValue + ") found in graph");
		}


		[Test]
		public void shouldShowLoggedInUsersIdealWeight() {
			var userProfileRepository = new UserProfileRepository();

			var userProfile = userProfileRepository.GetByUserId(userId);
			var userIdealWeight = userProfile.IdealWeight;

			var idealWeightString = userIdealWeight.ToString("N1") + "kg";
			Assert.That(Browser.ContainsText(idealWeightString), "Page did not contain: " + idealWeightString);
		}

		[Test]
		public void shouldHaveMultipleSeries() {
			const string seriesname = "seriesName";
			var matches = caloriHistoryFusionGraphElement.InnerHtml.Split(new[] { seriesname }, StringSplitOptions.None);

			Assert.That(matches.Length, Is.EqualTo(3 ), "Should be more than one series on page");
		}

	}
}