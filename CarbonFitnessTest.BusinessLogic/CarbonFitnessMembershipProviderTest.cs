using CarbonFitness.BusinessLogic.Implementation;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;
using Moq;
using NUnit.Framework;

namespace CarbonFitnessTest.BusinessLogic {
	[TestFixture]
	public class CarbonFitnessMembershipProviderTest {
		[Test]
		public void shouldCheckThatPasswordLenghtIsGreaterThanThreeCharacters() {
			var membershipProvider = new MembershipBusinessLogic(new Mock<IUserRepository>().Object);

			var minimumLength = membershipProvider.MinPasswordLength;
			Assert.That(minimumLength, Is.GreaterThan(3));
		}

		[Test]
		public void shouldReturnFalseWhenProvidingWrongPassword() {
			var username = "username";
			var userPassword = "sdfsdfsdf";
			var providedPassword = "nåttannat";

			var factory = new MockFactory(MockBehavior.Strict);
			var userRepositoryMock = factory.Create<IUserRepository>();

			userRepositoryMock.Setup(x => x.Get(username)).Returns(new User(username, userPassword));
			var membershipProvider = new MembershipBusinessLogic(userRepositoryMock.Object);
			var loginResult = membershipProvider.ValidateUser(username, providedPassword);

			Assert.That(loginResult, Is.False);
		}

		[Test]
		public void shouldReturnTrueWhenValidating() {
			var factory = new MockFactory(MockBehavior.Strict);
			var userRepositoryMock = factory.Create<IUserRepository>();

			var wrongUsername = "wrongUsesdlf";
			var wrongPassword = "wrongpadsasd";
			var existingUsername = "myUsesdlf";
			var existingPassword = "padsasd";

			userRepositoryMock.Setup(x => x.Get(existingUsername)).Returns(new User(existingUsername, existingPassword));
			userRepositoryMock.Setup(x => x.Get(wrongUsername)).Returns((User) null);

			var membershipProvider = new MembershipBusinessLogic(userRepositoryMock.Object);
			var loginSuccessfull = membershipProvider.ValidateUser(existingUsername, existingPassword);

			var loginFailed = membershipProvider.ValidateUser(wrongUsername, wrongPassword);

			Assert.That(loginSuccessfull);
			Assert.That(loginFailed, Is.False);
		}
	}
}