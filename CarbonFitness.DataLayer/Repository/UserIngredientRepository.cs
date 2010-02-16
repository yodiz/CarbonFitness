using System.Linq;
using CarbonFitness.Data.Model;
using NHibernate.Linq;
using SharpArch.Data.NHibernate;

namespace CarbonFitness.DataLayer.Repository
{
	public class UserIngredientRepository : NHibernateRepositoryWithTypedId<UserIngredient, int>, IUserIngredientRepository
	{
	    public UserIngredient[] GetUserIngredientsFromUserId(int userId) {
	        IQueryable<UserIngredient> q = from userIngredient in Session.Linq<UserIngredient>()
	                                       where userId.Equals(userId)
	                                       select userIngredient;

	        return q.ToArray();
	    }
	}
}