using System.Collections.Generic;
using CarbonFitness.Data.Model;

namespace CarbonFitness.BusinessLogic.IngredientImporter {
	public interface IIngredientParser {
		IList<Ingredient> CreateIngredientFromFileContents(string fileContents);
	}
}