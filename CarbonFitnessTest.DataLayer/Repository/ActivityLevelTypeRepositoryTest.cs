using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;
using NUnit.Framework;
using SharpArch.Testing.NUnit.NHibernate;

namespace CarbonFitnessTest.DataLayer.Repository
{
    [TestFixture]
    public class ActivityLevelTypeRepositoryTest : RepositoryTestsBase {
        [Test]
        public void shouldGetActivityLevelTypeByName() {
            var repository = new ActivityLevelTypeRepository();
            var activityLevelType = repository.GetByName("Medel");
            Assert.That(activityLevelType.Name, Is.EqualTo("Medel"));
        }

        protected override void LoadTestData() {
            var repository = new ActivityLevelTypeRepository();

            repository.SaveOrUpdate(new ActivityLevelType { Name = "Medel" });
            repository.SaveOrUpdate(new ActivityLevelType { Name = "Låg" });
        }
    }
}
