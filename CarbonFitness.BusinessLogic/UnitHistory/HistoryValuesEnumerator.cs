using System;
using System.Collections;
using System.Collections.Generic;

namespace CarbonFitness.BusinessLogic.UnitHistory {
	public class HistoryValuesEnumerator : IEnumerator<HistoryValue> {
		private DateTime currentDate;
		private HistoryValues historyValues;

		internal HistoryValuesEnumerator(HistoryValues historyValues) {
			this.historyValues = historyValues;
		}

		public void Dispose() {
			Reset();
		}

		public bool MoveNext() {
			if (currentDate == DateTime.MinValue) {
				currentDate = historyValues.GetFirstDate();
				return true;
			}

			if (currentDate.Date >= historyValues.GetLastDate().Date) {
				return false;
			}
			currentDate = currentDate.AddDays(1);
			return true;
		}

		public void Reset() {
			currentDate = DateTime.MinValue;
		}

		public HistoryValue Current { get { return historyValues.GetValue(currentDate); } }

		object IEnumerator.Current { get { return Current; } }
	}
}