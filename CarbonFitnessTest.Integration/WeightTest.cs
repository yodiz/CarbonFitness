using System;
using CarbonFitness.App.Web.Models;
using CarbonFitness.App.Web.ViewConstants;
using NUnit.Framework;
using WatiN.Core;

namespace CarbonFitnessTest.Integration {
	[TestFixture]
	public class WeightTest : IntegrationBaseTest {
		

		public override void Setup() {
			new CreateUserTest(Browser).getUniqueUserId();
			new AccountLogOnTest(Browser).LogOn(CreateUserTest.UserName, CreateUserTest.Password);
			base.Setup();
		}

		public override string Url {
			get { return BaseUrl + "/Weight/Input"; }
		}

		private Button submitButton {
			get { return Browser.Button(Find.By("type", "submit")); }
		}
		
		private void reloadPage() {
			Browser.Link(Find.ByText(SiteMasterConstant.WeightLinkText)).Click();
		}

		private string datePickerNameFieldName {
			get { return GetFieldNameOnModel<InputWeightModel>(m => m.Date); }
		}

		private TextField dateInputField {
			get{
				return Browser.TextField(datePickerNameFieldName);
			}
		}

		private string weightInputFieldName
		{
			get { return GetFieldNameOnModel<InputWeightModel>(m => m.Weight); }
		}

		private TextField weightInputField
		{
			get { return Browser.TextField(weightInputFieldName); }
		}
      
		[Test]
		public void shouldHaveDatePicker() {
			Assert.That(dateInputField.Exists, "No Textfield with name:" + datePickerNameFieldName + " exist on page");
		}

		[Test]
		public void shouldHaveTodaysDateInDatePicker() {
			Assert.That(dateInputField.Text, Is.EqualTo(DateTime.Now.ToShortDateString()), "Todays date should be in datepicker");
		}

		[Test]
		public void shouldHaveWeightInputField() {
			Assert.That(weightInputField.Exists, "No Textfield with name:" + weightInputFieldName + " exist on page");
		}

		[Test]
		public void shouldSaveWeight() {
			var submittedWeight = 80M;

			weightInputField.TypeText(submittedWeight.ToString());
			submitButton.Click();

			reloadPage();

			Assert.That(Convert.ToDecimal(weightInputField.Text), Is.EqualTo(submittedWeight));
		}

		[Test]
		public void shouldShowErrorWhenTryingToSaveZeroAsWeight() {
			weightInputField.TypeText(0.ToString());
			submitButton.Click();
			var element = Browser.Element(Find.ByText(WeightConstant.ZeroWeightErrorMessage));

			Assert.That(element.Exists, "No error message found on Page");
		}


		[Test]
		public void shouldShowWeightForDateOnPage() {
			const decimal originalWeight = 80M;
			const string newDate = "2023-10-10";
			const decimal newWeight = 75M;

			weightInputField.TypeText(originalWeight.ToString());
			submitButton.Click();

			reloadPage();
			
			dateInputField.TypeText(newDate);
			weightInputField.TypeText(newWeight.ToString());
         submitButton.Click();

			reloadPage();
			Assert.That(Convert.ToDecimal(weightInputField.Text), Is.EqualTo(originalWeight), "Should be " + originalWeight + "kg today");

			dateInputField.TypeText(newDate);
			Assert.That(Convert.ToDecimal(weightInputField.Text), Is.EqualTo(newWeight), "Should be " + newWeight + "kg at: "+ newDate + " date");
		}
	}
}