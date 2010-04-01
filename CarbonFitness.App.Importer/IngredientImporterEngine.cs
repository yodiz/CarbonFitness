using System;
using System.IO;
using Autofac;
using Autofac.Integration.Web;
using CarbonFitness.AppLogic;
using CarbonFitness.BusinessLogic;
using CarbonFitness.BusinessLogic.IngredientImporter;

namespace CarbonFitness.App.Importer {
	public class IngredientImporterEngine : IContainerProviderAccessor {
		private static IContainerProvider containerProvider;
		private readonly IIngredientImporter ingredientImporter;

		private Bootstrapper bootstrapper;

		public IngredientImporterEngine(IIngredientImporter ingredientImporter) {
			this.ingredientImporter = ingredientImporter;
		}

		public IngredientImporterEngine(string nhibernateConfiguration, bool exportSchema) : this(nhibernateConfiguration) {
			if (exportSchema) {
				getBootStrapper(nhibernateConfiguration).ExportDataBaseSchema();
			}
		}

		public IngredientImporterEngine(string nhibernateConfiguration) {
			var builder = new ContainerBuilder();

			new ComponentRegistrator().AutofacRegisterComponentes(builder, getBootStrapper(nhibernateConfiguration));
			containerProvider = new ContainerProvider(builder.Build());
         
			ingredientImporter = containerProvider.ApplicationContainer.Resolve<IIngredientImporter>();
		}

		public IContainerProvider ContainerProvider {
			get { return containerProvider; }
		}


		public static void Main(string[] args) {
			var filePath = "";

			if (args.Length < 1) {
				Console.WriteLine("No filePath argument specified! Enter filePath: Or use default by pressing [Enter]");
				filePath = Console.ReadLine();
				if (filePath == "") {
					filePath = @"Ingredients\Ingredients.csv";
				}
			}
			else {
				filePath = args[0];
			}

			filePath = VerifyFilePath(filePath);

			Console.WriteLine("Starting import. Please wait...");

			new IngredientImporterEngine("NHibernate.config").Import(filePath);

			Console.WriteLine("Done importing!");
			Console.ReadLine();
		}

		private static string VerifyFilePath(string filePath) {
			if (!File.Exists(filePath)) {
				Console.WriteLine("No such file exists! Enter new filePath:");
				return VerifyFilePath(Console.ReadLine());
			}

			return filePath;
		}

		public void Import(string filePath) {
			ingredientImporter.Import(filePath);
		}

		private IBootStrapper getBootStrapper(string nhibernateConfiguration) {
			if (bootstrapper == null) {
				bootstrapper = new Bootstrapper(nhibernateConfiguration);
				bootstrapper.InitDatalayer();
			}
			return bootstrapper;
		}
	}
}