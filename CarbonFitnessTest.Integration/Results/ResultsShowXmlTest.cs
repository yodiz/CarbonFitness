using System.Diagnostics;
using System.IO;
using System.Text;
using NUnit.Framework;
using System.Net;
using WatiN.Core;

namespace CarbonFitnessTest.Integration.Results {
	[TestFixture]
    public class ResultsShowXmlTest : ResultsTestBase
    {
        public override string Url { get { return BaseUrl + "/Result/ShowXmlInsideHtml"; } }

		[Test]
		public void shouldHaveCalorieHistory() {
			Browser.GoTo(Url);

			Assert.That(Browser.Html, Contains.Substring(Now.ToShortDateString() + "</VALUE>"));
			Assert.That(Browser.Html, !Contains.Substring(","));

			//Assert.That(Browser.Html, Contains.Substring("<VALUE>" + Now.ToShortDateString() + "</VALUE>"));
		}
	}
}