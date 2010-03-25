using System;
using System.Collections.Generic;

namespace CarbonFitness.BusinessLogic.UnitHistory {
	public interface ILine : IEnumerable<ValuePoint> {
		ValuePoint[] ValuesPoint { get; }
		ValuePoint GetValue(DateTime date);
		string Title { get; }
	}
}