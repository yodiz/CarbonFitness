using NUnit.Framework;

namespace CarbonFitnessTest.Integration.Results {
	[TestFixture]
	public class ResultsShowXmlTest : ResultsTestBase {
		public override string Url { get { return BaseUrl + "/Result/ShowXml"; } }

		[Test]
		public void shouldHaveCalorieHistory() {
			Browser.GoTo(Url);
			Assert.That(Browser.Html, Contains.Substring("<set value="));
		}
	}
}