using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;
using NUnit.Framework;
using System.Net;
using WatiN.Core;

namespace CarbonFitnessTest.Integration.Results {
	[TestFixture]
    public class ResultsShowXmlTest : ResultsTestBase
    {
		public override string Url { get { return getUrl("Result", "ShowXmlInsideHtml"); } }

		[Test]
		public void shouldHaveCalorieHistory() {
			Browser.GoTo(Url);

            var userIngredients = new UserIngredientRepository().GetUserIngredientsByUser(UserId, Now, Now);
            decimal sum = userIngredients.Sum(u => u.Ingredient.GetNutrient(NutrientEntity.EnergyInKcal).Value * (u.Measure / u.Ingredient.WeightInG)); 
            
            var chartSumOfCalorieValue = ">" + String.Format("{0:0.00000}", sum) +"</VALUE>";
		    chartSumOfCalorieValue = chartSumOfCalorieValue.Replace(",", ".");

            Assert.That(Browser.Html, Contains.Substring(chartSumOfCalorieValue));
			Assert.That(Browser.Html, Contains.Substring(Now.ToShortDateString() + "</VALUE>"));
			Assert.That(Browser.Html, !Contains.Substring(","));

		}

        [Test]
        public void shouldHaveFatHistory() {
            Browser.GoTo(Url + "/" + NutrientEntity.FatInG);

            //var nutrientDropDown = Browser.SelectList("Nutrients");
            //nutrientDropDown.Option(NutrientEntity.FatInG.ToString()).Select();

            var userIngredients = new UserIngredientRepository().GetUserIngredientsByUser(UserId, Now, Now);
            decimal sum = userIngredients.Sum(u => u.GetActualCalorieCount(x => x.GetNutrient(NutrientEntity.FatInG).Value));

            var chartSumOfFatValue = ">" + String.Format("{0:0.00000}", sum) + "</VALUE>";
            chartSumOfFatValue = chartSumOfFatValue.Replace(",", ".");

            Assert.That(Browser.Html, Contains.Substring(chartSumOfFatValue));
        }
	}
}