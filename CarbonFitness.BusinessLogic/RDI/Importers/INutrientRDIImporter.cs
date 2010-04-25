using CarbonFitness.DataLayer.Repository;

namespace CarbonFitness.BusinessLogic.RDI.Importers {
    public interface INutrientRDIImporter {
        void Import(INutrientRecommendationRepository nutrientRecommendationRepository);
    }
}