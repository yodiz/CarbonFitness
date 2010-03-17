using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CarbonFitness.BusinessLogic {
	public class HistoryValue {
		public double Value { get; set; }
		public DateTime Date { get; set; }
	}

	public interface IHistoryValues : IEnumerable<HistoryValue> {
		HistoryValue GetValue(DateTime date);
	}

	public class HistoryValues : IHistoryValues {
		private readonly List<HistoryValue> historyValues = new List<HistoryValue>();
		private HistoryValuesEnumerator historyValuesEnumerator;

		public HistoryValues(Dictionary<DateTime, double> values) {
			foreach (var kv in values) {
				historyValues.Add(new HistoryValue {Date = kv.Key, Value = kv.Value});
			}
		}

		public HistoryValues() {}

		public IEnumerator<HistoryValue> GetEnumerator() {
			if (historyValuesEnumerator == null) {
				historyValuesEnumerator = new HistoryValuesEnumerator(this);
			}
			return historyValuesEnumerator;
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}

		public HistoryValue GetValue(DateTime date) {
			if (date < GetFirstDate() || date > GetLastDate()) {
				throw new IndexOutOfRangeException("Date was:" + date + "First date is:" + GetFirstDate() + " Last date is:" + GetLastDate());
			}
			var historyValue = getActualHistoryValue(date);

			if (historyValue == null) {
				historyValue = GetCalculatedHistoryValue(date);
			}

			return historyValue;
		}

		private HistoryValue getActualHistoryValue(DateTime date) {
			return historyValues.Where(x => x.Date == date).FirstOrDefault();
		}

		private HistoryValue GetCalculatedHistoryValue(DateTime date) {
			var numberOfDaysFromPreviousValue = GetNumberOfDaysFromPreviousValue(date);
			return new HistoryValue {Date = date, Value = GetPreviousHistoryValue(date).Value - GetAverageDifferencePerDayBetweenActualValues(date) * numberOfDaysFromPreviousValue};
		}

		private int GetNumberOfDaysFromPreviousValue(DateTime date) {
			return (int) date.Subtract(GetPreviousHistoryValue(date).Date).TotalDays;
		}

		public HistoryValue GetPreviousHistoryValue(DateTime date) {
			return historyValues.Where(x => x.Date < date).OrderByDescending(x => x.Date).First();
		}

		public HistoryValue GetNextHistoryValue(DateTime date) {
			return historyValues.Where(x => x.Date > date).OrderBy(x => x.Date).First();
		}

		public int GetNumberOfDaysBetweenDates(DateTime first, DateTime second) {
			return (int) second.Subtract(first).TotalDays;
		}

		public double GetAverageDifferencePerDayBetweenActualValues(DateTime date) {
			var previousValue = GetPreviousHistoryValue(date);
			var nextValue = GetNextHistoryValue(date);
			var numberOfDays = GetNumberOfDaysBetweenDates(previousValue.Date, nextValue.Date);
			var totalDifference = previousValue.Value - nextValue.Value;

			return totalDifference / numberOfDays;
		}

		internal DateTime GetFirstDate() {
			return historyValues.Min(x => x.Date);
		}

		internal DateTime GetLastDate() {
			return historyValues.Max(x => x.Date);
		}
	}

	public class HistoryValuesEnumerator : IEnumerator<HistoryValue> {
		private DateTime currentDate;
		private HistoryValues historyValues;

		internal HistoryValuesEnumerator(HistoryValues historyValues) {
			this.historyValues = historyValues;
		}

		public void Dispose() {}

		public bool MoveNext() {
			if (currentDate == DateTime.MinValue) {
				Reset();
				return true;
			}

			if (currentDate >= historyValues.GetLastDate()) {
				return false;
			}
			currentDate = currentDate.AddDays(1);
			return true;
		}

		public void Reset() {
			currentDate = historyValues.GetFirstDate();
		}

		public HistoryValue Current { get { return historyValues.GetValue(currentDate); } }

		object IEnumerator.Current { get { return Current; } }
	}
}