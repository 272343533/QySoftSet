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
                return "BllApp";  
            }  
        }
        public override void RegisterArea(AreaRegistrationContext context)  
        {  
            context.MapRoute(
                "BllApp_default",
                "BllApp/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new string[] { "QyExpress.Controllers.BllApp" }
            );  
        }  
    }
}