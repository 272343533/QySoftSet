using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace QyExpress.Controllers.BllApp
{
    public class WebAppAreaRegistration : AreaRegistration  
    {  
        public override string AreaName  
        {  
            get  
            {
                return "gis";  
            }  
        }
        public override void RegisterArea(AreaRegistrationContext context)  
        {  
            context.MapRoute(
                "gis_default",
                "gis/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new string[] { "qyExpress.Controllers.gis" }
            );  
        }  
    }
}