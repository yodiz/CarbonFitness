using AutoMapper;
using CarbonFitness.App.Web.Models;
using CarbonFitness.BusinessLogic.UnitHistory;

namespace CarbonFitness.App.Web {
	public static class AutoMappingsBootStrapper {
		public static void MapAll() {
			MapHistoryValuesContainerToAmChartData();
		}

		public static void MapHistoryValuesContainerToAmChartData()
		{
			Mapper.CreateMap<Label, value>()
				.ForMember(x => x.Value, y => y.MapFrom(z => z.Value))
				.ForMember(x => x.xid, y => y.MapFrom(z => z.Index));

			Mapper.CreateMap<ValuePoint, value>()
				.ForMember(x => x.Value, y => y.MapFrom(z => z.Value.ToString(System.Globalization.CultureInfo.InvariantCulture)))
				.ForMember(x => x.xid, y => y.MapFrom(z => z.Index));

			Mapper.CreateMap<ILine, chartGraphsGraph>()
				.ForMember(x => x.values, y => y.MapFrom(z => z.ValuesPoint))
				.ForMember(x => x.gid, y => y.MapFrom(z => z.Title));

			Mapper.CreateMap<Lines, chartGraphs>()
				.ForMember(x => x.Graphs, y => y.MapFrom(z => z.lines));

			Mapper.CreateMap<Graph, AmChartData>()
				.ForMember(x => x.GraphRoot, y => y.MapFrom(z => z.Lines))
				.ForMember(x => x.DataPoints, y => y.MapFrom(z => z.labels));
		}




	}
}