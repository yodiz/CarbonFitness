using CarbonFitness.Data.Model;

namespace CarbonFitness.BusinessLogic {
    public interface IUserBusinessLogic {
        User  SaveOrUpdate(User user);
        User Get(int id);
    }
}