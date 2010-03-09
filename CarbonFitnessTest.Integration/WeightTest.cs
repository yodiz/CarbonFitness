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

		[Test]
		public void shouldHaveWeightInputField() {
			var weightInput = GetFieldNameOnModel<InputWeightModel>(m => m.Weight);
			Assert.That(Browser.TextField(weightInput).Exists, "No Textfield with name:" + weightInput + " exist on page");
		}

		[Test]
		public void shouldHaveDatePicker() {
			var datePickerName = GetFieldNameOnModel<InputWeightModel>(m => m.Date);
			Assert.That(Browser.TextField(datePickerName).Exists, "No Textfield with name:" + datePickerName + " exist on page");
		}

		[Test]
		public void shouldHaveTodaysDateInDatePicker() {
			var datePickerName = GetFieldNameOnModel<InputWeightModel>(m => m.Date);

			Assert.That(Browser.TextField(datePickerName).Text, Is.EqualTo(DateTime.Now.ToShortDateString()), "Todays date should be in datepicker");
		}

		[Test]
		public void shouldSaveWeight() {
			var weightInputField = GetFieldNameOnModel<InputWeightModel>(m => m.Weight);
			var submittedWeight = 80;

			Browser.TextField(weightInputField).TypeText(submittedWeight.ToString());
			Browser.Button(Find.By("type", "submit")).Click();

			Browser.Link(Find.ByText(SiteMasterConstant.WeightLinkText)).Click();

			Assert.That(Browser.TextField(weightInputField).Text, Is.EqualTo(submittedWeight));
		}
	}
}