using System;
using System.Collections.Generic;

namespace CarbonFitness.BusinessLogic.UnitHistory {
	public interface ILine : IEnumerable<ValuePoint> {
		string Title { get; }

		bool IsEmpty { get; }

		ValuePoint[] GetValuePoints();

		ValuePoint GetValue(DateTime date);

		DateTime GetFirstDate();

		DateTime GetLastDate();
	}
}