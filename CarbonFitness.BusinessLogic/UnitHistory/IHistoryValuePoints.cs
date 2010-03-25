using System;
using System.Collections.Generic;

namespace CarbonFitness.BusinessLogic.UnitHistory {
	public interface IHistoryValuePoints : IEnumerable<ValuePoint> {
		ValuePoint[] ValuesPoint { get; }
		ValuePoint GetValue(DateTime date);
		string Title { get; }
	}
}