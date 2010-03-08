using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarbonFitnessWeb.ViewConstants;
using NUnit.Framework;
using WatiN.Core;

namespace CarbonFitnessTest.Integration
{
    [TestFixture]
    public class EnergyTest : IntegrationBaseTest
    {
        //[Test]
        //public void shouldShowLenghtInputOnPage()
        //{
        //    browser.Link(Find.ByText(SiteMasterConstant.EnergyInputLinkText)).Click();
        //    string LenghtInput = GetFieldNameOnModel<EnergyModel>(m => m.Length);
        //    Assert.That(browser.TextField(LenghtInput).Exists, "No Textfield with name:" + LenghtInput + " exist on page");
        //}

        public override string Url {
            get { return baseUrl + "Energy/Input.aspx"; }
        }
    }
}
