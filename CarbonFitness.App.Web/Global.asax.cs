using System;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Web;
using Autofac.Integration.Web.Mvc;
using CarbonFitness.App.Web.Controllers.ViewTypeConverters;
using CarbonFitness.App.Web.Models;
using CarbonFitness.AppLogic;
using CarbonFitness.BusinessLogic;
using SharpArch.Data.NHibernate;
using SharpArch.Web.NHibernate;

namespace CarbonFitness.App.Web {
	public class MvcApplication : HttpApplication, IContainerProviderAccessor {
		private static IContainerProvider containerProvider;

		private WebSessionStorage webSessionStorage;
		private Bootstrapper bootstrapper;

		public IContainerProvider ContainerProvider { get { return containerProvider; } }

		public static void RegisterRoutes(RouteCollection routes) {
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				"Default", // Route name
				"{controller}.aspx/{action}/{id}", // URL with parameters
				new {controller = "Home", action = "Index", id = UrlParameter.Optional} // Parameter defaults
				);

			routes.MapRoute(
				"Root",
				"",
				new {controller = "Home", action = "Index", id = ""});
		}

		public override void Init() {
			base.Init();
			webSessionStorage = new WebSessionStorage(this);
		}


		protected void Application_BeginRequest(object sender, EventArgs e) {
			NHibernateInitializer.Instance().InitializeNHibernateOnce(() => {
				Bootstrapper bootStrapper = getBootStrapper();
				bootStrapper.InitDatalayer(webSessionStorage);
			});
		}

		private Bootstrapper getBootStrapper() {
			if (bootstrapper == null) {
				var nhibernateConfig = Server.MapPath("~/bin/NHibernate.config");
				bootstrapper = new Bootstrapper(nhibernateConfig);
			}
			
			return bootstrapper;
		}

		protected void Application_Start() {
			AutofacRegisterComponentes();

			AutoMappingsBootStrapper.MapAll();

			RegisterRoutes(RouteTable.Routes);
			//AreaRegistration.RegisterAllAreas();
		}

		private void AutofacRegisterComponentes() {
			var builder = new ContainerBuilder();

            builder.RegisterType<GenderViewTypeConverter>().As<IGenderViewTypeConverter>();
            builder.RegisterType<ActivityLevelViewTypeConverter>().As<IActivityLevelViewTypeConverter>();

			builder.RegisterControllers(Assembly.GetExecutingAssembly());
			builder.RegisterType<FormsAuthenticationService>().As<IFormsAuthenticationService>();
			builder.RegisterType<UserContext>().As<IUserContext>().HttpRequestScoped();

			new ComponentRegistrator().AutofacRegisterComponentes(builder, getBootStrapper());

			containerProvider = new ContainerProvider(builder.Build());

			ControllerBuilder.Current.SetControllerFactory(new AutofacControllerFactory(ContainerProvider));
		}
	}
}