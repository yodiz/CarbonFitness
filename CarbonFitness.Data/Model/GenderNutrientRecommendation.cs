using System.Collections.Generic;
using System.Linq;
using SharpArch.Core.DomainModel;

namespace CarbonFitness.Data.Model {
    public class GenderNutrientRecommendation : Entity {
        public virtual GenderType GenderType { get; set; }
        public virtual IEnumerable<GenderAgeNutrientRecommendation> GenderAgeRecommendations { get; set; }
        public virtual NutrientRecommendation NutrientRecommendation {get; set;}
       // public virtual int NutrientRecommendation_Id { get; set; }

        public virtual GenderAgeNutrientRecommendation GetGenderAgeRecommentation(int age) {
            return (from g in GenderAgeRecommendations where g.StartAgeInterval < age && g.EndAgeInterval >= age select g).FirstOrDefault();
        }
    }
}