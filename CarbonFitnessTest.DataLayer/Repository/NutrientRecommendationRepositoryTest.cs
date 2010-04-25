using System.Collections.Generic;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;
using NUnit.Framework;
using SharpArch.Testing.NUnit.NHibernate;

namespace CarbonFitnessTest.DataLayer.Repository
{
    [TestFixture]
    public class NutrientRecommendationRepositoryTest : RepositoryTestsBase
    {
        private Nutrient nutrient;

        [Test] 
        public void shouldGetNutrientRecommendationFromNutrientId () {
            var nutrientRecommendationRepository = new NutrientRecommendationRepository();
            var nutrientRecommendation = nutrientRecommendationRepository.GetByNutrient(nutrient);
            Assert.That(nutrientRecommendation, Is.Not.Null);
            Assert.That(nutrientRecommendation.GetValue("Kvinna", 2), Is.EqualTo(4M));
        }

        protected override void LoadTestData() {
            var nutrientRepository = new NutrientRepository();

            nutrient = nutrientRepository.SaveOrUpdate(new Nutrient {Name = "Iron"});
            var nutrient2 = nutrientRepository.SaveOrUpdate(new Nutrient { Name = "Waste" });
            var nutrientRecommendationRepository = new NutrientRecommendationRepository();

            nutrientRecommendationRepository.SaveOrUpdate(CreateRecommendation(nutrient2, "Man", 1,2,3));

            nutrientRecommendationRepository.SaveOrUpdate(CreateRecommendation(nutrient, "Kvinna", 1, 2, 4));
        }

        private NutrientRecommendation CreateRecommendation(Nutrient nutrient, string genderName, int startAge, int endAge, decimal value) {
            return new NutrientRecommendation { 
                Nutrient = nutrient,
                GenderRecommendations = new List<GenderNutrientRecommendation> {
                    new GenderNutrientRecommendation {
                        GenderType = new GenderType{Name = genderName},
                        GenderAgeRecommendations = new List<GenderAgeNutrientRecommendation> {
                            new GenderAgeNutrientRecommendation{StartAgeInterval = startAge,EndAgeInterval = endAge, Value = value}
                        }
                    }
                }
            };
        }
    }
}
