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
    public partial class frmbsRole : QyTech.UICreate.qyfLayout.qyfLayListWithLeftOrg
    {
        public frmbsRole() : base(GlobalVaribles.ObjContext_Base, null, GlobalVaribles.SqConn_Base, Guid.Parse("2b26f396-03c6-4b33-a7c7-210a9ddad1b5"), "")
        {
            InitializeComponent();
            ToolStripButton tsbAdd = AddtsbButton("新增");
            tsbAdd.Click += new System.EventHandler(this.tsbAdd_Click);

            ToolStripButton tsbRoleFun = AddtsbButton("角色功能");
            tsbRoleFun.Click += new System.EventHandler(this.tsbRoleFun_Click);

            ToolStripButton tsbRoleOper = AddtsbButton("角色操作");//默认具有所有操作
            tsbRoleOper.Click += new System.EventHandler(this.tsbRoleFun_Click);

            ToolStripButton tsbRoleTF = AddtsbButton("角色数据");
            tsbRoleTF.Click += new System.EventHandler(this.tsbRoleTF_Click);

        }

        private void frmbsRole_Load(object sender, EventArgs e)
        {

        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            bsRole objforadd = new bsRole();
            objforadd.bsR_Id = Guid.NewGuid();
            objforadd.bsO_Id = Guid.Parse(currLeftFPk.ToString());
            qyfAdd frm = new qyfAdd(AddOrEdit.Add, sqlConn, objforadd, bstable, bffs_byFormNo);
            frm.ShowDialog();
        }
        private void tsbRoleFun_Click(object sender, EventArgs e)
        {
            bsTable bstable = EntityManager_Static.GetByPk<bsTable>(GlobalVaribles.ObjContext_Base_, "TName", "bsRolNaviRel");
            frmRights frmobj = new frmRights(GlobalVaribles.currloginUser, (CurrRowObj as bsRole), BLL.RightType.RoleNaviFuns, bstable);
            frmobj.ShowDialog();
        }
        private void tsbRoleTF_Click(object sender, EventArgs e)
        {
            bsTable bstable = EntityManager_Static.GetByPk<bsTable>(GlobalVaribles.ObjContext_Base_, "TName", "bsRolNaviRel");
            frmRights frmobj = new frmRights(GlobalVaribles.currloginUser, (CurrRowObj as bsRole), BLL.RightType.RoleTFs, bstable);
            frmobj.ShowDialog();

        }

    }
}
