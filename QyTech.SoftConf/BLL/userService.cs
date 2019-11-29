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
using QyTech.SoftConf;
using QyTech.SkinForm.Controls;
using QyTech.Core.BLL;
using System.Data.Objects;

using QyTech.Core.Common;
namespace QyTech.SoftConf.BLL
{
    public class userService
    {


        public static List<qytvNode> GetCustNavigatios(ObjectContext dbcontext, bsSoftCustInfo cust)
        {
            List<qytvNode> nodes = new List<qytvNode>();
            //nodes = BLL.commService.GetNavigations(GlobalVaribles.ObjContext_Base);
            List<bsNavigation> dbts = EntityManager_Static.GetListNoPaging<bsNavigation>(dbcontext, "AppName='"+ cust.AppName + "' or  (AppName='系统配置' and NAccount=1)", "NaviNo");

            //用户具有的角色具有的功能
            List<bsSoftRelFuns> dbts_sub = EntityManager_Static.GetListNoPaging<bsSoftRelFuns>(dbcontext, "bsS_Id='"+cust.bsS_Id.ToString()+"'", "");
            List<Guid> subs = new List<Guid>();
            for(int i = 0; i < dbts_sub.Count; i++)
            {
                subs.Add(dbts_sub[i].bsN_Id);
            }
            if (dbts != null)
            {
                foreach (bsNavigation s in dbts)
                {
                    try
                    {
                        qytvNode n = new qytvNode();
                        n.id = s.bsN_Id.ToString();
                        if (s.pId == null)
                            n.pId = Guid.Empty.ToString();
                        else
                        {
                            if (s.pId == Guid.Parse("D2E65E66-B320-46B0-AF6E-8BD1F5B50774"))
                            {
                                if (nodes.Count > 0)
                                    n.pId = nodes[0].id;
                                else
                                    n.pId = Guid.Empty.ToString();
                            }
                            else
                                n.pId = s.pId.ToString();
                        }
                        n.name = s.NaviName;
                        n.type = s.NaviType;
                        if (subs.Contains(s.bsN_Id))
                            n.checkFlag = true;
                        else
                            n.checkFlag = false;
                        nodes.Add(n);
                    }
                    catch { }
                }
            }
          
            return nodes;
        }

        /// <summary>
        /// 当前登录用户把自己具有的权限赋予给指定角色//用存储过程直接返回吧
        /// </summary>
        /// <param name="dbcontext"></param>
        /// <param name="loginuser">当前登录用户</param>
        /// <param name="role">需要权限的角色</param>
        /// <returns></returns>
        public static List<qytvNode> GetRoleNavigatios(ObjectContext dbcontext,bsUser loginuser, bsRole role)
        {
            if (loginuser == null || role == null)
                return null;
            List<qytvNode> nodes = new List<qytvNode>();

            List<tmpTreeNode> dbts = EntityManager_Static.GetAllByStorProcedure<tmpTreeNode>(dbcontext, "bslyRightsTreeRelUser2UserAndRole", new object[] { "RoleFun", loginuser.bsU_Id,role.bsR_Id });
            foreach(tmpTreeNode tn in dbts)
            {
                qytvNode qtn = new qytvNode();
                qtn.id = tn.id.ToString();
                qtn.name = tn.name;
                qtn.pId = tn.pId.ToString();
                qtn.type = tn.type;
                qtn.checkFlag=(bool)tn.checkflag;

                nodes.Add(qtn);
            }

            return nodes;
        }

        public static List<qytvNode> GetRoleTFs(ObjectContext dbcontext, bsUser loginuser, bsRole role)
        {
            List<qytvNode> nodes = new List<qytvNode>();
            List<tmpTreeNode> dbts = EntityManager_Static.GetAllByStorProcedure<tmpTreeNode>(dbcontext, "bslyRightsTreeRelUser2UserAndRole", new object[] { "RoleTF", loginuser.bsU_Id, role.bsR_Id });
            foreach (tmpTreeNode tn in dbts)
            {
                qytvNode qtn = new qytvNode();
                qtn.id = tn.id.ToString();
                qtn.name = tn.name;
                qtn.pId = tn.pId.ToString();
                qtn.type = tn.type;
                qtn.checkFlag = (bool)tn.checkflag;

                nodes.Add(qtn);
            }

            return nodes;
        }

        public static List<qytvNode> GetUserOrgs(ObjectContext dbcontext, bsUser loginuser, bsUser user)
        {
            List<qytvNode> nodes = new List<qytvNode>();
            List<tmpTreeNode> dbts = EntityManager_Static.GetAllByStorProcedure<tmpTreeNode>(dbcontext, "bslyRightsTreeRelUser2UserAndRole", new object[] { "UserOrg", loginuser.bsU_Id, user.bsU_Id });
            foreach (tmpTreeNode tn in dbts)
            {
                qytvNode qtn = new qytvNode();
                qtn.id = tn.id.ToString();
                qtn.name = tn.name;
                qtn.pId = tn.pId.ToString();
                qtn.type = tn.type;
                qtn.checkFlag = (bool)tn.checkflag;

                nodes.Add(qtn);
            }

            return nodes;
        }

        public static List<qytvNode> GetUserRoles(ObjectContext dbcontext, bsUser loginuser, bsUser user)
        {
            List<qytvNode> nodes = new List<qytvNode>();
            List<tmpTreeNode> dbts = EntityManager_Static.GetAllByStorProcedure<tmpTreeNode>(dbcontext, "bslyRightsTreeRelUser2UserAndRole", new object[] { "UserRole", loginuser.bsU_Id, user.bsU_Id });
            foreach (tmpTreeNode tn in dbts)
            {
                qytvNode qtn = new qytvNode();
                qtn.id = tn.id.ToString();
                qtn.name = tn.name;
                qtn.pId = tn.pId.ToString();
                qtn.type = tn.type;
                qtn.checkFlag = (bool)tn.checkflag;

                nodes.Add(qtn);
            }

            return nodes;
        }
    }
}
