using System;
using System.Collections.Generic;
using CarbonFitness.BusinessLogic.UnitHistory;
using NUnit.Framework;
using System.Linq;

namespace CarbonFitnessTest.BusinessLogic {
	[TestFixture]
	public class HistoryValuesTest {
		private Dictionary<DateTime, decimal> getTestValues() {
			return new Dictionary<DateTime, decimal> {
				{DateTime.Today.AddDays(-6), 500},
				{DateTime.Today.AddDays(-2), 100},
				{DateTime.Today, 300}
			};
		}

		[Test]
		public void ShouldGetAverageDifferencePerDay() {
			var historyValues = new HistoryValuePoints(getTestValues());
			var result = historyValues.GetAverageDifferencePerDayBetweenActualValues(DateTime.Today.AddDays(-4));
			Assert.That(result, Is.EqualTo(100));
		}

		[Test]
		public void shouldGetTitle() {
			const string expectedTitle = "titel";
			var historyValues = new HistoryValuePoints(new Dictionary<DateTime, decimal>(), expectedTitle);
			Assert.That(historyValues.Title, Is.EqualTo(expectedTitle));
		}

		[Test]
		public void ShouldGetAverageHistoryValueForUndefinedDate() {
			var historyValues = new HistoryValuePoints(getTestValues());

			Assert.That(historyValues.GetValue(DateTime.Today.AddDays(-6)).Value, Is.EqualTo(500D));
			Assert.That(historyValues.GetValue(DateTime.Today.AddDays(-5)).Value, Is.EqualTo(400D));
			Assert.That(historyValues.GetValue(DateTime.Today.AddDays(-4)).Value, Is.EqualTo(300D));
			Assert.That(historyValues.GetValue(DateTime.Today.AddDays(-3)).Value, Is.EqualTo(200D));
			Assert.That(historyValues.GetValue(DateTime.Today.AddDays(-2)).Value, Is.EqualTo(100D));
			Assert.That(historyValues.GetValue(DateTime.Today.AddDays(-1)).Value, Is.EqualTo(200D));
			Assert.That(historyValues.GetValue(DateTime.Today).Value, Is.EqualTo(300D));

			Assert.Throws<IndexOutOfRangeException>(() => historyValues.GetValue(DateTime.Today.AddDays(-7)));
			Assert.Throws<IndexOutOfRangeException>(() => historyValues.GetValue(DateTime.Today.AddDays(1)));
		}

		[Test]
		public void ShouldGetNextKeyValue() {
			var historyValues = new HistoryValuePoints(getTestValues());
			var h = historyValues.GetNextHistoryValue(DateTime.Today.AddDays(-4));
			Assert.That(h.Date, Is.EqualTo(DateTime.Today.AddDays(-2)));
		}

		[Test]
		public void ShouldGetNumberOfDaysBetweenDates() {
			var historyValues = new HistoryValuePoints();
			var numberOfDaysBetweenValues = historyValues.GetNumberOfDaysBetweenDates(DateTime.Today.AddDays(-9), DateTime.Today);
			Assert.That(numberOfDaysBetweenValues, Is.EqualTo(9));
		}

		[Test]
		public void ShouldGetPreviousKeyValue() {
			var historyValues = new HistoryValuePoints(getTestValues());
			var h = historyValues.GetPreviousHistoryValue(DateTime.Today.AddDays(-1));
			Assert.That(h.Date, Is.EqualTo(DateTime.Today.AddDays(-2)));
		}

		[Test]
		public void shouldLoopSortedItems() {
			var historyValues = new HistoryValuePoints(getTestValues());
			DateTime first = DateTime.Today.AddDays(-6);
			int count = 0;

			foreach (ValuePoint historyValue in historyValues) {
				Assert.That(historyValue.Value, Is.GreaterThan(99));
				Assert.That(historyValue.Date, Is.EqualTo(first));
				first = first.AddDays(1);
				count++;
			}
			Assert.That(count, Is.EqualTo(7));
		}

		[Test]
		public void shouldHaveIndex() {
			var historyValuesDictonary = new Dictionary<DateTime, decimal>();
			historyValuesDictonary.Add(DateTime.Now.AddDays(-1), 123);
			historyValuesDictonary.Add(DateTime.Now.AddDays(-0), 123);
			historyValuesDictonary.Add(DateTime.Now.AddDays(1), 1234);

			var historyValues = new HistoryValuePoints(historyValuesDictonary);
			Assert.That(historyValues.Count(), Is.EqualTo(3));
			int count = 0;
			foreach(var item in historyValues) {
				Assert.That(item.Index, Is.EqualTo(count));
				count++;
			}
		}
	}
}