using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QyTech.Core.Common;
using qyExpress.Dao;
using System.Web.Configuration;
using QyTech.Core.BLL;
using QyTech.Core.ExController.Bll;

namespace QyTech.BLL
{
    public class bllbsNavigation
    {

        public List<qytvNode> GetNaviNodes(EntityManager EM,bsUser loginuser)
        {
            string where = "";
            if (InnerAccout.IsInnerAccount(loginuser))//内部账号
            {
                InnerAccountFilter iafitler = new InnerAccountFilter(loginuser, WebConfigurationManager.AppSettings["currappName"]);
                where = iafitler.Navi;
            }
            else
            {
                if (InnerAccout.IssysAdmin(loginuser))//系统管理员
                {
                    where = " AppName='" + WebSiteParams.currAppName + "' or  (AppName='系统配置' and NAccount=1)";
                }
                else
                {
                    //普通用户的权限获取，从数据库中读出来
                    //用户->角色-》导航功能
                    where = " bsN_Id in (select bsN_Id from bsRoleNaviRel where bsR_Id in (select bsR_Id from bsUserRelRole where bsU_Id='" + loginuser.bsU_Id.ToString() + "'))";
                }
            }
            List<bsNavigation> dbts = EM.GetListNoPaging<bsNavigation>(where + " and NaviStatus='正常'", "NaviNo");
            List<qytvNode> nodes = new List<qytvNode>();

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
                            if (s.pId == Guid.Parse("D2E65E66-B320-46B0-AF6E-8BD1F5B50774"))//系统配置菜单，需将对应子项内容放到应用菜单中
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
                        nodes.Add(n);
                    }
                    catch { }
                }
            }
            return nodes;
        }
    }
}