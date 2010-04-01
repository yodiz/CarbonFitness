using CarbonFitness.BusinessLogic;

namespace CarbonFitness.AppLogic {
	public class SchemaExportEngine : ISchemaExportEngine {
		private readonly IBootStrapper bootStrapper;

		public SchemaExportEngine(IBootStrapper bootStrapper) {
			this.bootStrapper = bootStrapper;
		}

		public void Export() {
			bootStrapper.ExportDataBaseSchema();
		}
	}
}