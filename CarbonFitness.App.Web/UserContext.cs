using System.Threading;
using CarbonFitness.App.Web.Models;
using CarbonFitness.BusinessLogic;
using CarbonFitness.Data.Model;

namespace CarbonFitness.App.Web {
	public class UserContext : IUserContext {
		private readonly IFormsAuthenticationService formsAuthenticationService;
		private readonly IUserBusinessLogic userBusinessLogic;

		public UserContext(IUserBusinessLogic userBusinessLogic, IFormsAuthenticationService formsAuthenticationService) {
			this.userBusinessLogic = userBusinessLogic;
			this.formsAuthenticationService = formsAuthenticationService;
		}

		public User User {
			get { return userBusinessLogic.Get(Thread.CurrentPrincipal.Identity.Name); }
		}

		public void LogIn(User user, bool persitantUser) {
			formsAuthenticationService.SignIn(user.Username, persitantUser);
		}
	}
}