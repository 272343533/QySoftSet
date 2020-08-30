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
    public class tA1企业基本信息表Controller : BllAppController
    {
        public override string GetAll(string sessionid, string fields = "", string where = "", string orderby = "")
        {
            tA1企业基本信息表 obj = new tA1企业基本信息表();
            return base.GetAll(sessionid, fields, where, orderby);
        }

    }
}
