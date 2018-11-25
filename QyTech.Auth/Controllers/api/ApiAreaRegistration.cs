using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace QyExpress.Controllers.api
{
    public class apiAreaRegistration: AreaRegistration  
    {  
        public override string AreaName  
        {  
            get  
            {  
                return "api";  
            }  
        }
        public override void RegisterArea(AreaRegistrationContext context)  
        {  
            context.MapRoute(
                "api_default",  
                "api/{controller}/{action}/{id}",
                new { controller = "bsNavigation", action = "Index", id = UrlParameter.Optional },
                new string[] { "QyExpress.Controllers.api" }
            );  
        }  
    }
}