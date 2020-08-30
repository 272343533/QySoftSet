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
    public class bsaddress_groupController : BllAppController
    {
        public string saveToGroup(string sessionid, string groupname, string ltdids="")
        {
            LogHelper.Info("saveToGroup："+groupname+"-"+ltdids);
            string strParams= groupname + "," + ltdids+","+LoginUser.bsU_Id.ToString();
            return base.ExcuteStoreProcedure(sessionid, "splysaveToGroup", strParams);
        }
       
    }
}
