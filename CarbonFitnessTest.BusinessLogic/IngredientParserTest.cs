using System;
using System.Linq;
using System.Linq.Expressions;
using CarbonFitness.BusinessLogic;
using CarbonFitness.BusinessLogic.Exceptions;
using CarbonFitness.BusinessLogic.IngredientImporter.Implementation;
using CarbonFitness.Data.Model;
using Moq;
using NUnit.Framework;

namespace CarbonFitnessTest.BusinessLogic {
	[TestFixture]
	public class IngredientParserTest {
		[Test]
		public void shouldContainRowNumberAndCloumnContent() {
			try {
				new IngredientParser(null).GetDecimalValue(10, new[] {"a"}, 0);
				Assert.Fail("Did not throw ingredient exception.");
			}
			catch (IngredientParserException e) {
				Assert.That(e.ColumnIndex, Is.EqualTo(0));
				Assert.That(e.ColumnContent, Is.EqualTo("a"));
				Assert.That(e.RowIndex, Is.EqualTo(10));
				Assert.That(e.InnerException.GetType(), Is.EqualTo(typeof(FormatException)));
			}
		}

		[Test]
		public void shouldParseIngredientsFromFile() {
			const string fileContents =
				@"Livsmedel 	Vikt (g)	Fibrer (g)	Energi kJ (kJ)	Energi kcal (kcal)	Kolhydrat (g)	Fett (g)	D vitamin (µg)	Askorbinsyra (mg)	Järn (mg)	Kalcium (mg)	Natrium (mg)	Protein (g)	Alkohol (g)	Mättade fettsyror (g)	Enkelomättade fettsyror (g)	Fleromättade fettsyror (g)	Fettsyra C4-C10 (g)	Fettsyra C12 (g)	Fettsyra C14 (g)	Fettsyra C16 (g)	Fettsyra C18 (g)	Fettsyra C20 (g)	Fettsyra C16:1 (g)	Fettsyra C18:1 (g)	Fettsyra C18:2 (g)	Fettsyra C18:3 (g)	Fettsyra C20:4 (g)	Fettsyra C20:5 (g)	Fettsyra C22:5 (g)	Monosackarider (g)	Disackarider (g)	Sackaros (g)	Retinolekvivalent (µg)	Retinol (µg)	E vitamin (mg)	?-Tokoferol (mg)	Karoten (µg)	Tiamin (mg)	Riboflavin (mg)	Niacinekvivalent (mg)	Niacin (mg)	Vitamin B6 (mg)	Vitamin B12 (µg)	Folat (µg)	Fosfor (mg)	Kalium (mg)	Magnesium (mg)	Selen (µg)	Zink (mg)	Kolesterol (mg)	Aska (g)	Vatten (g)	Avfall (%)	Fettsyra C22:6 (g)	
Abborre	100	0,0	359	86	0,0	0,6	21,40	0	0,24	16	33	19,8	0,0	0,1	0,1	0,2	0,0	0,0	0,0	0,1	0,0	0,0	0,0	0,1	0,0	0,0	0,0	0,0	0,0	0,0	0,0	0,0	15	15	0,74	0,74	0	0,15	0,11	8,9	5,3	0,47	3,49	12	219	389	27	44,1	0,52	77,8	1,0	78,6	60	0,1	
Abborre kokt	100	0,0	453	108	0,0	0,8	26,98	0	0,31	21	200	25,0	0,0	0,1	0,1	0,3	0,0	0,0	0,0	0,1	0,0	0,0	0,0	0,1	0,0	0,0	0,0	0,0	0,0	0,0	0,0	0,0	19	19	0,93	0,93	0	0,17	0,13	11,2	6,6	0,56	3,96	10	276	491	34	55,6	0,66	98,1	1,7	72,6	76	0,2	
Abborrfilé panerad stekt	100	0,3	599	143	3,8	5,7	19,36	0	0,44	21	450	18,7	0,0	0,8	3,0	1,6	0,0	0,0	0,0	0,5	0,2	0,0	0,1	2,9	0,9	0,4	0,0	0,0	0,0	0,1	0,1	0,0	27	68	1,87	1,87	3	0,13	0,11	8,2	4,7	0,34	2,68	12	216	364	28	40,3	0,55	95,2	2,0	69,4	53	0,1
";
		    var nutrientBusinessLogicMock = new Mock<INutrientBusinessLogic>();

            nutrientBusinessLogicMock.Setup(x => x.GetNutrient(It.Is<NutrientEntity>(y => entityToGet(y)))).Returns(DoReturn);
                //y => {entityToSave = Enum.GetName(typeof(NutrientEntity), y); return true; }))).Returns(new Nutrient());

            var ingredientParser = new IngredientParser(nutrientBusinessLogicMock.Object);

			var ingredients = ingredientParser.CreateIngredientFromFileContents(fileContents);

			Assert.That(ingredients.Count(), Is.EqualTo(3));
			Assert.That(ingredients[0].Name, Is.EqualTo("Abborre"));
			Assert.That(ingredients[0].WeightInG, Is.EqualTo(100M));
			Assert.That(ingredients[0].GetNutrient(NutrientEntity.FibresInG).Value, Is.EqualTo(0.0M));
            Assert.That(ingredients[0].GetNutrient(NutrientEntity.EnergyInKJ).Value, Is.EqualTo(359M));
			Assert.That(ingredients[0].GetNutrient(NutrientEntity.EnergyInKcal).Value, Is.EqualTo(86M));
			Assert.That(ingredients[0].GetNutrient(NutrientEntity.CarbonHydrateInG).Value, Is.EqualTo(0M));
			Assert.That(ingredients[0].GetNutrient(NutrientEntity.FatInG).Value, Is.EqualTo(0.6M));
			Assert.That(ingredients[0].GetNutrient(NutrientEntity.DVitaminInµG).Value, Is.EqualTo(21.4M));
			Assert.That(ingredients[0].GetNutrient(NutrientEntity.AscorbicAcidInmG).Value, Is.EqualTo(0M));
			Assert.That(ingredients[0].GetNutrient(NutrientEntity.IronInmG).Value, Is.EqualTo(0.24M));
			Assert.That(ingredients[0].GetNutrient(NutrientEntity.CalciumInmG).Value, Is.EqualTo(16M));
			Assert.That(ingredients[0].GetNutrient(NutrientEntity.NatriumInmG).Value, Is.EqualTo(33M));
			Assert.That(ingredients[0].GetNutrient(NutrientEntity.ProteinInG).Value, Is.EqualTo(19.8M));
			Assert.That(ingredients[0].GetNutrient(NutrientEntity.AlcoholInG).Value, Is.EqualTo(0M));
			Assert.That(ingredients[0].GetNutrient(NutrientEntity.SaturatedFatInG).Value, Is.EqualTo(0.1M)); //mättat
			Assert.That(ingredients[0].GetNutrient(NutrientEntity.MonoUnSaturatedFatInG).Value, Is.EqualTo(0.1M)); // enkelomättat
			Assert.That(ingredients[0].GetNutrient(NutrientEntity.PolyUnSaturatedFatInG).Value, Is.EqualTo(0.2M));

			Assert.That(ingredients[0].GetNutrient(NutrientEntity.FatAcidC4C10InG).Value, Is.EqualTo(0M));
			Assert.That(ingredients[0].GetNutrient(NutrientEntity.FatAcidC12InG).Value, Is.EqualTo(0M));
			Assert.That(ingredients[0].GetNutrient(NutrientEntity.FatAcidC14InG).Value, Is.EqualTo(0M));
			Assert.That(ingredients[0].GetNutrient(NutrientEntity.FatAcidC16InG).Value, Is.EqualTo(0.1M));
			Assert.That(ingredients[0].GetNutrient(NutrientEntity.FatAcidC18InG).Value, Is.EqualTo(0M));
			Assert.That(ingredients[0].GetNutrient(NutrientEntity.FatAcidC20InG).Value, Is.EqualTo(0M));
			Assert.That(ingredients[0].GetNutrient(NutrientEntity.FatAcidC16_1InG).Value, Is.EqualTo(0M));
			Assert.That(ingredients[0].GetNutrient(NutrientEntity.FatAcidC18_1InG).Value, Is.EqualTo(0.1M));
			Assert.That(ingredients[0].GetNutrient(NutrientEntity.FatAcidC18_2InG).Value, Is.EqualTo(0M));
			Assert.That(ingredients[0].GetNutrient(NutrientEntity.FatAcidC18_3InG).Value, Is.EqualTo(0M));
			Assert.That(ingredients[0].GetNutrient(NutrientEntity.FatAcidC20_4InG).Value, Is.EqualTo(0M));
			Assert.That(ingredients[0].GetNutrient(NutrientEntity.FatAcidC20_5InG).Value, Is.EqualTo(0M));
			Assert.That(ingredients[0].GetNutrient(NutrientEntity.FatAcidC22_5InG).Value, Is.EqualTo(0M));
			Assert.That(ingredients[0].GetNutrient(NutrientEntity.FatAcidC22_6InG).Value, Is.EqualTo(0.1M));

			Assert.That(ingredients[0].GetNutrient(NutrientEntity.MonosaccharidesInG).Value, Is.EqualTo(0M));
			Assert.That(ingredients[0].GetNutrient(NutrientEntity.DisaccharidesInG).Value, Is.EqualTo(0M));
			Assert.That(ingredients[0].GetNutrient(NutrientEntity.SucroseInG).Value, Is.EqualTo(0M)); // Sackaros
			Assert.That(ingredients[0].GetNutrient(NutrientEntity.RetinolEquivalentInµG).Value, Is.EqualTo(15M));
			Assert.That(ingredients[0].GetNutrient(NutrientEntity.RetinolInµG).Value, Is.EqualTo(15M));
			Assert.That(ingredients[0].GetNutrient(NutrientEntity.EVitaminInmG).Value, Is.EqualTo(0.74M));

			Assert.That(ingredients[0].GetNutrient(NutrientEntity.TokopherolInmG).Value, Is.EqualTo(0.74M));
			Assert.That(ingredients[0].GetNutrient(NutrientEntity.CaroteneInµG).Value, Is.EqualTo(0M));
			Assert.That(ingredients[0].GetNutrient(NutrientEntity.ThiamineInmG).Value, Is.EqualTo(0.15M));
			Assert.That(ingredients[0].GetNutrient(NutrientEntity.RiboflavinInmG).Value, Is.EqualTo(0.11M));
			Assert.That(ingredients[0].GetNutrient(NutrientEntity.NiacinEquivalentInmG).Value, Is.EqualTo(8.9M));
			Assert.That(ingredients[0].GetNutrient(NutrientEntity.NiacinInmG).Value, Is.EqualTo(5.3M));
			Assert.That(ingredients[0].GetNutrient(NutrientEntity.VitaminB6InmG).Value, Is.EqualTo(0.47M));
			Assert.That(ingredients[0].GetNutrient(NutrientEntity.VitaminB12InµG).Value, Is.EqualTo(3.49M));
			Assert.That(ingredients[0].GetNutrient(NutrientEntity.FolicAcidInµG).Value, Is.EqualTo(12M));
			Assert.That(ingredients[0].GetNutrient(NutrientEntity.PhosphorusInmG).Value, Is.EqualTo(219M));
			Assert.That(ingredients[0].GetNutrient(NutrientEntity.PotassiumInmG).Value, Is.EqualTo(389M)); //kalium
			Assert.That(ingredients[0].GetNutrient(NutrientEntity.MagnesiumInmG).Value, Is.EqualTo(27M));
			Assert.That(ingredients[0].GetNutrient(NutrientEntity.SeleniumInµG).Value, Is.EqualTo(44.1M));
			Assert.That(ingredients[0].GetNutrient(NutrientEntity.ZincInmG).Value, Is.EqualTo(0.52M));
			Assert.That(ingredients[0].GetNutrient(NutrientEntity.CholesteroleInmG).Value, Is.EqualTo(77.8M));
			Assert.That(ingredients[0].GetNutrient(NutrientEntity.AshInG).Value, Is.EqualTo(1M));
			Assert.That(ingredients[0].GetNutrient(NutrientEntity.AquaInG).Value, Is.EqualTo(78.6M));
			Assert.That(ingredients[0].GetNutrient(NutrientEntity.WasteInPercent).Value, Is.EqualTo(60M));
		}

        private Nutrient DoReturn() {
            return new Nutrient {Name = entityToSave};
        }

	    static string entityToSave = null;
        private bool entityToGet(NutrientEntity entity) {
            entityToSave = Enum.GetName(typeof(NutrientEntity), entity);
            return true;
	    }

	    [Test]
		public void shouldRemoveSpaceWhenParsingForDecimalValue() {
			Assert.That(new IngredientParser(null).GetDecimalValue(10, new[] {"1 52"}, 0), Is.EqualTo(152M));
		}

		[Test]
		public void shouldThrowErrorWhenNotUnderstandingDecimalValue() {
            Assert.Throws<IngredientParserException>(() => new IngredientParser(null).GetDecimalValue(10, new[] { "a" }, 0));
		}

		[Test]
		public void shouldTreatEmptyColumnValueAsZero() {
            Assert.That(new IngredientParser(null).GetDecimalValue(10, new[] { "" }, 0), Is.EqualTo(0M));
		}
	}
}