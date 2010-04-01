using System;
using System.IO;
using Autofac;
using CarbonFitness.BusinessLogic;
using CarbonFitness.BusinessLogic.IngredientImporter;

namespace CarbonFitness.AppLogic {
	public class IngredientImporterEngine : IIngredientImporterEngine {
		private readonly IIngredientImporter _ingredientImporter;
		private Bootstrapper bootstrapper;

		public IngredientImporterEngine(IIngredientImporter ingredientImporter)
		{
			this._ingredientImporter = ingredientImporter;
		}

		public IngredientImporterEngine(string nhibernateConfiguration, bool exportSchema, IIngredientImporter ingredientImporter)
			: this(nhibernateConfiguration, ingredientImporter)
		{
			if (exportSchema)
			{
				bootstrapper.ExportDataBaseSchema();
			}
		}

		public IngredientImporterEngine(string nhibernateConfiguration, IIngredientImporter ingredientImporter)
		{
			var builder = new ContainerBuilder();
			new ComponentRegistrator().AutofacRegisterComponentes(builder, bootstrapper);

			InitializeNHibernate(nhibernateConfiguration);

			_ingredientImporter = ingredientImporter;
		}


		private static string VerifyFilePath(string filePath)
		{
			if (!File.Exists(filePath))
			{
				Console.WriteLine("No such file exists! Enter new filePath:");
				return VerifyFilePath(Console.ReadLine());
			}

			return filePath;
		}

		public void Import(string filePath)
		{
			_ingredientImporter.Import(filePath);
		}

		private void InitializeNHibernate(string nhibernateConfiguration)
		{
			bootstrapper = new Bootstrapper(nhibernateConfiguration);
			bootstrapper.InitDatalayer();
		}
	}
}