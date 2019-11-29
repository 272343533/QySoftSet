using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dao.QyBllApp;
using QyTech.Core.BLL;
using QyTech.Core;
using QyExpress.BLL;
using QyTech.Json;
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
            return base.GetOnebySql("", "bsO_Id='F34E355B-1B8A-41B7-99EC-300ACCCCE6CB' and NUse=1 and NValidDt>='" + DateTime.Now.ToString("yyyy-MM-dd") + "'");
        }


    }
}
