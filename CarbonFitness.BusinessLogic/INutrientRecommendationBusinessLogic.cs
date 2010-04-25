using CarbonFitness.BusinessLogic.RDI.Importers;
using CarbonFitness.Data.Model;

namespace CarbonFitness.BusinessLogic {
    public interface INutrientRecommendationBusinessLogic {
        NutrientRecommendation GetNutrientRecommendation(NutrientEntity nutrientEntity);
        void ImportNutrientRecommendations(params INutrientRDIImporter[] nutrientRDIImporters);
    }
}