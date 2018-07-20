using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace QyTech.Auth.Controllers.WuTaiTemple
{
    public class WuTaiTempleAreaRegistration : AreaRegistration  
    {  
        public override string AreaName  
        {  
            get  
            {
                return "WuTaiTemple";  
            }  
        }
        public override void RegisterArea(AreaRegistrationContext context)  
        {  
            context.MapRoute(
                "WuTaiTemple_default",
                "WuTaiTemple/{controller}/{action}/{id}",
                new { controller = "FirePlan", action = "GetAll", id = UrlParameter.Optional }
            );  
        }  
    }
}