using System.Linq;
using Autofac;
using CarbonFitness.BusinessLogic;
using CarbonFitness.BusinessLogic.Implementation;
using CarbonFitness.BusinessLogic.IngredientImporter;
using CarbonFitness.BusinessLogic.IngredientImporter.Implementation;
using CarbonFitness.BusinessLogic.RDI.Calculators;
using CarbonFitness.BusinessLogic.RDI.Importers;

namespace CarbonFitness.AppLogic {
	public class ComponentRegistrator {

		public void AutofacRegisterComponentes(ContainerBuilder builder, IBootStrapper bootStrapper) {
			builder.RegisterModule(new BusinessLoginUsageModule());
			builder.RegisterType<MembershipBusinessLogic>().As<IMembershipBusinessLogic>();
			builder.RegisterType<IngredientBusinessLogic>().As<IIngredientBusinessLogic>();
			builder.RegisterType<UserBusinessLogic>().As<IUserBusinessLogic>();
			builder.RegisterType<UserIngredientBusinessLogic>().As<IUserIngredientBusinessLogic>();
            builder.RegisterType<UserProfileBusinessLogic>().As<IUserProfileBusinessLogic>();
            builder.RegisterType<NutrientBusinessLogic>().As<INutrientBusinessLogic>();
            builder.RegisterType<GenderTypeBusinessLogic>().As<IGenderTypeBusinessLogic>();
            builder.RegisterType<ActivityLevelTypeBusinessLogic>().As<IActivityLevelTypeBusinessLogic>();
            builder.RegisterType<CalorieCalculator>().As<ICalorieCalculator>();

			builder.RegisterType<SchemaExportEngine>().As<ISchemaExportEngine>();
			builder.RegisterType<IngredientImporterEngine>().As<IIngredientImporterEngine>();
            builder.RegisterType<InitialDataValuesExportEngine>().As<IInitialDataValuesExportEngine>();
            builder.RegisterType<NutrientRecommendationBusinessLogic>().As<INutrientRecommendationBusinessLogic>();

            builder.RegisterType<NutrientRecommendationImportEngine>().SingleInstance().As<INutrientRecommendationImporter>();
            builder.RegisterType<IronRDIImporter>().As<IIronRDIImporter>();

			builder.RegisterInstance(bootStrapper);

			builder.RegisterType<UserWeightBusinessLogic>().As<IUserWeightBusinessLogic>();

			builder.RegisterType<IngredientFileReader>().As<IIngredientFileReader>();
			builder.RegisterType<IngredientImporter>().As<IIngredientImporter>();
			builder.RegisterType<IngredientParser>().As<IIngredientParser>();

            builder.RegisterType<MineralRDICalculator>().As<IMineralRDICalculator>();
            builder.RegisterType<CarbonHydrateRDICalculator>().As<ICarbonHydrateRDICalculator>();
            builder.RegisterType<FatRDICalculator>().As<IFatRDICalculator>();
            builder.RegisterType<FibresRDICalculator>().As<IFibresRDICalculator>();
            builder.RegisterType<ProteinRDICalculator>().As<IProteinRDICalculator>();

            
            builder.RegisterType<RDICalculatorFactory>().SingleInstance().As<IRDICalculatorFactory>();
		}


        public void populateRDICalculatorFactory(IComponentContext container)
        {
            var calculatorFactory = container.Resolve<IRDICalculatorFactory>();
            calculatorFactory.AddRDICalculator(container.Resolve<IMineralRDICalculator>());
            calculatorFactory.AddRDICalculator(container.Resolve<ICarbonHydrateRDICalculator>());
            calculatorFactory.AddRDICalculator(container.Resolve<IFatRDICalculator>());
            calculatorFactory.AddRDICalculator(container.Resolve<IProteinRDICalculator>());
            calculatorFactory.AddRDICalculator(container.Resolve<IFibresRDICalculator>());
        }

        public void populateNutrientRecommendationImporter(IComponentContext componentContext)
        {
            var nutrientRecommendationImporter = componentContext.Resolve<INutrientRecommendationImporter>();

            var nutrinetImporterAssembly = typeof(IIronRDIImporter).Assembly;
            var types = nutrinetImporterAssembly.GetExportedTypes().Where(t => typeof(INutrientRDIImporter).IsAssignableFrom(t)).ToList();
            var importerTypes = from t in types where t.IsInterface && t.Name != "INutrientRDIImporter" select t;
            foreach (var type in importerTypes)
            {
                nutrientRecommendationImporter.AddImporter(componentContext.Resolve(type) as INutrientRDIImporter);
            }
        }
	}
}