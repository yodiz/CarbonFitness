using CarbonFitness.Model;
using SharpArch.Core.PersistenceSupport.NHibernate;

namespace CarbonFitness.Repository {
    public interface IUserRepository : INHibernateRepositoryWithTypedId<User, int> {
        User Get(string userName);
    }
}