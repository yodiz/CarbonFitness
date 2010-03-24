using System;
using System.Linq;
using CarbonFitness.App.Web.Models;
using CarbonFitness.DataLayer.Repository;
using CarbonFitnessTest.Util;
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
			addOneUserIngredient(now.ToString());
		}

		private void addOneUserIngredient(string date) {
			var inputFoodTest = new InputFoodTest(Browser);

			inputFoodTest.changeDate(date);
			inputFoodTest.createIngredientIfNotExist("Arne anka");
			inputFoodTest.addUserIngredient("Arne anka", "100");
		}

		private int userId;
		private TextField dateField { get { return Browser.TextField(GetFieldNameOnModel<ResultModel>(m => m.Date)); } }
		private DateTime testDate = ValueGenerator.getRandomDate();

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
			var sum = userIngredients.Sum(u => u.Ingredient.EnergyInKcal);

            var fusionChartSumOfCalorieValue = "y='" + ((int)sum);

		    bool caloriePointFound = false;
            foreach (var scriptElement in Browser.ElementsWithTag("Script", null, null)) {
                caloriePointFound = scriptElement.OuterHtml.Contains(fusionChartSumOfCalorieValue) | caloriePointFound;
            }

            Assert.That(caloriePointFound, "No calorieValueWith (" + fusionChartSumOfCalorieValue + ") found in page");
		}

		[Test]
		public void shouldHaveCalorieHistoryInDailyDataPoints() {
			addOneUserIngredient(now.AddDays(-2).ToString()); // adds the second user ingredient
			reloadPage();

			var fusionChartSumOfCalorieValue = "y='";

		    string scriptContent = string.Empty;
            foreach (var scriptElement in Browser.ElementsWithTag("Script", null, null))
            {
                scriptContent = scriptElement.OuterHtml;
            }

			var matches = caloriHistoryFusionGraphElement.InnerHtml.Split(new[] {fusionChartSumOfCalorieValue}, StringSplitOptions.None);
			Assert.That(matches.Length, Is.EqualTo(3 + 1), "No calorieValueWith (" + fusionChartSumOfCalorieValue + ") found in graph");
		}


        [Test]
        public void shouldShowLoggedInUsersIdealWeight()
        {
            var userProfileRepository = new UserProfileRepository();

            var userProfile = userProfileRepository.GetByUserId(userId);
            var userIdealWeight = userProfile.IdealWeight;

            var idealWeightString = userIdealWeight.ToString("N2") + "kg";
            Assert.That(this.Browser.ContainsText(idealWeightString), "Page did not contain: " + idealWeightString);

        }
	}
}