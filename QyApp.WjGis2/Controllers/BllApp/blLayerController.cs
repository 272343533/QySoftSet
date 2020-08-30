using System.Web;
using System.Web.Mvc;
using QyTech.Core.ExController;
using QyTech.Core.BLL;
using QyTech.Core;
using QyTech.Core.Common;
using QyTech.Json;
using System.Collections;
using System.Collections.Generic;
using QyTech.Core.ExController.Bll;
using Dao.QyBllApp;

namespace QyExpress.Controllers.BllApp
{
    public class blLayerController : BllAppController
    {
        public override string getTreeDefault(string sessionid, string where, string orderby="DispNo")
        {
            List<blLayer> objs;
            
            objs = EManager_App.GetListNoPaging<blLayer>("bsS_Id='"+WebSiteParams.currSoftCustId +"'", orderby);

            var treelist = new List<qytvNode>();
            qytvNode treenode = new qytvNode();

            foreach (var cc in objs)
            {
                treenode = new qytvNode();
                treenode.id = cc.Id.ToString();
                treenode.name = cc.Name;
                treenode.pId = null;
                treenode.type ="";
                treenode.exInfo = cc.FullName ;
                treenode.addBtnFlag = false;
                treenode.removeBtnFlag = false;
                treenode.editBtnFlag = false;
                treenode.checkFlag = (bool)cc.LoadVisible;

                //if (objs.Count < 50)
                    treenode.open = true;
                treelist.Add(treenode);
            }
            return jsonMsgHelper.Create(0, treelist, "", treenode.GetType(), null);
        }


    }
}
