using System;
using System.Collections.Generic;
using System.Linq;
using SharpArch.Core.DomainModel;

namespace CarbonFitness.Data.Model {
    public class NutrientRecommendation : Entity {
        public virtual Nutrient Nutrient { get; set; }
        public virtual IEnumerable<GenderNutrientRecommendation> GenderRecommendations { get; set; }

        public virtual decimal GetValue(string genderName, int age) {
            try {
                GenderNutrientRecommendation genderRecommendation = (from g in GenderRecommendations where g.GenderType.Name == genderName select g).FirstOrDefault();
                if (genderRecommendation == null) {
                    throw new ArgumentNullException("Tried to fetch nutrient recommendation for Nutrient:" + Nutrient.Name + " with gender:" + genderName + " at age:" + age + " but found none.");
                }
                GenderAgeNutrientRecommendation ageRecommendation = genderRecommendation.GetGenderAgeRecommentation(age);

                return ageRecommendation.Value;
            } catch (NullReferenceException n) {
                throw new NullReferenceException("Tried to fetch nutrient recommendation for Nutrient:" + Nutrient.Name + " with gender:" + genderName + " at age:" + age + " but found none.", n);
            }
        }
    }
}