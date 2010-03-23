using AutoMapper;
using CarbonFitness.App.Web.Models;
using CarbonFitness.BusinessLogic;

namespace CarbonFitness.App.Web {
	public static class AutoMappingsBootStrapper {
		public static void MapAll() {
			MapHistoryValuesContainerToAmChartData();
		}

		public static void MapHistoryValuesContainerToAmChartData() {
			Mapper.CreateMap<strangeObject, value>()
				.ForMember(x => x.Value, y => y.MapFrom(z => z.value));

			Mapper.CreateMap<HistoryValue, value>()
				.ForMember(x => x.Value, y => y.MapFrom(z => z.Value))
				.ForMember(x => x.xid, y => y.MapFrom(z => z.Date));

			Mapper.CreateMap<IHistoryValues, chartGraphsGraph>()
				.ForMember(x => x.values, y => y.MapFrom(z => z.Values))
				.ForMember(x => x.gid, y => y.MapFrom(z => z.Title));

			Mapper.CreateMap<UnnecessaryContainer, chartGraphs>()
				.ForMember(x => x.Graphs, y => y.MapFrom(z => z.HistoryValueses));

			Mapper.CreateMap<HistoryValuesContainer, AmChartData>()
				.ForMember(x => x.GraphRoot, y => y.MapFrom(z => z.unnecessaryContainer))
				.ForMember(x => x.DataPoints, y => y.MapFrom(z => z.labels));
		}
	}
}