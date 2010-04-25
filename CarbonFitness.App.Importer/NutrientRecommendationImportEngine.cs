using Autofac;
using Autofac.Integration.Web;
using CarbonFitness.AppLogic;
using CarbonFitness.BusinessLogic;

namespace CarbonFitness.App.Importer {
    public class NutrientRecommendationImportEngine : IContainerProviderAccessor {
        private static IContainerProvider containerProvider;
        private readonly INutrientRecommendationImporter nutrientRecommendationImporter;

        private Bootstrapper bootstrapper;
        private IInitialDataValuesExportEngine initialDataValuesExporter;

        public NutrientRecommendationImportEngine(INutrientRecommendationImporter nutrientRecommendationImporter) {
            this.nutrientRecommendationImporter = nutrientRecommendationImporter;
        }

        public NutrientRecommendationImportEngine(string nhibernateConfiguration, bool exportSchema) : this(nhibernateConfiguration) {
            if (exportSchema) {
                getBootStrapper(nhibernateConfiguration).ExportDataBaseSchema();
            }
        }

        public NutrientRecommendationImportEngine(string nhibernateConfiguration) {
            var builder = new ContainerBuilder();

            var componentRegistrator = new ComponentRegistrator();
            componentRegistrator.AutofacRegisterComponentes(builder, getBootStrapper(nhibernateConfiguration));
            containerProvider = new ContainerProvider(builder.Build());

            nutrientRecommendationImporter = containerProvider.ApplicationContainer.Resolve<INutrientRecommendationImporter>();
            initialDataValuesExporter = containerProvider.ApplicationContainer.Resolve<IInitialDataValuesExportEngine>();
            componentRegistrator.populateNutrientRecommendationImporter(containerProvider.ApplicationContainer);
        }

        public IContainerProvider ContainerProvider { get { return containerProvider; } }

        public void Import() {
            if (initialDataValuesExporter!= null) {
                initialDataValuesExporter.Export();
            }

            nutrientRecommendationImporter.Import();
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