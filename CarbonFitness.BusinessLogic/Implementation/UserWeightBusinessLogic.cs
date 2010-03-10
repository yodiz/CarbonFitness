using System;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;

namespace CarbonFitness.BusinessLogic.Implementation {
	public class UserWeightBusinessLogic : IUserWeightBusinessLogic {
		private readonly IUserWeightRepository userWeightRepository;

		public UserWeightBusinessLogic(IUserWeightRepository userWeightRepository) {
			this.userWeightRepository = userWeightRepository;
		}

		public UserWeight SaveWeight(User user, decimal weight, DateTime date) {
			return userWeightRepository.SaveOrUpdate(new UserWeight {Date = date, User = user, Weight = weight});
		}

		public UserWeight GetWeight(User user, DateTime date) {
			return userWeightRepository.FindUserWeightByDate(user, date);
		}
	}
}