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

using QyTech.Auth.Dao;
using QyTech.SoftConf;
using QyTech.SkinForm.Controls;
using QyTech.Core.BLL;
using System.Data.Objects;

namespace QyTech.SoftConf.BLL
{
    public class commService
    {

        public static string AppNameWhere = "";
        // "AppName in('" + GlobalVaribles.currAppObj.AppName + "','系统配置')";
        public static string DbWhere = "";
        // "bsD_Id in ( select bsD_Id from bsDb where AppName in('" + GlobalVaribles.currAppObj.AppName + "','系统配置'))";
        public static string bsTWhere= "";
        //"bsT_Id in (select bsT_Id from bsTable where bsD_Name in (select DbName from bsDb where AppName in('"+GlobalVaribles.currAppObj.AppName+ "','系统配置')";
        public static string NavigationWhere = "";
        // "bsA_Id in ('" + GlobalVaribles.currAppObj.AppId.ToString() + "','3e44dd5b-6d6f-4c39-846a-4e7c20332dd2')";
        public static string FunConfWhere = "";
        //"bsFC_Id in (select bsFC_Id from bsFunConf where bsN_Id in (select bsN_Id from bsNavigation where bsA_Id in ('" + GlobalVaribles.currAppObj.AppId.ToString() + "','3e44dd5b-6d6f-4c39-846a-4e7c20332dd2')))";



            /// <summary>
            /// 获取所有应用程序
            /// </summary>
            /// <param name="dbcontext"></param>
            /// <param name="where"></param>
            /// <returns></returns>
        public static List<qytvNode> GetAppNames(ObjectContext dbcontext, string where)
        {
            List<qytvNode> nodes = new List<qytvNode>();
            List<bsAppName> dbobjs = EntityManager_Static.GetListNoPaging<bsAppName>(dbcontext, "", "");
            foreach (bsAppName s in dbobjs)
            {
                qytvNode n = new qytvNode();
                n.Id = s.AppId.ToString();
                n.PId = Guid.Empty.ToString();
                n.Name = s.AppName;
                n.Tag = "";
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
        public static List<qytvNode> GetNavigations(ObjectContext dbcontext,string where)
        {
            List<qytvNode> nodes = new List<qytvNode>();
            //暂时获取所有导航，还没给用户赋予权限呢
            List<bsNavigation> dbts = EntityManager_Static.GetListNoPaging<bsNavigation>(dbcontext, NavigationWhere, "NaviNo");
            foreach (bsNavigation s in dbts)
            {
                qytvNode n = new qytvNode();
                n.Id = s.bsN_Id.ToString();
                if (s.pId == null)
                    n.PId = Guid.Empty.ToString();
                else
                    n.PId = s.pId.ToString();
                n.Name = s.NaviName;
                n.Tag = n.Id.ToString();
                nodes.Add(n);
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
            List<qytvNode> nodes = new List<qytvNode>();

            List<bsDb> dbbs = EntityManager_Static.GetListNoPaging<bsDb>(dbcontext, AppNameWhere, "");
            foreach (bsDb s in dbbs)
            {
                qytvNode n = new qytvNode();
                n.Id = s.bsD_Id.ToString();

                n.Name = s.DbName;
                n.PId= Guid.Empty.ToString(); 
                n.Tag = "Db";
                List<bsTable> dbts = EntityManager_Static.GetListNoPaging<bsTable>(dbcontext, "bsD_Id='"+s.bsD_Id.ToString()+"'", "");
                foreach (bsTable t in dbts)
                {
                    qytvNode n1 = new qytvNode();
                    n1.Id = t.bsT_Id.ToString();

                    n1.Name = t.TName;
                    n1.PId = t.bsD_Id.ToString();
                    n1.Tag = "Table";
                    nodes.Add(n1);
                };
                nodes.Add(n);
            }

            return nodes;
        }


        public static List<qytvNode> GetTFRights(ObjectContext dbcontext,string where)
        {
            List<qytvNode> nodes = new List<qytvNode>();

            List<bsTable> tables = EntityManager_Static.GetListNoPaging<bsTable>(dbcontext, "HaveRightType is not null", "TName");
            foreach (bsTable s in tables)
            {
                qytvNode n = new qytvNode();
                n.Id = s.bsT_Id.ToString();

                n.Name = s.TName;
                n.PId = Guid.Empty.ToString();
                n.Tag = "Table";
                List<bsField> dbfs = EntityManager_Static.GetListNoPaging<bsField>(dbcontext, "UseFieldRight=1", "FName");
                foreach (bsField t in dbfs)
                {
                    qytvNode n1 = new qytvNode();
                    n1.Id = t.bsF_Id.ToString();

                    n1.Name = t.FName;
                    n1.PId = t.bsT_Id.ToString();
                    n1.Tag = "Field";
                    nodes.Add(n1);
                };
                nodes.Add(n);
            }

            return nodes;
        }
        /// <summary>
        /// 获取所有未导入数据字典的表
        /// </summary>
        /// <param name="dbcontext"></param>
        /// <returns></returns>
        public static List<qytvNode> GetNotinBsTables(ObjectContext dbcontext)
        {
            List<qytvNode> nodes = new List<qytvNode>();

            List<tmpTreeNode> dbts = EntityManager_Static.GetAllByStorProcedure<tmpTreeNode>(dbcontext, "splyGetDbAndTable", new object[] { GlobalVaribles.currAppObj.AppName, 0 });
            foreach (tmpTreeNode s in dbts)
            {
                qytvNode n = new qytvNode();
                n.Id = s.Id.ToString();
                n.PId = s.PId.ToString();
                n.Name = s.Name;
                n.Tag = s.Tag;
                nodes.Add(n);
            }
            return nodes;
        }
        
        /// <summary>
        /// 获取所有功能配置项
        /// </summary>
        /// <param name="dbcontext"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public static List<qytvNode> GetFunConfs(ObjectContext dbcontext,string where)
        {
            List<qytvNode> nodes = new List<qytvNode>();
            try
            {
                List<bsNavigation> dbns = EntityManager_Static.GetListNoPaging<bsNavigation>(dbcontext, NavigationWhere, "NaviNo");
                foreach (bsNavigation s in dbns)
                {
                    qytvNode n = new qytvNode();
                    n.Id = s.bsN_Id.ToString();
                    if (s.pId == null)
                        n.PId = Guid.Empty.ToString();
                    else
                        n.PId = s.pId.ToString();
                    n.Name = s.NaviName + "-1";
                    n.Tag = "Navi";
                    nodes.Add(n);
                }
                List<bsFunConf> dbts = EntityManager_Static.GetListNoPaging<bsFunConf>(dbcontext, FunConfWhere, "FunCode");
                foreach (bsFunConf s in dbts)
                {
                    qytvNode n = new qytvNode();
                    n.Id = s.bsFC_Id.ToString();
                    n.PId = s.bsN_Id.ToString();
                    n.Name = s.FunName + "-2";
                    n.Tag = "Fun";
                    nodes.Add(n);
                }
            }
            catch { }
            return nodes;
        }

        /// <summary>
        /// 获取组织机构数据
        /// </summary>
        /// <param name="dbcontext"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public static List<qytvNode> GetOrgs(ObjectContext dbcontext, string where)
        {
            List<qytvNode> nodes = new List<qytvNode>();
            try
            {
                List<bsOrganize> dbns = EntityManager_Static.GetListNoPaging<bsOrganize>(dbcontext, "", "");
                foreach (bsOrganize s in dbns)
                {
                    qytvNode n = new qytvNode();
                    n.Id = s.bsO_Id.ToString();
                    if (s.PId == null)
                        n.PId = Guid.Empty.ToString();
                    else
                        n.PId = s.PId.ToString();
                    n.Name = s.Name + "-1";
                    n.Tag = s.bsoAttr;
                    nodes.Add(n);
                }
            }
            catch { }
            return nodes;
        }

        public static List<qytvNode> GetRoles(ObjectContext dbcontext, string where)
        {
            List<qytvNode> nodes = new List<qytvNode>();
            try
            {
                //部门，角色
                List<bsOrganize> os = EntityManager_Static.GetListNoPaging<bsOrganize>(dbcontext, "", "");
                foreach (bsOrganize o in os)
                {
                    qytvNode n = new qytvNode();
                    n.Id = o.bsO_Id.ToString();
                    if (o.PId == null)
                        n.PId = Guid.Empty.ToString();
                    else
                        n.PId = o.PId.ToString();
                    n.Name = o.Name;
                    n.Tag = "Organzie";
                    nodes.Add(n);
                    List<bsRole> rs = EntityManager_Static.GetListNoPaging<bsRole>(dbcontext, "bsO_Id='" + o.bsO_Id.ToString() + "'", "");
                    foreach (bsRole s in rs)
                    {
                        qytvNode n1 = new qytvNode();
                        n1.Id = s.bsR_Id.ToString();
                        n1.PId = s.bsO_Id.ToString();
                        n1.Name = s.Name;
                        n1.Tag = "Role";
                        nodes.Add(n1);
                    }
                }
            }
            catch { }
            return nodes;
        }
    }
}
