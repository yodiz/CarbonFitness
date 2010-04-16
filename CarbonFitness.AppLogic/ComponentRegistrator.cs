using Autofac;
using CarbonFitness.BusinessLogic;
using CarbonFitness.BusinessLogic.Implementation;
using CarbonFitness.BusinessLogic.IngredientImporter;
using CarbonFitness.BusinessLogic.IngredientImporter.Implementation;

namespace CarbonFitness.AppLogic {
	public class ComponentRegistrator {

		public IBootStrapper Bootstrapper { get; set; }

		public void AutofacRegisterComponentes(ContainerBuilder builder, IBootStrapper bootStrapper) {
			builder.RegisterModule(new BusinessLoginUsageModule());
			builder.RegisterType<MembershipBusinessLogic>().As<IMembershipBusinessLogic>();
			builder.RegisterType<IngredientBusinessLogic>().As<IIngredientBusinessLogic>();
			builder.RegisterType<UserBusinessLogic>().As<IUserBusinessLogic>();
			builder.RegisterType<UserIngredientBusinessLogic>().As<IUserIngredientBusinessLogic>();
            builder.RegisterType<UserProfileBusinessLogic>().As<IUserProfileBusinessLogic>();
            builder.RegisterType<NutrientBusinessLogic>().As<INutrientBusinessLogic>();
            builder.RegisterType<GenderTypeBusinessLogic>().As<IGenderTypeBusinessLogic>();

			builder.RegisterType<SchemaExportEngine>().As<ISchemaExportEngine>();
			builder.RegisterType<IngredientImporterEngine>().As<IIngredientImporterEngine>();
            builder.RegisterType<InitialDataValuesExportEngine>().As<IInitialDataValuesExportEngine>();

			builder.RegisterInstance(bootStrapper);

			builder.RegisterType<UserWeightBusinessLogic>().As<IUserWeightBusinessLogic>();

			builder.RegisterType<IngredientFileReader>().As<IIngredientFileReader>();
			builder.RegisterType<IngredientImporter>().As<IIngredientImporter>();
			builder.RegisterType<IngredientParser>().As<IIngredientParser>();
		}
	}
}