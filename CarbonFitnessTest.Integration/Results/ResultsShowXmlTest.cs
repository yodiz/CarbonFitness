using System;
using System.Linq;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;
using NUnit.Framework;

namespace CarbonFitnessTest.Integration.Results {
	[TestFixture]
    public class ResultsShowXmlTest : ResultsTestBase
    {
		public override string Url { get { return getUrl("Result", "ShowXmlInsideHtml"); } }

		[Test]
		public void shouldHaveCalorieHistory() {
            Browser.GoTo(Url + "/?graphlines=" + NutrientEntity.EnergyInKcal);

            var userIngredients = new UserIngredientRepository().GetUserIngredientsByUser(UserId, Now, Now.AddDays(1));
            decimal sum = userIngredients.Sum(u => u.Ingredient.GetNutrient(NutrientEntity.EnergyInKcal).Value * (u.Measure / u.Ingredient.WeightInG));

            var chartSumOfCalorieValue = ">" + String.Format("{0:0.00000}", sum) + "</VALUE>";
		    chartSumOfCalorieValue = chartSumOfCalorieValue.Replace(",", ".");

            Assert.That(Browser.Html, Contains.Substring(chartSumOfCalorieValue));
			Assert.That(Browser.Html, Contains.Substring(Now.ToShortDateString() + "</VALUE>"));
			Assert.That(Browser.Html, !Contains.Substring(","));

		}

        [Test]
        public void shouldHaveFatHistory() {
            Browser.GoTo(Url + "/?graphlines=" + NutrientEntity.FatInG);

            var userIngredients = new UserIngredientRepository().GetUserIngredientsByUser(UserId, Now, Now.AddDays(1));
            decimal sum = userIngredients.Sum(u => u.GetActualCalorieCount(x => x.GetNutrient(NutrientEntity.FatInG).Value));

            var chartSumOfFatValue = ">" + String.Format("{0:0.00000}", sum) + "</VALUE>";
            chartSumOfFatValue = chartSumOfFatValue.Replace(",", ".");

            Assert.That(Browser.Html, Contains.Substring(chartSumOfFatValue));
        }
        
        [Test]
        public void shouldHaveWeightHistory() {
            Browser.GoTo(Url + "/?graphlines=" + "Weight");

            var userWeight = new UserWeightRepository().GetAll().Where(x=>x.User.Id == UserId).FirstOrDefault();

            var chartSumOfFatValue = ">" + String.Format("{0:0.00000}", userWeight.Weight) + "</VALUE>";
            chartSumOfFatValue = chartSumOfFatValue.Replace(",", ".");

            Assert.That(Browser.Html, Contains.Substring(chartSumOfFatValue));
        }

        [Test]
        public void shouldHaveGidInGraph() {
            Browser.GoTo(Url + "/?graphlines=" + NutrientEntity.EnergyInKcal);
            var nutrient = new NutrientRepository().GetByName(NutrientEntity.EnergyInKcal.ToString());
            Assert.That(Browser.Html, Contains.Substring("<GRAPH gid=\"" + nutrient.Id));
        }
    }
}