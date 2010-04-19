using CarbonFitness.BusinessLogic;

namespace CarbonFitness.AppLogic {
    public class InitialDataValuesExportEngine : IInitialDataValuesExportEngine {
        private readonly INutrientBusinessLogic nutrientBusinessLogic;
        private readonly IGenderTypeBusinessLogic genderTypeBusinessLogic;
        private readonly IActivityLevelTypeBusinessLogic activityLevelTypeBusinessLogic;

        public InitialDataValuesExportEngine(INutrientBusinessLogic nutrientBusinessLogic, IGenderTypeBusinessLogic genderTypeBusinessLogic, IActivityLevelTypeBusinessLogic activityLevelTypeBusinessLogic) {
            this.activityLevelTypeBusinessLogic = activityLevelTypeBusinessLogic;
            this.nutrientBusinessLogic = nutrientBusinessLogic;
            this.genderTypeBusinessLogic = genderTypeBusinessLogic;
        }

        public void Export() {
          nutrientBusinessLogic.ExportInitialValues();
          genderTypeBusinessLogic.ExportInitialValues();
          activityLevelTypeBusinessLogic.ExportInitialValues();
        }
    }
}