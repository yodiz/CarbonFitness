using System;
using System.Collections.Generic;
using System.Linq;
using CarbonFitness.Data.Model;
using NHibernate.Linq;
using SharpArch.Data.NHibernate;

namespace CarbonFitness.DataLayer.Repository {
	public class UserWeightRepository : NHibernateRepositoryWithTypedId<UserWeight, int>, IUserWeightRepository {
		public UserWeight FindByDate(User user, DateTime date) {
			return Session.Linq<UserWeight>().Where(x => x.Date == date && x.User.Id == user.Id).FirstOrDefault();
		}

		public IEnumerable<UserWeight> GetHistoryList(User user) {
			return Session.Linq<UserWeight>().Where(x => x.User.Id == user.Id).ToList().OrderBy(x => x.Date);
		}
	}
}