using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using CarbonFitness.BusinessLogic;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using SharpArch.Data.NHibernate;
using SharpArch.Web.NHibernate;

namespace CarbonFitnessWeb {
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication {
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

            RegisterRoutes(RouteTable.Routes);
        }
    }
}