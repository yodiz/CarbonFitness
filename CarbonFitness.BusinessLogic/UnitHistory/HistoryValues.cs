using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CarbonFitness.BusinessLogic.UnitHistory {
	public class HistoryValues : IHistoryValues {
		private readonly List<HistoryValue> historyValues = new List<HistoryValue>();
		private HistoryValuesEnumerator historyValuesEnumerator;

		public HistoryValues(Dictionary<DateTime, decimal> values) {
			foreach (var kv in values) {
				historyValues.Add(new HistoryValue { Date = kv.Key, Value = kv.Value });
			}
		}

		public HistoryValues(Dictionary<DateTime, decimal> values, string title) : this (values){
			Title = title;
		}

		public HistoryValues() {}

		public string Title { get; protected set; }

		public IEnumerator<HistoryValue> GetEnumerator() {
			if (historyValuesEnumerator == null) {
				historyValuesEnumerator = new HistoryValuesEnumerator(this);
			}
			return historyValuesEnumerator;
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}

		public HistoryValue[] Values {
			get {
				var values = new List<HistoryValue>();
				foreach (var historyValue in this) {
					values.Add(historyValue);
				}
				return values.ToArray();
			}
		}

		public HistoryValue GetValue(DateTime date) {
			if (date < GetFirstDate() || date > GetLastDate()) {
				throw new IndexOutOfRangeException("Date was:" + date + " First date is:" + GetFirstDate() + " Last date is:" + GetLastDate());
			}
			var historyValue = getActualHistoryValue(date);

			if (historyValue == null) {
				historyValue = GetCalculatedHistoryValue(date);
			}

			return getHistoryValueWithIndex(date, historyValue);
		}

		private HistoryValue getActualHistoryValue(DateTime date) {
			return historyValues.Where(x => x.Date == date).FirstOrDefault();
		}

		private HistoryValue getHistoryValueWithIndex(DateTime date, HistoryValue historyValue) {
			historyValue.Index = getIndexForHistoryValue(date);
			return historyValue;
		}

		private int getIndexForHistoryValue(DateTime date) {
			return (int)date.Subtract(GetFirstDate()).TotalDays;
		}

		private HistoryValue GetCalculatedHistoryValue(DateTime date) {
			var numberOfDaysFromPreviousValue = GetNumberOfDaysFromPreviousValue(date);
			return new HistoryValue { Date = date, Value = GetPreviousHistoryValue(date).Value - GetAverageDifferencePerDayBetweenActualValues(date) * numberOfDaysFromPreviousValue};
		}

		private int GetNumberOfDaysFromPreviousValue(DateTime date) {
			return (int) date.Subtract(GetPreviousHistoryValue(date).Date).TotalDays;
		}

		public HistoryValue GetPreviousHistoryValue(DateTime date) {
			if (date <= GetFirstDate()) {
				return null;
			}
			return historyValues.Where(x => x.Date < date).OrderByDescending(x => x.Date).First();
		}

		public HistoryValue GetNextHistoryValue(DateTime date) {
			return historyValues.Where(x => x.Date > date).OrderBy(x => x.Date).First();
		}

		public int GetNumberOfDaysBetweenDates(DateTime first, DateTime second) {
			return (int) second.Subtract(first).TotalDays;
		}

		public decimal GetAverageDifferencePerDayBetweenActualValues(DateTime date) {
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
}