using SharpArch.Core.DomainModel;

namespace CarbonFitness.Data.Model {
    public class IngredientNutrient : Entity {
        public virtual Ingredient Ingredient { get; set; }
        public virtual Nutrient Nutrient { get; set; }
        public virtual decimal Value { get; set; }
    }
}