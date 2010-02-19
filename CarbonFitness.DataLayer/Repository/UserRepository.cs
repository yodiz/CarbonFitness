using System.Linq;
using CarbonFitness.Data;
using CarbonFitness.Data.Model;
using NHibernate.Linq;
using SharpArch.Data.NHibernate;

namespace CarbonFitness.DataLayer.Repository {
    public class UserRepository : NHibernateRepositoryWithTypedId<User, int>, IUserRepository {
        public User Get(string username) {
            IQueryable<User> q = from user in Session.Linq<User>()
                                 where user.Username.Equals(username)
                                 select user;

            return q.FirstOrDefault();
        }
    }
}