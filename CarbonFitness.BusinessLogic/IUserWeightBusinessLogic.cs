using System;
using CarbonFitness.Data.Model;

namespace CarbonFitness.BusinessLogic {
	public interface IUserWeightBusinessLogic {
		UserWeight SaveWeight(User user, decimal weight, DateTime date);
	}
}