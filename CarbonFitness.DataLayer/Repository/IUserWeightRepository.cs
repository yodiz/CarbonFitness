using System;
using CarbonFitness.Data.Model;
using SharpArch.Core.PersistenceSupport.NHibernate;

namespace CarbonFitness.DataLayer.Repository {
	public interface IUserWeightRepository : INHibernateRepositoryWithTypedId<UserWeight,int> {
		UserWeight FindUserWeightByDate(User user, DateTime date);
	}
}