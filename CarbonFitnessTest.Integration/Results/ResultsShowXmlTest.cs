using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
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
            decimal sum = userIngredients.Sum(u => u.Ingredient.EnergyInKcal * (u.Measure / u.Ingredient.WeightInG)); 
            
            var chartSumOfCalorieValue = ">" + sum +"</VALUE>";
		    chartSumOfCalorieValue = chartSumOfCalorieValue.Replace(",", ".");

            Assert.That(Browser.Html, Contains.Substring(chartSumOfCalorieValue));
			Assert.That(Browser.Html, Contains.Substring(Now.ToShortDateString() + "</VALUE>"));
			Assert.That(Browser.Html, !Contains.Substring(","));

			//Assert.That(Browser.Html, Contains.Substring("<VALUE>" + Now.ToShortDateString() + "</VALUE>"));
		}
	}
}