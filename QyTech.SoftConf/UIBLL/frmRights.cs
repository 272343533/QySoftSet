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
using QyTech.DbUtils;
using QyTech.SoftConf.BLL;
namespace QyTech.SoftConf.UIBLL
{

    /// <summary>
    /// 权限分类，包括角色导航功能权限，角色操作权限，角色表权限，角色字段权限，用户部门权限，用户角色权限
    /// 考虑：每个部门预置角色：管理员，浏览用户，这样用户可以不用管角色管理，直接给用户分配即可
    /// </summary>

    public partial class frmRights : Form
    {
        private bsUser loginUser_;

        private bsUser operUser_;
        private bsRole operRole_;
        private bsSoftCustInfo operCust_;
        RightType rightType_;

        List<qytvNode> allNodes;
        List<qytvNode> haveNodes;

        bsTable bstable_;

        public frmRights()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="operUser"></param>
        public frmRights(bsUser loginUser, bsUser oper, RightType rightType,bsTable bstable)
        {
            InitializeComponent();
            loginUser_ = loginUser;
            operUser_ = oper as bsUser;

            rightType_ = rightType;
            bstable_ = bstable;
        }
        public frmRights(bsUser loginUser, bsRole oper, RightType rightType, bsTable bstable)
        {
            InitializeComponent();
            loginUser_ = loginUser;
            operRole_ = oper;

            rightType_ = rightType;
            bstable_ = bstable;
        }
        public frmRights(bsUser loginUser, bsSoftCustInfo oper, RightType rightType, bsTable bstable)
        {
            InitializeComponent();
            loginUser_ = loginUser;
            operCust_ = oper;

            rightType_ = rightType;
            bstable_ = bstable;
        }
        //base(, null, GlobalVaribles.SqConn_Base, Guid.Parse("0bd9e80f-1e96-4672-b34c-1266adb55f86"), "")
        private void frmRoleFuns_Load(object sender, EventArgs e)
        {
            //用户具有的功能
            string initwhere = "",operwhere="";
            if (rightType_ == RightType.RoleNaviFuns)
            {
                initwhere = "";
                operwhere = "";
                allNodes = BLL.commService.GetNavigations(GlobalVaribles.ObjContext_Base, initwhere);
                haveNodes = BLL.commService.GetNavigations(GlobalVaribles.ObjContext_Base, operwhere);
            }
            else  if (rightType_ == RightType.RoleTFs)
            {
                //启用表，字段权限的表和字段
                initwhere = "";
                operwhere = "";
                allNodes = BLL.commService.GetTFRights(GlobalVaribles.ObjContext_Base, initwhere);
                haveNodes = BLL.commService.GetTFRights(GlobalVaribles.ObjContext_Base, operwhere);

            }
            else if (rightType_ == RightType.UserOrgs)
            {
                initwhere = "";
                operwhere = "";
                allNodes = BLL.commService.GetOrgs(GlobalVaribles.ObjContext_Base, initwhere);
                haveNodes = BLL.commService.GetOrgs(GlobalVaribles.ObjContext_Base, operwhere);

            }
            else if (rightType_ == RightType.UserRoles)
            {
                initwhere = "bsN_Id in (select bsN_Id from bsRolNaviRel where bsR_Id in (select bsR_Id from bsUserRoleRel where bsU_Id='" + loginUser_.bsU_Id.ToString() + "'))";
                operwhere = "bsN_Id in (select bsN_Id from bsRolNaviRel where bsR_Id in (select bsR_Id from bsUserRoleRel where bsU_Id='" + operUser_.bsU_Id.ToString() + "'))";
                allNodes = BLL.commService.GetRoles(GlobalVaribles.ObjContext_Base, initwhere);
                haveNodes = BLL.commService.GetRoles(GlobalVaribles.ObjContext_Base, operwhere);

            }

            qytvRights.LoadData(allNodes, true);

 
            qytvRights.RefreshChecked(haveNodes);
        }

        private void qyBtn_Ok_Click(object sender, EventArgs e)
        {
            List<qytvNode> SelectNodes = qytvRights.GetCheckedNode();

            List<qytvNode> delNodes = new List<qytvNode>();
            List<qytvNode> addNodes = new List<qytvNode>();
            //保存
            string delsql = "",insertsql="";
            #region 1.删除原来所有的，直接新增新的
            if (rightType_ == RightType.UserOrgs)
            {
                delsql= "delete from " + bstable_.TName + " where bsU_id='"+operUser_.bsU_Id.ToString() + "'";
           }
            QyTech.DbUtils.SqlUtils.ExceuteSql(GlobalVaribles.SqConn_Base, delsql);
            foreach (qytvNode tn in SelectNodes)
            {
                if (rightType_ == RightType.UserOrgs)
                {
                    insertsql = "insert into " + bstable_.TName + "(bsU_Id,bsO_Id) values('" + operUser_.bsU_Id.ToString() + "','" + tn.Id + "'）";
                }
                else if (rightType_ == RightType.UserRoles)
                {
                    insertsql = "insert into " + bstable_.TName + "(bsU_Id,bsR_Id) values('" + operUser_.bsU_Id.ToString() + "','" + tn.Id + "'）";
                }
                else if (rightType_ == RightType.RoleNaviFuns)
                {
                    insertsql = "insert into " + bstable_.TName + "(bsR_Id,bsN_Id) values('" + operRole_.bsR_Id.ToString() + "','" + tn.Id + "'）";
                }
                else if (rightType_ == RightType.RoleTFs)
                {
                    insertsql = "insert into " + bstable_.TName + "(bsR_Id,bsTF_Id,TF_Type) values('" + operRole_.bsR_Id.ToString() + "','" + tn.Id + "','"+tn.Tag+"'）";
                }
                QyTech.DbUtils.SqlUtils.ExceuteSql(GlobalVaribles.SqConn_Base, delsql);
            }
            #endregion
            #region 2.找到差异，删除没有的，增加新的 ,采用这个，安全些,但使用的是第一种，省事些
            //foreach(qytvNode node in SelectNodes)
            //{
            //    if (!haveNodes.Contains(node))
            //        addNodes.Add(node);
            //}
            //foreach (qytvNode node in haveNodes)
            //{
            //    if (!SelectNodes.Contains(node))
            //        delNodes.Add(node);
            //}

            //foreach(qytvNode tn in delNodes)
            //{
            //    //调用某个方法直接删除
            //    QyTech.DbUtils.SqlUtils.ExceuteSql(GlobalVaribles.SqConn_Base, "delete from " + bstable_.TName + " where " + bstable_.TPk + "='" + tn.Id+"'");

            //}
            //foreach (qytvNode tn in addNodes)
            //{
            //    //调用某个方法直接增加
            //}
            #endregion
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            qytvRights.NodeCheckedWithParent = checkBox1.Checked;
        }
    }
}
