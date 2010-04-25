using CarbonFitness.Data.Model;
using SharpArch.Core.PersistenceSupport.NHibernate;

namespace CarbonFitness.DataLayer.Repository {
    public interface INutrientRecommendationRepository : INHibernateRepositoryWithTypedId<NutrientRecommendation, int>{
        NutrientRecommendation GetByNutrient(Nutrient nutrient);
    }
}