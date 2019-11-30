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
using QyTech.Core.Helpers;


using System.Web.Configuration;

namespace QyExpress.Controllers.api
{
    public class bsSoftCustInfoController : apiController
    {




        /// <summary>
        /// 把当前用户的角色权限赋给用户
        /// </summary>
        /// <param name="idvalue"></param>
        /// <returns></returns>
        public string AssignRightCustomerNavigatios(string idvalue)
        {
            List<qytvNode> nodes = new List<qytvNode>();
            //nodes = BLL.commService.GetNavigations(GlobalVaribles.ObjContext_Base);
            bsSoftCustInfo sci = EntityManager_Static.GetByPk<bsSoftCustInfo>(DbContext, "bsS_Id", Guid.Parse(idvalue));
            List<bsNavigation> dbts = EntityManager_Static.GetListNoPaging<bsNavigation>(DbContext, "AppName='" + sci.AppName + "' or  (AppName='系统配置' and NAccount=1)", "NaviNo");// WebConfigurationManager.AppSettings["currAppName"].ToString()

            //用户具有的角色具有的功能
            List<bsSoftRelFuns> dbts_sub = EntityManager_Static.GetListNoPaging<bsSoftRelFuns>(DbContext, "bsS_Id='" + idvalue.ToString() +"'", "");// WebConfigurationManager.AppSettings["currSoftCustId"].ToString()  + "'", "");
            List<Guid> subs = new List<Guid>();
            for (int i = 0; i < dbts_sub.Count; i++)
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

            return jsonMsgHelper.Create(0, nodes, "");
        }
    }
}
