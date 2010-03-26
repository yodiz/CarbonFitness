using System;
using System.Collections.Generic;
using System.Linq;

namespace CarbonFitness.BusinessLogic.UnitHistory {
	public class GraphBuilder : IGraphBuilder {
		public Graph GetGraph(params ILine[] lines) {

			lines = RemoveEmptyLines(lines);
			
			return new Graph {
				LinesContainer = new LinesContainer {Lines = lines} ,
				Labels = getLabels(lines)
			};
		}

		private Label[] getLabels(ILine[] lines) {
			IList<Label> labels = new List<Label>();

			var firstDate = lines.Min(x => x.GetFirstDate()).Date;
			var lastDate = lines.Max(x => x.GetLastDate()).Date;

			for (var d = firstDate; d <= lastDate; d = d.AddDays(1)) {
				labels.Add(new Label {
					Index = d.Subtract(firstDate).TotalDays.ToString(),
					Value = d.ToShortDateString()
				});
			}
			return labels.ToArray();
		}

		public ILine[] RemoveEmptyLines(ILine[] lines) {
			var result = new List<ILine>();
			foreach (var line in lines) {
				if(!line.IsEmpty) {
					result.Add(line);
				}
			}
			return result.ToArray();
		}
	}
}