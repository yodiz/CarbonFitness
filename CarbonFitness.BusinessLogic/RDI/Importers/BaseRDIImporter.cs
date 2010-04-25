using System.Collections.Generic;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;

namespace CarbonFitness.BusinessLogic.RDI.Importers {
    public abstract class BaseRDIImporter {
        private readonly INutrientBusinessLogic nutrientBusinessLogic;
        private readonly IGenderTypeBusinessLogic genderTypeBusinessLogic;

        protected List<GenderAgeNutrientRecommendation> ManRecommendations = new List<GenderAgeNutrientRecommendation>();
        protected List<GenderAgeNutrientRecommendation> WomanRecommendations = new List<GenderAgeNutrientRecommendation>();

        protected abstract NutrientEntity nutrientEntity { get; }
        private Nutrient nutrient { get { return nutrientBusinessLogic.GetNutrient(nutrientEntity); } }
        protected GenderType man { get { return genderTypeBusinessLogic.GetGenderType("Man"); } }
        protected GenderType woman { get { return genderTypeBusinessLogic.GetGenderType("Kvinna"); } }

        public BaseRDIImporter(INutrientBusinessLogic nutrientBusinessLogic, IGenderTypeBusinessLogic genderTypeBusinessLogic) {
            this.nutrientBusinessLogic = nutrientBusinessLogic;
            this.genderTypeBusinessLogic = genderTypeBusinessLogic;
        }

        public void Import(INutrientRecommendationRepository nutrientRecommendationRepository) {
            nutrientRecommendationRepository.Save(getNutrientRecommendation());
        }

        public GenderAgeNutrientRecommendation createAgeRecommendation(int startAge, int endAge, decimal value) {
            return new GenderAgeNutrientRecommendation {
                StartAgeInterval = startAge,
                EndAgeInterval = endAge,
                Value = value
            };
        }

        protected NutrientRecommendation getNutrientRecommendation() {
            var nutrientRecommendation = new NutrientRecommendation {
                Nutrient = nutrient
            };
            nutrientRecommendation.GenderRecommendations = GetGenderNutrientRecommendations(nutrientRecommendation);
            return nutrientRecommendation;
        }

        protected IEnumerable<GenderNutrientRecommendation> GetGenderNutrientRecommendations(NutrientRecommendation nutrientRecommendation) {
            var manNutrientRecommendations = new GenderNutrientRecommendation {
                GenderType = man,
                NutrientRecommendation = nutrientRecommendation,
            };
            manNutrientRecommendations.GenderAgeRecommendations = GetAgeRecommendations(ManRecommendations, manNutrientRecommendations);
            var womanNutrientRecommendations = new GenderNutrientRecommendation {
                GenderType = woman,
                NutrientRecommendation = nutrientRecommendation
            };
            womanNutrientRecommendations.GenderAgeRecommendations = GetAgeRecommendations(WomanRecommendations, womanNutrientRecommendations);
            return new List<GenderNutrientRecommendation> { manNutrientRecommendations, womanNutrientRecommendations };
        }

        private IEnumerable<GenderAgeNutrientRecommendation> GetAgeRecommendations(IEnumerable<GenderAgeNutrientRecommendation> ageNutrientRecommendations, GenderNutrientRecommendation genderNutrientRecommendation) {
            foreach (var recommendation in ageNutrientRecommendations) {
                recommendation.GenderNutrientRecommendation = genderNutrientRecommendation;
            }
            return ageNutrientRecommendations;
        }
    }
}