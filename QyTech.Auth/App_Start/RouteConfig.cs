using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace QyTech.Auth
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "Default",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "auth",
                url: "api/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
              name: "softconf",
              url: "softconf/{controller}/{action}/{id}",
              defaults: new { controller = "bsSoftconf", action = "getall", id = UrlParameter.Optional }
             );
          
         
            routes.MapRoute(
            "CatchAll",
            "{*dynamicRoute}",
            new { controller = "QyTechDefault", action = "Index" }
        );
        }
    }
}