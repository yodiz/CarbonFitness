using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Web;
using CarbonFitness;
using CarbonFitness.BusinessLogic;
using CarbonFitness.BusinessLogic.Implementation;
using CarbonFitnessWeb.Controllers;
using CarbonFitnessWeb.Models;
using SharpArch.Web.NHibernate;
using System.Reflection;
using Autofac.Integration.Web.Mvc;

namespace CarbonFitnessWeb {
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : HttpApplication, IContainerProviderAccessor {
			private static IContainerProvider _containerProvider;

        /// <summary>
        /// Private, static object used only for synchronization
        /// </summary>
        private static readonly object lockObject = new object();

        private static bool wasNHibernateInitialized;
        
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

            // Only allow the NHibernate session to be initialized once
            if (!wasNHibernateInitialized) {
                lock (lockObject) {
                    if (!wasNHibernateInitialized) {
                        string nHibernateConfig = Server.MapPath("~/NHibernate.config");
                        //string mappingAssembly = Server.MapPath("~/bin/CarbonFitness.Data.dll");

								var initBusinessLogic = new Bootstrapper();
                       
                        initBusinessLogic.InitDatalayer(new WebSessionStorage(this), nHibernateConfig);
                        
                        //initBusinessLogic.InitNhibernateSession(this, mappingAssembly, nHibernateConfig);

                        //initNhibernateSession(carbonFitnessDll, nHibernateConfig);
                        
                        //exportDatabaseSchema(nHibernateConfig);
                        
                        wasNHibernateInitialized = true;
                    }
                }
            }
        }

        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();

				AutofacRegisterComponentes();
        }

		  private void AutofacRegisterComponentes() {
			  var builder = new ContainerBuilder();

			  builder.RegisterControllers(Assembly.GetExecutingAssembly());
			  builder.RegisterModule(new BusinessLoginUsageModule());
			  builder.RegisterType<MembershipBusinessLogic>().As<IMembershipBusinessLogic>();
			  builder.RegisterType<FormsAuthenticationService>().As<IFormsAuthenticationService>();
              builder.RegisterType<IngredientBusinessLogic>().As<IIngredientBusinessLogic>();
			  builder.RegisterType<UserContext>().As<IUserContext>().HttpRequestScoped();
			  builder.RegisterType<UserBusinessLogic>().As<IUserBusinessLogic>();
			  builder.RegisterType<UserIngredientBusinessLogic>().As<IUserIngredientBusinessLogic>();
			  
			  _containerProvider = new ContainerProvider(builder.Build());

			  ControllerBuilder.Current.SetControllerFactory(new AutofacControllerFactory(ContainerProvider));

			  RegisterRoutes(RouteTable.Routes);
		  }

		public IContainerProvider ContainerProvider
		{
			get { return _containerProvider; }
		}
	}
}