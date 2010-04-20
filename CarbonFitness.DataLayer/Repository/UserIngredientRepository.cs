using System;
using System.Linq;
using CarbonFitness.Data.Model;
using NHibernate.Linq;
using SharpArch.Data.NHibernate;

namespace CarbonFitness.DataLayer.Repository {
	public class UserIngredientRepository : NHibernateRepositoryWithTypedId<UserIngredient, int>, IUserIngredientRepository {
		public UserIngredient[] GetUserIngredientsByUser(int userId, DateTime fromDate, DateTime toDate) {
			return Session.Linq<UserIngredient>()
				.Where(x => x.User.Id == userId)
                .Where(x => x.Date >= fromDate.Date)
                .Where(x => x.Date <= toDate.Date)
				.ToArray();
		}
	}
}