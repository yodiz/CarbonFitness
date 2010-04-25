using CarbonFitness.BusinessLogic.RDI.Importers;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;

namespace CarbonFitness.BusinessLogic.Implementation {
    public class NutrientRecommendationBusinessLogic : INutrientRecommendationBusinessLogic {
        private readonly INutrientBusinessLogic nutrientBusinessLogic;
        private readonly INutrientRecommendationRepository nutrientRecommendationRepository;

        public NutrientRecommendationBusinessLogic(INutrientBusinessLogic nutrientBusinessLogic, INutrientRecommendationRepository nutrientRecommendationRepository)
        {
            this.nutrientBusinessLogic = nutrientBusinessLogic;
            this.nutrientRecommendationRepository = nutrientRecommendationRepository;
        }

        public NutrientRecommendation GetNutrientRecommendation(NutrientEntity nutrientEntity) {
            Nutrient nutrient = nutrientBusinessLogic.GetNutrient(nutrientEntity);
            return nutrientRecommendationRepository.GetByNutrient(nutrient);
        }

        public void ImportNutrientRecommendations(params INutrientRDIImporter[] nutrientRDIImporters) {
            if(nutrientRDIImporters != null) {
                foreach (var importer in nutrientRDIImporters) {
                    importer.Import(nutrientRecommendationRepository);
                }
            }
        }
    }
}