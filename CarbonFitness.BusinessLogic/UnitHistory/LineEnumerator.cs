using System;
using System.Collections;
using System.Collections.Generic;

namespace CarbonFitness.BusinessLogic.UnitHistory {
	public class LineEnumerator : IEnumerator<ValuePoint> {
		private DateTime currentDate;
		private Line line;

		internal LineEnumerator(Line line) {
			this.line = line;
		}

		public void Dispose() {
			Reset();
		}

		public bool MoveNext() {
			if (currentDate == DateTime.MinValue) {
				currentDate = line.GetFirstDate();
				return true;
			}

			if (currentDate.Date >= line.GetLastDate().Date) {
				return false;
			}
			currentDate = currentDate.AddDays(1);
			return true;
		}

		public void Reset() {
			currentDate = DateTime.MinValue;
		}

		public ValuePoint Current { get { return line.GetValue(currentDate); } }

		object IEnumerator.Current { get { return Current; } }
	}
}