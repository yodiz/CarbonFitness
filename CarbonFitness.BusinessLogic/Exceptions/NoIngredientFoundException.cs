using System;

namespace CarbonFitness.BusinessLogic.Exceptions {
	public class NoIngredientFoundException : Exception {
		private readonly string ingredientName;

		public NoIngredientFoundException(string ingredientName) {
			this.ingredientName = ingredientName;
		}

		public string IngredientName {
			get { return ingredientName; }
		}
	}
}