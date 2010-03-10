using Autofac;
using CarbonFitness.DataLayer.Repository;
using SharpArch.Data.NHibernate;

namespace CarbonFitness.BusinessLogic {
	public class Bootstrapper {
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
		}
	}
}