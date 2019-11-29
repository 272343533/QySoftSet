using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dao.QyBllApp;
using QyTech.Core;
using System.Data.Objects;
using QyTech.Core.BLL;
using QyTech.Json;
using QyTech.Core.Common;

namespace QyExpress.Controllers.BllApp
{
    public class TotalController : BllAppController
    {
        //
        // GET: /Default1/

        public ActionResult Index()
        {
            return View();
        }

        public string ReportOnTime(string sessiond,string kvwhere)
        {
            return GetByStoreProcedure(sessiond, "splyTotalWjReportOnTime", kvwhere);
        }

        public override string GetByStoreProcedure(string sessionid, string spname, string kvwhere)
        {
            AddLogTable("获取", spname, "统计图表", kvwhere);
            Dictionary<string, string> dics = kvWhere2Dic(kvwhere);

            object[] objs = new object[dics.Count];
            for(int i=1;i<=dics.Count;i++)
            {
                objs[i - 1] = dics["param" + i.ToString()];
            }

            List<bsTotalResult> lst = EManager_App.GetAllByStorProcedure<bsTotalResult>(spname, objs);

            if (lst.Count > 0)
            {
                Type type = lst[0].GetType();
                return QyTech.Json.JsonHelper.SerializeObject(lst, type, null);
            }
            else
            {
                return "";
            }
        }
    }
}
