using System.Linq;

namespace CarbonFitness.DataLayer.ObjectBased
{
	public interface IObjectDbContext
	{
		IQueryable<TEntity> AsQueryable<TEntity>();
	}
}