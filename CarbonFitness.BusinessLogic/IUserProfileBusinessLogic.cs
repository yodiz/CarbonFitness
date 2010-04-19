using CarbonFitness.Data.Model;

namespace CarbonFitness.BusinessLogic {
    public interface IUserProfileBusinessLogic {
        decimal GetIdealWeight(User user);
        decimal GetLength(User user);
        decimal GetWeight(User user);
        decimal GetBMI(User user);
        decimal GetBMR(User user);
        int GetAge(User user);
        GenderType GetGender(User user);
        void SaveProfile(User user, decimal idealWeight, decimal length, decimal weight, int age, string genderName, string activityLevelName);
        ActivityLevelType GetActivityLevel(User user);
        
    }
}