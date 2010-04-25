using System;
using System.Linq;
using System.Text;
using CarbonFitness.App.Importer;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;
using NUnit.Framework;
using SharpArch.Data.NHibernate;

namespace CarbonFitnessTest.Integration
{
    [TestFixture]
    public class ImportRDITest {
        [Test]
        public void shouldImportIronRDI() {
            var nutrientRecommendationRepository = new NutrientRecommendationRepository();
            var nutrientRepository = new NutrientRepository();
            new NutrientRecommendationImportEngine("Hibernate.cfg.xml", true).Import();

            var nutrientRecommendation = nutrientRecommendationRepository.GetByNutrient(nutrientRepository.GetByName(NutrientEntity.IronInmG.ToString()));
            Assert.That(nutrientRecommendation.GetValue("Man",12), Is.EqualTo(11M));
        }


        [TearDown]
        public void TearDown() {
            NHibernateSession.Reset();
        }
    }
}
