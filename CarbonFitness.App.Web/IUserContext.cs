using CarbonFitness.Data.Model;

namespace CarbonFitness.App.Web {
	public interface IUserContext {
		User User { get; }
		void LogIn(User user, bool persistantUser);
	}
}