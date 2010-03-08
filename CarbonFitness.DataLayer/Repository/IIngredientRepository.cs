using CarbonFitness.Data.Model;
using SharpArch.Core.PersistenceSupport.NHibernate;

namespace CarbonFitness.DataLayer.Repository {
	public interface IIngredientRepository : INHibernateRepositoryWithTypedId<Ingredient, int> {
		Ingredient Get(string ingredientName);
		Ingredient[] Search(string queryText);
	}
}