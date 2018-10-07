using System.Web;
using System.Web.Mvc;
using QyTech.Core.ExController;
using QyTech.Auth.Dao;
using QyTech.Core.BLL;
using QyTech.Core;
using QyTech.Core.Common;
using QyTech.Json;
using System.Collections;
using System.Collections.Generic;

namespace QyTech.Auth.Controllers.api
{
    public class bsOrganizeController:  AuthController
    {


        public override string GetAll(string fields, string where, string orderby)
        {
            return TreeDis(where, orderby);
        }

        public override string TreeDis(string where, string orderby)
        {
            List<bsOrganize> objs;
            if (LoginUser.bsU_Id == devAdmin.Id)
            {
                objs = EManager.GetListNoPaging<bsOrganize>("", "Code");
            }
            else
            {
                bsUser userObj = LoginUser;
                //objs=LoginUser.bsOrganize1.GetEnumerator();
                //objs = userObj.bsOrganize1.Where(u => u.OrgType == "公司" || u.OrgType == "部门" || u.OrgType == "供热站").OrderBy(u => u.Code).ToList<bsOrganize>();
            }
            objs = EManager.GetListNoPaging<bsOrganize>("", "Code");

            var treelist = new List<TreeNode>();
            TreeNode treenode = new TreeNode();
                
            foreach (var cc in objs)
            {

                treenode = new TreeNode();
                treenode.id = cc.bsO_Id.ToString();
                treenode.name = cc.Name;
                treenode.pId = cc.PId.ToString();
                treenode.type = cc.OrgType;
                treenode.addBtnFlag = false;
                treenode.removeBtnFlag = false;
                treenode.editBtnFlag = false;


                if (objs.Count < 50)
                    treenode.openFlag = true;
                else if (cc.OrgType == "公司")
                    treenode.openFlag = true;
                else
                    treenode.openFlag = false;

                treelist.Add(treenode);
            }
            return jsonMsgHelper.Create(0, treelist, "", treenode.GetType(),null);
        }
    }
}
