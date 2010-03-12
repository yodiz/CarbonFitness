using System;
using System.Linq;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;
using CarbonFitnessTest.Util;
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
			var foundUserWeight = repository.FindByDate(user, expectedDate);

			Assert.That(foundUserWeight, Is.Not.Null);
			Assert.That(foundUserWeight.Date, Is.EqualTo(expectedDate));
			Assert.That(foundUserWeight.Weight, Is.EqualTo(expectedWeight));
		}

		[Test]
		public void shouldGetHistoryList() {
			const string username = "UserWeightRepositoryShouldGetHistoryListTester";
			var user = new User(username);

			var session = NHibernateSession.Current;

			session.Save(user);

			for (var i = 0; i < 5; i++) {
				session.Save(new UserWeight { Date = DateTime.Now.AddDays(i), Weight = i, User = user });
				session.Flush();
			}

			var repository = new UserWeightRepository();

			var result = repository.GetHistoryList(user);

			Assert.That(result.ElementAt(0).Weight, Is.EqualTo(0));
			Assert.That(result.Count(), Is.EqualTo(5));
			Assert.That(result.Where(x => x.User.Username == username).Count(), Is.EqualTo(5));
		}

		[Test]
		public void shouldGetHistoryListInDateOrder() {
			const string username = "UserWeightRepositoryShouldGetHistoryListDateOrderTester";
			var user = new User(username);
			NHibernateSession.Current.Save(user);

			for (var i = 0; i < 10; i++) {
				NHibernateSession.Current.Save(new UserWeight { Date = ValueGenerator.getRandomDate(), Weight = i, User = user });
			}

			var result = new UserWeightRepository().GetHistoryList(user);

			Assert.That(result.Count(), Is.EqualTo(10));

			var tempDate = DateTime.MinValue;
			foreach (var userWeight in result) {
				Assert.That(userWeight.Date, Is.GreaterThanOrEqualTo(tempDate));
				tempDate = userWeight.Date;
			}
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