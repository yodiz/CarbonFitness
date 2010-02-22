using System.IO;
using System.Reflection;
using System.Text;
using CarbonFitness.Data.Model;
using CarbonFitness.DataLayer.Maps;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using SharpArch.Data.NHibernate;

namespace CarbonFitness.DataLayer {
    public class Bootstrapper {
        public void InitNhibernateSession(ISessionStorage sessionStorage, string nHibernateConfig) {
            var assembly = Assembly.GetAssembly(typeof (User));
            var mappingAssembly = assembly.CodeBase.ToLower();

            NHibernateSession.Init(sessionStorage,
                                   new[] {mappingAssembly},
                                   new AutoPersistenceModelGenerator().Generate(),
                                   nHibernateConfig);
        }

        public void ExportDatabaseSchema(string nHibernateConfig) {
            Configuration _cfg = generateConfiguration(nHibernateConfig);

            var export = new SchemaExport(_cfg);
            TextWriter output = new StringWriter(new StringBuilder());
            export.Execute(true, true, false, true, NHibernateSession.Current.Connection, output);
        }

        //public void UpdateDatabaseSchema(string nHibernateConfig) {
        //    Configuration _cfg = generateConfiguration(nHibernateConfig);

        //    var update = new SchemaUpdate(_cfg);
        //    update.Execute(true, true);
        //}

        private Configuration generateConfiguration(string nHibernateConfig) {
            var _cfg = new Configuration();
            _cfg.Configure(nHibernateConfig);
            new AutoPersistenceModelGenerator().Generate().Configure(_cfg);
            return _cfg;
        }
    }
}