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
using QyTech.DbUtils;
using QyTech.Json;

using QyTech.Core.Common;
using System.Data.Objects;
using QyTech.Utils;

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

        ObjectContext DB_Base;

        public frmRights()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="operUser"></param>
        public frmRights(ObjectContext dbcontext, bsUser loginUser, bsUser oper, RightType rightType,bsTable bstable)
        {
            InitializeComponent();
            DB_Base = dbcontext;
            loginUser_ = loginUser;
            operUser_ = oper as bsUser;

            rightType_ = rightType;
            bstable_ = bstable;
        }
        public frmRights(ObjectContext dbcontext, bsUser loginUser, bsRole oper, RightType rightType, bsTable bstable)
        {
            InitializeComponent();
            DB_Base = dbcontext;
            loginUser_ = loginUser;
            operRole_ = oper;

            rightType_ = rightType;
            bstable_ = bstable;
        }
        public frmRights(ObjectContext dbcontext,bsUser loginUser, bsSoftCustInfo cust, RightType rightType, bsTable bstable)
        {
            InitializeComponent();
            DB_Base = dbcontext;
            loginUser_ = loginUser;
            operCust_ = cust;

            rightType_ = rightType;
            bstable_ = bstable;
            this.Text = "客户功能";
        }
        //base(, null, GlobalVaribles.SqConn_Base, Guid.Parse("0bd9e80f-1e96-4672-b34c-1266adb55f86"), "")
        private void frmRoleFuns_Load(object sender, EventArgs e)
        {
            string url="";
            Dictionary<string, string> dicparas = new Dictionary<string, string>();
            if (rightType_ == RightType.RoleNaviFuns)
            {
                url = GlobalVaribles.ServerUrl + "/api/bsRole/AssignRightsRoleNavigatios";
                dicparas.Add("idValue", operRole_.bsR_Id.ToString());
                //allNodes = BLL.userService.GetRoleNavigatios(DB_Base,loginUser_, operRole_);
            }
            else  if (rightType_ == RightType.RoleTFs)
            {
                //启用表，字段权限的表和字段
                url = GlobalVaribles.ServerUrl + "/api/bsRole/AssignRightsRoleDataTFs";
                dicparas.Add("idValue", operRole_.bsR_Id.ToString());
                //allNodes = BLL.userService.GetRoleTFs(DB_Base, loginUser_, operRole_);
            }
            else if (rightType_ == RightType.UserOrgs)
            {
                url = GlobalVaribles.ServerUrl + "/api/bsUser/AssignRightsUserOrgs";
                dicparas.Add("idValue", operUser_.bsU_Id.ToString());
                //allNodes = BLL.userService.GetUserOrgs(DB_Base, loginUser_, operUser_);

            }
            else if (rightType_ == RightType.UserRoles)
            {
                url = GlobalVaribles.ServerUrl + "/api/bsUser/AssignRightsUserRoles";
                dicparas.Add("idValue", operUser_.bsU_Id.ToString());
                //allNodes = BLL.userService.GetUserRoles(DB_Base, loginUser_, operUser_);
            }
            else if (rightType_ == RightType.CustFuns)
            {
                url = GlobalVaribles.ServerUrl + "/api/bsSoftCustInfo/AssignRightCustomerNavigatios";
                dicparas.Add("idValue", operCust_.bsS_Id.ToString());
                //allNodes = BLL.userService.GetCustNavigatios(DB_Base,operCust_);
            }

            QyJsonData jsonData = HttpRequestUtils.PostRemoteJsonQy(url, dicparas);
            allNodes = JsonHelper.DeserializeJsonToList<qytvNode>(jsonData.data.ToString());
            qytvRights.LoadData(allNodes, true);

            haveNodes=qytvRights.GetCheckedNode();
            if (haveNodes!=null && haveNodes.Count>0)
                qytvRights.RefreshChecked(haveNodes);
        }

        private void qyBtn_Ok_Click(object sender, EventArgs e)
        {
            List<qytvNode> SelectNodes = qytvRights.GetCheckedNode();

            List<qytvNode> delNodes = new List<qytvNode>();
            List<qytvNode> addNodes = new List<qytvNode>();
            //保存
            string insertsql="";
            string delNodeIds = "",addNodeIds="";
             
            foreach (qytvNode node in SelectNodes)
            {
                if (!haveNodes.Contains(node))
                {
                    addNodes.Add(node);
                    addNodeIds += ","+node.id.ToString();
                }
            }
            foreach (qytvNode node in haveNodes)
            {
                if (!SelectNodes.Contains(node))
                {
                    delNodes.Add(node);
                    delNodeIds += "," + node.id.ToString();
                }
            }
            if (addNodes.Count==0 && delNodes.Count==0)
            {
                MessageBox.Show("没有改变!");
                return;
            }
            string url = "";
            Dictionary<string, string> dicparas = new Dictionary<string, string>();
            #region 1.删除原来所有的，直接新增新的
            //#region 2.找到差异，删除没有的，增加新的 ,采用这个，安全些,但使用的是第一种，省事些
            if (delNodes.Count > 0)
            {
                delNodeIds = delNodeIds.Substring(1);
                if (rightType_ == RightType.UserOrgs)
                {
                    url = GlobalVaribles.ServerUrl + "/api/bsUserOrgRel/DeleteByWhereSql";
                    dicparas.Add("wheresql", "bsU_id = '" + operUser_.bsU_Id.ToString() + "' and bsO_Id in ('"+ delNodeIds.Replace(",","','")+"')");
                }
                else if (rightType_ == RightType.UserRoles)
                {
                    url = GlobalVaribles.ServerUrl + "/api/bsUserRoleRel/DeleteByWhereSql";
                    dicparas.Add("wheresql", "bsU_id = '" + operUser_.bsU_Id.ToString() + "' and bsR_Id in ('" + delNodeIds.Replace(",", "','") + "')");
                }
                else if (rightType_ == RightType.RoleTFs)
                {
                    url = GlobalVaribles.ServerUrl + "/api/bsRoleTFDataRel/DeleteByWhereSql";
                    dicparas.Add("wheresql", "bsR_id = '" + operRole_.bsR_Id.ToString() + "' and bsTF_Id in ('" + delNodeIds.Replace(",", "','") + "')");
                }
                else if (rightType_ == RightType.RoleNaviFuns)
                {
                    url = GlobalVaribles.ServerUrl + "/api/bsRoleNaviRel/DeleteByWhereSql";
                    dicparas.Add("wheresql", "bsR_id = '" + operRole_.bsR_Id.ToString() + "' and bsN_Id in ('" + delNodeIds.Replace(",", "','") + "')");
                }
                else if (rightType_ == RightType.CustFuns)
                {
                    url = GlobalVaribles.ServerUrl + "/api/bsSoftRelFuns/DeleteByWhereSql";
                    dicparas.Add("wheresql", "bsS_id = '" + operCust_.bsS_Id.ToString() + "' and bsN_Id in ('" + delNodeIds.Replace(",", "','") + "')");
                }
                QyJsonData jsonData = HttpRequestUtils.PostRemoteJsonQy(url, dicparas);
                if (jsonData.code==1)
                {
                    MessageBox.Show(jsonData.msg);
                }
            }

            List<bsUserOrgRel> userorgs = new List<bsUserOrgRel>();
            string json="";
            if (addNodes.Count > 0)
            {
                addNodeIds = addNodeIds.Substring(1);
                foreach (qytvNode tn in SelectNodes)
                {
                    bsUserOrgRel userorg = new bsUserOrgRel();
                    if (rightType_ == RightType.UserOrgs)
                    {
                        insertsql = "insert into " + bstable_.TName + "(bsU_Id,bsO_Id) values('" + operUser_.bsU_Id.ToString() + "','" + tn.id + "')";

                        //userorg.Id = Guid.NewGuid();
                        //userorg.bsU_Id = operUser_.bsU_Id;
                        //userorg.bsO_Id = Guid.Parse(tn.id);
                        //json = JsonHelper.SerializeObject(userorg);
                        //url = GlobalVaribles.ServerUrl + "/api/bsSoftRelFuns/Add";
                    }
                    else if (rightType_ == RightType.UserRoles)
                    {
                        insertsql = "insert into " + bstable_.TName + "(bsU_Id,bsR_Id) values('" + operUser_.bsU_Id.ToString() + "','" + tn.id + "')";
                    }
                    else if (rightType_ == RightType.RoleNaviFuns)
                    {
                        insertsql = "insert into " + bstable_.TName + "(bsR_Id,bsN_Id) values('" + operRole_.bsR_Id.ToString() + "','" + tn.id + "')";
                    }
                    else if (rightType_ == RightType.RoleTFs)
                    {
                        insertsql = "insert into " + bstable_.TName + "(bsR_Id,bsTF_Id,TF_Type) values('" + operRole_.bsR_Id.ToString() + "','" + tn.id + "','" + tn.type + "')";
                    }
                    else if (rightType_ == RightType.CustFuns)
                    {
                        insertsql = "insert into " + bstable_.TName + "(Id,bsS_Id,bsN_Id) values('" + Guid.NewGuid().ToString() + "','" + operCust_.bsS_Id.ToString() + "','" + tn.id + "')";
                    }

                    //dicparas.Clear();
                    //dicparas.Add("strjson", json);
                    //QyJsonData jsonData = HttpRequestUtils.PostRemoteJsonQy(url, dicparas);
                    //if (jsonData.code == 1)
                    //{
                    //    MessageBox.Show(jsonData.msg);
                    //}

                    QyTech.DbUtils.SqlUtils.ExceuteSql(GlobalVaribles.SqConn_Base, insertsql);
                }
            }
            #endregion

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            qytvRights.NodeCheckedWithParent = checkBox1.Checked;
        }

        private void btnCheckAll_Click(object sender, EventArgs e)
        {
            qytvRights.SetAllNodeCheckStatus();
        }

        private void btnReverse_Click(object sender, EventArgs e)
        {
            qytvRights.CheckReverseNode();
        }
    }
}
