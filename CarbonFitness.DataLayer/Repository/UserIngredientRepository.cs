using CarbonFitness.Data.Model;
using SharpArch.Data.NHibernate;

namespace CarbonFitness.DataLayer.Repository
{
	public class UserIngredientRepository : NHibernateRepositoryWithTypedId<UserIngredient, int>, IUserIngredientRepository
	{
	}
}