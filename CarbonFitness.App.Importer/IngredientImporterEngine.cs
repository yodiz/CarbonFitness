using Autofac;
using Autofac.Integration.Web;
using CarbonFitness.AppLogic;
using CarbonFitness.BusinessLogic;
using CarbonFitness.BusinessLogic.IngredientImporter;
using SharpArch.Data.NHibernate;

namespace CarbonFitness.App.Importer {
	public class IngredientImporterEngine : IContainerProviderAccessor {
		private static IContainerProvider containerProvider;
		private readonly IIngredientImporter ingredientImporter;

		public IngredientImporterEngine(IIngredientImporter ingredientImporter) {
			this.ingredientImporter = ingredientImporter;
		}

		public IngredientImporterEngine(string nhibernateConfiguration) {
			var builder = new ContainerBuilder();
			new ComponentRegistrator().AutofacRegisterComponentes(builder);
			containerProvider = new ContainerProvider(builder.Build());

			InitializeNHibernate(nhibernateConfiguration);

			ingredientImporter = containerProvider.ApplicationContainer.Resolve<IIngredientImporter>();
		}

		public IContainerProvider ContainerProvider {
			get { return containerProvider; }
		}


		private static void Main(string[] args) {
			new IngredientImporterEngine("SHOULD BE SET");
		}

		public void Import(string filePath) {
			ingredientImporter.Import(filePath);
		}

		private static void InitializeNHibernate(string nhibernateConfiguration) {
			var initBusinessLogic = new Bootstrapper();
			initBusinessLogic.InitDatalayer(new SimpleSessionStorage(), nhibernateConfiguration);
		}
	}
}