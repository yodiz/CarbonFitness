using CarbonFitness.BusinessLogic;
using CarbonFitness.BusinessLogic.Implementation;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;
using Moq;
using NUnit.Framework;

namespace CarbonFitnessTest.BusinessLogic {
    [TestFixture]
    public class UserProfileBusinessLogicTest {
        private Mock<User> getUserMock() {
            var expectedUserId = 32;
            var userMock = new Mock<User>();
            userMock.Setup(x => x.Id).Returns(expectedUserId);
            return userMock;
        }

        [Test]
        public void shouldCreateUserProfileIfNotExistingForUser() {
            decimal expectedIdealWeight = 42;
            var userMock = getUserMock();

            var userProfileRepositoryMock = new Mock<IUserProfileRepository>();
            userProfileRepositoryMock.Setup(x => x.GetByUserId(userMock.Object.Id)).Returns((UserProfile) null);
            userProfileRepositoryMock.Setup(x => x.SaveOrUpdate(It.Is<UserProfile>(y => y.Id == 0)));

            new UserProfileBusinessLogic(userProfileRepositoryMock.Object).SaveIdealWeight(userMock.Object, expectedIdealWeight);

            userProfileRepositoryMock.VerifyAll();
        }

        [Test]
        public void shouldHaveImplementation() {
            var userProfileRepositoryMock = new Mock<IUserProfileRepository>();
            IUserProfileBusinessLogic userProfileBusinessLogic = new UserProfileBusinessLogic(userProfileRepositoryMock.Object);
            Assert.That(userProfileBusinessLogic, Is.Not.Null);
        }


        [Test]
        public void shouldReuseExistingUserProfileForUserIfExisting() {
            decimal expectedIdealWeight = 42;
            var userMock = getUserMock();

            var userProfileMock = new Mock<UserProfile>();
            userProfileMock.Setup(x => x.Id).Returns(234);
            var userProfile = userProfileMock.Object;

            var userProfileRepositoryMock = new Mock<IUserProfileRepository>();
            userProfileRepositoryMock.Setup(x => x.GetByUserId(userMock.Object.Id)).Returns(userProfile);
            userProfileRepositoryMock.Setup(x => x.SaveOrUpdate(userProfile));

            new UserProfileBusinessLogic(userProfileRepositoryMock.Object).SaveIdealWeight(userMock.Object, expectedIdealWeight);

            userProfileRepositoryMock.VerifyAll();
        }

        [Test]
        public void shouldSaveIdealWeight() {
            var userMock = getUserMock();

            var expectedIdealWeight = 64;
            var userProfileRepositoryMock = new Mock<IUserProfileRepository>();
            userProfileRepositoryMock.Setup(x => x.SaveOrUpdate(It.Is<UserProfile>(y => y.IdealWeight == expectedIdealWeight && y.User == userMock.Object)));

            new UserProfileBusinessLogic(userProfileRepositoryMock.Object).SaveIdealWeight(userMock.Object, expectedIdealWeight);
            userProfileRepositoryMock.VerifyAll();
        }

        [Test]
        public void shouldGetIdealWeight() {
            var user = getUserMock().Object;
            const decimal expectedIdealWeight = 42M;
            var userProfile = new UserProfile {IdealWeight = expectedIdealWeight, User = user};
            var userProfileRepositoryMock = new Mock<IUserProfileRepository>();
            userProfileRepositoryMock.Setup(x => x.GetByUserId(user.Id)).Returns(userProfile);

            var returnedIdealWeight = new UserProfileBusinessLogic(userProfileRepositoryMock.Object).GetIdealWeight(user);

            Assert.That(returnedIdealWeight, Is.EqualTo(expectedIdealWeight));
            userProfileRepositoryMock.VerifyAll();
        }
    }
}