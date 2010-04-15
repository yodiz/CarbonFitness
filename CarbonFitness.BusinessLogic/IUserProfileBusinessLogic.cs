using CarbonFitness.Data.Model;

namespace CarbonFitness.BusinessLogic {
    public interface IUserProfileBusinessLogic {
        decimal GetIdealWeight(User user);
        decimal GetLength(User user);
        decimal GetWeight(User user);
        decimal GetBMI(User user);
        void SaveProfile(User user, decimal idealWeight, decimal length, decimal weight);

        
    }
}