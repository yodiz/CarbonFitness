using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Autofac.Builder;
using CarbonFitness.DataLayer.Maps;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using SharpArch.Data.NHibernate;
using SharpArch.Web.NHibernate;
using CarbonFitness.DataLayer.Repository;

namespace CarbonFitness.BusinessLogic
{
    public class Bootstrapper
    {

        public void InitDatalayer(ISessionStorage sessionStorage, string nHibernateConfig) {
            var bootstrapper = new CarbonFitness.DataLayer.Bootstrapper();
            bootstrapper.InitNhibernateSession(sessionStorage,  nHibernateConfig);
        }


        public void InitAutofac(ContainerBuilder builder) {
            builder.Register<UserRepository>().As<IUserRepository>();
        }

    }
}
