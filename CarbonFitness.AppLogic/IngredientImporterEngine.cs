using Autofac;
using CarbonFitness.BusinessLogic;
using CarbonFitness.BusinessLogic.IngredientImporter;

namespace CarbonFitness.AppLogic {
    public class IngredientImporterEngine : IIngredientImporterEngine {
        private readonly IIngredientImporter ingredientImporter;
        private Bootstrapper bootstrapper;

        public IngredientImporterEngine(IIngredientImporter ingredientImporter) {
            this.ingredientImporter = ingredientImporter;
        }

        public IngredientImporterEngine(string nhibernateConfiguration, bool exportSchema, IIngredientImporter ingredientImporter) : this(nhibernateConfiguration, ingredientImporter) {
            if (exportSchema) {
                bootstrapper.ExportDataBaseSchema();
            }
        }

        public IngredientImporterEngine(string nhibernateConfiguration, IIngredientImporter ingredientImporter) {
            var builder = new ContainerBuilder();
            new ComponentRegistrator().AutofacRegisterComponentes(builder, bootstrapper);

            InitializeNHibernate(nhibernateConfiguration);

            this.ingredientImporter = ingredientImporter;
        }


        public void Import(string filePath) {
            ingredientImporter.Import(filePath);
        }

        private void InitializeNHibernate(string nhibernateConfiguration) {
            bootstrapper = new Bootstrapper(nhibernateConfiguration);
            bootstrapper.InitDatalayer();
        }
    }
}