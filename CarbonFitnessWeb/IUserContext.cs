using CarbonFitness.Data.Model;

namespace CarbonFitnessWeb
{
	public interface IUserContext
	{
		User User { get; }
		void LogIn(User user, bool persistantUser);
	}
}