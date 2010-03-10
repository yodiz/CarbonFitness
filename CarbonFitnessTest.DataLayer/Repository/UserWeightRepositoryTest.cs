using System;
using System.Linq;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;
using NHibernate.Linq;
using NUnit.Framework;
using SharpArch.Data.NHibernate;
using SharpArch.Testing.NUnit.NHibernate;

namespace CarbonFitnessTest.DataLayer.Repository {
	[TestFixture]
	public class UserWeightRepositoryTest : RepositoryTestsBase {
		protected override void LoadTestData() {}

		[Test]
		public void shouldFindByDate() {
			var expectedDate = DateTime.Now.Date;
			var expectedWeight = 80.5M;

			var user = new User("UserWeightRepositoryFindByDateTester");
			var userWeight = new UserWeight {Date = expectedDate, User = user, Weight = expectedWeight};

			NHibernateSession.Current.Save(user);
			NHibernateSession.Current.Save(userWeight);

			var repository = new UserWeightRepository();
			var foundUserWeight = repository.FindUserWeightByDate(user, expectedDate);

			Assert.That(foundUserWeight, Is.Not.Null);
			Assert.That(foundUserWeight.Date, Is.EqualTo(expectedDate));
			Assert.That(foundUserWeight.Weight, Is.EqualTo(expectedWeight));
		}

		[Test]
		public void shouldSaveOrUpdate() {
			var repository = new UserWeightRepository();
			var username = "UserWeightRepositoryTester";
			var date = DateTime.Now;

			repository.SaveOrUpdate(new UserWeight {Date = date, User = new User(username), Weight = 80});

			var foundUserWeight = NHibernateSession.Current.Linq<UserWeight>().Where(x => x.Date == date).FirstOrDefault();

			Assert.That(foundUserWeight, Is.Not.Null, "Couldn't find saved UserWeight");
		}
	}
}