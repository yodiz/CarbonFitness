using System;
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
			var assembly = Assembly.GetAssembly(typeof(User));
			var mappingAssembly = assembly.CodeBase.ToLower();

			try {
				NHibernateSession.Init(sessionStorage,
					new[] {mappingAssembly},
					new AutoPersistenceModelGenerator().Generate(),
					nHibernateConfig);
			} catch (Exception ex) {
				Console.WriteLine(ex.ToString());
				throw;
			}
		}

		public void ExportDatabaseSchema(string nHibernateConfig) {
			var cfg = generateConfiguration(nHibernateConfig);

			var export = new SchemaExport(cfg);
			TextWriter output = new StringWriter(new StringBuilder());
			//export.Execute(true, true, false);
			export.Execute(true, true, false, NHibernateSession.Current.Connection, output);
		}

		//public void UpdateDatabaseSchema(string nHibernateConfig) {
		//    Configuration _cfg = generateConfiguration(nHibernateConfig);

		//    var update = new SchemaUpdate(_cfg);
		//    update.Execute(true, true);
		//}

		private static Configuration generateConfiguration(string nHibernateConfig) {
			var cfg = new Configuration();
			cfg.Configure(nHibernateConfig);
			new AutoPersistenceModelGenerator().Generate().Configure(cfg);
			return cfg;
		}
	}
}