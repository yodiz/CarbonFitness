using CarbonFitness.Data.Model;
using SharpArch.Core.PersistenceSupport.NHibernate;

namespace CarbonFitness.DataLayer.Repository {
    public interface IGenderTypeRepository : INHibernateRepositoryWithTypedId<GenderType, int>{
        GenderType GetByName(string name);
    }
}