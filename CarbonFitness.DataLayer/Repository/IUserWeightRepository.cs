using System;
using System.Collections.Generic;
using CarbonFitness.Data.Model;
using SharpArch.Core.PersistenceSupport.NHibernate;

namespace CarbonFitness.DataLayer.Repository {
	public interface IUserWeightRepository : INHibernateRepositoryWithTypedId<UserWeight,int> {
		UserWeight FindByDate(User user, DateTime date);
		IEnumerable<UserWeight> GetHistoryList(User user);
	}
}