using System;
using System.Collections.Generic;
using System.Linq;
using CarbonFitness.BusinessLogic.Exceptions;
using CarbonFitness.Data.Model;

namespace CarbonFitness.BusinessLogic.IngredientImporter.Implementation {
	public class IngredientParser : IIngredientParser {
		public IList<Ingredient> ParseTabSeparatedFileContents(string fileContents) {
			IList<Ingredient> ingredients = new List<Ingredient>();
			var rows = fileContents.Split(new[] {'\n'}, StringSplitOptions.RemoveEmptyEntries);
			for (var j = 1; j < rows.Length; j++) {
				var columns = rows[j].Split('\t');
				var ingredient = new Ingredient();
				var i = 0;
				ingredient.Name = columns[i++];

				ingredient.WeightInG = GetDecimalValue(j, columns, i++);
				ingredient.FibresInG = GetDecimalValue(j, columns, i++);
				ingredient.EnergyInKJ = GetDecimalValue(j, columns, i++);
				ingredient.EnergyInKcal = GetDecimalValue(j, columns, i++);
				ingredient.CarbonHydrateInG = GetDecimalValue(j, columns, i++);
				ingredient.FatInG = GetDecimalValue(j, columns, i++);
				ingredient.DVitaminInµG = GetDecimalValue(j, columns, i++);
				ingredient.AscorbicAcidInmG = GetDecimalValue(j, columns, i++);
				ingredient.IronInmG = GetDecimalValue(j, columns, i++);
				ingredient.CalciumInmG = GetDecimalValue(j, columns, i++);
				ingredient.NatriumInmG = GetDecimalValue(j, columns, i++);
				ingredient.ProteinInG = GetDecimalValue(j, columns, i++);
				ingredient.AlcoholInG = GetDecimalValue(j, columns, i++);
				ingredient.SaturatedFatInG = GetDecimalValue(j, columns, i++);
				ingredient.MonoUnSaturatedFatInG = GetDecimalValue(j, columns, i++);
				ingredient.PolyUnSaturatedFatInG = GetDecimalValue(j, columns, i++);
				ingredient.FatAcidC4C10InG = GetDecimalValue(j, columns, i++);
				ingredient.FatAcidC12InG = GetDecimalValue(j, columns, i++);
				ingredient.FatAcidC14InG = GetDecimalValue(j, columns, i++);
				ingredient.FatAcidC16InG = GetDecimalValue(j, columns, i++);
				ingredient.FatAcidC18InG = GetDecimalValue(j, columns, i++);
				ingredient.FatAcidC20InG = GetDecimalValue(j, columns, i++);
				ingredient.FatAcidC16_1InG = GetDecimalValue(j, columns, i++);
				ingredient.FatAcidC18_1InG = GetDecimalValue(j, columns, i++);
				ingredient.FatAcidC18_2InG = GetDecimalValue(j, columns, i++);
				ingredient.FatAcidC18_3InG = GetDecimalValue(j, columns, i++);
				ingredient.FatAcidC20_4InG = GetDecimalValue(j, columns, i++);
				ingredient.FatAcidC20_5InG = GetDecimalValue(j, columns, i++);
				ingredient.FatAcidC22_5InG = GetDecimalValue(j, columns, i++);
				ingredient.MonosaccharidesInG = GetDecimalValue(j, columns, i++);
				ingredient.DisaccharidesInG = GetDecimalValue(j, columns, i++);
				ingredient.SucroseInG = GetDecimalValue(j, columns, i++);
				ingredient.RetinolEquivalentInµG = GetDecimalValue(j, columns, i++);
				ingredient.RetinolInµG = GetDecimalValue(j, columns, i++);
				ingredient.EVitaminInmG = GetDecimalValue(j, columns, i++);
				ingredient.TokopherolInmG = GetDecimalValue(j, columns, i++);
				ingredient.CaroteneInµG = GetDecimalValue(j, columns, i++);
				ingredient.ThiamineInmG = GetDecimalValue(j, columns, i++);
				ingredient.RiboflavinInmG = GetDecimalValue(j, columns, i++);
				ingredient.NiacinEquivalentInmG = GetDecimalValue(j, columns, i++);
				ingredient.NiacinInmG = GetDecimalValue(j, columns, i++);
				ingredient.VitaminB6InmG = GetDecimalValue(j, columns, i++);
				ingredient.VitaminB12InµG = GetDecimalValue(j, columns, i++);
				ingredient.FolicAcidInµG = GetDecimalValue(j, columns, i++);
				ingredient.PhosphorusInmG = GetDecimalValue(j, columns, i++);
				ingredient.PotassiumInmG = GetDecimalValue(j, columns, i++);
				ingredient.MagnesiumInmG = GetDecimalValue(j, columns, i++);
				ingredient.SeleniumInµG = GetDecimalValue(j, columns, i++);
				ingredient.ZincInmG = GetDecimalValue(j, columns, i++);
				ingredient.CholesteroleInmG = GetDecimalValue(j, columns, i++);
				ingredient.AshInG = GetDecimalValue(j, columns, i++);
				ingredient.AquaInG = GetDecimalValue(j, columns, i++);
				ingredient.WasteInPercent = GetDecimalValue(j, columns, i++);
				ingredient.FatAcidC22_6InG = GetDecimalValue(j, columns, i++);
				ingredients.Add(ingredient);
			}
			return ingredients;
		}

		public decimal GetDecimalValue(int rowIndex, string[] columns, int columnIndex) {
			var column = columns[columnIndex];
			try {
				if (column.Length == 0){
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