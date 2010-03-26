using System.Collections.Generic;
using System.Linq;

namespace CarbonFitness.BusinessLogic.UnitHistory {
	public class Graph {
		public Label[] Labels;

		//public IHistoryValues[] HistoryValueses;
		public LinesContainer LinesContainer;
		public Graph() {}

		public Graph(ILine line) {
			IList<Label> result = new List<Label>();
			foreach (var valuePoint in line) {
				result.Add(new Label {Value = valuePoint.Date.ToShortDateString(), Index=result.Count.ToString() });
			}
			Labels = result.ToArray();

			LinesContainer = new LinesContainer { Lines = new[] { line } };
		}
	}
}