using System.Collections.Generic;
using CarbonFitness.Data.Model;
using SharpArch.Core.PersistenceSupport.NHibernate;

namespace CarbonFitness.DataLayer.Repository {
    public interface IMealIngredientRepository : INHibernateRepositoryWithTypedId<MealIngredient, int> {
		 IEnumerable<MealIngredient> GetByMealId(int mealId);
	 }
}