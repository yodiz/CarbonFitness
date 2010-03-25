using System.Collections.Generic;
using System.Linq;

namespace CarbonFitness.BusinessLogic.UnitHistory {
	public class Graph {
		public Label[] labels;

		//public IHistoryValues[] HistoryValueses;
		public Lines Lines;
		public Graph() {}

		public Graph(ILine line) {
			IList<Label> result = new List<Label>();
			foreach (var valuePoint in line) {
				result.Add(new Label {Value = valuePoint.Date.ToShortDateString(), Index=result.Count.ToString() });
			}
			labels = result.ToArray();

			Lines = new Lines { lines = new[] { line } };
		}
	}
}