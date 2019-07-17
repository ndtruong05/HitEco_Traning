using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AnyCash
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Page404Error",
                url: "Error/NotFound",
                defaults: new { controller = "Notify", action = "Page404" }
            );

            routes.MapRoute(
                name: "SystemError",
                url: "Error/SystemError",
                defaults: new { controller = "Notify", action = "Error" }
            );

            routes.MapRoute(
                name: "Login",
                url: "Login/{id}",
                defaults: new { controller = "Home", action = "Login", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Logout",
                url: "Logout/{id}",
                defaults: new { controller = "Home", action = "Logout", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
