using CarbonFitness.BusinessLogic;

namespace CarbonFitness.AppLogic {
    public class InitialDataValuesExportEngine : IInitialDataValuesExportEngine {
        private readonly INutrientBusinessLogic nutrientBusinessLogic;

        public InitialDataValuesExportEngine(INutrientBusinessLogic nutrientBusinessLogic) {
            this.nutrientBusinessLogic = nutrientBusinessLogic;
        }

        public void Export() {
          nutrientBusinessLogic.Export();
        }
    }
}