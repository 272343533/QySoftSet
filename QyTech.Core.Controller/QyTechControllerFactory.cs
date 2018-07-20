using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Reflection;
using log4net;
using System.Collections;


using System.Web.Http;
using System.Web;

using System.Web.Routing;
using System.Web.SessionState;




using QyTech.Core.Helpers;
using QyTech.Core;

using QyTech.Core.BLL;
using QyTech.Json;
using QyTech.Core.Common;
using QyTech.Core.ExController.Bll;
using QyTech.Core.ExController;

using QyTech.Auth.Dao;

namespace QyTech.Core.ExController
{
    public class QyTechControllerFactory : DefaultControllerFactory
    {
        //
        // GET: /FolerControllerFactory/
        EntityManager EM = new EntityManager(new QyTech.Auth.Dao.QyTech_AuthEntities());
                  
        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            try
            {
                var controllerType = GetControllerType(requestContext, controllerName);
                // 如果controller不存在，替换为FolderController
                if (controllerType == null)
                {
                    // 用路由字段动态构建路由变量
                    var dynamicRoute = string.Join("/", requestContext.RouteData.Values.Values);
                    string[] routes = dynamicRoute.Split(new char[] { '/' });

                    bsFunInterface bsfi = EM.GetBySql<bsFunInterface>("LinkController='" + routes[routes.Length - 2] + "' and LinkAction='" + routes[routes.Length - 1] + "'");
                    controllerName = bsfi.AreaName+ "Default";//根据controller觉得对应的QyTechDefault
                    //if (routes.Length >= 3)
                   //     controllerName = routes[0] + "Default";//根据controller觉得对应的QyTechDefault
                   // else
                   //     controllerName = "QyTech";
                    controllerType = GetControllerType(requestContext, controllerName);
                    requestContext.RouteData.Values["Controller"] = controllerName;
                    requestContext.RouteData.Values["action"] = requestContext.RouteData.Values["action"];
                    requestContext.RouteData.Values["dynamicRoute"] = dynamicRoute;
                }
                IController controller = GetControllerInstance(requestContext, controllerType);
                return controller;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
