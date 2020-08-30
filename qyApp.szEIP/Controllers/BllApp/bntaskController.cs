using System.Web;
using System.Web.Mvc;
using QyTech.Core.ExController;
using QyExpress.Dao;
using QyTech.Core.BLL;
using QyTech.Core;
using QyTech.Core.Common;
using QyTech.Json;
using System.Collections;
using System.Collections.Generic;
using QyTech.Core.ExController.Bll;
using Dao.QyBllApp;

namespace QyExpress.Controllers.BllApp
{
    public class bntaskController : BllAppController
    {
       
        public override string Save(string sessionid, string strjson)
        {
            LogHelper.Info(sessionid);
            LogHelper.Info(strjson);
            string ret = "";
            strjson = strjson.Replace("|||amp;", "&");
            strjson = strjson.Replace("|||lt;", "<");
            strjson = strjson.Replace("|||gt;", ">");
            strjson = strjson.Replace("|||quot;", "'");
            //strjson = strjson.Replace("|||#039;", "'");
            LogHelper.Info(strjson);

            return base.Save(sessionid, strjson);
        }
    }
}
