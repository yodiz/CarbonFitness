using System;
using Autofac;
using Autofac.Integration.Web;
using CarbonFitness.AppLogic;
using CarbonFitness.BusinessLogic;

namespace CarbonFitness.App.SchemaExport {
	internal class SchemaExportEngine : IContainerProviderAccessor {
		private static IContainerProvider containerProvider;
		private readonly string nhibernateConfiguration;

		public SchemaExportEngine(string nhibernateConfiguration) {
			this.nhibernateConfiguration = nhibernateConfiguration;
			var builder = new ContainerBuilder();
			new ComponentRegistrator().AutofacRegisterComponentes(builder);
			containerProvider = new ContainerProvider(builder.Build());
		}

		public IContainerProvider ContainerProvider {
			get { return containerProvider; }
		}

		private void Export() {
			var initBusinessLogic = new Bootstrapper(nhibernateConfiguration);
			initBusinessLogic.InitDatalayer();
			initBusinessLogic.ExportDataBaseSchema();
		}

		private static void Main(string[] args) {
			new SchemaExportEngine("NHibernate.config").Export();

			Console.ReadLine();
		}
	}
}