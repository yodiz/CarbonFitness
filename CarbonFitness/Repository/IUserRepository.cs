using CarbonFitness.Model;

namespace CarbonFitness.Repository {
    public interface IUserRepository {
        int Create(User user);
        User Get(int userId);
        User Get(string userName);
    }
}