using System;
using CarbonFitness.Data.Model;
using SharpArch.Core.DomainModel;

namespace CarbonFitness.Data.Model {
    public class Meal : Entity {
        public virtual DateTime CreatedDate { get; set; }
        public virtual User User { get; set; }

        public virtual MealIngredient AddIngredient(User user, Ingredient ingredient) {
            return null;
        }
    }
}