using System;
using System.Linq;
using CarbonFitness.Data.Model;
using NHibernate.Linq;
using SharpArch.Data.NHibernate;

namespace CarbonFitness.DataLayer.Repository
{
	public class IngredientRepository : NHibernateRepositoryWithTypedId<Ingredient, int>, IIngredientRepository
	{
		public Ingredient Get(string ingredientName)
		{
			return Session.Linq<Ingredient>().Where(x => x.Name == ingredientName).FirstOrDefault();
		}
	}
}