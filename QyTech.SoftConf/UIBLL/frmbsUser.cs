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
using QyTech.Core.Common;

using QyExpress.Dao;
using QyTech.SoftConf;
using QyTech.SkinForm.Controls;
using QyTech.Core.BLL;
using QyTech.Utils;

namespace QyTech.SoftConf.UIBLL
{
    public partial class frmbsUser : QyTech.UICreate.qyfLayout.qyfLayListWithLeftOrg
    {
        public frmbsUser() 
            : base(GlobalVaribles.ObjContext_Base, null, GlobalVaribles.SqConn_Base, Guid.Parse("0bd9e80f-1e96-4672-b34c-1266adb55f86"), "bsS_Id='" + GlobalVaribles.currSoftCutomer.bsS_Id.ToString() + "'")
        {
            InitializeComponent();
        }

        private void frm_Load(object sender, EventArgs e)
        {
          
            ToolStripButton tsbAdd = AddtsbButton("新增");
            tsbAdd.Click += new System.EventHandler(this.tsbAdd_Click);
            ToolStripButton tsbUserOrgs = AddtsbButton("部门权限");
            tsbUserOrgs.Click += new System.EventHandler(this.tsbUserOrgs_Click);
            ToolStripButton tsbUserRoles = AddtsbButton("用户角色");
            tsbUserRoles.Click += new System.EventHandler(this.tsbUserRoles_Click);

            ToolStripButton tsbUserPwds = AddtsbButton("密码密文化");
            tsbUserPwds.Click += new System.EventHandler(this.tsbUserPwdss_Click);

        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            bsUser objforadd = new bsUser();
            objforadd.bsU_Id = Guid.NewGuid();
            objforadd.bsO_Id = Guid.Parse(currLeftFPk.ToString());
            objforadd.bsO_Name = CurrbsO_Name;
            qyfAdd frm = new qyfAdd(AddOrEdit.Add, sqlConn, objforadd, bstable, bffs_byFormNo);
            frm.ShowDialog();
        }
        private void tsbUserOrgs_Click(object sender, EventArgs e)
        {
            if (CurrRowObj == null)
            {
                MessageBox.Show("请首先选择数据！");
                return;
            }
            //Dictionary<string, string> paras = new Dictionary<string, string>();
            //paras.Add("FName", "TName");
            //paras.Add("FValue", "bsUserOrgRel");
            //bsTable bstable = HttpRequestUtils.PostRemoteJsonQy<bsTable>("/api/bsTable/GetOneByFName", paras);
            bsTable bstable = EntityManager_Static.GetByPk<bsTable>(DB_Base, "TName", "bsUserOrgRel");

            frmRights frmobj = new frmRights(DB_Base, GlobalVaribles.currloginUser,(CurrRowObj as bsUser),RightType.UserOrgs,bstable);
            frmobj.ShowDialog();
        }
        private void tsbUserRoles_Click(object sender, EventArgs e)
        {
            if (CurrRowObj == null)
            {
                MessageBox.Show("请首先选择数据！");
                return;
            }
            //Dictionary<string, string> paras = new Dictionary<string, string>();
            //paras.Add("FName", "TName");
            //paras.Add("FValue", "bsUserRoleRel");
            //bsTable bstable = HttpRequestUtils.PostRemoteJsonQy<bsTable>("/api/bsTable/GetOneByFName", paras);
            bsTable bstable = EntityManager_Static.GetByPk<bsTable>(DB_Base, "TName", "bsUserRoleRel");

            frmRights frmobj = new frmRights(DB_Base, GlobalVaribles.currloginUser, (CurrRowObj as bsUser), RightType.UserRoles, bstable);
            frmobj.ShowDialog();
        }
        private void tsbUserPwdss_Click(object sender, EventArgs e)
        {
            try
            {
                List<bsUser> users = EntityManager_Static.GetListNoPaging<bsUser>(DB_Base, "isSysUser=0", "");
                foreach (bsUser user in users)
                {
                    user.LoginPwd = user.LoginName.Substring(user.LoginName.Length - 6, 6);
                    user.LoginPwd = QyTech.Core.Helpers.LockerHelper.MD5(user.LoginPwd);
                    EntityManager_Static.Modify<bsUser>(DB_Base, user);
                }
            }
            catch(Exception ex)
            {
                QyTech.Core.LogHelper.Error(ex);
                MessageBox.Show("出现错误！（"+ex.Message+")");
            }
        }
        
    }
}
