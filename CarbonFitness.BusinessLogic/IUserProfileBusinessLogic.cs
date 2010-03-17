using CarbonFitness.Data.Model;

namespace CarbonFitness.BusinessLogic {
    public interface IUserProfileBusinessLogic {
        void SaveIdealWeight(User user, decimal weight);
        decimal GetIdealWeight(User user);
    }
}