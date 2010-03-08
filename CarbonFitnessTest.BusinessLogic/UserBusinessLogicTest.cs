using CarbonFitness.BusinessLogic.Implementation;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;
using Moq;
using NUnit.Framework;

namespace CarbonFitnessTest.BusinessLogic {
	[TestFixture]
	public class UserBusinessLogicTest {
		[Test]
		public void shouldCallGetOnDataLayer() {
			var mockFactory = new MockFactory(MockBehavior.Strict);
			var userRepositoryMock = mockFactory.Create<IUserRepository>();
			userRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(new User());

			var userBusinessLogic = new UserBusinessLogic(userRepositoryMock.Object);
			userBusinessLogic.Get(2);
			userRepositoryMock.VerifyAll();
		}

		[Test]
		public void shouldCallSaveOrUpdateOnDataLayer() {
			var mockFactory = new MockFactory(MockBehavior.Strict);
			var userRepositoryMock = mockFactory.Create<IUserRepository>();
			userRepositoryMock.Setup(x => x.SaveOrUpdate(It.IsAny<User>())).Returns(new User("myUser"));

			var userBusinessLogic = new UserBusinessLogic(userRepositoryMock.Object);
			userBusinessLogic.SaveOrUpdate(new User("myUserToSave"));

			userRepositoryMock.VerifyAll();
		}

		[Test]
		public void shouldGetUserFromUsername() {
			var mockFactory = new MockFactory(MockBehavior.Strict);
			var userRepositoryMock = mockFactory.Create<IUserRepository>();
			userRepositoryMock.Setup(x => x.Get(It.IsAny<string>())).Returns(new User());

			var userBusinessLogic = new UserBusinessLogic(userRepositoryMock.Object);
			userBusinessLogic.Get("myUser");
			userRepositoryMock.VerifyAll();
		}
	}
}