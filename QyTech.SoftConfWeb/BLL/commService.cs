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

        public static List<qytvNode> GetAppNames(ObjectContext dbcontext, string where)
        {
            List<qytvNode> nodes = new List<qytvNode>();
            List<bsAppName> dbobjs = EntityManager.GetListNoPaging<bsAppName>(dbcontext, "", "");
            foreach (bsAppName s in dbobjs)
            {
                qytvNode n = new qytvNode();
                n.Id = s.AppId;
                n.PId = Guid.Empty;
                n.Name = s.AppName;
                n.Tag = "";
                nodes.Add(n);
            }

            return nodes;
        }
        public static List<qytvNode> GetNavigations(ObjectContext dbcontext,string where)
        {
            List<qytvNode> nodes = new List<qytvNode>();

            List<bsNavigation> dbts = EntityManager.GetListNoPaging<bsNavigation>(dbcontext, where, "");
            foreach (bsNavigation s in dbts)
            {
                qytvNode n = new qytvNode();
                n.Id = s.bsN_Id;
                if (s.pId == null)
                    n.PId = Guid.Empty;
                else
                    n.PId = (Guid)s.pId;
                n.Name = s.NaviName;
                n.Tag = n.Id.ToString();
                nodes.Add(n);
            }
            return nodes;
        }
        public static List<qytvNode> GetbsTables(ObjectContext dbcontext)
        {
            List<qytvNode> nodes = new List<qytvNode>();

            List<bsDb> dbbs = EntityManager.GetListNoPaging<bsDb>(dbcontext, "AppName='" + GlobalVaribles.currAppObj.AppName + "'", "");
            foreach (bsDb s in dbbs)
            {
                qytvNode n = new qytvNode();
                n.Id = s.bsD_Id;

                n.Name = s.DbName;
                n.PId= Guid.Empty; 
                n.Tag = "Db";
                List<bsTable> dbts = EntityManager.GetListNoPaging<bsTable>(dbcontext, "bsD_Id='" + s.bsD_Id + "'", "");
                foreach (bsTable t in dbts)
                {
                    qytvNode n1 = new qytvNode();
                    n1.Id = t.bsT_Id;

                    n1.Name = t.TName;
                    n1.PId = (Guid)t.bsD_Id;
                    n1.Tag = "Table";
                    nodes.Add(n1);
                };
                nodes.Add(n);
            }

            return nodes;
        }
        public static List<qytvNode> GetNotinBsTables(ObjectContext dbcontext)
        {
            List<qytvNode> nodes = new List<qytvNode>();

            List<tmpTreeNode> dbts = EntityManager.GetAllByStorProcedure<tmpTreeNode>(dbcontext, "splyGetDbAndTable", new object[] { GlobalVaribles.currAppObj.AppName, 0 });
            foreach (tmpTreeNode s in dbts)
            {
                qytvNode n = new qytvNode();
                n.Id = s.Id;
                n.PId = s.PId;
                n.Name = s.Name;
                n.Tag = s.Tag;
                nodes.Add(n);
            }
            return nodes;
        }
        
        public static List<qytvNode> GetFunConfs(ObjectContext dbcontext,string where)
        {
            List<qytvNode> nodes = new List<qytvNode>();
            try
            {
                List<bsNavigation> dbns = EntityManager.GetListNoPaging<bsNavigation>(dbcontext, "bsA_Id='" + GlobalVaribles.currAppObj.AppId.ToString() + "'", "");
                foreach (bsNavigation s in dbns)
                {
                    qytvNode n = new qytvNode();
                    n.Id = s.bsN_Id;
                    if (s.pId == null)
                        n.PId = Guid.Empty;
                    else
                        n.PId = (Guid)s.pId;
                    n.Name = s.NaviName + "-1";
                    n.Tag = "Navi";
                    nodes.Add(n);
                }
                List<bsFunConf> dbts = EntityManager.GetListNoPaging<bsFunConf>(dbcontext, where, "");
                foreach (bsFunConf s in dbts)
                {
                    qytvNode n = new qytvNode();
                    n.Id = s.bsFC_Id;
                    n.PId = s.bsN_Id;
                    n.Name = s.FunName + "-2";
                    n.Tag = "Fun";
                    nodes.Add(n);
                }
            }
            catch { }
            return nodes;
        }
    }
}
