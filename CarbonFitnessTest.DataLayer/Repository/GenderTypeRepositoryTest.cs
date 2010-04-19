using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;
using NUnit.Framework;
using SharpArch.Testing.NUnit.NHibernate;

namespace CarbonFitnessTest.DataLayer.Repository {
    [TestFixture]
    public class GenderTypeRepositoryTest : RepositoryTestsBase {
        [Test]
        public void shouldGetGenderTypes() {
            var repository = new GenderTypeRepository();
            var all = repository.GetAll();
            Assert.That(all.Count, Is.EqualTo(2));
        }

        [Test]
        public void shouldGetGenderType() {
            var repository = new GenderTypeRepository();
            var genderType = repository.GetByName("Man");
            Assert.That(genderType.Name, Is.EqualTo("Man"));
        }

        protected override void LoadTestData() {
            var repository = new GenderTypeRepository();

            repository.SaveOrUpdate(new GenderType { Name = "Man" });
            repository.SaveOrUpdate(new GenderType { Name = "Kvinna" });
        }
    }
}
