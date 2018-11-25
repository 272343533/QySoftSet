using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QyTech.UICreate;
using QyTech.SkinForm;

using QyExpress.Dao;
using QyTech.Json;
using QyTech.Utils;
using QyTech.Core.BLL;
using System.Data.Objects;

using QyTech.Core.Common;

namespace QyTech.SoftConf.BLL
{
    public class commService
    {
            /// <summary>
            /// 获取所有应用程序
            /// </summary>
            /// <param name="dbcontext"></param>
            /// <param name="where"></param>
            /// <returns></returns>
        public static List<qytvNode> GetAppNames(ObjectContext dbcontext, string where="")
        {
            List<qytvNode> nodes = new List<qytvNode>();
            List<bsAppName> dbobjs = EntityManager_Static.GetListNoPaging<bsAppName>(dbcontext, GlobalVaribles.currloginUserFilter.App, "");
            foreach (bsAppName s in dbobjs)
            {
                qytvNode n = new qytvNode();
                n.id = s.AppId.ToString();
                n.pId = Guid.Empty.ToString();
                n.name = s.AppName;
                n.type = "app";
                nodes.Add(n);
            }

            return nodes;
        }
        public static List<qytvNode> GetAppCustomers(ObjectContext dbcontext, string where="")
        {
            List<qytvNode> nodes = new List<qytvNode>();
            List<bsAppName> dbobjs = EntityManager_Static.GetListNoPaging<bsAppName>(dbcontext, GlobalVaribles.currloginUserFilter.App, "");
            foreach (bsAppName s in dbobjs)
            {
                qytvNode n = new qytvNode();
                n.id = s.AppId.ToString();
                n.pId = Guid.Empty.ToString();
                n.name = s.AppName;
                n.type = "appname";
                nodes.Add(n);
            }

            List<bsSoftCustInfo> dbobjs_cust = EntityManager_Static.GetListNoPaging<bsSoftCustInfo>(dbcontext, GlobalVaribles.currloginUserFilter.Cust, "");
            foreach (bsSoftCustInfo s in dbobjs_cust)
            {
                qytvNode n = new qytvNode();
                n.id = s.bsS_Id.ToString();
                n.pId = (EntityManager_Static.GetByPk<bsAppName>(dbcontext,"AppName", s.AppName)).AppId.ToString();
                n.name = s.Name;
                n.type = "customer";
                nodes.Add(n);
            }

            return nodes;
        }

        public static List<qytvNode> GetAppAreas(ObjectContext dbcontext, string where="")
        {
            List<qytvNode> nodes = new List<qytvNode>();
            List<bsAreaRegistration> dbobjs = EntityManager_Static.GetListNoPaging<bsAreaRegistration>(dbcontext, GlobalVaribles.currloginUserFilter.Area, "");
            foreach (bsAreaRegistration s in dbobjs)
            {
                qytvNode n = new qytvNode();
                n.id = s.bsA_Id.ToString();
                n.pId = Guid.Empty.ToString();
                n.name = s.AreaName;
                n.type = "";
                nodes.Add(n);
            }

            return nodes;
        }






        /// <summary>
        /// 获取导航数据
        /// </summary>
        /// <param name="dbcontext"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public static List<qytvNode> GetNavigations(ObjectContext dbcontext)
        {
            List<qytvNode> nodes = new List<qytvNode>();
            //string url = "";

            //url = GlobalVaribles.ServerUrl + "/api/bsNavigation/getTree";
            //QyJsonData jsonData = HttpRequestUtils.PostRemoteJsonQy(url, null);
            //nodes = JsonHelper.DeserializeJsonToList<qytvNode>(jsonData.data.ToString());
            string where = "";
            if (InnerAccout.expressAdminUser.bsU_Id == GlobalVaribles.currloginUser.bsU_Id)
            {
                where = "appName='系统配置'";
            }
            else
                where="bsN_Id in (select bsN_Id from bsSoftRelFuns where bsS_Id='" + GlobalVaribles.currSoftCutomer.bsS_Id.ToString() + "')";
            List<bsNavigation> objs = EntityManager_Static.GetListNoPaging<bsNavigation>(dbcontext, where, "NaviNo");
            foreach(bsNavigation obj in objs)
            {
                qytvNode node = new qytvNode();
                node.id = obj.bsN_Id.ToString();
                node.name = obj.NaviName;
                node.pId = obj.pId.ToString();
                node.type = obj.NaviType;
                nodes.Add(node);
            }

            return nodes; 
        }

        /// <summary>
        /// 获取所有表
        /// </summary>
        /// <param name="dbcontext"></param>
        /// <returns></returns>
        public static List<qytvNode> GetbsTables(ObjectContext dbcontext)
        {

            //List<qytvNode> nodes = new List<qytvNode>();
            //string url = "";

            //url = GlobalVaribles.ServerUrl + "/api/bsTable/getTree";
            //QyJsonData jsonData = HttpRequestUtils.PostRemoteJsonQy(url, null);
            //nodes = JsonHelper.DeserializeJsonToList<qytvNode>(jsonData.data.ToString());
            string dbwhere = "";
            string dtwhere = "";
            string appName = System.Configuration.ConfigurationManager.AppSettings["curAppName"];
            if (GlobalVaribles.currloginUser.bsU_Id == InnerAccout.expressAdminUser.bsU_Id)
            {
                appName = "系统配置";
            }
            if (InnerAccout.IsInnerAccount(GlobalVaribles.currloginUser))//内部账号
            {
                InnerAccountFilter iafitler = new InnerAccountFilter(GlobalVaribles.currloginUser, appName);
                dbwhere = iafitler.Db;
                dtwhere = iafitler.DTable;
            }
            else if (InnerAccout.IssysAdmin(GlobalVaribles.currloginUser))//系统管理员
            {
                dbwhere = " AppName='" + appName + "' or  (AppName='系统配置')";
                dtwhere = "(bsD_Id in (select bsD_Id from bsDb where AppName='" + appName + "' ))";//应用的表
            }
            else
            {
                //除非是Excel导入列维护界面，否则不具有权限
                //用户-》角色-》所有需要导入数据的表信息
                //NeedImport=1
                MessageBox.Show( "不具有操作权限！");
            }

            List<qytvNode> nodes = new List<qytvNode>();
 
            List<bsDb> dbbs = EntityManager_Static.GetListNoPaging<bsDb>(dbcontext, GlobalVaribles.currloginUserFilter.Db, "");
            foreach (bsDb s in dbbs)
            {
                qytvNode n = new qytvNode();
                n.id = s.bsD_Id.ToString();

                n.name = s.DbName;
                n.pId= Guid.Empty.ToString(); 
                n.type = "Db";
                //List<bsTable> dbts = EntityManager_Static.GetListNoPaging<bsTable>(dbcontext, "bsD_Id='"+s.bsD_Id.ToString()+ "' and TRank <= 10", "");
                
                nodes.Add(n);
            }
            List<bsTable> dbts = EntityManager_Static.GetListNoPaging<bsTable>(dbcontext, GlobalVaribles.currloginUserFilter.DTable, "");
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



        /// <summary>
        /// 获取所有未导入数据字典的表
        /// </summary>
        /// <param name="dbcontext"></param>
        /// <returns></returns>
        //public static List<qytvNode> GetNotinBsTables(ObjectContext dbcontext)
        //{
        //    List<qytvNode> nodes = new List<qytvNode>();

        //    List<tmpTreeNode> dbts = EntityManager_Static.GetAllByStorProcedure<tmpTreeNode>(dbcontext, "splyGetDbAndTable", new object[] { GlobalVaribles.currAppObj.AppName, 0 });
        //    foreach (tmpTreeNode s in dbts)
        //    {
        //        qytvNode n = new qytvNode();
        //        n.id = s.id.ToString();
        //        n.pId = s.pId.ToString();
        //        n.name = s.name;
        //        n.type = s.type;
        //        nodes.Add(n);
        //    }
        //    return nodes;
        //}
        
        /// <summary>
        /// 获取所有功能配置项
        /// </summary>
        /// <param name="dbcontext"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public static List<qytvNode> GetFunConfs(ObjectContext dbcontext,string where)
        {

            //List<qytvNode> nodes = new List<qytvNode>();
            //string url = "";

            //url = GlobalVaribles.ServerUrl + "/SoftConf/bsFunConf/getTree";
            //QyJsonData jsonData = HttpRequestUtils.PostRemoteJsonQy(url, null);
            //nodes = JsonHelper.DeserializeJsonToList<qytvNode>(jsonData.data.ToString());
            //return nodes;

            List<qytvNode> nodes = new List<qytvNode>();
            try
            {
                nodes = GetNavigations(dbcontext);
                List<bsFunConf> dbts = EntityManager_Static.GetListNoPaging<bsFunConf>(dbcontext, GlobalVaribles.currloginUserFilter.FunConf, "FunCode");
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
            return nodes;
        }


    }
}
