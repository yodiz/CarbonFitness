using CarbonFitness.Data.Model;

namespace CarbonFitnessTest.BusinessLogic {
	public interface IUserWeightRepository {
		UserWeight SaveOrUpdate(UserWeight userWeight);
	}
}