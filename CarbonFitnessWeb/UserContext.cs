using System.Threading;
using CarbonFitness.BusinessLogic;
using CarbonFitness.Data.Model;

namespace CarbonFitnessWeb
{
	public class UserContext {
		private readonly IUserBusinessLogic userBusinessLogic;

		public UserContext(IUserBusinessLogic userBusinessLogic)
		{
			this.userBusinessLogic = userBusinessLogic;
		}

		public User User { 
			get {
				return userBusinessLogic.Get(Thread.CurrentPrincipal.Identity.Name);
			}
		}
	}
}