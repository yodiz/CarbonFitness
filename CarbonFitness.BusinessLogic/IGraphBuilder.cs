using CarbonFitness.BusinessLogic.UnitHistory;
using CarbonFitness.Data.Model;

namespace CarbonFitness.BusinessLogic {
	public interface IGraphBuilder {
		Graph GetGraph(params ILine[] lines);
	}
}