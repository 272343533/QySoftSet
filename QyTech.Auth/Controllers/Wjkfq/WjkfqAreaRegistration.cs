using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace QyTech.Auth.Controllers.Wjkfa
{
    public class WebAppAreaRegistration : AreaRegistration  
    {  
        public override string AreaName  
        {  
            get  
            {
                return "Wjkfq";  
            }  
        }
        public override void RegisterArea(AreaRegistrationContext context)  
        {  
            context.MapRoute(
                "Wjkfq_default",
                "Wjkfq/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new string[] { "QyTech.Auth.Controllers.Wjkfq" }
            );  
        }  
    }
}