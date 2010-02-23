using System;
using CarbonFitness.DataLayer.Repository;
using CarbonFitnessWeb.Models;
using CarbonFitnessWeb.ViewConstants;
using NUnit.Framework;

namespace CarbonFitnessTest.Integration {
    [TestFixture]
    public class ResultsTest : IntegrationBaseTest {
        public override string Url {
            get { return baseUrl + "/Result/Show"; }
        }

        [Test]
        public void shouldShowSumOfCaloriesForADay() {
            var userId = new CreateUserTest(browser).getUniqueUserId();

            var inputFoodTest = new InputFoodTest(browser);
            string date = "2008-02-22";
            inputFoodTest.changeDate(date);
            inputFoodTest.addUserIngredient("Abborre", "100");
            browser.GoTo(Url);

            var dateName = GetFieldNameOnModel<ResultModel>(m => m.Date);
            browser.TextField(dateName).TypeText(date);

            var sumOfCalories = GetFieldNameOnModel<ResultModel>(m => m.SumOfCalories);
            var result = browser.Label(sumOfCalories).Text;

            var d = DateTime.Parse(date);

            var userIngredientRepository = new UserIngredientRepository();
            userIngredientRepository.GetUserIngredientsFromUserId(userId, d, d.AddDays(1));

            Assert.That(int.Parse(result) > 0);
            Assert.Fail("Implement this as next test. First need to get the current user and therefore need unique usernames");
        }
    }
}