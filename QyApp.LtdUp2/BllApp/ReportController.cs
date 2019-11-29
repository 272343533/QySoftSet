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
using QyTech.Core.ExController.Bll;

namespace QyExpress.Controllers.BllApp
{
    public class ReportController : BllAppController
    {
        /// <summary>
        /// 地主企业上传信息
        /// </summary>
        /// <param name="sessionid"></param>
        ///<param name="kvwhere">cycle（周期），flowstatus（审核状态）,ltdchar（企业情况）</param>
        /// <returns></returns>
        public string RptLandLtdUpInfo(string sessionid, string kvwhere)
        {
            AddLogTable("获取", "rptLandLtdUpInfo", "报表1", kvwhere);
            Dictionary<string, string> dics = kvWhere2Dic(kvwhere);
            List<rptLandLtdUpInfo> lst;
            if (WebSiteParams.currSoftCustCode!="wj")
            {
                 lst = EntityManager_Static.GetAllByStorProcedure<rptLandLtdUpInfo>(DbContext, "bllSp_rptLandLtdUpInfo", new object[] { dics["cycle"], dics["flowstatus"], dics["ltdchar"], WebSiteParams.currSoftCustCode });
            }
            else
            {
                lst = EntityManager_Static.GetAllByStorProcedure<rptLandLtdUpInfo>(DbContext, "bllSp_rptLandLtdUpInfo", new object[] { dics["cycle"], dics["flowstatus"], dics["ltdchar"] });
            }

            if (lst.Count > 0)
            {
                Type type = lst[0].GetType();
                return QyTech.Json.JsonHelper.SerializeObject(lst, type, null);
            }
            else
            {
                return "";
            }
            // return jsonMsgHelper.Create(0, lst, "");
        }
        public ActionResult RptLandLtdUpInfo_Report(string sessionid, string kvwhere)
        {
            AddLogTable("导出", "rptLandLtdUpInfo", "报表1", kvwhere);
            Dictionary<string, string> dics = kvWhere2Dic(kvwhere);
            List<rptLandLtdUpInfo> lst;
            if (WebSiteParams.currSoftCustCode != "wj")
            {
                lst = EntityManager_Static.GetAllByStorProcedure<rptLandLtdUpInfo>(DbContext, "bllSp_rptLandLtdUpInfo", new object[] { dics["cycle"], dics["flowstatus"], dics["ltdchar"], WebSiteParams.currSoftCustCode });
            }
            else
            {
                 lst = EntityManager_Static.GetAllByStorProcedure<rptLandLtdUpInfo>(DbContext, "bllSp_rptLandLtdUpInfo", new object[] { dics["cycle"], dics["flowstatus"], dics["ltdchar"] });
            }
            QyTech.ExcelOper.QyExcelHelper excl = new QyTech.ExcelOper.QyExcelHelper("web");

            string downFileName;
            string templateFileName;
            templateFileName = "用地企业统计表1.xls";
            downFileName = "用地企业统计表1" + DateTime.Now.ToString("yyyyMMddHHmmsss") + ".xls";

            string fields = "B,B_1,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,AA,AB,AC,AD,AE,AF,AG,AH,AI,AJ,AK,AL,AM,AN,AO,AP,AQ,AR,AS1";

            string saveToPath = excl.ExportListToExcl<rptLandLtdUpInfo>(lst, downFileName, templateFileName, fields, "yyyy-MM-dd HH:mm:ss", true, 4, 2, "web");
            return DownFile(sessionid, saveToPath, downFileName);
        }

        /// <summary>
        /// 地主企业租户信息
        /// </summary>
        /// <param name="sessionid"></param>
        ///<param name="kvwhere">cycle（周期），flowstatus（审核状态）,ltdchar（企业情况）</param>
        /// <returns></returns>
        public string RptLandLtdUpRent(string sessionid, string kvwhere)
        {
            AddLogTable("获取", "rptLandLtdUpRent", "报表2", kvwhere);
            Dictionary<string, string> dics = kvWhere2Dic(kvwhere);
            List<rptLandLtdUpRent> lst;
            if (WebSiteParams.currSoftCustCode != "wj")
                lst = EntityManager_Static.GetAllByStorProcedure<rptLandLtdUpRent>(DbContext, "bllSp_rptLandLtdUpRent", new object[] { dics["cycle"], dics["flowstatus"], dics["ltdchar"], WebSiteParams.currSoftCustCode });
            else
                lst = EntityManager_Static.GetAllByStorProcedure<rptLandLtdUpRent>(DbContext, "bllSp_rptLandLtdUpRent", new object[] { dics["cycle"], dics["flowstatus"], dics["ltdchar"] });

            if (lst.Count > 0)
            {
                Type type = lst[0].GetType();
                return QyTech.Json.JsonHelper.SerializeObject(lst, type, null);
            }
            else
            {
                return "";
            }
            // return jsonMsgHelper.Create(0, lst, "");
        }

        public ActionResult RptLandLtdUpRent_Report(string sessionid, string kvwhere)
        {
            AddLogTable("导出", "rptLandLtdUpRent", "报表2", kvwhere);
            Dictionary<string, string> dics = kvWhere2Dic(kvwhere);
            List<rptLandLtdUpRent> lst;
            if (WebSiteParams.currSoftCustCode != "wj")
                lst = EntityManager_Static.GetAllByStorProcedure<rptLandLtdUpRent>(DbContext, "bllSp_rptLandLtdUpRent", new object[] { dics["cycle"], dics["flowstatus"], dics["ltdchar"], WebSiteParams.currSoftCustCode });
            else
                lst = EntityManager_Static.GetAllByStorProcedure<rptLandLtdUpRent>(DbContext, "bllSp_rptLandLtdUpRent", new object[] { dics["cycle"], dics["flowstatus"], dics["ltdchar"] });


            QyTech.ExcelOper.QyExcelHelper excl = new QyTech.ExcelOper.QyExcelHelper("web");
            string downFileName;
            string templateFileName;
            templateFileName = "用地企业统计表2.xls";
            downFileName = "用地企业统计表2" + DateTime.Now.ToString("yyyyMMddHHmmsss") + ".xls";


            string fields = "B,C,D,E,F,G,H,I,J";

            string saveToPath = excl.ExportListToExcl<rptLandLtdUpRent>(lst, downFileName, templateFileName, fields, "yyyy-MM-dd HH:mm:ss", true, 3, 2, "web");
            return DownFile(sessionid, saveToPath, downFileName);

        }
    
        /// <summary>
        /// 租赁企业上传信息
        /// </summary>
        /// <param name="sessionid"></param>
        ///<param name="kvwhere">cycle（周期），flowstatus（审核状态）,ltdchar（企业情况）</param>
        /// <returns></returns>
        public string RptRentLtdUpInfo(string sessionid, string kvwhere)
        {
            AddLogTable("获取", "rptRentLtdUpInfo", "报表3", kvwhere);

            Dictionary<string, string> dics = kvWhere2Dic(kvwhere);
            List<rptRentLtdUpInfo> lst;
            if (WebSiteParams.currSoftCustCode != "wj")
                lst = EntityManager_Static.GetAllByStorProcedure<rptRentLtdUpInfo>(DbContext, "bllSp_rptRentLtdUpInfo", new object[] { dics["cycle"], dics["flowstatus"], dics["ltdchar"], WebSiteParams.currSoftCustCode });
            else
                lst = EntityManager_Static.GetAllByStorProcedure<rptRentLtdUpInfo>(DbContext, "bllSp_rptRentLtdUpInfo", new object[] { dics["cycle"], dics["flowstatus"], dics["ltdchar"]});
            if (lst.Count > 0)
            {
                Type type = lst[0].GetType();
                return QyTech.Json.JsonHelper.SerializeObject(lst, type, null);
            }
            else
            {
                return "";
            }
            // return jsonMsgHelper.Create(0, lst, "");
        }

        public ActionResult RptRentLtdUpInfo_Report(string sessionid, string kvwhere)
        {
            AddLogTable("导出", "rptRentLtdUpInfo", "报表3", kvwhere);

            Dictionary<string, string> dics = kvWhere2Dic(kvwhere);

            List<rptRentLtdUpInfo> lst;
            if (WebSiteParams.currSoftCustCode != "wj")
                lst = EntityManager_Static.GetAllByStorProcedure<rptRentLtdUpInfo>(DbContext, "bllSp_rptRentLtdUpInfo", new object[] { dics["cycle"], dics["flowstatus"], dics["ltdchar"], WebSiteParams.currSoftCustCode });
            else
                lst = EntityManager_Static.GetAllByStorProcedure<rptRentLtdUpInfo>(DbContext, "bllSp_rptRentLtdUpInfo", new object[] { dics["cycle"], dics["flowstatus"], dics["ltdchar"] });

            QyTech.ExcelOper.QyExcelHelper excl = new QyTech.ExcelOper.QyExcelHelper("web");
            string downFileName;
            string templateFileName;
            templateFileName = "租赁企业统计表.xls";
            downFileName = "租赁企业统计表" + DateTime.Now.ToString("yyyyMMddHHmmsss") + ".xls";

            string fields = "B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,AA,AB,AC,AD,AE,AF,AG,AH,AI,AJ,AK,AL,AM,AN";
            string saveToPath = excl.ExportListToExcl<rptRentLtdUpInfo>(lst, downFileName, templateFileName, fields, "yyyy-MM-dd HH:mm:ss", true, 4, 2, "web");
            return DownFile(sessionid, saveToPath, downFileName);
        }

        /// <summary>
        /// 企业体检情况
        /// </summary>
        /// <param name="sessionid"></param>
        ///<param name="kvwhere">cycle(周期)，checkstatus(体检状态）</param>
        /// <returns></returns>
        public string RptLtdCheck(string sessionid, string kvwhere)
        {
            AddLogTable("获取", "rptLtdCheck", "报表4", kvwhere);

            Dictionary<string, string> dics = kvWhere2Dic(kvwhere);
            if (!dics.ContainsKey("haverelltd"))
                dics["haverelltd"] = "0";

            List<rptLtdCheck> lst;
            if (WebSiteParams.currSoftCustCode != "wj")
                lst = EntityManager_Static.GetAllByStorProcedure<rptLtdCheck>(DbContext, "bllSp_rptLtdCheck", new object[] { dics["checkstatus"], dics["ltdname"],dics["haverelltd"],WebSiteParams.currSoftCustCode });
            else
                lst = EntityManager_Static.GetAllByStorProcedure<rptLtdCheck>(DbContext, "bllSp_rptLtdCheck", new object[] { dics["checkstatus"], dics["ltdname"], dics["haverelltd"]});

            if (lst.Count > 0)
            {
                Type type = lst[0].GetType();
                return QyTech.Json.JsonHelper.SerializeObject(lst, type, null);
            }
            else
            {
                return "";
            }
            // return jsonMsgHelper.Create(0, lst, "");
        }


        public ActionResult RptLtdCheck_Report(string sessionid, string kvwhere)
        {
            AddLogTable("导出", "rptLtdCheck", "报表4", kvwhere);

            Dictionary<string, string> dics = kvWhere2Dic(kvwhere);
            if (!dics.ContainsKey("haverelltd"))
                dics["haverelltd"] = "0";
            List<rptLtdCheck> lst;
            if (WebSiteParams.currSoftCustCode != "wj")
                lst = EntityManager_Static.GetAllByStorProcedure<rptLtdCheck>(DbContext, "bllSp_rptLtdCheck", new object[] { dics["checkstatus"], dics["ltdname"], dics["haverelltd"], WebSiteParams.currSoftCustCode });
            else
                lst = EntityManager_Static.GetAllByStorProcedure<rptLtdCheck>(DbContext, "bllSp_rptLtdCheck", new object[] { dics["checkstatus"], dics["ltdname"], dics["haverelltd"]});

            QyTech.ExcelOper.QyExcelHelper excl = new QyTech.ExcelOper.QyExcelHelper("web");
            string downFileName;
            string templateFileName;
            templateFileName = "企业体检信息.xls";
            downFileName = "企业体检信息" + DateTime.Now.ToString("yyyyMMddHHmmsss") + ".xls";

            string fields = "Cycle,LtdName,NSRSBH,LtdAddr,UserDKBH,Contacter,ContactTel,CurrCheckStatus,FlowStatus";

            string saveToPath = excl.ExportListToExcl<rptLtdCheck>(lst, downFileName, templateFileName, fields, "yyyy-MM-dd HH:mm:ss", true, 3, 2, "web");
            return DownFile(sessionid, saveToPath, downFileName);
        }

        /// <summary>
        /// 企业位置报表
        /// </summary>
        /// <param name="sessionid"></param>
        /// <param name="kvwhere">islandltd（空:全部企业；1： 地主企业；0：租赁企业）;ltdname（企业名称）</param>
        /// <returns></returns>
        public string RptLtdPostition(string sessionid,string kvwhere)
        {
            AddLogTable("获取", "rptLtdPostition", "报表5", kvwhere);

            Dictionary<string, string> dics = kvWhere2Dic(kvwhere);
            List<rptLtdPostition> lst;
            if (WebSiteParams.currSoftCustCode != "wj")
                lst = EntityManager_Static.GetAllByStorProcedure<rptLtdPostition>(DbContext, "bllSp_rptLtdPostition", new object[] { dics["islandltd"], dics["ltdname"], WebSiteParams.currSoftCustCode });
            else
                lst = EntityManager_Static.GetAllByStorProcedure<rptLtdPostition>(DbContext, "bllSp_rptLtdPostition", new object[] { dics["islandltd"], dics["ltdname"]});

            if (lst.Count > 0)
            {
                Type type = lst[0].GetType();
                return QyTech.Json.JsonHelper.SerializeObject(lst, type, null);
            }
            else
            {
                return "";
            }
          //  return jsonMsgHelper.Create(0, lst, "");
        }

        public ActionResult RptLtdPostition_Report(string sessionid, string kvwhere)
        {
            AddLogTable("导出", "rptLtdPostition", "报表5", kvwhere);

            Dictionary<string, string> dics = kvWhere2Dic(kvwhere);
            List<rptLtdPostition> lst;
            if (WebSiteParams.currSoftCustCode != "wj")
                lst = EntityManager_Static.GetAllByStorProcedure<rptLtdPostition>(DbContext, "bllSp_rptLtdPostition", new object[] { dics["islandltd"], dics["ltdname"], WebSiteParams.currSoftCustCode });
            else
                lst = EntityManager_Static.GetAllByStorProcedure<rptLtdPostition>(DbContext, "bllSp_rptLtdPostition", new object[] { dics["islandltd"], dics["ltdname"] });

            QyTech.ExcelOper.QyExcelHelper excl = new QyTech.ExcelOper.QyExcelHelper("web");
            string downFileName;
            string templateFileName;
            templateFileName = "企业定位信息.xls";
            downFileName = "企业定位信息" + DateTime.Now.ToString("yyyyMMddHHmmsss") + ".xls";


            string fields = "LtdName,NSRSBH,DKBH,Addr,LongLatitude";

            string saveToPath = excl.ExportListToExcl<rptLtdPostition>(lst, downFileName, templateFileName, fields, "yyyy-MM-dd HH:mm:ss", true, 3, 2, "web");
            return DownFile(sessionid, saveToPath, downFileName);
        }


        public string RptLtdReginfo(string sessionid, string kvwhere)
        {
            AddLogTable("获取", "rptLtdPostition", "报表6", kvwhere);

            Dictionary<string, string> dics = kvWhere2Dic(kvwhere);
            List<rptLtdRegInfo> lst;
            if (WebSiteParams.currSoftCustCode != "wj")
                lst = EntityManager_Static.GetAllByStorProcedure<rptLtdRegInfo>(DbContext, "bllSp_rptLtdRegInfo", new object[] { dics["islandltd"], dics["ltdname"], WebSiteParams.currSoftCustCode });
            else
                lst = EntityManager_Static.GetAllByStorProcedure<rptLtdRegInfo>(DbContext, "bllSp_rptLtdRegInfo", new object[] { dics["islandltd"], dics["ltdname"]});

            if (lst.Count > 0)
            {
                Type type = lst[0].GetType();
                return QyTech.Json.JsonHelper.SerializeObject(lst, type, null);
            }
            else
            {
                return "";
            }
            //  return jsonMsgHelper.Create(0, lst, "");
        }

        public ActionResult RptLtdReginfo_Report(string sessionid, string kvwhere)
        {
            AddLogTable("导出", "bllSp_rptLtdRegInfo", "报表6", kvwhere);

            Dictionary<string, string> dics = kvWhere2Dic(kvwhere);
            List<rptLtdRegInfo> lst;
            if (WebSiteParams.currSoftCustCode != "wj")
                lst = EntityManager_Static.GetAllByStorProcedure<rptLtdRegInfo>(DbContext, "bllSp_rptLtdRegInfo", new object[] { dics["islandltd"], dics["ltdname"], WebSiteParams.currSoftCustCode });
            else
                lst = EntityManager_Static.GetAllByStorProcedure<rptLtdRegInfo>(DbContext, "bllSp_rptLtdRegInfo", new object[] { dics["islandltd"], dics["ltdname"]});

            QyTech.ExcelOper.QyExcelHelper excl = new QyTech.ExcelOper.QyExcelHelper("web");
            string downFileName;
            string templateFileName;
            templateFileName = "企业信息统计.xls";
            downFileName = "企业信息统计" + DateTime.Now.ToString("yyyyMMddHHmmsss") + ".xls";


            string fields = "LtdName,IsLandLtd,NSRSBH,LtdAddr,Contacter,ContactTe";

            string saveToPath = excl.ExportListToExcl<rptLtdRegInfo>(lst, downFileName, templateFileName, fields, "yyyy-MM-dd HH:mm:ss", true, 3, 2, "web");
            return DownFile(sessionid, saveToPath, downFileName);
        }
    }
}
