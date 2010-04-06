using System;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;

namespace CarbonFitness.BusinessLogic.Implementation {
    public class UserProfileBusinessLogic : IUserProfileBusinessLogic {
        public IUserProfileRepository UserProfileRepository { get; set; }

        public UserProfileBusinessLogic(IUserProfileRepository userProfileRepository) {
            UserProfileRepository = userProfileRepository;
        }

        public void SaveIdealWeight(User user, decimal weight) {

            var userProfile = UserProfileRepository.Get(user.Id);
            if (userProfile != null) {
                userProfile.IdealWeight = weight;
            } else {
                userProfile = new UserProfile() {IdealWeight = weight, User = user}; 
            }

            UserProfileRepository.SaveOrUpdate(userProfile);
        }

        public decimal GetIdealWeight(User user) {
            var userProfile = UserProfileRepository.Get(user.Id);
            if (userProfile == null) {
                return 0;
            }
            return userProfile.IdealWeight;
        }
    }
}