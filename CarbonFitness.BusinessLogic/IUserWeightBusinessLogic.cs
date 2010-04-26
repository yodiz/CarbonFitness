using System;
using System.Collections.Generic;
using CarbonFitness.BusinessLogic.UnitHistory;
using CarbonFitness.Data.Model;

namespace CarbonFitness.BusinessLogic {
	public interface IUserWeightBusinessLogic {
		UserWeight SaveWeight(User user, decimal weight, DateTime date);
		UserWeight GetUserWeight(User user, DateTime date);
		IEnumerable<UserWeight> GetHistoryList(User user);

		ILine GetProjectionList(User user);
	    ILine GetHistoryLine(User user);
	}
}