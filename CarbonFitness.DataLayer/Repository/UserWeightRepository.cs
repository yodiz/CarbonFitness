using System;
using System.Linq;
using CarbonFitness.Data.Model;
using NHibernate.Linq;
using SharpArch.Data.NHibernate;

namespace CarbonFitness.DataLayer.Repository {
	public class UserWeightRepository : NHibernateRepositoryWithTypedId<UserWeight, int>, IUserWeightRepository {
		public UserWeight FindUserWeightByDate(User user, DateTime date) {
			return Session.Linq<UserWeight>().Where(x => x.Date == date && x.User.Id == user.Id).FirstOrDefault();
		}
	}
}