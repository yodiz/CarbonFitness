using SharpArch.Core.DomainModel;

namespace CarbonFitness.Data.Model {
    public class MealIngredient : Entity {
		 public virtual Ingredient Ingredient{ get; set; }
		 public virtual int Measure { get; set; }
    }
}