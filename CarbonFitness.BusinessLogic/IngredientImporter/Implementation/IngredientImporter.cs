using System;
using System.Collections.Generic;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;

namespace CarbonFitness.BusinessLogic.IngredientImporter.Implementation {
    public class IngredientImporter : IIngredientImporter {
        public IngredientImporter(IIngredientParser ingredientParser, IIngredientFileReader ingredientFileReader, IIngredientRepository ingredientRepository) {
            throw new NotImplementedException();
        }

        public IList<Ingredient> ParseTabSeparatedFileContents(string fileContents) {
            IList<Ingredient> ingredients = new List<Ingredient>();
            var rows = fileContents.Split(new []{'\n'}, StringSplitOptions.RemoveEmptyEntries);
            for (int j = 1; j < rows.Length; j++) {
                var columns = rows[j].Split('\t');
                var ingredient = new Ingredient();
                var i = 0;
                ingredient.Name = columns[i++];
                ingredient.WeightInG = Convert.ToDecimal(columns[i++]);
                ingredient.FibresInG = Convert.ToDecimal(columns[i++]);
                ingredient.EnergyInKJ = Convert.ToDecimal(columns[i++]);
                ingredient.EnergyInKcal = Convert.ToDecimal(columns[i++]);
                ingredient.CarbonHydrateInG = Convert.ToDecimal(columns[i++]);
                ingredient.FatInG = Convert.ToDecimal(columns[i++]);
                ingredient.DVitaminInµG = Convert.ToDecimal(columns[i++]);
                ingredient.AscorbicAcidInmG = Convert.ToDecimal(columns[i++]);
                ingredient.IronInmG = Convert.ToDecimal(columns[i++]);
                ingredient.CalciumInmG = Convert.ToDecimal(columns[i++]);
                ingredient.NatriumInmG = Convert.ToDecimal(columns[i++]);
                ingredient.ProteinInG = Convert.ToDecimal(columns[i++]);
                ingredient.AlcoholInG = Convert.ToDecimal(columns[i++]);
                ingredient.SaturatedFatInG = Convert.ToDecimal(columns[i++]);
                ingredient.MonoUnSaturatedFatInG = Convert.ToDecimal(columns[i++]);
                ingredient.PolyUnSaturatedFatInG = Convert.ToDecimal(columns[i++]);
                ingredient.FatAcidC4C10InG = Convert.ToDecimal(columns[i++]);
                ingredient.FatAcidC12InG = Convert.ToDecimal(columns[i++]);
                ingredient.FatAcidC14InG = Convert.ToDecimal(columns[i++]);
                ingredient.FatAcidC16InG = Convert.ToDecimal(columns[i++]);
                ingredient.FatAcidC18InG = Convert.ToDecimal(columns[i++]);
                ingredient.FatAcidC20InG = Convert.ToDecimal(columns[i++]);
                ingredient.FatAcidC16_1InG = Convert.ToDecimal(columns[i++]);
                ingredient.FatAcidC18_1InG = Convert.ToDecimal(columns[i++]);
                ingredient.FatAcidC18_2InG = Convert.ToDecimal(columns[i++]);
                ingredient.FatAcidC18_3InG = Convert.ToDecimal(columns[i++]);
                ingredient.FatAcidC20_4InG = Convert.ToDecimal(columns[i++]);
                ingredient.FatAcidC20_5InG = Convert.ToDecimal(columns[i++]);
                ingredient.FatAcidC22_5InG = Convert.ToDecimal(columns[i++]);
                ingredient.MonosaccharidesInG = Convert.ToDecimal(columns[i++]);
                ingredient.DisaccharidesInG = Convert.ToDecimal(columns[i++]);
                ingredient.SucroseInG = Convert.ToDecimal(columns[i++]);
                ingredient.RetinolEquivalentInµG = Convert.ToDecimal(columns[i++]);
                ingredient.RetinolInµG = Convert.ToDecimal(columns[i++]);
                ingredient.EVitaminInmG = Convert.ToDecimal(columns[i++]);
                ingredient.TokopherolInmG = Convert.ToDecimal(columns[i++]);
                ingredient.CaroteneInµG = Convert.ToDecimal(columns[i++]);
                ingredient.ThiamineInmG = Convert.ToDecimal(columns[i++]);
                ingredient.RiboflavinInmG = Convert.ToDecimal(columns[i++]);
                ingredient.NiacinEquivalentInmG = Convert.ToDecimal(columns[i++]);
                ingredient.NiacinInmG = Convert.ToDecimal(columns[i++]);
                ingredient.VitaminB6InmG = Convert.ToDecimal(columns[i++]);
                ingredient.VitaminB12InµG = Convert.ToDecimal(columns[i++]);
                ingredient.FolicAcidInµG = Convert.ToDecimal(columns[i++]);
                ingredient.PhosphorusInmG = Convert.ToDecimal(columns[i++]);
                ingredient.PotassiumInmG = Convert.ToDecimal(columns[i++]);
                ingredient.MagnesiumInmG = Convert.ToDecimal(columns[i++]);
                ingredient.SeleniumInµG = Convert.ToDecimal(columns[i++]);
                ingredient.ZincInmG = Convert.ToDecimal(columns[i++]);
                ingredient.CholesteroleInmG = Convert.ToDecimal(columns[i++]);
                ingredient.AshInG = Convert.ToDecimal(columns[i++]);
                ingredient.AquaInG = Convert.ToDecimal(columns[i++]);
                ingredient.WasteInPercent = Convert.ToDecimal(columns[i++]);
                ingredient.FatAcidC22_6InG = Convert.ToDecimal(columns[i++]);
                ingredients.Add(ingredient);
            }
            return ingredients;
        }

        public void Import(string filename) {
            throw new NotImplementedException();
        }
    }
}