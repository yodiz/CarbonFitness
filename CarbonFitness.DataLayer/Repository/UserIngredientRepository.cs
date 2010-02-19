using System;
using System.Linq;
using CarbonFitness.Data;
using CarbonFitness.Data.Model;
using NHibernate.Linq;
using SharpArch.Data.NHibernate;

namespace CarbonFitness.DataLayer.Repository
{
	public class UserIngredientRepository : NHibernateRepositoryWithTypedId<UserIngredient, int>, IUserIngredientRepository
	{
		public UserIngredient[] GetUserIngredientsFromUserId(int userId, DateTime dateTime)
		{
			var fromdate = DateTime.Parse(dateTime.ToShortDateString());
			var todate = DateTime.Parse(dateTime.AddDays(1).Date.ToShortDateString());

			var q = from userIngredient in Session.Linq<UserIngredient>()
					  where userId.Equals(userId) &&
						userIngredient.Date >= fromdate && userIngredient.Date < todate
					  select userIngredient;

			return q.ToArray();
		}
	}
}