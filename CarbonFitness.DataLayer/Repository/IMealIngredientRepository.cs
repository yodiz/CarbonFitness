using CarbonFitness.Data.Model;
using SharpArch.Core.PersistenceSupport.NHibernate;

namespace CarbonFitness.DataLayer.Repository {
    public interface IMealIngredientRepository : INHibernateRepositoryWithTypedId<MealIngredient, int> {
    }
}