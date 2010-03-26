using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CarbonFitness.BusinessLogic.Exceptions;

namespace CarbonFitness.BusinessLogic.UnitHistory {
	public class Line : ILine {
		private readonly List<ValuePoint> valuePoints = new List<ValuePoint>();
		private LineEnumerator lineEnumerator;

		public Line(Dictionary<DateTime, decimal> values) {
			foreach (var kv in values) {
				valuePoints.Add(new ValuePoint {Date = kv.Key, Value = kv.Value});
			}
		}

		public Line(Dictionary<DateTime, decimal> values, string title) : this(values) {
			Title = title;
		}

		public Line() {}

		public string Title { get; protected set; }

		public IEnumerator<ValuePoint> GetEnumerator() {
			if (lineEnumerator == null) {
				lineEnumerator = new LineEnumerator(this);
			}
			return lineEnumerator;
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}

		public ValuePoint[] GetValuePoints() {
			if (IsEmpty) {
				throw new NoValuesOnLineException();
			}
			var values = new List<ValuePoint>();
			foreach (var valuePoint in this) {
				values.Add(valuePoint);
			}
			return values.ToArray();
		}

		public bool IsEmpty { get { return valuePoints.Count == 0; } }

		public ValuePoint GetValue(DateTime date) {
			if (IsEmpty) {
				throw new NoValuesOnLineException();
			}

			if (date < GetFirstDate() || date > GetLastDate()) {
				throw new IndexOutOfRangeException("Date was:" + date + " First date is:" + GetFirstDate() + " Last date is:" + GetLastDate());
			}
			var valuePoint = getActualValuePoint(date);

			if (valuePoint == null) {
				valuePoint = GetCalculatedValuePoint(date);
			}

			return getValuePointWithIndex(date, valuePoint);
		}

		public DateTime GetFirstDate() {
			if (IsEmpty) {
				throw new NoValuesOnLineException();
			}
			return valuePoints.Min(x => x.Date);
		}

		public DateTime GetLastDate() {
			if (IsEmpty) {
				throw new NoValuesOnLineException();
			}
			return valuePoints.Max(x => x.Date);
		}

		private ValuePoint getActualValuePoint(DateTime date) {
			return valuePoints.Where(x => x.Date == date).FirstOrDefault();
		}

		private ValuePoint getValuePointWithIndex(DateTime date, ValuePoint valuePoint) {
			valuePoint.Index = getIndexForValuePoint(date);
			return valuePoint;
		}

		private int getIndexForValuePoint(DateTime date) {
			return (int) date.Subtract(GetFirstDate()).TotalDays;
		}

		private ValuePoint GetCalculatedValuePoint(DateTime date) {
			var numberOfDaysFromPreviousValue = GetNumberOfDaysFromPreviousValue(date);
			return new ValuePoint {Date = date, Value = GetPreviousValuePoint(date).Value - GetAverageDifferencePerDayBetweenActualValues(date) * numberOfDaysFromPreviousValue};
		}

		private int GetNumberOfDaysFromPreviousValue(DateTime date) {
			return (int) date.Subtract(GetPreviousValuePoint(date).Date).TotalDays;
		}

		public ValuePoint GetPreviousValuePoint(DateTime date) {
			if (date <= GetFirstDate()) {
				return null;
			}
			return valuePoints.Where(x => x.Date < date).OrderByDescending(x => x.Date).First();
		}

		public ValuePoint GetNextValuePoint(DateTime date) {
			return valuePoints.Where(x => x.Date > date).OrderBy(x => x.Date).First();
		}

		public int GetNumberOfDaysBetweenDates(DateTime first, DateTime second) {
			return (int) second.Subtract(first).TotalDays;
		}

		public decimal GetAverageDifferencePerDayBetweenActualValues(DateTime date) {
			var previousValue = GetPreviousValuePoint(date);
			var nextValue = GetNextValuePoint(date);
			var numberOfDays = GetNumberOfDaysBetweenDates(previousValue.Date, nextValue.Date);
			var totalDifference = previousValue.Value - nextValue.Value;

			return totalDifference / numberOfDays;
		}
	}
}