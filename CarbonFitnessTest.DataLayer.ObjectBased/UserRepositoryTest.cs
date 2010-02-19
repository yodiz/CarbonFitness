using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.ObjectBased;
using CarbonFitness.DataLayer.ObjectBased.Repository;
using Moq;
using NUnit.Framework;

namespace CarbonFitnessTest.DataLayer.ObjectBased
{
	[TestFixture]
	public class UserRepositoryTest
	{
		[Test]
		public void shouldGetUserByUsername()
		{
			var userId = 9;
			var userName = "myUser";

			var userMock = new Mock<User>();
			userMock.Setup(x => x.Id).Returns(userId);
			userMock.Setup(x => x.Username).Returns(userName);

			var queryable = new List<User> {userMock.Object}.AsQueryable();

			var dbContextMock = new Mock<IObjectDbContext>();
			dbContextMock.Setup(x => x.AsQueryable<User>()).Returns(queryable);

			var userRepository = new UserRepository(dbContextMock.Object);

			var user = userRepository.Get(userName);

			Assert.That(user.Id, Is.EqualTo(userId));
		}
	}
}
