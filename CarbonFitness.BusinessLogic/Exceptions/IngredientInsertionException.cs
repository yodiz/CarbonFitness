using System;
using CarbonFitness.Data.Model;

namespace CarbonFitness.BusinessLogic.Exceptions {
	public class IngredientInsertionException: Exception {
		public Ingredient Ingredient { get; set; }
		public IngredientInsertionException(string message, Exception innerException) : base(message, innerException) {}
		public override string Message {
			get { return base.Message + " IngredientName was: " + Ingredient.Name + "."; }
		}
	}
}