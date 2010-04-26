using NUnit.Framework;

namespace CarbonFitnessTest.Integration.Results
{

    [TestFixture]
    class ShowWeightPrognosisXmlTest : ResultsTestBase
    {
        public override string Url { get { return getUrl("Result", "ShowWeightPrognosisXml"); } }


        [Test]
        public void shouldHavePrognoseXmlForWeight()
        {
            Browser.GoTo(Url);

            Assert.That(Browser.Html, Contains.Substring(Now.AddDays(1).ToShortDateString() + "</VALUE>"));
            Assert.That(Browser.Html, !Contains.Substring(","));
        }
    }
}
