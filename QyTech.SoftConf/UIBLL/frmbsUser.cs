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

namespace QyTech.SoftConf.UIBLL
{
    public partial class frmbsUser : QyTech.UICreate.qyfLayout.qyfLayListWithLeftOrg
    {
        public frmbsUser() : base(GlobalVaribles.ObjContext_Base, null, GlobalVaribles.SqConn_Base, Guid.Parse("0bd9e80f-1e96-4672-b34c-1266adb55f86"), "")
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
            bsTable bstable = EntityManager_Static.GetByPk<bsTable>(GlobalVaribles.ObjContext_Base_,"TName", "bsUserOrgRel");
            frmRights frmobj = new frmRights(GlobalVaribles.currloginUser,(CurrRowObj as bsUser),BLL.RightType.UserOrgs,bstable);
            frmobj.ShowDialog();
        }
        private void tsbUserRoles_Click(object sender, EventArgs e)
        {
            bsTable bstable = EntityManager_Static.GetByPk<bsTable>(GlobalVaribles.ObjContext_Base_, "TName", "bsUserRoleRel");
            frmRights frmobj = new frmRights(GlobalVaribles.currloginUser, (CurrRowObj as bsUser), BLL.RightType.UserRoles, bstable);
            frmobj.ShowDialog();
        }

    }
}
