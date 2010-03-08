using NUnit.Framework;

namespace CarbonFitnessTest.Integration {
	[TestFixture]
	public class EnergyTest : IntegrationBaseTest {
		//[Test]
		//public void shouldShowLenghtInputOnPage()
		//{
		//    Browser.Link(Find.ByText(SiteMasterConstant.EnergyInputLinkText)).Click();
		//    string LenghtInput = GetFieldNameOnModel<EnergyModel>(m => m.Length);
		//    Assert.That(Browser.TextField(LenghtInput).Exists, "No Textfield with name:" + LenghtInput + " exist on page");
		//}

		public override string Url {
			get { return BaseUrl + "Energy/Input.aspx"; }
		}
	}
}