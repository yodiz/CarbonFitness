using System;
using CarbonFitness.App.Web.Models;
using CarbonFitness.App.Web.ViewConstants;
using NUnit.Framework;
using WatiN.Core;

namespace CarbonFitnessTest.Integration {
	[TestFixture]
	public class ProfileTest : IntegrationBaseTest {
        private TextField IdealWeightInputField { get { return Browser.TextField(idealWeightInputName); } }
	    private string idealWeightInputName { get { return GetFieldNameOnModel<ProfileModel>(m => m.IdealWeight); } }
        private Button SaveButton { get { return Browser.Button(Find.ByValue("Spara")); } }

	    public override void TestFixtureSetUp(){
            base.TestFixtureSetUp();
			new CreateUserTest(Browser).getUniqueUserId();
			new AccountLogOnTest(Browser).LogOn(CreateUserTest.UserName, CreateUserTest.Password);
		}

		[Test]
		public void shouldShowLenghtInputOnPage() {
			string lenghtInput = GetFieldNameOnModel<ProfileModel>(m => m.Length);
			Assert.That(Browser.TextField(lenghtInput).Exists, "No Textfield with name:" + lenghtInput + " exist on page");
		}


        [Test]
        public void shouldShowIdealWeightSetting() {
            Assert.That(IdealWeightInputField.Exists, "No Textfield with name:" + idealWeightInputName + " exist on page");
        }

        [Test]
        public void shouldSaveIdealWeightSetting() {
            decimal weight = 75;

            IdealWeightInputField.TypeText(weight.ToString());
            SaveButton.Click();
            reloadPage();
            Assert.That(decimal.Parse(IdealWeightInputField.Text), Is.EqualTo(weight));
        }

	    private void reloadPage() {
	        Browser.Link(Find.ByText(SiteMasterConstant.ProfileInputLinkText)).Click();
	    }

	    public override string Url {
			get { return BaseUrl + "/Profile/Input"; }
		}
	}
}