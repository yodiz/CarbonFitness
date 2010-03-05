using System;
using System.Linq;
using CarbonFitness.DataLayer.Repository;
using CarbonFitnessWeb.Models;
using CarbonFitnessWeb.ViewConstants;
using NUnit.Framework;
using WatiN.Core;

namespace CarbonFitnessTest.Integration {
    [TestFixture]
    public class ResultsTest : IntegrationBaseTest {
        public override string Url {
            get { return baseUrl + "/Result/Show"; }
        }

        [Test]
        public void shouldShowSumOfCaloriesForADay() {
            var userId = new CreateUserTest(browser).getUniqueUserId();
            new AccountLogOnTest(browser).LogOn(CreateUserTest.UserName, CreateUserTest.Password);

            var inputFoodTest = new InputFoodTest(browser);
            string date = "2008-02-22";
            inputFoodTest.changeDate(date);
            inputFoodTest.createIngredientIfNotExist("Pannbiff");
            inputFoodTest.addUserIngredient("Pannbiff", "100");
            browser.GoTo(Url);

            var dateName = GetFieldNameOnModel<ResultModel>(m => m.Date);
            browser.TextField(dateName).TypeText(date);

            var sumOfCalories = GetFieldNameOnModel<ResultModel>(m => m.SumOfCalories);
            var result = browser.Div(sumOfCalories).Text.Trim();

            var d = DateTime.Parse(date);

            var userIngredientRepository = new UserIngredientRepository();
            var userIngredients = userIngredientRepository.GetUserIngredientsFromUserId(userId, d, d.AddDays(1));
            var sum = userIngredients.Sum(u => u.Ingredient.Calories);

            Assert.That(result, Is.Not.Empty);
            Assert.That(int.Parse(result), Is.GreaterThan(0));
            Assert.That(int.Parse(result), Is.EqualTo(sum));
        }
    }
}