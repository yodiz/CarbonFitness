using System;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;

namespace CarbonFitness.BusinessLogic.Implementation {
    public class UserProfileBusinessLogic : IUserProfileBusinessLogic {

        public UserProfileBusinessLogic(IUserProfileRepository userProfileRepository, IGenderTypeBusinessLogic genderTypeBusinessLogic, IActivityLevelTypeBusinessLogic activityLevelTypeBusinessLogic, ICalorieCalculator calorieCalculator) {
            CalorieCalculator = calorieCalculator;
            GenderTypeBusinessLogic = genderTypeBusinessLogic;
            UserProfileRepository = userProfileRepository;
            ActivityLevelTypeBusinessLogic = activityLevelTypeBusinessLogic;
        }

        public ICalorieCalculator CalorieCalculator { get; set; }
        public IActivityLevelTypeBusinessLogic ActivityLevelTypeBusinessLogic { get; set; }
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
            var lenghtInMeter = profile.Length / 100;
            return profile.Weight / (lenghtInMeter * lenghtInMeter);
        }

        public decimal GetBMR(User user) {
            return CalorieCalculator.GetBMR(GetWeight(user), GetLength(user), GetAge(user), GetGender(user));
        }

        public decimal GetDailyCalorieNeed(User user) {
            return CalorieCalculator.GetDailyCalorieNeed(GetWeight(user), GetLength(user), GetAge(user), GetGender(user), GetActivityLevel(user));
        }

        public int GetAge(User user) {
            return GetUserProfile(user).Age;
        }

        public GenderType GetGender(User user) {
            var gender = GetUserProfile(user).Gender;
            if(gender == null) {
                return GetGenderTypeFromString("Man"); //Default
            }
            return gender;
        }

        public ActivityLevelType GetActivityLevel(User user) {
            var activityLevel = GetUserProfile(user).ActivityLevel;
            if(activityLevel == null) {
                return GetActivityLevelFromString("Låg");
            }
            return activityLevel;
        }

        public void SaveProfile(User user, decimal idealWeight, decimal length, decimal weight, int age, string genderName, string activityLevelName) {
            var userProfile = GetUserProfile(user);
            userProfile.IdealWeight = idealWeight;
            userProfile.Length = length;
            userProfile.Weight = weight;
            userProfile.Age = age;
            userProfile.Gender = GetGenderTypeFromString(genderName);
            userProfile.ActivityLevel = GetActivityLevelFromString(activityLevelName);
            UserProfileRepository.SaveOrUpdate(userProfile);
        }

        private ActivityLevelType GetActivityLevelFromString(string activityLevel) {
            return ActivityLevelTypeBusinessLogic.GetActivityLevelType(activityLevel);
        }

        private GenderType GetGenderTypeFromString(string gender){
            return GenderTypeBusinessLogic.GetGenderType(gender);
        }
        
        private UserProfile userProfile = null;
        private UserProfile GetUserProfile(User user) {
            if (userProfile == null) {
                userProfile = UserProfileRepository.GetByUserId(user.Id);
                if (userProfile == null) {
                    userProfile = new UserProfile {User = user, IdealWeight = 0};
                }
            }
            return userProfile;
        }

    }
}