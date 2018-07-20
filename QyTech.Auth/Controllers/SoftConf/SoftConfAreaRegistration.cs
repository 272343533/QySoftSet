using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace QyTech.Auth.Controllers.SoftConf
{
    public class SoftConfAreaRegistration : AreaRegistration  
    {  
        public override string AreaName  
        {  
            get  
            {
                return "SoftConf";  
            }  
        }
        public override void RegisterArea(AreaRegistrationContext context)  
        {  
            context.MapRoute(
                "SoftConf_default",
                "SoftConf/{controller}/{action}/{id}",
                new { controller = "bsFunConf", action = "Index", id = UrlParameter.Optional },
                new string[] { "QyTech.Auth.Controllers.SoftConf" }
            );  
        }  
    }
}