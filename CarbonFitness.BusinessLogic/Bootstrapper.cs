using System;
using Autofac;

using CarbonFitness.DataLayer.Repository;
using SharpArch.Data.NHibernate;

namespace CarbonFitness.BusinessLogic
{
    public class Bootstrapper
    {

        public void InitDatalayer(ISessionStorage sessionStorage, string nHibernateConfig) {
            var bootstrapper = new DataLayer.Bootstrapper();
            bootstrapper.InitNhibernateSession(sessionStorage,  nHibernateConfig);
            bootstrapper.UpdateDatabaseSchema(nHibernateConfig);
        	//bootstrapper.ExportDatabaseSchema(nHibernateConfig);
        }
    }

	public class BusinessLoginUsageModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<UserRepository>().As<IUserRepository>();
			builder.RegisterType<IngredientRepository>().As<IIngredientRepository>();
			builder.RegisterType<UserIngredientRepository>().As<IUserIngredientRepository>();
			
		}
	}
}
