using CarbonFitness.Data.Model;
using SharpArch.Core.PersistenceSupport.NHibernate;

namespace CarbonFitness.DataLayer.Repository {
    public interface IUserProfileRepository : INHibernateRepositoryWithTypedId<UserProfile, int> {
        UserProfile GetByUserId(int userId);
    }
}