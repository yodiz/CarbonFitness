using CarbonFitness.Data.Model;

namespace CarbonFitness.BusinessLogic {
	public interface IUserBusinessLogic {
		User Create(User user);
		User Get(int id);
		User Get(string userName);
	}
}