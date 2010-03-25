using System.Collections.Generic;
using System.Linq;

namespace CarbonFitness.BusinessLogic.UnitHistory {
	public class HistoryValuesContainer {
		public Label[] labels;

		//public IHistoryValues[] HistoryValueses;
		public UnnecessaryContainer unnecessaryContainer;
		public HistoryValuesContainer() {}

		public HistoryValuesContainer(IHistoryValuePoints historyValuePoints) {
			IList<Label> strangeObjects = new List<Label>();
			foreach (var historyValue in historyValuePoints) {
				strangeObjects.Add(new Label {Value = historyValue.Date.ToShortDateString(), Xid=strangeObjects.Count.ToString() });
			}
			labels = strangeObjects.ToArray();

			//HistoryValueses = new[] {historyValues};
			unnecessaryContainer = new UnnecessaryContainer { HistoryValuesCollection = new[] { historyValuePoints } };
		}
	}
}