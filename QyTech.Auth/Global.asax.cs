﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using QyTech.Core.ExController;

namespace QyTech.Auth
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            ControllerBuilder.Current.SetControllerFactory(new QyTechControllerFactory());

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //RegisterView();//注册视图访问规则
        }

        //protected void RegisterView()
        //{

        //   // ViewEngines.Engines.Clear();
        //    ViewEngines.Engines.Add(new AuthRazorViewEngine());
        //}
    }
}

