using System;
using CarbonFitness.App.Web.Models;
using CarbonFitness.App.Web.ViewConstants;
using NUnit.Framework;
using WatiN.Core;

namespace CarbonFitnessTest.Integration
{
	[TestFixture]
	public class ProfileTest : IntegrationBaseTest
	{
        private string lenghtFieldName { get { return GetFieldNameOnModel<ProfileModel>(m => m.Length); } }
        private TextField LengthInputField { get { return Browser.TextField(lenghtFieldName); } }

	    private TextField IdealWeightInputField { get { return Browser.TextField(idealWeightFieldName); } }
		private string idealWeightFieldName { get { return GetFieldNameOnModel<ProfileModel>(m => m.IdealWeight); } }

        private TextField WeightInputField { get { return Browser.TextField(WeightFieldName); } }
        private string WeightFieldName { get { return GetFieldNameOnModel<ProfileModel>(m => m.Weight); } }

        private Div BMIField { get { return Browser.Div("BMIField"); } }
        private string BMIFieldName { get { return GetFieldNameOnModel<ProfileModel>(m => m.BMI); } }

		private Button SaveButton { get { return Browser.Button(Find.ByValue("Spara")); } }

		public override void TestFixtureSetUp()
		{
			base.TestFixtureSetUp();
			new CreateUserTest(Browser).getUniqueUserId();
			new AccountLogOnTest(Browser).LogOn(CreateUserTest.UserName, CreateUserTest.Password);
		}

		[Test]
		public void shouldShowLenghtInputOnPage() {
            Assert.That(LengthInputField.Exists, "No Textfield with name:" + lenghtFieldName + " exist on page");
		}

        [Test]
        public void shouldShowWeightInputOnPage() {
            Assert.That(WeightInputField.Exists, "No Textfield with name:" + WeightFieldName + " exist on page");
        }

        [Test]
        public void shouldShowBMIFieldOnPage() {
            Assert.That(BMIField.Exists, "No field with name:" + BMIFieldName + " exist on page");
        }

        [Test]
        public void shouldShowBMIOnPage() {
            Assert.That(BMIField.InnerHtml, Contains.Substring(GetBmi().ToString("N2")));
        }

        [Test]
        public void shouldShowBMIOnPageAfterSave() {
            WeightInputField.TypeText("85,00");
            SaveButton.Click();
            Assert.That(BMIField.InnerHtml, Contains.Substring(GetBmi().ToString("N2")));
        }

	    private decimal GetBmi() {
	        decimal weight = decimal.Parse(WeightInputField.Text);
	        decimal lenght = decimal.Parse(LengthInputField.Text);
	        return weight / (lenght * lenght);
	    }

	    [Test]
        public void shouldSaveWeight() {
            const string expectedWeight = "85,00";
            WeightInputField.TypeText(expectedWeight);
            SaveButton.Click();
            reloadPage();
            Assert.That(WeightInputField.Text, Is.EqualTo(expectedWeight));
        }


	    [Test]
        public void shouldSaveLength() {
            const string expectedLenght = "1,83";
            LengthInputField.TypeText(expectedLenght);
            SaveButton.Click();
            reloadPage();
            Assert.That(LengthInputField.Text, Is.EqualTo(expectedLenght));
        }

	    [Test]
		public void shouldShowIdealWeightSetting() {
			Assert.That(IdealWeightInputField.Exists, "No Textfield with name:" + idealWeightFieldName + " exist on page");
		}

		[Test]
		public void shouldSaveIdealWeightSetting() {
			decimal weight = 75;

			IdealWeightInputField.TypeText(weight.ToString());
			SaveButton.Click();
			reloadPage();
			Assert.That(decimal.Parse(IdealWeightInputField.Text), Is.EqualTo(weight));
		}

		[Test]
		public void shouldShowNiceErrorMessageWhenSavingInvalidIdealWeightNumber() {

			IdealWeightInputField.TypeText("notANumber");
			SaveButton.Click();

			var expectedErrorMessage = ProfileConstant.InvalidIdealWeightInput;
			var errorElement = Browser.Element(Find.ByClass("validation-summary-errors"));

			Assert.That(errorElement.Text, Contains.Substring(expectedErrorMessage));
		}

		[Test]
		public void shouldNotSaveWhenStateIsInvalid() {
			var expectedWeight = 75M;
			IdealWeightInputField.TypeText("75");
			SaveButton.Click();

			IdealWeightInputField.TypeText("notANumber");
			SaveButton.Click();
			reloadPage();

			Assert.That(Convert.ToDecimal(IdealWeightInputField.Text), Is.EqualTo(expectedWeight));
		}

		private void reloadPage()
		{
			ReloadPage(SiteMasterConstant.ProfileInputLinkText);
		}

		public override string Url
		{
			get { return getUrl("Profile", "Input"); }
		}
	}
}