using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dao.QyBllApp;

using QyTech.Core;
using QyTech.Core.ExController.Bll;
namespace QyExpress.Controllers.BllApp
{
    public class ltdNoticeController : BllAppController
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionid"></param>
        /// <returns></returns>
        public string PublishInLogin()
        {
            SetObjectClassNamebyTName("ltdNotice");
            return base.GetOnebySql("", "bsO_Id='"+WebSiteParams.currSoftCustId+"' and NUse=1 and NValidDt>='" + DateTime.Now.ToString("yyyy-MM-dd") + "'");
        }

        public override string GetOneByFName(string sessionid,string FName,string FValue)
        {
            return base.GetOneByFName(sessionid, "bsO_Id", WebSiteParams.currSoftCustId);
        }
    }
}
