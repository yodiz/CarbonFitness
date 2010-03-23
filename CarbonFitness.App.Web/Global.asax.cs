using System;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Web;
using Autofac.Integration.Web.Mvc;
using CarbonFitness.App.Web.Models;
using CarbonFitness.AppLogic;
using CarbonFitness.BusinessLogic;
using SharpArch.Data.NHibernate;
using SharpArch.Web.NHibernate;

namespace CarbonFitness.App.Web {
	public class MvcApplication : HttpApplication, IContainerProviderAccessor {
		/// <summary>
		/// Private, static object used only for synchronization
		/// </summary>
		private static readonly object lockObject = new object();

		private static IContainerProvider containerProvider;

		private static bool wasNHibernateInitialized;
		private WebSessionStorage webSessionStorage;

		public IContainerProvider ContainerProvider { get { return containerProvider; } }

		public static void RegisterRoutes(RouteCollection routes) {
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				"Default", // Route name
				"{controller}/{action}/{id}", // URL with parameters
				new {controller = "Home", action = "Index", id = ""} // Parameter defaults
				);
		}

		/// <summary>
		/// Due to issues on IIS7, the NHibernate initialization must occur in Init().
		/// But Init() may be invoked more than once; accordingly, we introduce a thread-safe
		/// mechanism to ensure it's only initialized once.
		/// 
		/// See http://msdn.microsoft.com/en-us/magazine/cc188793.aspx for explanation details.
		/// </summary>
		public override void Init() {
			base.Init();

			webSessionStorage = new WebSessionStorage(this);

			//// Only allow the NHibernate session to be initialized once			
			//if (!wasNHibernateInitialized) {
			//   lock (lockObject) {
			//      if (!wasNHibernateInitialized) {

			//         wasNHibernateInitialized = true;
			//      }
			//   }
			//}
		}


		protected void Application_BeginRequest(object sender, EventArgs e) {
			NHibernateInitializer.Instance().InitializeNHibernateOnce(() => {
				string nhibernateConfig = Server.MapPath("~/bin/NHibernate.config");
				new Bootstrapper(nhibernateConfig).InitDatalayer(webSessionStorage);
			});
		}

		protected void Application_Start() {
			AreaRegistration.RegisterAllAreas();

			AutofacRegisterComponentes();

			AutoMappingsBootStrapper.MapAll();
		}

		private void AutofacRegisterComponentes() {
			var builder = new ContainerBuilder();

			builder.RegisterControllers(Assembly.GetExecutingAssembly());
			builder.RegisterType<FormsAuthenticationService>().As<IFormsAuthenticationService>();
			builder.RegisterType<UserContext>().As<IUserContext>().HttpRequestScoped();

			new ComponentRegistrator().AutofacRegisterComponentes(builder);

			containerProvider = new ContainerProvider(builder.Build());

			ControllerBuilder.Current.SetControllerFactory(new AutofacControllerFactory(ContainerProvider));

			RegisterRoutes(RouteTable.Routes);
		}
	}
}