using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarbonFitness.App.Web.Controllers.ViewTypeConverters;
using CarbonFitness.BusinessLogic;
using CarbonFitness.Data.Model;
using CarbonFitness.Translation;
using Moq;
using NUnit.Framework;

namespace CarbonFitnessTest.Web.Controller.ResultController
{
    [TestFixture]
    public class NutrientViewTypeConverterTest
    {
        public NutrientViewTypeConverterTest() {
            nutrientBusinessLogicMock =  new Mock<INutrientBusinessLogic>();
            nutrientTranslatorMock = new Mock<INutrientTranslator>();
        }

        private readonly Mock<INutrientBusinessLogic> nutrientBusinessLogicMock;
        private readonly Mock<INutrientTranslator> nutrientTranslatorMock;

        private GraphLineOptionViewTypeConverter getGraphLineOptionViewTypeConverter() {
            return new GraphLineOptionViewTypeConverter(nutrientBusinessLogicMock.Object, nutrientTranslatorMock.Object); 
        }

        [Test]
        public void shouldShowNutrientsAndWeight() {
            IEnumerable<Nutrient> expectedNutrients = new[] { new Nutrient(), new Nutrient() };
            nutrientBusinessLogicMock.Setup(x => x.GetNutrients()).Returns(expectedNutrients);
            var translatedNutrient = "expect :)";
            nutrientTranslatorMock.Setup(x => x.GetString(It.IsAny<string>())).Returns(translatedNutrient);
            var graphlines = getGraphLineOptionViewTypeConverter().GetViewTypes(null);
            var nutrient = (from n in graphlines where n.Text == translatedNutrient select n).FirstOrDefault();

            Assert.That(nutrient.Text, Is.EqualTo(translatedNutrient));

            var expectedWeightName = "Vikt (kg)";
            var weight = (from n in graphlines where n.Text == expectedWeightName select n).FirstOrDefault();

            Assert.That(weight.Text, Is.EqualTo(expectedWeightName));
        }
        [Test]
        public void shouldNotThrowWhenUnexpectedStringTransformsToNutrientEntity() {
              string[] nutrients = new[] { "No GOod Nutrient Entity", NutrientEntity.ZincInmG.ToString() };
              NutrientEntity[] nutrientEntitys = getGraphLineOptionViewTypeConverter().GetNutrientEntitys(nutrients);

            Assert.That(nutrientEntitys.Length, Is.EqualTo(1));
            Assert.That(nutrientEntitys[0], Is.EqualTo(NutrientEntity.ZincInmG));

        }

        [Test]
        public void shouldGetNutrientEntitysFromStrings() {
           string[] nutrients = new[] { NutrientEntity.ZincInmG.ToString(), NutrientEntity.EVitaminInmG.ToString() };
           NutrientEntity[] nutrientEntitys = getGraphLineOptionViewTypeConverter().GetNutrientEntitys(nutrients);

            Assert.That(nutrientEntitys.Length, Is.EqualTo(2));
            Assert.That(nutrientEntitys[0], Is.EqualTo(NutrientEntity.ZincInmG));
            Assert.That(nutrientEntitys[1], Is.EqualTo(NutrientEntity.EVitaminInmG));
        }

        [Test]
        public void shouldTellWhenGraphlineWeightIsIncluded() {
            var converter = getGraphLineOptionViewTypeConverter();
            Assert.That(converter.shouldShowWeight(new[] { "adfssaf", "Weight", "dsf" }));
            Assert.That(converter.shouldShowWeight(new[] { "adfssaf", "noewigh", "dsf" }), Is.EqualTo(false));
        }

    }
}
