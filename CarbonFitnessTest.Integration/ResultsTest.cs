using CarbonFitness.DataLayer.Repository;
using NUnit.Framework;

namespace CarbonFitnessTest.Integration {
    [TestFixture]
    public class ResultsTest : IntegrationBaseTest {
        public override string Url {
            get { return baseUrl + "/Result/Show"; }
        }

        [Test]
        public void shouldShowSumOfCaloriesForADay() {
            //var inputFoodTest = new InputFoodTest(browser);
            //inputFoodTest.changeDate("2010-02-22");
            //inputFoodTest.addUserIngredient("Abborre", "100");
            //browser.GoTo(Url);

            //string dateName = GetFieldNameOnModel<ResultModel>(m => m.Date);
            //browser.TextField(dateName).TypeText("2010-02-22");
            //var result = browser.Label("ResultCalories").Text;

            //new UserIngredientRepository().GetUserIngredientsFromUserId(Cu);

            //Assert.That(int.Parse(result) > 0);
            Assert.Fail("Implement this as next test. First need to get the current user and therefore need unique usernames");
        }

        

        
    }
}