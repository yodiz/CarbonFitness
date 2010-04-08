using System;
using CarbonFitness.BusinessLogic.Exceptions;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;

namespace CarbonFitness.BusinessLogic.IngredientImporter.Implementation {
	public class IngredientImporter : IIngredientImporter {
		private readonly IIngredientFileReader ingredientFileReader;
		private readonly IIngredientParser ingredientParser;
		private readonly IIngredientRepository ingredientRepository;

		public IngredientImporter(IIngredientParser ingredientParser, IIngredientFileReader ingredientFileReader,
			IIngredientRepository ingredientRepository) {
			this.ingredientParser = ingredientParser;
			this.ingredientFileReader = ingredientFileReader;
			this.ingredientRepository = ingredientRepository;
		}

		public void Import(string filename) {
			var fileContents = ingredientFileReader.ReadIngredientFile(filename);
			var ingredients = ingredientParser.CreateIngredientFromFileContents(fileContents);

			foreach (var ingredient in ingredients) {
				SaveIngredient(ingredient);
			}
		}

		public void SaveIngredient(Ingredient ingredient) {
			try {
				ingredientRepository.SaveOrUpdate(ingredient);	
			} catch(Exception e) {
				throw new IngredientInsertionException("Error inserting ingredient.", e) {Ingredient = ingredient}; 
			}
		}
	}
}