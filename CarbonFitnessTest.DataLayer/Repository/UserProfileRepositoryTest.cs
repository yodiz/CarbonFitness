using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;
using Moq;
using NUnit.Framework;
using SharpArch.Core.DomainModel;
using SharpArch.Data.NHibernate;
using SharpArch.Testing.NUnit.NHibernate;

namespace CarbonFitnessTest.DataLayer.Repository
{

    [TestFixture]
    public class UserProfileRepositoryTest : RepositoryTestsBase
    {

        private IUserProfileRepository userProfileRepository;
        private User userA;

        [Test]
        public void shouldHaveGetByUserIdMethod() {
            var profile = userProfileRepository.GetByUserId(1);

            Assert.That(profile, Is.Not.Null);
        }

        [Test]
        public void shouldReturnProfileForUser() {
            var profile = userProfileRepository.GetByUserId(userA.Id);
            Assert.That(profile.User.Id, Is.EqualTo(userA.Id));         
        }

        protected override void LoadTestData() {
            var userRepository = new UserRepository();
            userA = userRepository.SaveOrUpdate(new User());

            userProfileRepository = new UserProfileRepository();
            userProfileRepository.SaveOrUpdate(new UserProfile() { User = userA });

            NHibernateSession.Current.Flush();
        }
    }

}
