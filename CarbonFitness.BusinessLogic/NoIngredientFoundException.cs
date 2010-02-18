using System;

namespace CarbonFitness.BusinessLogic
{
    public class NoIngredientFoundException : Exception {
        private readonly string _ingredientName;

        public NoIngredientFoundException(string ingredientName)
        {
            _ingredientName = ingredientName;
        }

        public string IngredientName
        {
            get { return _ingredientName; }
        }
    }
}