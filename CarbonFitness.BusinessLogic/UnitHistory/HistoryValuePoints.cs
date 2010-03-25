using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CarbonFitness.BusinessLogic.UnitHistory {
	public class HistoryValuePoints : IHistoryValuePoints {
		private readonly List<ValuePoint> valuePoints = new List<ValuePoint>();
		private HistoryValuePointsEnumerator historyValuePointsEnumerator;

		public HistoryValuePoints(Dictionary<DateTime, decimal> values) {
			foreach (var kv in values) {
				valuePoints.Add(new ValuePoint { Date = kv.Key, Value = kv.Value });
			}
		}

		public HistoryValuePoints(Dictionary<DateTime, decimal> values, string title) : this (values){
			Title = title;
		}

		public HistoryValuePoints() {}

		public string Title { get; protected set; }

		public IEnumerator<ValuePoint> GetEnumerator() {
			if (historyValuePointsEnumerator == null) {
				historyValuePointsEnumerator = new HistoryValuePointsEnumerator(this);
			}
			return historyValuePointsEnumerator;
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}

		public ValuePoint[] ValuesPoint {
			get {
				var values = new List<ValuePoint>();
				foreach (var historyValue in this) {
					values.Add(historyValue);
				}
				return values.ToArray();
			}
		}

		public ValuePoint GetValue(DateTime date) {
			if (date < GetFirstDate() || date > GetLastDate()) {
				throw new IndexOutOfRangeException("Date was:" + date + " First date is:" + GetFirstDate() + " Last date is:" + GetLastDate());
			}
			var historyValue = getActualHistoryValue(date);

			if (historyValue == null) {
				historyValue = GetCalculatedHistoryValue(date);
			}

			return getHistoryValueWithIndex(date, historyValue);
		}

		private ValuePoint getActualHistoryValue(DateTime date) {
			return valuePoints.Where(x => x.Date == date).FirstOrDefault();
		}

		private ValuePoint getHistoryValueWithIndex(DateTime date, ValuePoint valuePoint) {
			valuePoint.Index = getIndexForHistoryValue(date);
			return valuePoint;
		}

		private int getIndexForHistoryValue(DateTime date) {
			return (int)date.Subtract(GetFirstDate()).TotalDays;
		}

		private ValuePoint GetCalculatedHistoryValue(DateTime date) {
			var numberOfDaysFromPreviousValue = GetNumberOfDaysFromPreviousValue(date);
			return new ValuePoint { Date = date, Value = GetPreviousHistoryValue(date).Value - GetAverageDifferencePerDayBetweenActualValues(date) * numberOfDaysFromPreviousValue};
		}

		private int GetNumberOfDaysFromPreviousValue(DateTime date) {
			return (int) date.Subtract(GetPreviousHistoryValue(date).Date).TotalDays;
		}

		public ValuePoint GetPreviousHistoryValue(DateTime date) {
			if (date <= GetFirstDate()) {
				return null;
			}
			return valuePoints.Where(x => x.Date < date).OrderByDescending(x => x.Date).First();
		}

		public ValuePoint GetNextHistoryValue(DateTime date) {
			return valuePoints.Where(x => x.Date > date).OrderBy(x => x.Date).First();
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
			return valuePoints.Min(x => x.Date);
		}

		internal DateTime GetLastDate() {
			return valuePoints.Max(x => x.Date);
		}
	}
}