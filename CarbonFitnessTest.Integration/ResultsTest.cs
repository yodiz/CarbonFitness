using System;
using System.Linq;
using CarbonFitness.App.Web.Models;
using CarbonFitness.DataLayer.Repository;
using NUnit.Framework;
using WatiN.Core;

namespace CarbonFitnessTest.Integration {
	[TestFixture]
	public class ResultsTest : IntegrationBaseTest {
		[TestFixtureSetUp]
		public override void TestFixtureSetUp() {
			base.TestFixtureSetUp();
			userId = new CreateUserTest(Browser).getUniqueUserId();
			new AccountLogOnTest(Browser).LogOn(CreateUserTest.UserName, CreateUserTest.Password);
		}

		private void setupUserIngredients() {
			var inputFoodTest = new InputFoodTest(Browser);

			inputFoodTest.changeDate(testDate);
			inputFoodTest.createIngredientIfNotExist("Pannbiff");
			inputFoodTest.addUserIngredient("Pannbiff", "100");

			Browser.GoTo(Url);
		}

		private int userId;
		private TextField dateField { get { return Browser.TextField(GetFieldNameOnModel<ResultModel>(m => m.Date)); } }
		private const string testDate = "2008-02-22";

		public override string Url { get { return BaseUrl + "/Result/Show"; } }

		[Test]
		public void shouldShowSumOfCaloriesForADay() {
			setupUserIngredients();
			dateField.TypeText(testDate);

			var sumOfCalories = GetFieldNameOnModel<ResultModel>(m => m.SumOfCalories);
			var result = Browser.Div(sumOfCalories).Text.Trim();

			var d = DateTime.Parse(testDate);

			var userIngredients = new UserIngredientRepository().GetUserIngredientsFromUserId(userId, d, d.AddDays(1));
			var sum = userIngredients.Sum(u => u.Ingredient.EnergyInKcal);

			Assert.That(result, Is.Not.Empty);
			Assert.That(decimal.Parse(result), Is.GreaterThan(0));
			Assert.That(decimal.Parse(result), Is.EqualTo(sum));
		}

		[Test]
		public void shouldShowTodaysDateInDateField() {
			Assert.That(dateField.Text, Is.EqualTo(DateTime.Now.ToShortDateString()));
		}
	}
}