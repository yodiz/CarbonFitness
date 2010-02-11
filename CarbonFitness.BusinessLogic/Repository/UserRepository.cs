using System.Linq;
using CarbonFitness.Model;
using SharpArch.Core.PersistenceSupport.NHibernate;
using SharpArch.Data.NHibernate;
using NHibernate.Linq;

namespace CarbonFitness.Repository
{
	 public class UserRepository : NHibernateRepositoryWithTypedId<User, int>, IUserRepository
	 {
		  public User Get(string username)
		  {
				var q = from user in Session.Linq<User>()
						  where user.Username.Equals(username)
						  select user;

				return q.FirstOrDefault();
		  }
	 }
}