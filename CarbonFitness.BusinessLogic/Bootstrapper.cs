using Autofac;
using CarbonFitness.BusinessLogic.UnitHistory;
using CarbonFitness.DataLayer.Repository;
using SharpArch.Data.NHibernate;
using System.IO;

namespace CarbonFitness.BusinessLogic {
	public class Bootstrapper : IBootStrapper  {
		private readonly DataLayer.Bootstrapper bootstrapper;
		private readonly string nHibernateConfig;

		public Bootstrapper(string nHibernateConfig) {
			this.nHibernateConfig = nHibernateConfig;
			bootstrapper = new DataLayer.Bootstrapper();
		}

		public void InitDatalayer() {
			InitDatalayer(new SimpleSessionStorage());
		}

		public void InitDatalayer(ISessionStorage sessionStorage) {
            /*
             * Ugly hack?
             *  The thought was that if the file doesnt exist - use template for basic configuration,
             *  Then ask user to enter location of sql-server, username password etc.
             */
		    var templateConfigFile = nHibernateConfig.Replace(".config", ".template.config");
            if (!File.Exists(nHibernateConfig) && File.Exists(templateConfigFile))
            {
                File.Copy(templateConfigFile, nHibernateConfig);    
            }

			bootstrapper.InitNhibernateSession(sessionStorage, nHibernateConfig);
		}

		public void ExportDataBaseSchema() {
			bootstrapper.ExportDatabaseSchema(nHibernateConfig);
		}
	}

	public class BusinessLoginUsageModule : Module {
		protected override void Load(ContainerBuilder builder) {
			builder.RegisterType<UserRepository>().As<IUserRepository>();
			builder.RegisterType<IngredientRepository>().As<IIngredientRepository>();
			builder.RegisterType<UserIngredientRepository>().As<IUserIngredientRepository>();
			builder.RegisterType<UserWeightRepository>().As<IUserWeightRepository>();
            builder.RegisterType<UserProfileRepository>().As<IUserProfileRepository>();
			builder.RegisterType<GraphBuilder>().As<IGraphBuilder>();
            builder.RegisterType<NutrientRepository>().As<INutrientRepository>();
            builder.RegisterType<GenderTypeRepository>().As<IGenderTypeRepository>();
            builder.RegisterType<ActivityLevelTypeRepository>().As<IActivityLevelTypeRepository>();
            builder.RegisterType<NutrientRecommendationRepository>().As<INutrientRecommendationRepository>();
		}
	}
}