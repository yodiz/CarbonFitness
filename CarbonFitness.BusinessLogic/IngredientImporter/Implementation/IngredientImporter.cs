using System;
using System.Collections.Generic;
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
			var ingredients = ingredientParser.ParseTabSeparatedFileContents(fileContents);

			foreach (var ingredient in ingredients) {
				ingredientRepository.SaveOrUpdate(ingredient);
			}
		}

	}
}