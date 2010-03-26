using System.Globalization;
using AutoMapper;
using CarbonFitness.App.Web.Models;
using CarbonFitness.BusinessLogic.UnitHistory;

namespace CarbonFitness.App.Web {
	public static class AutoMappingsBootStrapper {
		public static void MapAll() {
			MapHistoryGraphToAmChartData();
		}

		public static void MapHistoryGraphToAmChartData() {
			Mapper.CreateMap<Label, value>()
				.ForMember(x => x.Value, y => y.MapFrom(z => z.Value))
				.ForMember(x => x.xid, y => y.MapFrom(z => z.Index));

			Mapper.CreateMap<ValuePoint, value>()
				.ForMember(x => x.Value, y => y.MapFrom(z => z.Value.ToString(CultureInfo.InvariantCulture)))
				.ForMember(x => x.xid, y => y.MapFrom(z => z.Index));

			Mapper.CreateMap<ILine, chartGraphsGraph>()
				.ForMember(x => x.values, y => y.MapFrom(z => z.GetValuePoints()))
				.ForMember(x => x.gid, y => y.MapFrom(z => z.Title));

			//Mapper.CreateMap<Line, chartGraphsGraph>()
			//.ForMember(x => x.values, y => y.MapFrom(z => z.GetValuePoints))
			//.ForMember(x => x.gid, y => y.MapFrom(z => z.Title));

			Mapper.CreateMap<LinesContainer, chartGraphs>()
				.ForMember(x => x.Graphs, y => y.MapFrom(z => z.Lines));

			Mapper.CreateMap<Graph, AmChartData>()
				.ForMember(x => x.GraphRoot, y => y.MapFrom(z => z.LinesContainer))
				.ForMember(x => x.DataPoints, y => y.MapFrom(z => z.Labels));
		}
	}
}