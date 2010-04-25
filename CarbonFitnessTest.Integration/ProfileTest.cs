using System;
using CarbonFitness.App.Web.Models;
using CarbonFitness.App.Web.ViewConstants;
using NUnit.Framework;
using WatiN.Core;

namespace CarbonFitnessTest.Integration
{
	[TestFixture]
	public class ProfileTest : IntegrationBaseTest {
        private string lenghtFieldName { get { return GetFieldNameOnModel<ProfileModel>(m => m.Length); } }
        private TextField LengthInputField { get { return Browser.TextField(lenghtFieldName); } }

	    private TextField IdealWeightInputField { get { return Browser.TextField(idealWeightFieldName); } }
		private string idealWeightFieldName { get { return GetFieldNameOnModel<ProfileModel>(m => m.IdealWeight); } }

        private TextField WeightInputField { get { return Browser.TextField(WeightFieldName); } }
        private string WeightFieldName { get { return GetFieldNameOnModel<ProfileModel>(m => m.Weight); } }

        private Div BMIField { get { return Browser.Div("BMIField"); } }
        private string BMIFieldName { get { return GetFieldNameOnModel<ProfileModel>(m => m.BMI); } }

        private Div BMRField { get { return Browser.Div("BMRField"); } }
        private string BMRFieldName { get { return GetFieldNameOnModel<ProfileModel>(m => m.BMR); } }

        private Div DailyCalorieNeedField { get { return Browser.Div("DailyCalorieNeedField"); } }
        private string DailyCalorieNeedFieldName { get { return GetFieldNameOnModel<ProfileModel>(m => m.DailyCalorieNeed); } }

        private TextField AgeField { get { return Browser.TextField(AgeFieldName); } }
        private string AgeFieldName { get { return GetFieldNameOnModel<ProfileModel>(m => m.Age); } }

        private RadioButton GenderRadioButton { get { return Browser.RadioButton(GenderRadioButtonsFieldName); } }
        private string GenderRadioButtonsFieldName { get { return GetFieldNameOnModel<ProfileModel>(m => m.SelectedGender); } }

		private Button SaveButton { get { return Browser.Button(Find.ByValue("Spara")); } }
        
        private RadioButton GetRadioButtonFromName(string genderName) {
            foreach (var gender in Browser.RadioButtons) {
                if (gender.OuterHtml.Contains(genderName)) {
                    return gender;
                }
            }
            return null;
        }

		public override void TestFixtureSetUp() {
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
        public void shouldShowAgeFieldOnPage() {
            Assert.That(AgeField.Exists, "No field with name:" + AgeFieldName + " exist on page");
        }

        [Test]
        public void shouldShowBMIFieldOnPage() {
            Assert.That(BMIField.Exists, "No field with name:" + BMIFieldName + " exist on page");
        }

        [Test]
        public void shouldShowBMRFieldOnPage() {
            Assert.That(BMRField.Exists, "No field with name:" + BMRFieldName + " exist on page");
        }

        [Test]
        public void shouldShowDailyCalorieNeedFieldOnPage() {
            Assert.That(DailyCalorieNeedField.Exists, "No field with name:" + DailyCalorieNeedFieldName + " exist on page");
        }

        [Test]
        public void shouldShowGenderRadioButtonsOnPage() {
            Assert.That(GenderRadioButton.Exists, "No field with name:" + GenderRadioButtonsFieldName + " exist on page");
        }

        [Test] 
        public void shouldShowActivityOnPageAfterSave() {
            GetRadioButtonFromName("Medel").Click();
            SaveButton.Click();
            reloadPage();
            var middleActivityRadioButton = GetRadioButtonFromName("Medel");

            Assert.That(middleActivityRadioButton, Is.Not.Null);
            Assert.That(middleActivityRadioButton.Checked, "Activitylevel medel is not checked after checking it.");
        }


        [Test]
        public void shouldShowGenderWomanOnPageAfterSave() {
            GetRadioButtonFromName("Kvinna").Click();
            SaveButton.Click();
            reloadPage();
            var kvinna = GetRadioButtonFromName("Kvinna");
   
            Assert.That(kvinna, Is.Not.Null);
            Assert.That(kvinna.Checked, "Kvinna is not checked after checking it.");
        }

        [Test]
        public void shouldStoredAgeAfterSave() {
            const string expectedAge = "23";
            AgeField.TypeText(expectedAge); 
            SaveButton.Click();
            Assert.That(AgeField.Exists, "No field with name:" + AgeFieldName + " exist on page");
            reloadPage();
            Assert.That(AgeField.Text, Is.EqualTo(expectedAge));
        }

        [Test]
        public void shouldShowBMROnPageAfterSave() {
            AgeField.TypeText("32");
            WeightInputField.TypeText("73");
            LengthInputField.TypeText("183");
            GetRadioButtonFromName("Man").Click();
            GetRadioButtonFromName("Mycket hög").Click();
            SaveButton.Click();
            Assert.That(BMRField.InnerHtml, Contains.Substring("1720,58"));
        }

        [Test]
        public void shouldShowDailyCalorieNeedOnPageAfterSave() {
            AgeField.TypeText("32");
            WeightInputField.TypeText("73");
            LengthInputField.TypeText("183");
            GetRadioButtonFromName("Man").Click();
            GetRadioButtonFromName("Mycket hög").Click();
            SaveButton.Click();
            Assert.That(DailyCalorieNeedField.InnerHtml, Contains.Substring("3269,10"));
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
            if(lenght == 0) {
                return 0;
            }
	        var lenghtInMeter = (lenght / 100);
            return weight / (lenghtInMeter * lenghtInMeter);
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
            const string expectedLenght = "183,00";
            LengthInputField.TypeText(expectedLenght);
            SaveButton.Click();
            reloadPage();
            Assert.That(LengthInputField.Text, Is.EqualTo(expectedLenght));
        }

        [Test]
        public void shouldShowErrorMessageWhenLengthIsUnderTwentyCentimeters() {
            const string expectedLenght = "1,83";
            LengthInputField.TypeText(expectedLenght);
            SaveButton.Click();
            var expectedErrorMessage = ProfileConstant.InvalidInput;
            var errorElement = Browser.Element(Find.ByClass("validation-summary-errors"));

            Assert.That(errorElement.Text, Contains.Substring(expectedErrorMessage));
            //Assert.That(LengthInputField.Exists, "No Textfield with name:" + lenghtFieldName + " exist on page");
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

			var expectedErrorMessage = ProfileConstant.InvalidInput;
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

		private void reloadPage() {
			ReloadPage(SiteMasterConstant.ProfileInputLinkText);
		}

		public override string Url {
			get { return getUrl("Profile", "Input"); }
		}
	}
}