using System.Collections.Generic;
using CarbonFitness.BusinessLogic;
using CarbonFitness.BusinessLogic.RDI.Importers;

namespace CarbonFitness.AppLogic {
    public class NutrientRecommendationImportEngine : INutrientRecommendationImporter {
        private readonly List<INutrientRDIImporter> importers = new List<INutrientRDIImporter>();
        private readonly INutrientRecommendationBusinessLogic nutrientRecommendationBusinessLogic;

        public NutrientRecommendationImportEngine(INutrientRecommendationBusinessLogic nutrientRecommendationBusinessLogic) {
            this.nutrientRecommendationBusinessLogic = nutrientRecommendationBusinessLogic;
        }

        public void Import() {
            nutrientRecommendationBusinessLogic.ImportNutrientRecommendations(importers.ToArray());
        }

        public void AddImporter(INutrientRDIImporter importer) {
            importers.Add(importer);
        }
    }
}