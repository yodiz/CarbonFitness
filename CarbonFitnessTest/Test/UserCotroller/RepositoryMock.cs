using CarbonFitness.Model;
using CarbonFitness.Repository;
using Moq;

namespace CarbonFitnessTest.Test.UserCotroller {
    internal class RepositoryMock {
        internal static IUserRepository GetUserRepository() {
            IUserRepository userRepository;
            var factory = new MockFactory(MockBehavior.Strict);
            var mock = factory.Create<IUserRepository>();
            mock.Setup(x => x.Get("username")).Returns(new User {Username = "username"});
            mock.Setup(x => x.SaveOrUpdate(new User { Username = "username" })).Returns(new User { Username = "username" });
            userRepository = mock.Object;
            return userRepository;
        }
    }
}