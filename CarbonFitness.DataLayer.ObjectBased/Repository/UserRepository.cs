using System.Linq;
using CarbonFitness.Data.Model;

namespace CarbonFitness.DataLayer.ObjectBased.Repository
{
	public class UserRepository
	{
		private readonly IObjectDbContext dbContext;

		public UserRepository(IObjectDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public User Get(string userName)
		{
			return dbContext.AsQueryable<User>().Where(x => x.Username == userName).FirstOrDefault();
		}
	}
}