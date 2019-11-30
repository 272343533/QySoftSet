using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Reflection;
using log4net;
using System.Collections;



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

using qyExpress.Dao;

namespace QyTech.Core.ExController
{
    public class QyTechControllerFactory : DefaultControllerFactory
    {
        //
        // GET: /FolerControllerFactory/
        EntityManager EM = new EntityManager(new qyExpress.Dao.qyExpressEntities());
                  
        /// <summary>
        /// 如果没有实现具体的控制器，则转为使用默认的控制器，具体的控制器只需要实现私有的action即可
        /// </summary>
        /// <param name="requestContext"></param>
        /// <param name="controllerName"></param>
        /// <returns></returns>
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

                    //zhwsun modified on 2018-10-08 是从数据库配置中确定area，还是按照请求来确定area？理顺这些关系
                    //bsFunInterface bsfi = EM.GetBySql<bsFunInterface>("LinkController='" + routes[routes.Length - 2] + "' and LinkAction='" + routes[routes.Length - 1] + "'");
                    //controllerName = bsfi.AreaName+ "Default";//根据controller觉得对应的QyTechDefault
                    //目前为了效率高些，直接按照传输过来的路由进行，如果更改，需要更改为从bsTTinterface寻找，原来的bsFunInterface已经被bsTInterface代替
                    controllerName = requestContext.RouteData.DataTokens["area"] + "Default";
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
