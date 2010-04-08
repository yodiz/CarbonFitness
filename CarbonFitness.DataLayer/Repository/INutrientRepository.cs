using CarbonFitness.Data.Model;
using SharpArch.Core.PersistenceSupport.NHibernate;

namespace CarbonFitness.DataLayer.Repository {
    public interface INutrientRepository : INHibernateRepositoryWithTypedId<Nutrient, int> {
        Nutrient GetByName(string name);
    }
}