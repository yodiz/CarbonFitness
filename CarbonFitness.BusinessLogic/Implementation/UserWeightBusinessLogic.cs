using System;
using System.Collections.Generic;
using CarbonFitness.BusinessLogic.Exceptions;
using CarbonFitness.BusinessLogic.UnitHistory;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;

namespace CarbonFitness.BusinessLogic.Implementation {
	public class UserWeightBusinessLogic : IUserWeightBusinessLogic {
		private readonly IUserWeightRepository userWeightRepository;

		public UserWeightBusinessLogic(IUserWeightRepository userWeightRepository) {
			this.userWeightRepository = userWeightRepository;
		}

		public UserWeight SaveWeight(User user, decimal weight, DateTime date) {
			if (weight == 0) {
				throw new InvalidWeightException("Cannot save zero weight");
			}

			var userWeight = userWeightRepository.FindByDate(user, date);

			if (userWeight == null) {
				userWeight = new UserWeight {Date = date, User = user, Weight = weight};
			}

			userWeight.Weight = weight;

			var savedUserWeight = userWeightRepository.SaveOrUpdate(userWeight);
			return savedUserWeight;
		}

		public UserWeight GetUserWeight(User user, DateTime date) {
			return userWeightRepository.FindByDate(user, date);
		}

		public IEnumerable<UserWeight> GetHistoryList(User user) {
			return userWeightRepository.GetHistoryList(user);
		}



		public ILine GetProjectionList(User user)
		{
			throw new NotImplementedException();
		}

	    public ILine GetHistoryLine(User user) {
	        var userWeights = GetUserWeightDictionary(GetHistoryList(user));
	        return new Line(0, userWeights, "Vikt");
	    }

	    private Dictionary<DateTime, decimal> GetUserWeightDictionary(IEnumerable<UserWeight> userWeights) {
	        var result = new Dictionary<DateTime, decimal>();
	        foreach (var userWeight in userWeights) {
                result.Add(userWeight.Date, userWeight.Weight);    
	        }
	        return result;
	    }
	}
}