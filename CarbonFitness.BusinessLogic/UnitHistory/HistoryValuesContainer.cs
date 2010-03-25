using System.Collections.Generic;
using System.Linq;

namespace CarbonFitness.BusinessLogic.UnitHistory {
	public class HistoryValuesContainer {
		public strangeObject[] labels;

		//public IHistoryValues[] HistoryValueses;
		public UnnecessaryContainer unnecessaryContainer;
		public HistoryValuesContainer() {}

		public HistoryValuesContainer(IHistoryValues historyValues) {
			IList<strangeObject> strangeObjects = new List<strangeObject>();
			foreach (var historyValue in historyValues) {
				strangeObjects.Add(new strangeObject {value = historyValue.Date.ToShortDateString(), xid=strangeObjects.Count.ToString() });
			}
			labels = strangeObjects.ToArray();

			//HistoryValueses = new[] {historyValues};
			unnecessaryContainer = new UnnecessaryContainer { HistoryValueses = new[] { historyValues } };
		}
	}
}