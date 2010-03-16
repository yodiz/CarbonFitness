using System;
using System.Linq;
using System.Linq.Expressions;
using CarbonFitness.Data.Model;
using FluentNHibernate.Data;
using NHibernate.Linq;
using SharpArch.Data.NHibernate;

namespace CarbonFitness.DataLayer.Repository {
	public class IngredientRepository : NHibernateRepositoryWithTypedId<Ingredient, int>, IIngredientRepository {
		public Ingredient Get(string ingredientName) {
			return Session.Linq<Ingredient>().Where(x => x.Name.Contains(ingredientName))
				.ToList().Where(x => x.Name.Equals(ingredientName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
		}

		public Ingredient[] Search(string queryText) {
			return Session.Linq<Ingredient>().Where(x => x.Name.StartsWith(queryText)).ToArray();
		}
	}
}