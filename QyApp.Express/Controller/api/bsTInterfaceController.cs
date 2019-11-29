using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QyTech.Json;
using QyTech.Core.Common;
using QyTech.Core.CommUtils;

namespace QyExpress.Controllers.SoftConf
{
    public class bsTInterfaceController : apiController
    {

        public override string getTreeDefault(string sessionid, string where, string orderby)
        {
            List<qytvNode> nodes = qyDefalutInterface.GetSysPreDefineTInterface();
            List<qytvNode> checknodes=base.getTreeNodes(where, orderby);

            nodes=qyTreeViewUtil.RefreshCheckedByName(nodes, checknodes);
            return jsonMsgHelper.Create(0, nodes, "", typeof(qytvNode), null);
        }
    }
}