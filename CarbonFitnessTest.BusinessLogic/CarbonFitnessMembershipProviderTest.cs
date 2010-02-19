using CarbonFitness;
using CarbonFitness.Data;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer;
using CarbonFitness.DataLayer.Repository;
using Moq;
using NUnit.Framework;

namespace CarbonFitnessTest.BusinessLogic
{
	[TestFixture]
	public class CarbonFitnessMembershipProviderTest {
		[Test]
		public void shouldReturnFalseWhenProvidingWrongPassword() {
			string username = "username";
			string userPassword = "sdfsdfsdf";
			string providedPassword = "nåttannat";

			var factory = new MockFactory(MockBehavior.Strict);
			Mock<IUserRepository> userRepositoryMock = factory.Create<IUserRepository>();

			userRepositoryMock.Setup(x => x.Get(username)).Returns(new User(username, userPassword));
			var membershipProvider = new MembershipBusinessLogic(userRepositoryMock.Object);
			bool loginResult = membershipProvider.ValidateUser(username, providedPassword);

			Assert.That(loginResult, Is.False);
		}

		[Test]
		public void shouldReturnTrueWhenValidating() {
			var factory = new MockFactory(MockBehavior.Strict);
			var userRepositoryMock = factory.Create<IUserRepository>();

			string wrongUsername = "wrongUsesdlf";
			string wrongPassword = "wrongpadsasd";
			string existingUsername = "myUsesdlf";
			string existingPassword = "padsasd";

			userRepositoryMock.Setup(x => x.Get(existingUsername)).Returns(new User(existingUsername, existingPassword));
			userRepositoryMock.Setup(x => x.Get(wrongUsername)).Returns((User) null);

			var membershipProvider = new MembershipBusinessLogic(userRepositoryMock.Object);
			bool loginSuccessfull = membershipProvider.ValidateUser(existingUsername, existingPassword);

			bool loginFailed = membershipProvider.ValidateUser(wrongUsername, wrongPassword);

			Assert.That(loginSuccessfull);
			Assert.That(loginFailed, Is.False);
		}

		[Test]
		public void shouldCheckThatPasswordLenghtIsGreaterThanThreeCharacters() {
			var membershipProvider = new MembershipBusinessLogic(new Mock<IUserRepository>().Object);
	 		
			var minimumLength = membershipProvider.MinPasswordLength;
			Assert.That(minimumLength, Is.GreaterThan(3));
		}
	}
}