using SharpArch.Core.DomainModel;

namespace CarbonFitness.Data.Model {
    public class GenderAgeNutrientRecommendation : Entity{
        public virtual int StartAgeInterval { get; set; }
        public virtual int EndAgeInterval { get; set; }
        public virtual decimal Value { get; set; }
        public virtual GenderNutrientRecommendation GenderNutrientRecommendation { get; set; }
    }
}