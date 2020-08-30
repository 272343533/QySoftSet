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
    public class ExcelExportController : BllAppController
    {
        public ActionResult Test(string sessionid, string kvwhere)
        {
            AddLogTable("导出", "rptLandLtdUpInfo", "报表1", kvwhere);
            Dictionary<string, string> dics = kvWhere2Dic(kvwhere);
            List<t企业基础数据> lst;
            if (WebSiteParams.currSoftCustCode != "wj")
            {
                lst = EntityManager_Static.GetAllByStorProcedure<t企业基础数据>(DbContext, "bllSp_rptLandLtdUpInfo", new object[] { dics["cycle"], dics["flowstatus"], dics["ltdchar"], WebSiteParams.currSoftCustCode });
            }
            else
            {
                lst = EntityManager_Static.GetAllByStorProcedure<t企业基础数据>(DbContext, "bllSp_rptLandLtdUpInfo", new object[] { dics["cycle"], dics["flowstatus"], dics["ltdchar"] });
            }
            QyTech.ExcelOper.QyExcelHelper excl = new QyTech.ExcelOper.QyExcelHelper("web");

            string downFileName;
            string templateFileName;
            templateFileName = "用地企业统计表1.xls";
            downFileName = "用地企业统计表1" + DateTime.Now.ToString("yyyyMMddHHmmsss") + ".xls";

            string fields = "B,B_1,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,AA,AB,AC,AD,AE,AF,AG,AH,AI,AJ,AK,AL,AM,AN,AO,AP,AQ,AR,AS1";
            //提交给wcf宿主程序
            string saveToPath = excl.ExportListToExcl<rptLandLtdUpInfo>(lst, downFileName, templateFileName, fields, "yyyy-MM-dd HH:mm:ss", true, 4, 2, "web");


            return DownFile(sessionid, saveToPath, downFileName);
        }
    }
}
