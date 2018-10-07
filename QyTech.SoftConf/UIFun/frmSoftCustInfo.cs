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


namespace QyTech.SoftConf.UIList
{
    public partial class frmSoftCustInfo : qyfLayListWithLeftTree
    {
        public frmSoftCustInfo()
            :base(GlobalVaribles.ObjContext_Base, GlobalVaribles.ObjContext_App, GlobalVaribles.SqConn_Base
                 ,Guid.Parse("DBAA027D-2057-4F1B-B4FB-300EF90EB04F"), BLL.commService.AppNameWhere)
        {
            InitializeComponent();
        }
        private void frm_Load(object sender, EventArgs e)
        {
            this.Text = bsFc.FunDesp;

            ToolStripButton tsbAdd = AddtsbButton("新增");
            tsbAdd.Click += new System.EventHandler(this.tsbAdd_Click);
            ToolStripButton tsbCustOrgs = AddtsbButton("应用权限");
            tsbCustOrgs.Click += new System.EventHandler(this.tsbCustOrgs_Click);


            List<qytvNode> nodes = BLL.commService.GetAppNames(DB_Base, "");
            qytvLeft.LoadData(nodes);
            qytvLeft.SetSelectNode("系统配置");
        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            bsSoftCustInfo objforadd = new bsSoftCustInfo();
            objforadd.bsS_Id = Guid.NewGuid();
            objforadd.AppName =currLeftText;
  
            qyfAdd frm = new qyfAdd(AddOrEdit.Add, sqlConn, objforadd, bstable, bffs_byFormNo);
            frm.ShowDialog();
        }
        private void tsbCustOrgs_Click(object sender, EventArgs e)
        {
            bsTable bstable = EntityManager_Static.GetByPk<bsTable>(GlobalVaribles.ObjContext_Base_, "TName", "bsSoftRelFuns");
            UIBLL.frmRights frmobj = new UIBLL.frmRights(GlobalVaribles.currloginUser, (CurrRowObj as bsSoftCustInfo), BLL.RightType.CustFuns, bstable);
            frmobj.ShowDialog();
        }
    }
}
