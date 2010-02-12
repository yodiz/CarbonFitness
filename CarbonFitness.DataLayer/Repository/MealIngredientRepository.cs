using System;
using System.Collections.Generic;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;
using SharpArch.Data.NHibernate;

namespace CarbonFitness.DataLayer.Repository {
    public class MealIngredientRepository : NHibernateRepositoryWithTypedId<MealIngredient, int>, IMealIngredientRepository {
    	public IEnumerable<MealIngredient> GetByMealId(int p)
    	{
    		throw new NotImplementedException();
    	}
    }
}