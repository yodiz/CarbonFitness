using CarbonFitness;
using CarbonFitness.Model;
using CarbonFitness.Repository;
using Moq;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace CarbonFitnessTest.Test
{
    [TestFixture]
    public class CarbonFitnessMembershipProviderTest
    {

        [Test]
        public void shouldReturnTrueWhenValidating() {
            var factory = new MockFactory(MockBehavior.Strict);
            var userRepositoryMock =  factory.Create<IUserRepository>();

            var wrongUsername = "wrongUsesdlf";
            var wrongPassword = "wrongpadsasd";
            var existingUsername = "myUsesdlf";
            var existingPassword = "padsasd";

            userRepositoryMock.Setup(x => x.Get(existingUsername)).Returns(new User(existingUsername, existingPassword));
            userRepositoryMock.Setup(x => x.Get(wrongUsername)).Returns(null);

            var membershipProvider = new MembershipService(userRepositoryMock.Object);
            var loginSuccessfull = membershipProvider.ValidateUser(existingUsername, existingPassword);

            var loginFailed = membershipProvider.ValidateUser(wrongUsername, wrongPassword);

            Assert.That(loginSuccessfull);
            Assert.That(loginFailed, Is.False);
        }
    }
}
