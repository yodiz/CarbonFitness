using System;
using System.Collections.Generic;
using CarbonFitness.BusinessLogic.Exceptions;
using CarbonFitness.BusinessLogic.IngredientImporter;
using CarbonFitness.BusinessLogic.IngredientImporter.Implementation;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Repository;
using Moq;
using NUnit.Framework;

namespace CarbonFitnessTest.BusinessLogic {
	[TestFixture]
	public class IngredientImporterTest {
		[Test]
		public void shouldImportIngredients() {
			var factory = new MockFactory(MockBehavior.Strict);
			var ingredientParserMock = factory.Create<IIngredientParser>();
			var ingredientFileReaderMock = factory.Create<IIngredientFileReader>();
			var ingredientRepositoryMock = factory.Create<IIngredientRepository>();

			const string abc = "abc";
			const string fileName = "fileName";
			ingredientFileReaderMock.Setup(x => x.ReadIngredientFile(fileName)).Returns(abc);
			const string abborre = "Abborre";
			ingredientParserMock.Setup(x => x.ParseTabSeparatedFileContents(abc)).Returns(new List<Ingredient> {new Ingredient {Name = abborre}});
			ingredientRepositoryMock
				.Setup(x => x.SaveOrUpdate(It.Is<Ingredient>(y => y.Name == abborre)))
				.Returns(null as Ingredient);

			new IngredientImporter(ingredientParserMock.Object, ingredientFileReaderMock.Object, ingredientRepositoryMock.Object)
				.Import(fileName);

			factory.VerifyAll();
		}

		[Test]
		public void shouldThrowIngredientInsertionExceptionWhenInsertingFails() {
			var factory = new MockFactory(MockBehavior.Strict);
			var ingredientRepositoryMock = factory.Create<IIngredientRepository>();
			ingredientRepositoryMock.Setup(x => x.SaveOrUpdate(It.IsAny<Ingredient>())).Throws(new Exception());

			Assert.Throws<IngredientInsertionException>(() => new IngredientImporter(null, null, ingredientRepositoryMock.Object).SaveIngredient(new Ingredient()));
		}
	}
}