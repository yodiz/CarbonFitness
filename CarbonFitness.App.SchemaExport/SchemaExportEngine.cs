using System;
using Autofac;
using Autofac.Integration.Web;
using CarbonFitness.AppLogic;
using CarbonFitness.BusinessLogic;

namespace CarbonFitness.App.SchemaExport {
	internal class SchemaExportEngineApp : IContainerProviderAccessor {
		private static IContainerProvider containerProvider;

		public IContainerProvider ContainerProvider {
			get { return containerProvider; }
		}


		private static void Main(string[] args) {
			var builder = new ContainerBuilder();
			var bootstrapper = new Bootstrapper("NHibernate.config");
			bootstrapper.InitDatalayer();

			new ComponentRegistrator().AutofacRegisterComponentes(builder, bootstrapper);
			containerProvider = new ContainerProvider(builder.Build());


			new SchemaExportEngine(bootstrapper).Export();

			Console.WriteLine("");
			Console.WriteLine("Schema export finished. Press any key to continue...");
			Console.ReadLine();
		}
	}
}