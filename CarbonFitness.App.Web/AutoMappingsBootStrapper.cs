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
				.ForMember(x => x.Value, y => y.MapFrom(z => z.Value));

			Mapper.CreateMap<ValuePoint, value>()
				.ForMember(x => x.Value, y => y.MapFrom(z => z.Value.ToString(System.Globalization.CultureInfo.InvariantCulture)))
				.ForMember(x => x.xid, y => y.MapFrom(z => z.Index));

			Mapper.CreateMap<IHistoryValuePoints, chartGraphsGraph>()
				.ForMember(x => x.values, y => y.MapFrom(z => z.ValuesPoint))
				.ForMember(x => x.gid, y => y.MapFrom(z => z.Title));

			Mapper.CreateMap<UnnecessaryContainer, chartGraphs>()
				.ForMember(x => x.Graphs, y => y.MapFrom(z => z.HistoryValuesCollection));

			Mapper.CreateMap<HistoryValuesContainer, AmChartData>()
				.ForMember(x => x.GraphRoot, y => y.MapFrom(z => z.unnecessaryContainer))
				.ForMember(x => x.DataPoints, y => y.MapFrom(z => z.labels));
		}




	}
}