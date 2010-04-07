using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;

namespace CarbonFitness.BusinessLogic.Implementation {
    public class UserProfileBusinessLogic : IUserProfileBusinessLogic {
        public UserProfileBusinessLogic(IUserProfileRepository userProfileRepository) {
            UserProfileRepository = userProfileRepository;
        }

        public IUserProfileRepository UserProfileRepository { get; set; }

        public void SaveIdealWeight(User user, decimal weight) {
            var userProfile = GetUserProfile(user);
            userProfile.IdealWeight = weight;
            UserProfileRepository.SaveOrUpdate(userProfile);
        }

        public decimal GetIdealWeight(User user) {
            var userProfile = GetUserProfile(user);
            return userProfile.IdealWeight;
        }

        private UserProfile GetUserProfile(User user) {
            var userProfile = UserProfileRepository.GetByUserId(user.Id);
            if (userProfile == null) {
                userProfile = new UserProfile {User = user, IdealWeight = 0};
            }
            return userProfile;
        }
    }
}