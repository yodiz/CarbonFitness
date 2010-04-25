using Autofac;
using CarbonFitness.BusinessLogic.RDI.Importers;

namespace CarbonFitness.AppLogic {
    public interface INutrientRecommendationImporter {
        void Import();
        void AddImporter(INutrientRDIImporter importer);
    }
}