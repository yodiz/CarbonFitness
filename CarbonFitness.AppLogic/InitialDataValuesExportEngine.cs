using CarbonFitness.BusinessLogic;

namespace CarbonFitness.AppLogic {
    public class InitialDataValuesExportEngine : IInitialDataValuesExportEngine {
        private readonly INutrientBusinessLogic nutrientBusinessLogic;
        private readonly IGenderTypeBusinessLogic genderTypeBusinessLogic;

        public InitialDataValuesExportEngine(INutrientBusinessLogic nutrientBusinessLogic, IGenderTypeBusinessLogic genderTypeBusinessLogic) {
            this.nutrientBusinessLogic = nutrientBusinessLogic;
            this.genderTypeBusinessLogic = genderTypeBusinessLogic;
        }

        public void Export() {
          nutrientBusinessLogic.ExportInitialValues();
          genderTypeBusinessLogic.ExportInitialValues();
        }
    }
}