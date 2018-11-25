using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace QyExpress.Controllers.bll
{
    public class BllAreaRegistration : AreaRegistration  
    {  
        public override string AreaName  
        {  
            get  
            {  
                return "bll";  
            }  
        }
        public override void RegisterArea(AreaRegistrationContext context)  
        {  
            context.MapRoute(
                "bll_default",  
                "bll/{controller}/{action}/{id}",
                new { controller = "bsFunConf", action = "Index", id = UrlParameter.Optional },
                new string[] { "QyExpress.Controllers.bll" }
            );  
        }  
    }
}