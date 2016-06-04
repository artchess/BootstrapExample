using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using BootstrapExample.DAL;

namespace BootstrapExample
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register); //Web Api
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //En lugar de inicializar en aplication start. Podríamos inicializar en la misma clase LibreriaContext
            var libreriaContext = new LibreriaContext();
            Database.SetInitializer(new LibreriaDBInicializador());
            libreriaContext.Database.Initialize(true);
        }
    }
}
