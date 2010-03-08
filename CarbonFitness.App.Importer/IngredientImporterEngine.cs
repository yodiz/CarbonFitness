using System;
using System.Reflection;
using Autofac;
using Autofac.Integration.Web;
using CarbonFitness.AppLogic;
using CarbonFitness.BusinessLogic.IngredientImporter;

namespace CarbonFitness.App.Importer {
    public class IngredientImporterEngine : IContainerProviderAccessor
    {
        private readonly IIngredientImporter ingredientImporter;
        private static IContainerProvider _containerProvider;

        static void Main(string[] args) {
            new IngredientImporterEngine("SHOULD BE SET");

	
        }

        public IngredientImporterEngine(IIngredientImporter ingredientImporter) {
            this.ingredientImporter = ingredientImporter;
        }

        public IngredientImporterEngine(string nhibernateConfiguration) {
            var builder = new ContainerBuilder();

            new ComponentRegistrator().AutofacRegisterComponentes(builder);

            _containerProvider = new ContainerProvider(builder.Build());
            ingredientImporter = _containerProvider.ApplicationContainer.Resolve<IIngredientImporter>();
        }

        public void Import(string filePath) {
            ingredientImporter.Import(filePath);
        }

        public IContainerProvider ContainerProvider {
            get { return _containerProvider; }
        }
    }
}