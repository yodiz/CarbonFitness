using CarbonFitness.Data.Model;
using SharpArch.Core.PersistenceSupport.NHibernate;

namespace CarbonFitness.DataLayer.Repository {
    public interface IActivityLevelTypeRepository : INHibernateRepositoryWithTypedId<ActivityLevelType, int> {
        ActivityLevelType GetByName(string name);
    }
}