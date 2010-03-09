using System;
using CarbonFitness.App.Web.Models;
using CarbonFitness.App.Web.ViewConstants;
using NUnit.Framework;
using WatiN.Core;

namespace CarbonFitnessTest.Integration {
	[TestFixture]
	public class EnergyTest : IntegrationBaseTest {

		public override void Setup() {
			new CreateUserTest(Browser).getUniqueUserId();
			new AccountLogOnTest(Browser).LogOn(CreateUserTest.UserName, CreateUserTest.Password);
			base.Setup();
		}

		[Test]
		public void shouldShowLenghtInputOnPage() {
			Browser.Link(Find.ByText(SiteMasterConstant.EnergyInputLinkText)).Click();
			string lenghtInput = GetFieldNameOnModel<EnergyModel>(m => m.Length);
			Assert.That(Browser.TextField(lenghtInput).Exists, "No Textfield with name:" + lenghtInput + " exist on page");
		}

		public override string Url {
			get { return BaseUrl + "/Energy/Input"; }
		}
	}
}