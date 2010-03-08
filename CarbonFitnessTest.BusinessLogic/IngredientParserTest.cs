using System;
using System.Linq;
using CarbonFitness.BusinessLogic.Exceptions;
using CarbonFitness.BusinessLogic.IngredientImporter.Implementation;
using NUnit.Framework;

namespace CarbonFitnessTest.BusinessLogic {
	[TestFixture]
	public class IngredientParserTest {
		[Test]
		public void shouldContainRowNumberAndCloumnContent() {
			try {
				new IngredientParser().GetDecimalValue(10, new[] {"a"}, 0);
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
			var ingredientParser = new IngredientParser();

			var resultList = ingredientParser.ParseTabSeparatedFileContents(fileContents);

			Assert.That(resultList.Count(), Is.EqualTo(3));
			Assert.That(resultList[0].Name, Is.EqualTo("Abborre"));
			Assert.That(resultList[0].WeightInG, Is.EqualTo(100M));
			Assert.That(resultList[0].FibresInG, Is.EqualTo(0.0M));
			Assert.That(resultList[0].EnergyInKJ, Is.EqualTo(359M));
			Assert.That(resultList[0].EnergyInKcal, Is.EqualTo(86M));
			Assert.That(resultList[0].CarbonHydrateInG, Is.EqualTo(0M));
			Assert.That(resultList[0].FatInG, Is.EqualTo(0.6M));
			Assert.That(resultList[0].DVitaminInµG, Is.EqualTo(21.4M));
			Assert.That(resultList[0].AscorbicAcidInmG, Is.EqualTo(0M));
			Assert.That(resultList[0].IronInmG, Is.EqualTo(0.24M));
			Assert.That(resultList[0].CalciumInmG, Is.EqualTo(16M));
			Assert.That(resultList[0].NatriumInmG, Is.EqualTo(33M));
			Assert.That(resultList[0].ProteinInG, Is.EqualTo(19.8M));
			Assert.That(resultList[0].AlcoholInG, Is.EqualTo(0M));
			Assert.That(resultList[0].SaturatedFatInG, Is.EqualTo(0.1M)); //mättat
			Assert.That(resultList[0].MonoUnSaturatedFatInG, Is.EqualTo(0.1M)); // enkelomättat
			Assert.That(resultList[0].PolyUnSaturatedFatInG, Is.EqualTo(0.2M));

			Assert.That(resultList[0].FatAcidC4C10InG, Is.EqualTo(0M));
			Assert.That(resultList[0].FatAcidC12InG, Is.EqualTo(0M));
			Assert.That(resultList[0].FatAcidC14InG, Is.EqualTo(0M));
			Assert.That(resultList[0].FatAcidC16InG, Is.EqualTo(0.1M));
			Assert.That(resultList[0].FatAcidC18InG, Is.EqualTo(0M));
			Assert.That(resultList[0].FatAcidC20InG, Is.EqualTo(0M));
			Assert.That(resultList[0].FatAcidC16_1InG, Is.EqualTo(0M));
			Assert.That(resultList[0].FatAcidC18_1InG, Is.EqualTo(0.1M));
			Assert.That(resultList[0].FatAcidC18_2InG, Is.EqualTo(0M));
			Assert.That(resultList[0].FatAcidC18_3InG, Is.EqualTo(0M));
			Assert.That(resultList[0].FatAcidC20_4InG, Is.EqualTo(0M));
			Assert.That(resultList[0].FatAcidC20_5InG, Is.EqualTo(0M));
			Assert.That(resultList[0].FatAcidC22_5InG, Is.EqualTo(0M));
			Assert.That(resultList[0].FatAcidC22_6InG, Is.EqualTo(0.1M));

			Assert.That(resultList[0].MonosaccharidesInG, Is.EqualTo(0M));
			Assert.That(resultList[0].DisaccharidesInG, Is.EqualTo(0M));
			Assert.That(resultList[0].SucroseInG, Is.EqualTo(0M)); // Sackaros
			Assert.That(resultList[0].RetinolEquivalentInµG, Is.EqualTo(15M));
			Assert.That(resultList[0].RetinolInµG, Is.EqualTo(15M));
			Assert.That(resultList[0].EVitaminInmG, Is.EqualTo(0.74M));

			Assert.That(resultList[0].TokopherolInmG, Is.EqualTo(0.74M));
			Assert.That(resultList[0].CaroteneInµG, Is.EqualTo(0M));
			Assert.That(resultList[0].ThiamineInmG, Is.EqualTo(0.15M));
			Assert.That(resultList[0].RiboflavinInmG, Is.EqualTo(0.11M));
			Assert.That(resultList[0].NiacinEquivalentInmG, Is.EqualTo(8.9M));
			Assert.That(resultList[0].NiacinInmG, Is.EqualTo(5.3M));
			Assert.That(resultList[0].VitaminB6InmG, Is.EqualTo(0.47M));
			Assert.That(resultList[0].VitaminB12InµG, Is.EqualTo(3.49M));
			Assert.That(resultList[0].FolicAcidInµG, Is.EqualTo(12M));
			Assert.That(resultList[0].PhosphorusInmG, Is.EqualTo(219M));
			Assert.That(resultList[0].PotassiumInmG, Is.EqualTo(389M)); //kalium
			Assert.That(resultList[0].MagnesiumInmG, Is.EqualTo(27M));
			Assert.That(resultList[0].SeleniumInµG, Is.EqualTo(44.1M));
			Assert.That(resultList[0].ZincInmG, Is.EqualTo(0.52M));
			Assert.That(resultList[0].CholesteroleInmG, Is.EqualTo(77.8M));
			Assert.That(resultList[0].AshInG, Is.EqualTo(1M));
			Assert.That(resultList[0].AquaInG, Is.EqualTo(78.6M));
			Assert.That(resultList[0].WasteInPercent, Is.EqualTo(60M));
		}

		[Test]
		public void shouldRemoveSpaceWhenParsingForDecimalValue() {
			Assert.That(new IngredientParser().GetDecimalValue(10, new[] {"1 52"}, 0), Is.EqualTo(152M));
		}

		[Test]
		public void shouldTreatEmptyColumnValueAsZero()
		{
			Assert.That(new IngredientParser().GetDecimalValue(10, new[] { "" }, 0), Is.EqualTo(0M));
		}

		[Test]
		public void shouldThrowErrorWhenNotUnderstandingDecimalValue() {
			Assert.Throws<IngredientParserException>(() => new IngredientParser().GetDecimalValue(10, new[] {"a"}, 0));
		}
	}
}