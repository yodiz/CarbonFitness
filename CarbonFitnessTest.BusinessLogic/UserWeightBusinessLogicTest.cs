using System;
using CarbonFitness.BusinessLogic.Exceptions;
using CarbonFitness.BusinessLogic.Implementation;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;
using Moq;
using NUnit.Framework;

namespace CarbonFitnessTest.BusinessLogic {
	[TestFixture]
	public class UserWeightBusinessLogicTest {
		private Mock<UserWeight> expectedUserWeightMock;
		private Mock<IUserWeightRepository> userWeightRepositoryMock;
		private UserWeight expectedUserWeight;

		[SetUp]
		public void Setup() {
			
			expectedUserWeightMock = new Mock<UserWeight>();
			expectedUserWeightMock.Setup(x => x.User).Returns(new User("arne"));
			expectedUserWeightMock.SetupProperty(x => x.Weight);
			expectedUserWeightMock.Object.Weight = 80.5M;
			expectedUserWeightMock.Setup(x => x.Date).Returns(DateTime.Now.Date);
			expectedUserWeight = expectedUserWeightMock.Object;
			
			userWeightRepositoryMock = new Mock<IUserWeightRepository>();
		}

		[Test]
		public void shouldSaveUserWeight() {
			userWeightRepositoryMock.Setup(z => z.SaveOrUpdate(It.Is<UserWeight>(x => x.Date == expectedUserWeight.Date && x.User.Username == expectedUserWeight.User.Username && x.Weight == expectedUserWeight.Weight))).Returns(expectedUserWeight);

			var createdUserWeight = new UserWeightBusinessLogic(userWeightRepositoryMock.Object).SaveWeight(expectedUserWeight.User, expectedUserWeight.Weight, expectedUserWeight.Date);

			Assert.That(ReferenceEquals(createdUserWeight, expectedUserWeight));
			userWeightRepositoryMock.VerifyAll();
		}

		[Test] 
		public void shouldGetWeight() {
			userWeightRepositoryMock.Setup(x => x.FindUserWeightByDate(expectedUserWeight.User, expectedUserWeight.Date)).Returns(expectedUserWeight);

			var fetchedUserWeight = new UserWeightBusinessLogic(userWeightRepositoryMock.Object).GetWeight(expectedUserWeight.User, expectedUserWeight.Date);

			Assert.That(ReferenceEquals(fetchedUserWeight, expectedUserWeight));	
			userWeightRepositoryMock.VerifyAll();
		}

		
		[Test]
		public void shouldOverwriteUserWeight() {
			var userWeightId = 62;
			expectedUserWeightMock.Setup(x => x.Id).Returns(userWeightId);

			userWeightRepositoryMock.Setup(x => x.FindUserWeightByDate(expectedUserWeight.User, expectedUserWeight.Date)).Returns(expectedUserWeight);

			var newWeight = 75M;
			userWeightRepositoryMock.Setup(z => z.SaveOrUpdate(It.Is<UserWeight>(x => x.Weight == newWeight && x.Id == userWeightId))).Returns(expectedUserWeight);

			var createdUserWeight = new UserWeightBusinessLogic(userWeightRepositoryMock.Object).SaveWeight(expectedUserWeight.User, newWeight, expectedUserWeight.Date);

			userWeightRepositoryMock.VerifyAll();
			Assert.That(createdUserWeight.Weight, Is.EqualTo( newWeight));
		}

		[Test]
		public void shouldThrowErrorIfTryingToSaveZeroInWeight() {
			Assert.Throws<InvalidWeightException>(() => new UserWeightBusinessLogic(userWeightRepositoryMock.Object).SaveWeight(expectedUserWeight.User, 0, expectedUserWeight.Date));
		}

	}
}