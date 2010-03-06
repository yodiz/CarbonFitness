using System;
using System.Linq;
using CarbonFitness.DataLayer.Repository;
using CarbonFitnessWeb.Models;
using NUnit.Framework;

namespace CarbonFitnessTest.Integration {
    [TestFixture]
    public class ResultsTest : IntegrationBaseTest {
        private int userId;
        private const string testDate = "2008-02-22";

        public override string Url {
            get { return baseUrl + "/Result/Show"; }
        }

        [SetUp]
        public override void Setup() {
            userId = new CreateUserTest(browser).getUniqueUserId();
            new AccountLogOnTest(browser).LogOn(CreateUserTest.UserName, CreateUserTest.Password);

            var inputFoodTest = new InputFoodTest(browser);

            inputFoodTest.changeDate(testDate);
            inputFoodTest.createIngredientIfNotExist("Pannbiff");
            inputFoodTest.addUserIngredient("Pannbiff", "100");
            browser.GoTo(Url);
        }

        [Test]
        public void shouldShowSumOfCaloriesForADay() {
            var dateName = GetFieldNameOnModel<ResultModel>(m => m.Date);
            browser.TextField(dateName).TypeText(testDate);

            var sumOfCalories = GetFieldNameOnModel<ResultModel>(m => m.SumOfCalories);
            var result = browser.Div(sumOfCalories).Text.Trim();

            var d = DateTime.Parse(testDate);

            var userIngredients = new UserIngredientRepository().GetUserIngredientsFromUserId(userId, d, d.AddDays(1));
            var sum = userIngredients.Sum(u => u.Ingredient.Calories);

            Assert.That(result, Is.Not.Empty);
            Assert.That(int.Parse(result), Is.GreaterThan(0));
            Assert.That(int.Parse(result), Is.EqualTo(sum));
        }

    }
}