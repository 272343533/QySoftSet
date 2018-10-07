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
    public partial class frmDtInterface : qyfLayListWithLeftTree
    {
        public frmDtInterface()
            :base(GlobalVaribles.ObjContext_Base, GlobalVaribles.ObjContext_App, GlobalVaribles.SqConn_Base
                 ,Guid.Parse("7582bbab-0733-4a73-b551-ae705f1f9f36"), BLL.commService.AppNameWhere)
        {
            InitializeComponent();
        }
        private void frm_Load(object sender, EventArgs e)
        {
            this.Text = bsFc.FunDesp;

            ToolStripButton tsbAdd = AddtsbButton("新增");
            tsbAdd.Click += new System.EventHandler(this.tsbAdd_Click);
            ToolStripButton tsbAddDefault = AddtsbButton("默认所有");
            tsbAddDefault.Click += new System.EventHandler(this.tsbAddDefault_Click);


            List<qytvNode> nodes = BLL.commService.GetbsTables(DB_Base);
            qytvLeft.LoadData(nodes);
        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            bsTInterface objforadd = new bsTInterface();
            objforadd.bsTI_Id = Guid.NewGuid();
            objforadd.bsT_Id =Guid.Parse(currLeftFPk.ToString());
  
            qyfAdd frm = new qyfAdd(AddOrEdit.Add, sqlConn, objforadd, bstable, bffs_byFormNo);
            frm.ShowDialog();
        }
        private void tsbAddDefault_Click(object sender, EventArgs e)
        {
            //存储过程加入，
            //bsTable bstable = EntityManager_Static.GetByPk<bsTable>(GlobalVaribles.ObjContext_Base_, "TName", "bsSoftRelFuns");
            //UIBLL.frmRights frmobj = new UIBLL.frmRights(GlobalVaribles.currloginUser, (CurrRowObj as bsSoftCustInfo), BLL.RightType.CustFuns, bstable);
            //frmobj.ShowDialog();
        }
    }
}
