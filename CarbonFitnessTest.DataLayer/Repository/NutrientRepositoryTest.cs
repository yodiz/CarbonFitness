using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;
using NUnit.Framework;
using SharpArch.Testing.NUnit.NHibernate;

namespace CarbonFitnessTest.DataLayer.Repository {
    [TestFixture]
    public class NutrientRepositoryTest : RepositoryTestsBase {
        protected override void LoadTestData() {
            var nutrientRepository = new NutrientRepository();
            var nutrient = nutrientRepository.SaveOrUpdate(new Nutrient {Name = "Energy In Cal"});
            var nutrient2 = nutrientRepository.SaveOrUpdate(new Nutrient {Name = "Iron"});
        }

        [Test]
        public void shouldGetAll() {
            Assert.That(new NutrientRepository().GetAll().Count, Is.EqualTo(2));
        }
    }
}