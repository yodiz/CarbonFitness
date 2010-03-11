using System;
using CarbonFitness.BusinessLogic.Exceptions;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;

namespace CarbonFitness.BusinessLogic.Implementation {
	public class UserWeightBusinessLogic : IUserWeightBusinessLogic {
		private readonly IUserWeightRepository userWeightRepository;

		public UserWeightBusinessLogic(IUserWeightRepository userWeightRepository) {
			this.userWeightRepository = userWeightRepository;
		}

		public UserWeight SaveWeight(User user, decimal weight, DateTime date) {
			if(weight == 0) {
				throw new InvalidWeightException("Cannot save zero weight");
			}

			var userWeight = userWeightRepository.FindUserWeightByDate(user, date);

			if (userWeight == null) {
				userWeight = new UserWeight {Date = date, User = user, Weight = weight};
			}

			userWeight.Weight = weight;
			return userWeightRepository.SaveOrUpdate(userWeight);
		}

		public UserWeight GetWeight(User user, DateTime date) {
			return userWeightRepository.FindUserWeightByDate(user, date);
		}
	}
}