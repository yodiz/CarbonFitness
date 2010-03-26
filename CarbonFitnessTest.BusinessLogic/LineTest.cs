using System;
using System.Collections.Generic;
using System.Linq;
using CarbonFitness.BusinessLogic.Exceptions;
using CarbonFitness.BusinessLogic.UnitHistory;
using NUnit.Framework;

namespace CarbonFitnessTest.BusinessLogic {
	[TestFixture]
	public class LineTest {
		private Dictionary<DateTime, decimal> getTestValues() {
			return new Dictionary<DateTime, decimal> {
				{DateTime.Today.AddDays(-6), 500},
				{DateTime.Today.AddDays(-2), 100},
				{DateTime.Today, 300}
			};
		}

		[Test]
		public void ShouldGetAverageDifferencePerDay() {
			var historyValues = new Line(getTestValues());
			var result = historyValues.GetAverageDifferencePerDayBetweenActualValues(DateTime.Today.AddDays(-4));
			Assert.That(result, Is.EqualTo(100));
		}

		[Test]
		public void ShouldGetAverageHistoryValueForUndefinedDate() {
			var historyValues = new Line(getTestValues());

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
			var historyValues = new Line(getTestValues());
			var h = historyValues.GetNextValuePoint(DateTime.Today.AddDays(-4));
			Assert.That(h.Date, Is.EqualTo(DateTime.Today.AddDays(-2)));
		}

		[Test]
		public void ShouldGetNumberOfDaysBetweenDates() {
			var historyValues = new Line();
			var numberOfDaysBetweenValues = historyValues.GetNumberOfDaysBetweenDates(DateTime.Today.AddDays(-9), DateTime.Today);
			Assert.That(numberOfDaysBetweenValues, Is.EqualTo(9));
		}

		[Test]
		public void ShouldGetPreviousKeyValue() {
			var historyValues = new Line(getTestValues());
			var h = historyValues.GetPreviousValuePoint(DateTime.Today.AddDays(-1));
			Assert.That(h.Date, Is.EqualTo(DateTime.Today.AddDays(-2)));
		}

		[Test]
		public void shouldGetTitle() {
			const string expectedTitle = "titel";
			var historyValues = new Line(new Dictionary<DateTime, decimal>(), expectedTitle);
			Assert.That(historyValues.Title, Is.EqualTo(expectedTitle));
		}

		[Test]
		public void shouldHaveIndex() {
			var historyValuesDictonary = new Dictionary<DateTime, decimal>();
			historyValuesDictonary.Add(DateTime.Now.AddDays(-1), 123);
			historyValuesDictonary.Add(DateTime.Now.AddDays(-0), 123);
			historyValuesDictonary.Add(DateTime.Now.AddDays(1), 1234);

			var historyValues = new Line(historyValuesDictonary);
			Assert.That(historyValues.Count(), Is.EqualTo(3));
			var count = 0;
			foreach (var item in historyValues) {
				Assert.That(item.Index, Is.EqualTo(count));
				count++;
			}
		}

		[Test]
		public void shouldLoopSortedItems() {
			var historyValues = new Line(getTestValues());
			var first = DateTime.Today.AddDays(-6);
			var count = 0;

			foreach (var historyValue in historyValues) {
				Assert.That(historyValue.Value, Is.GreaterThan(99));
				Assert.That(historyValue.Date, Is.EqualTo(first));
				first = first.AddDays(1);
				count++;
			}
			Assert.That(count, Is.EqualTo(7));
		}

		[Test]
		public void shouldTellIfLineIsEmpty() {
			var emptyline = new Line();
			var lineWithElements = new Line(new Dictionary<DateTime, decimal> {{DateTime.Now, 123}});

			Assert.That(emptyline.IsEmpty, Is.True);
			Assert.That(lineWithElements.IsEmpty, Is.False);
		}

		[Test]
		public void shouldThrowWhenThereAreNoValuePoints() {
			ILine emptyline = new Line();

			Assert.Throws<NoValuesOnLineException>(() => emptyline.GetValuePoints());
			Assert.Throws<NoValuesOnLineException>(() => emptyline.GetValue(DateTime.Now));
			Assert.Throws<NoValuesOnLineException>(() => emptyline.GetFirstDate());
			Assert.Throws<NoValuesOnLineException>(() => emptyline.GetLastDate());
		}
	}
}