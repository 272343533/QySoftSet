using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QyTech.Core.ExController;
using qyExpress.Dao;
using QyTech.Core.BLL;
using QyTech.Core;
using QyTech.Core.Common;
using QyTech.Json;
using System.Web.Configuration;
using QyTech.BLL;

namespace QyExpress.Controllers.api
{
    public class bsFunConfController : SoftConfController
    {

        /// <summary>
        /// 重写的gettree
        /// </summary>
        /// <param name="where">不需要</param>
        /// <param name="orderby">不需要</param>
        /// <returns></returns>
        public override string getTreeDefault(string sessionid, string where = "", string orderby = "")
        {
            if (InnerAccout.IsInnerAccount(LoginUser))//内部账号
            {
                InnerAccountFilter iafitler = new InnerAccountFilter(LoginUser, WebConfigurationManager.AppSettings["currappName"]);
                where = iafitler.FunConf;
            }
            else
            {
                //除非是Excel导入列维护界面，否则不具有权限
                //用户-》角色-》所有需要导入数据的表信息
                //NeedImport=1
                return jsonMsgHelper.Create(1, null, "不具有操作权限！");
            }

            List<qytvNode> nodes = new List<qytvNode>();
            try
            {
                bllbsNavigation bobj = new bllbsNavigation();
                nodes = bobj.GetNaviNodes(EManager, LoginUser);

                List<bsFunConf> dbts = EntityManager_Static.GetListNoPaging<bsFunConf>(DbContext, where, "FunCode");
                foreach (bsFunConf s in dbts)
                {
                    qytvNode n = new qytvNode();
                    n.id = s.bsFC_Id.ToString();
                    n.pId = s.bsN_Id.ToString();
                    n.name = s.FunName + "-2";
                    n.type = "Fun";
                    nodes.Add(n);
                }
            }
            catch (Exception ex)
            { }
            string json = jsonMsgHelper.Create(0, nodes, "");
            return json;
        }

        private List<qytvNode> getdbtTreeNodes(string dbwhere, string dtwhere)
        {
            List<qytvNode> nodes = new List<qytvNode>();
            List<bsDb> dbbs = EntityManager_Static.GetListNoPaging<bsDb>(DbContext, dbwhere, "");
            foreach (bsDb s in dbbs)
            {
                qytvNode n = new qytvNode();
                n.id = s.bsD_Id.ToString();

                n.name = s.DbName;
                n.pId = Guid.Empty.ToString();
                n.type = "Db";
                //List<bsTable> dbts = EntityManager_Static.GetListNoPaging<bsTable>(dbcontext, "bsD_Id='"+s.bsD_Id.ToString()+ "' and TRank <= 10", "");

                nodes.Add(n);
            }
            List<bsTable> dbts = EntityManager_Static.GetListNoPaging<bsTable>(DbContext, dtwhere, "TName");
            foreach (bsTable t in dbts)
            {
                qytvNode n1 = new qytvNode();
                n1.id = t.bsT_Id.ToString();

                n1.name = t.TName;
                n1.pId = t.bsD_Id.ToString();
                n1.type = "Table";
                nodes.Add(n1);
            };
            return nodes;
        }


        public string getTreeForExcelMap()
        {
            string dbwhere = "";
            string dtwhere = "";
            if (InnerAccout.IsInnerAccount(LoginUser))//内部账号
            {
                InnerAccountFilter iafitler = new InnerAccountFilter(LoginUser, WebConfigurationManager.AppSettings["currappName"]);
                dbwhere = iafitler.Db;
                dtwhere = iafitler.DTable;
            }
            else if (InnerAccout.IssysAdmin(LoginUser))//系统管理员
            {
                dbwhere = " AppName='" + WebConfigurationManager.AppSettings["currAppName"].ToString() + "' or  (AppName='系统配置' and NAccount=1)";
                dtwhere = "((bsD_Id in (select bsD_Id from bsDb where " + dbwhere + " ))";//应用的表
            }
            else
            {
                //除非是Excel导入列维护界面，否则不具有权限
                //用户-》角色-》所有需要导入数据的表信息
                //NeedImport=1
                return jsonMsgHelper.Create(1, null, "不具有操作权限！");
            }

            dtwhere = dtwhere + " and (NeedExcelImpo=1)";
            List<qytvNode> nodes = getdbtTreeNodes(dbwhere, dtwhere);

            string json = jsonMsgHelper.Create(0, nodes, "");
            return json;
        }




        public string GetDtNotinBsTables()
        {
            List<qytvNode> nodes = new List<qytvNode>();

            List<tmpTreeNode> dbts = EntityManager_Static.GetAllByStorProcedure<tmpTreeNode>(DbContext, "splyGetDbAndTable", new object[] { WebConfigurationManager.AppSettings["currappName"], 0 });
            foreach (tmpTreeNode s in dbts)
            {
                qytvNode n = new qytvNode();
                n.id = s.id.ToString();
                n.pId = s.pId.ToString();
                n.name = s.name;
                n.type = s.type;
                nodes.Add(n);
            }
            string json = jsonMsgHelper.Create(0, nodes, "");
            return json;
        }
    }
}
