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


namespace QyExpress.Controllers.api
{
    public class bsRoleController : apiController
    {

        /// <summary>
        /// 把当前用户的角色导航权限付给角色
        /// </summary>
        /// <param name="idvalue"></param>
        /// <returns></returns>
        public string AssignRightsRoleNavigatios(string sessionid, string idvalue)
        {
            List<qytvNode> nodes = new List<qytvNode>();

            List<tmpTreeNode> dbts = EntityManager_Static.GetAllByStorProcedure<tmpTreeNode>(DbContext, "bslyRightsTreeRelUser2UserAndRole", new object[] { "RoleFun", LoginUser.bsU_Id, Guid.Parse(idvalue) });
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

            return jsonMsgHelper.Create(0, nodes, "");
        }


        /// <summary>
        /// 把当前用户的数据权限权限付给角色
        /// </summary>
        /// <param name="idvalue"></param>
        /// <returns></returns>
        public string AssignRightsRoleDataTFs(string sessionid, string idvalue)
        {
            List<qytvNode> nodes = new List<qytvNode>();
            List<tmpTreeNode> dbts = EntityManager_Static.GetAllByStorProcedure<tmpTreeNode>(DbContext, "bslyRightsTreeRelUser2UserAndRole", new object[] { "RoleTF", LoginUser.bsU_Id, Guid.Parse(idvalue) });
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

            return jsonMsgHelper.Create(0, nodes, "");
        }


    }
}
