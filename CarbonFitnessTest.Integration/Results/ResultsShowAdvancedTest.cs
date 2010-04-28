using CarbonFitness.App.Web.ViewConstants;
using CarbonFitness.DataLayer.Repository;
using NUnit.Framework;
using WatiN.Core;

namespace CarbonFitnessTest.Integration.Results {
	[TestFixture]
	public class ResultsShowAdvancedTest : ResultsTestBase {
        
	    private Div amChartContent { get { return Browser.Div("amChartContent"); } }
	    private SelectList nutrientDropDown { get { return Browser.SelectList("GraphLineOptions"); } }
	    public override string Url { get { return getUrl("Result", "ShowAdvanced"); } }

		[Test]
		public void shouldShowLoggedInUsersIdealWeight() {
			var userProfileRepository = new UserProfileRepository();

			var userProfile = userProfileRepository.GetByUserId(UserId);
			var userIdealWeight = userProfile.IdealWeight;

			var idealWeightString = userIdealWeight.ToString("N1") + "kg";
			Assert.That(Browser.ContainsText(idealWeightString), "Page did not contain: " + idealWeightString);
		}

        [Test]
        public void shouldHaveNutrientResultDropDown() {
            Assert.That(nutrientDropDown.Option("Energi (Kcal)").Exists, "Page did not contain: GraphLineOptions dropdown");
        }

        [Test]
        public void shouldHaveWeightOptionIngGraphlineDropdown() {
            Assert.That(nutrientDropDown.Option("Vikt (kg)").Exists, "Page did not contain: Weight in nutrients dropdown");
        }

        [Test]
        public void shouldGoToEnergyWeightResult() {
            Browser.Link(Find.ByText(x => x.Contains(ResultConstant.WeightEnergyResult))).Click();

            Assert.That(nutrientDropDown.Exists, Is.False);
            Assert.That(amChartContent.Exists);
        }

        [Test]
        public void shouldGoToAdvancedResult() {
            Browser.Link(Find.ByText(x => x.Contains(ResultConstant.AdvancedResult))).Click();

            Assert.That(amChartContent.Exists);
            Assert.That(nutrientDropDown.Exists);
        }
	}
}