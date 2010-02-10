using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Maps;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using SharpArch.Data.NHibernate;
using System.Reflection;

namespace CarbonFitness.DataLayer
{
    public class Bootstrapper
    {

        public void InitNhibernateSession(ISessionStorage sessionStorage, string nHibernateConfig)
        {

            var assembly = Assembly.GetAssembly(typeof (User));
            string mappingAssembly = assembly.CodeBase.ToLower();

            NHibernateSession.Init(sessionStorage,
                                   new[] { mappingAssembly },
                                   new AutoPersistenceModelGenerator().Generate(),
                                   nHibernateConfig);
        }

        public void ExportDatabaseSchema(string nHibernateConfig)
        {
            var _cfg = new Configuration();
            _cfg.Configure(nHibernateConfig);
            new AutoPersistenceModelGenerator().Generate().Configure(_cfg);

            var export = new SchemaExport(_cfg);
            TextWriter output = new StringWriter(new StringBuilder());
            export.Execute(true, true, false, true, NHibernateSession.Current.Connection, output);
        }

    }
}
