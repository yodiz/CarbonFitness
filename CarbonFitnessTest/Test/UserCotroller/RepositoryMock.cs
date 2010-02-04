using CarbonFitness.Model;
using CarbonFitness.Repository;
using Moq;

namespace CarbonFitnessTest.Test.UserCotroller {
    internal class RepositoryMock {
        internal static IUserRepository GetUserRepository() {
            IUserRepository userRepository;
            var mock = new Mock<IUserRepository>();
            mock.Setup(x => x.Get("Micke")).Returns(new User {Username = "Micke"});
            mock.Setup(x => x.Get("username")).Returns(new User {Username = "username"});
            userRepository = mock.Object;
            return userRepository;
        }
    }
}