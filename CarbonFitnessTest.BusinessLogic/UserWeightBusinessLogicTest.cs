using System;
using CarbonFitness.BusinessLogic.Implementation;
using CarbonFitness.Data.Model;
using Moq;
using NUnit.Framework;

namespace CarbonFitnessTest.BusinessLogic {
	[TestFixture]
	public class UserWeightBusinessLogicTest {
		[Test]
		public void shouldSaveUserWeight() {
			var userWeight = new UserWeight {
				User = new User("arne"),
				Weight = 80.5M,
				Date = DateTime.Now.Date
			};

			var userWeightRepositoryMock = new Mock<IUserWeightRepository>();
			userWeightRepositoryMock.Setup(z => z.SaveOrUpdate(It.Is<UserWeight>(x => x.Date == userWeight.Date && x.User.Username == userWeight.User.Username && x.Weight == userWeight.Weight))).Returns(userWeight);

			var userWeightBusinessLogic = new UserWeightBusinessLogic(userWeightRepositoryMock.Object);
			var createdUserWeight = userWeightBusinessLogic.SaveWeight(userWeight.User, userWeight.Weight, userWeight.Date);

			Assert.That(ReferenceEquals(createdUserWeight, userWeight));
			userWeightRepositoryMock.VerifyAll();
		}
	}
}