using System;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;

namespace CarbonFitness.BusinessLogic.Implementation {
    public class UserProfileBusinessLogic : IUserProfileBusinessLogic {

        public UserProfileBusinessLogic(IUserProfileRepository userProfileRepository, IGenderTypeBusinessLogic genderTypeBusinessLogic) {
            GenderTypeBusinessLogic = genderTypeBusinessLogic;
            UserProfileRepository = userProfileRepository;
        }

        public IUserProfileRepository UserProfileRepository { get; set; }
        public IGenderTypeBusinessLogic GenderTypeBusinessLogic { get; set; }
        
        public decimal GetIdealWeight(User user) {
            return GetUserProfile(user).IdealWeight;
        }

        public decimal GetLength(User user) {
            return GetUserProfile(user).Length;
        }

        public decimal GetWeight(User user) {
            return GetUserProfile(user).Weight;
        }

        public decimal GetBMI(User user) {
            var profile = GetUserProfile(user);
            if(profile.Length == 0 ) {
                return 0;
            }
            return profile.Weight / (profile.Length * profile.Length);
        }

        public GenderType GetGender(User user) {
            var gender = GetUserProfile(user).Gender;
            if(gender == null) {
                return GenderTypeBusinessLogic.GetGenderType("Man"); //Default
            }
            return gender;
        }

        public void SaveProfile(User user, decimal idealWeight, decimal length, decimal weight, string @is) {
            var userProfile = GetUserProfile(user);
            userProfile.IdealWeight = idealWeight;
            userProfile.Length = length;
            userProfile.Weight = weight;
            UserProfileRepository.SaveOrUpdate(userProfile);
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