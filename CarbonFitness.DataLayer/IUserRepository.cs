using CarbonFitness.Data.Model;
using SharpArch.Core.PersistenceSupport.NHibernate;

namespace CarbonFitness.DataLayer {
	public interface IUserRepository : INHibernateRepositoryWithTypedId<User, int> {
		User Get(string username);
	}
}