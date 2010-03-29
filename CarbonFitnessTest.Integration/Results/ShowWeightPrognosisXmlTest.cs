using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace CarbonFitnessTest.Integration.Results
{

    [TestFixture]
    class ShowWeightPrognosisXmlTest : ResultsTestBase
    {
        public override string Url { get { return BaseUrl + "/Result/ShowWeightPrognosisXml"; } }


        [Test]
        public void shouldHavePrognoseXmlForWeight()
        {
            Browser.GoTo(Url);

            Assert.That(Browser.Html, Contains.Substring(Now.AddDays(1).ToShortDateString() + "</VALUE>"));
            Assert.That(Browser.Html, !Contains.Substring(","));
        }
    }
}
