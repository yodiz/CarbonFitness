using System;
using System.Threading;
using CarbonFitness.BusinessLogic;
using CarbonFitness.Data.Model;
using CarbonFitnessWeb.Models;

namespace CarbonFitnessWeb
{
	public class UserContext : IUserContext
	{
		private readonly IUserBusinessLogic userBusinessLogic;
		private readonly IFormsAuthenticationService formsAuthenticationService;

		public UserContext(IUserBusinessLogic userBusinessLogic, IFormsAuthenticationService formsAuthenticationService)
		{
			this.userBusinessLogic = userBusinessLogic;
			this.formsAuthenticationService = formsAuthenticationService;
		}

		public User User { 
			get {
				return userBusinessLogic.Get(Thread.CurrentPrincipal.Identity.Name);
			}
		}

		public void LogIn(User user, bool persitantUser) {
			formsAuthenticationService.SignIn(user.Username, persitantUser);
		}
	}
}