using CarbonFitness.Model;
using SharpArch.Core.PersistenceSupport.NHibernate;
using SharpArch.Data.NHibernate;

namespace CarbonFitness.Repository {
    public class UserRepository : NHibernateRepositoryWithTypedId<User, int>, IUserRepository
    {
        public User Get(string userName) {
            return null;
        }
    }
}