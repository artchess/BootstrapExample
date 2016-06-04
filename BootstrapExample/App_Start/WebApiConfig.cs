using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace BootstrapExample
{
    // Esta clase es como el RouteConfig de MVC pero este es para Web Api
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuraciones y servicios de Web Api

            // rutas de web API
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}