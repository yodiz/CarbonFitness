using System;
using System.Collections.Generic;
using System.Linq;
using CarbonFitness.BusinessLogic.Exceptions;
using CarbonFitness.Data.Model;

namespace CarbonFitness.BusinessLogic.IngredientImporter.Implementation {
	public class IngredientParser : IIngredientParser {
	    private readonly INutrientBusinessLogic nutrientBusinessLogic;
	    public IngredientParser(INutrientBusinessLogic nutrientBusinessLogic) {
	        this.nutrientBusinessLogic = nutrientBusinessLogic;
	    }

	    public IList<Ingredient> CreateIngredientFromFileContents(string fileContents) {
			IList<Ingredient> ingredients = new List<Ingredient>();
			var rows = fileContents.Split(new[] {'\n'}, StringSplitOptions.RemoveEmptyEntries);
			for (var j = 1; j < rows.Length; j++) {
				var columns = rows[j].Split('\t');
				var ingredient = new Ingredient();
			    var ingredientNutrients = new List<IngredientNutrient>();
				var i = 0;
				ingredient.Name = columns[i++];
                ingredient.WeightInG = GetDecimalValue(j, columns, i++);

				addIngredientNutrient(ingredient, ingredientNutrients, NutrientEntity.FibresInG, GetDecimalValue(j, columns, i++));
				addIngredientNutrient(ingredient, ingredientNutrients, NutrientEntity.EnergyInKJ, GetDecimalValue(j, columns, i++));
				addIngredientNutrient(ingredient, ingredientNutrients, NutrientEntity.EnergyInKcal, GetDecimalValue(j, columns, i++));
				addIngredientNutrient(ingredient, ingredientNutrients, NutrientEntity.CarbonHydrateInG, GetDecimalValue(j, columns, i++));
				addIngredientNutrient(ingredient, ingredientNutrients, NutrientEntity.FatInG, GetDecimalValue(j, columns, i++));
				addIngredientNutrient(ingredient, ingredientNutrients, NutrientEntity.DVitaminInµG, GetDecimalValue(j, columns, i++));
				addIngredientNutrient(ingredient, ingredientNutrients, NutrientEntity.AscorbicAcidInmG, GetDecimalValue(j, columns, i++));
				addIngredientNutrient(ingredient, ingredientNutrients, NutrientEntity.IronInmG, GetDecimalValue(j, columns, i++));
				addIngredientNutrient(ingredient, ingredientNutrients, NutrientEntity.CalciumInmG, GetDecimalValue(j, columns, i++));
				addIngredientNutrient(ingredient, ingredientNutrients, NutrientEntity.NatriumInmG, GetDecimalValue(j, columns, i++));
				addIngredientNutrient(ingredient, ingredientNutrients, NutrientEntity.ProteinInG, GetDecimalValue(j, columns, i++));
				addIngredientNutrient(ingredient, ingredientNutrients, NutrientEntity.AlcoholInG, GetDecimalValue(j, columns, i++));
				addIngredientNutrient(ingredient, ingredientNutrients, NutrientEntity.SaturatedFatInG, GetDecimalValue(j, columns, i++));
				addIngredientNutrient(ingredient, ingredientNutrients, NutrientEntity.MonoUnSaturatedFatInG, GetDecimalValue(j, columns, i++));
				addIngredientNutrient(ingredient, ingredientNutrients, NutrientEntity.PolyUnSaturatedFatInG, GetDecimalValue(j, columns, i++));
				addIngredientNutrient(ingredient, ingredientNutrients, NutrientEntity.FatAcidC4C10InG, GetDecimalValue(j, columns, i++));
				addIngredientNutrient(ingredient, ingredientNutrients, NutrientEntity.FatAcidC12InG, GetDecimalValue(j, columns, i++));
				addIngredientNutrient(ingredient, ingredientNutrients, NutrientEntity.FatAcidC14InG, GetDecimalValue(j, columns, i++));
				addIngredientNutrient(ingredient, ingredientNutrients, NutrientEntity.FatAcidC16InG, GetDecimalValue(j, columns, i++));
				addIngredientNutrient(ingredient, ingredientNutrients, NutrientEntity.FatAcidC18InG, GetDecimalValue(j, columns, i++));
				addIngredientNutrient(ingredient, ingredientNutrients, NutrientEntity.FatAcidC20InG, GetDecimalValue(j, columns, i++));
				addIngredientNutrient(ingredient, ingredientNutrients, NutrientEntity.FatAcidC16_1InG, GetDecimalValue(j, columns, i++));
				addIngredientNutrient(ingredient, ingredientNutrients, NutrientEntity.FatAcidC18_1InG, GetDecimalValue(j, columns, i++));
				addIngredientNutrient(ingredient, ingredientNutrients, NutrientEntity.FatAcidC18_2InG, GetDecimalValue(j, columns, i++));
				addIngredientNutrient(ingredient, ingredientNutrients, NutrientEntity.FatAcidC18_3InG, GetDecimalValue(j, columns, i++));
				addIngredientNutrient(ingredient, ingredientNutrients, NutrientEntity.FatAcidC20_4InG, GetDecimalValue(j, columns, i++));
				addIngredientNutrient(ingredient, ingredientNutrients, NutrientEntity.FatAcidC20_5InG, GetDecimalValue(j, columns, i++));
				addIngredientNutrient(ingredient, ingredientNutrients, NutrientEntity.FatAcidC22_5InG, GetDecimalValue(j, columns, i++));
				addIngredientNutrient(ingredient, ingredientNutrients, NutrientEntity.MonosaccharidesInG, GetDecimalValue(j, columns, i++));
				addIngredientNutrient(ingredient, ingredientNutrients, NutrientEntity.DisaccharidesInG, GetDecimalValue(j, columns, i++));
				addIngredientNutrient(ingredient, ingredientNutrients, NutrientEntity.SucroseInG, GetDecimalValue(j, columns, i++));
				addIngredientNutrient(ingredient, ingredientNutrients, NutrientEntity.RetinolEquivalentInµG, GetDecimalValue(j, columns, i++));
				addIngredientNutrient(ingredient, ingredientNutrients, NutrientEntity.RetinolInµG, GetDecimalValue(j, columns, i++));
				addIngredientNutrient(ingredient, ingredientNutrients, NutrientEntity.EVitaminInmG, GetDecimalValue(j, columns, i++));
				addIngredientNutrient(ingredient, ingredientNutrients, NutrientEntity.TokopherolInmG, GetDecimalValue(j, columns, i++));
				addIngredientNutrient(ingredient, ingredientNutrients, NutrientEntity.CaroteneInµG, GetDecimalValue(j, columns, i++));
				addIngredientNutrient(ingredient, ingredientNutrients, NutrientEntity.ThiamineInmG, GetDecimalValue(j, columns, i++));
				addIngredientNutrient(ingredient, ingredientNutrients, NutrientEntity.RiboflavinInmG, GetDecimalValue(j, columns, i++));
				addIngredientNutrient(ingredient, ingredientNutrients, NutrientEntity.NiacinEquivalentInmG, GetDecimalValue(j, columns, i++));
				addIngredientNutrient(ingredient, ingredientNutrients, NutrientEntity.NiacinInmG, GetDecimalValue(j, columns, i++));
				addIngredientNutrient(ingredient, ingredientNutrients, NutrientEntity.VitaminB6InmG, GetDecimalValue(j, columns, i++));
				addIngredientNutrient(ingredient, ingredientNutrients, NutrientEntity.VitaminB12InµG, GetDecimalValue(j, columns, i++));
				addIngredientNutrient(ingredient, ingredientNutrients, NutrientEntity.FolicAcidInµG, GetDecimalValue(j, columns, i++));
				addIngredientNutrient(ingredient, ingredientNutrients, NutrientEntity.PhosphorusInmG, GetDecimalValue(j, columns, i++));
				addIngredientNutrient(ingredient, ingredientNutrients, NutrientEntity.PotassiumInmG, GetDecimalValue(j, columns, i++));
				addIngredientNutrient(ingredient, ingredientNutrients, NutrientEntity.MagnesiumInmG, GetDecimalValue(j, columns, i++));
				addIngredientNutrient(ingredient, ingredientNutrients, NutrientEntity.SeleniumInµG, GetDecimalValue(j, columns, i++));
				addIngredientNutrient(ingredient, ingredientNutrients, NutrientEntity.ZincInmG, GetDecimalValue(j, columns, i++));
				addIngredientNutrient(ingredient, ingredientNutrients, NutrientEntity.CholesteroleInmG, GetDecimalValue(j, columns, i++));
				addIngredientNutrient(ingredient, ingredientNutrients, NutrientEntity.AshInG, GetDecimalValue(j, columns, i++));
				addIngredientNutrient(ingredient, ingredientNutrients, NutrientEntity.AquaInG, GetDecimalValue(j, columns, i++));
				addIngredientNutrient(ingredient, ingredientNutrients, NutrientEntity.WasteInPercent, GetDecimalValue(j, columns, i++));
				addIngredientNutrient(ingredient, ingredientNutrients, NutrientEntity.FatAcidC22_6InG, GetDecimalValue(j, columns, i++));

			    ingredient.IngredientNutrients = ingredientNutrients;
				ingredients.Add(ingredient);
			}
			return ingredients;
		}

	    private void addIngredientNutrient(Ingredient ingredient, ICollection<IngredientNutrient> ingredientNutrients, NutrientEntity entity, decimal value) {
	        var ingredientNutrient = new IngredientNutrient {Ingredient = ingredient, Nutrient = nutrientBusinessLogic.GetNutrient(entity), Value = value};
            ingredientNutrients.Add(ingredientNutrient);
	    }

	    public decimal GetDecimalValue(int rowIndex, string[] columns, int columnIndex) {
			var column = columns[columnIndex];
			try {
				if (column.Length == 0) {
					return 0;
				}

				var cleanedColumn = new string(column.Where(c => (c >= 48 && c <= 57) || c == 44).ToArray());
				return Convert.ToDecimal(cleanedColumn);
			}
			catch (FormatException e) {
				throw new IngredientParserException("Error parsing decimal value.", e) {ColumnContent = column, ColumnIndex = columnIndex, RowIndex = rowIndex};
			}
		}
	}

    
}