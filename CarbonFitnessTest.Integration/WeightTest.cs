using System;
using CarbonFitness.App.Web.Models;
using CarbonFitness.App.Web.ViewConstants;
using CarbonFitnessTest.Util;
using NUnit.Framework;
using WatiN.Core;

namespace CarbonFitnessTest.Integration {
	[TestFixture]
	public class WeightTest : IntegrationBaseTest {
		public override void TestFixtureSetUp() {
			base.TestFixtureSetUp();
			new CreateUserTest(Browser).getUniqueUserId();
			new AccountLogOnTest(Browser).LogOn(CreateUserTest.UserName, CreateUserTest.Password);
		}

		private string firstDate;
		private decimal firstWeight;
		private decimal todayWeight;

		public override string Url { get { return getUrl("Weight", "Input"); } }

		private Button submitButton { get { return Browser.Button(Find.By("type", "submit")); } }

		private void reloadPage() {
			ReloadPage(SiteMasterConstant.WeightLinkText);
		}

		private string datePickerNameFieldName { get { return GetFieldNameOnModel<InputWeightModel>(m => m.Date); } }

		private TextField dateInputField { get { return Browser.TextField(datePickerNameFieldName); } }

		private string weightInputFieldName { get { return GetFieldNameOnModel<InputWeightModel>(m => m.Weight); } }
		private TextField weightInputField { get { return Browser.TextField(weightInputFieldName); } }

		private void setupUserWeightHistory() {
			todayWeight = ValueGenerator.getRandomInteger();
			firstDate = ValueGenerator.getRandomDate().ToShortDateString();
			firstWeight = ValueGenerator.getRandomInteger();

			weightInputField.TypeText(todayWeight.ToString());
			submitButton.Click();

			reloadPage();

			dateInputField.TypeText(firstDate);
			weightInputField.TypeText(firstWeight.ToString());
			submitButton.Click();

			reloadPage();
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
		public void shouldHaveWeightHistory() {
			setupUserWeightHistory();

			var fusionChartOriginalWeightValue = "<set value='" + todayWeight;
			var fusionChartFutureWeightValue = "<set value='" + firstWeight;
			Assert.That(Browser.Html.Contains(fusionChartOriginalWeightValue), "No original weight (" + todayWeight + ") found in graph");
			Assert.That(Browser.Html.Contains(fusionChartFutureWeightValue), "No new weight (" + firstWeight + ") found in graph");
		}

		[Test]
		public void shouldHaveWeightHistoryInDateOrder()
		{
			setupUserWeightHistory();

			var fusionChartOriginalWeightValue = "<set value='" + todayWeight;
			var fusionChartFutureWeightValue = "<set value='" + firstWeight;

			var html = Browser.Html;
			var originalIndex = html.IndexOf(fusionChartOriginalWeightValue);
			Assert.That(originalIndex, Is.GreaterThan(0), "No original weight (" + todayWeight + ") found in graph");

			html = html.Substring(0, originalIndex + fusionChartOriginalWeightValue.Length);
			Assert.That(html.Contains(fusionChartFutureWeightValue), "No new weight (" + firstWeight + ") found in graph");
		}

		[Test]
		public void shouldHaveWeightInputField() {
			Assert.That(weightInputField.Exists, "No Textfield with name:" + weightInputFieldName + " exist on page");
		}


		[Test]
		public void shouldSaveWeight() {
			var submittedWeight = (decimal) ValueGenerator.getRandomInteger();

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
			setupUserWeightHistory();

			Assert.That(Convert.ToDecimal(weightInputField.Text), Is.EqualTo(todayWeight), "Should be " + todayWeight + "kg today");

			dateInputField.TypeText(firstDate);

			Assert.That(Convert.ToDecimal(weightInputField.Text), Is.EqualTo(firstWeight), "Should be " + firstWeight + "kg at: " + firstDate + " date");
		}
	}
}