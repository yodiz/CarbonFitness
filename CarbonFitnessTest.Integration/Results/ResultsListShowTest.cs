using CarbonFitness.App.Web.ViewConstants;
using CarbonFitness.DataLayer.Repository;
using NUnit.Framework;
using WatiN.Core;

namespace CarbonFitnessTest.Integration.Results {
	[TestFixture]
	public class ResultsListShowTest : ResultsTestBase {
        private Table nutrientSummaryList { get { return Browser.Table("nutrientsummarylist"); } }
	    public override string Url { get { return getUrl("Result", "ShowResultList"); } }

        [Test]
        public void shouldGoToResultList() {
            Browser.Link(Find.ByText(x => x.Contains(ResultConstant.ResultList))).Click();
 
            Assert.That(nutrientSummaryList.Exists);
        }
	}
}