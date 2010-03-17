using System;
using System.Linq;
using CarbonFitness.Data.Model;
using NHibernate.Linq;
using SharpArch.Data.NHibernate;

namespace CarbonFitness.DataLayer.Repository {
	public class UserIngredientRepository : NHibernateRepositoryWithTypedId<UserIngredient, int>, IUserIngredientRepository {
		public UserIngredient[] GetUserIngredientsByUser(int userId, DateTime fromDate, DateTime toDate) {
			//var q = from userIngredient in Session.Linq<UserIngredient>()
			//        //where (userIngredient.User.Id == userId
			//        where (userId == userIngredient.User.Id
			//   && (userIngredient.Date >= fromDate
			//   && userIngredient.Date < toDate))
			//        select userIngredient;
			return Session.Linq<UserIngredient>()
				.Where(x => x.User.Id == userId)
				.Where(x => x.Date >= fromDate)
				.Where(x => x.Date <= toDate)
				.ToArray();
		}
	}
}