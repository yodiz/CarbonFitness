using System;
using System.Collections;
using System.Collections.Generic;

namespace CarbonFitness.BusinessLogic.UnitHistory {
	public class HistoryValuePointsEnumerator : IEnumerator<ValuePoint> {
		private DateTime currentDate;
		private HistoryValuePoints historyValuePoints;

		internal HistoryValuePointsEnumerator(HistoryValuePoints historyValuePoints) {
			this.historyValuePoints = historyValuePoints;
		}

		public void Dispose() {
			Reset();
		}

		public bool MoveNext() {
			if (currentDate == DateTime.MinValue) {
				currentDate = historyValuePoints.GetFirstDate();
				return true;
			}

			if (currentDate.Date >= historyValuePoints.GetLastDate().Date) {
				return false;
			}
			currentDate = currentDate.AddDays(1);
			return true;
		}

		public void Reset() {
			currentDate = DateTime.MinValue;
		}

		public ValuePoint Current { get { return historyValuePoints.GetValue(currentDate); } }

		object IEnumerator.Current { get { return Current; } }
	}
}