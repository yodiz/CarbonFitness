using System;
using System.Collections.Generic;

namespace CarbonFitness.BusinessLogic.UnitHistory {
	public interface IHistoryValues : IEnumerable<HistoryValue> {
		HistoryValue[] Values { get; }
		HistoryValue GetValue(DateTime date);
		string Title { get; }
	}
}