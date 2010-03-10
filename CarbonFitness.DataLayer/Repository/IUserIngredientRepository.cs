using System;
using CarbonFitness.Data.Model;
using SharpArch.Core.PersistenceSupport.NHibernate;

namespace CarbonFitness.DataLayer.Repository {
	public interface IUserIngredientRepository: INHibernateRepositoryWithTypedId<UserIngredient, int> {
		UserIngredient[] GetUserIngredientsFromUserId(int userId, DateTime fromDate, DateTime toDate);
	}
}