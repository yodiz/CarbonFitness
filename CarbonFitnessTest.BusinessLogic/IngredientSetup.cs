using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarbonFitness.Data.Model;
using NUnit.Framework;

namespace CarbonFitnessTest.BusinessLogic
{
   public class IngredientSetup
    {

       public Ingredient GetNewIngredient(string ingredientName, NutrientEntity nutrientEntity, decimal value, decimal weightInG)
       {
           return GetNewIngredient(ingredientName, Enum.GetName(typeof(NutrientEntity), nutrientEntity), value, weightInG);
       }

       public Ingredient GetNewIngredient(string ingredientName, string nutrientName, decimal value, decimal weightInG) {
           return new Ingredient { Name= ingredientName, WeightInG = weightInG, IngredientNutrients = new List<IngredientNutrient> { new IngredientNutrient() { Nutrient = new Nutrient() { Name = nutrientName }, Value = value } } };
       } 

    }
}
