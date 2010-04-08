using CarbonFitness.BusinessLogic.Exceptions;
using CarbonFitness.BusinessLogic.Implementation;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;
using Moq;
using NUnit.Framework;

namespace CarbonFitnessTest.BusinessLogic {
	[TestFixture]
	public class UserBusinessLogicTest {
		[Test]
		public void shouldGetUser() {
			var mockFactory = new MockFactory(MockBehavior.Strict);
			var userRepositoryMock = mockFactory.Create<IUserRepository>();
			userRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(new User());

            var userBusinessLogic = new UserBusinessLogic(userRepositoryMock.Object, null);
			userBusinessLogic.Get(2);
			userRepositoryMock.VerifyAll();
		}

		[Test]
		public void shouldCreateUser() {
			var userRepositoryMock = new Mock<IUserRepository>();

			userRepositoryMock.Setup(x => x.Save(It.IsAny<User>())).Returns(new User("myUser")).Verifiable();

            var userBusinessLogic = new UserBusinessLogic(userRepositoryMock.Object, new Mock<IUserProfileRepository>().Object);
			userBusinessLogic.Create(new User("myUserToSave"));

			userRepositoryMock.Verify();
		}

        [Test]
        public void shouldNotCreateUserIfExisting() {
            var userName = "myUserToSave";
            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepositoryMock.Setup(x => x.Get(userName)).Returns(new User());
            Assert.Throws<UserAlreadyExistException>(() => {
                new UserBusinessLogic(userRepositoryMock.Object, null).Create(new User(userName));
            });
            

            userRepositoryMock.VerifyAll();
        }

	    [Test]
		public void shouldGetUserFromUsername() {
			var mockFactory = new MockFactory(MockBehavior.Strict);
			var userRepositoryMock = mockFactory.Create<IUserRepository>();
			userRepositoryMock.Setup(x => x.Get(It.IsAny<string>())).Returns(new User());

			var userBusinessLogic = new UserBusinessLogic(userRepositoryMock.Object, null);
			userBusinessLogic.Get("myUser");
			userRepositoryMock.VerifyAll();
		}

        [Test]
        public void shouldCreateUserProfileWhenCreatingAUser() {
            var user = new User();
            var userProfileRepositoryMock = new Mock<IUserProfileRepository>(MockBehavior.Strict);
            userProfileRepositoryMock.Setup(x => x.Save(It.Is<UserProfile>(y=> y.User == user))).Returns(new UserProfile());

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(x => x.Save(user)).Returns(user);

            var userBusinessLogic = new UserBusinessLogic(userRepositoryMock.Object, userProfileRepositoryMock.Object);
            userBusinessLogic.Create(user);

            userProfileRepositoryMock.VerifyAll();
        }
	}
}