using System.Web;
using System.Web.Mvc;
using QyTech.Core.ExController;
using qyExpress.Dao;
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
    public class bscommentController : BllAppController
    {
        public override string GetAll(string sessionid, string fields = "", string where = "", string orderby = "last_reply_dt")
        {
            return base.GetAll(sessionid, fields, where, orderby);
        }
        public override string Upload(string subpath)
        {
            return base.Upload("announce/");
        }
        public override string Uploads(string subpath)
        {
            return base.Uploads("announce/");
        }
    }
}
