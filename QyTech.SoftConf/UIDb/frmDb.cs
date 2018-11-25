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

namespace QyTech.SoftConf.UIList
{
    public partial class frmDb : qyfLayList
    {
        public frmDb()
            :base(GlobalVaribles.ObjContext_Base,GlobalVaribles.ObjContext_App, GlobalVaribles.SqConn_Base,
                 Guid.Parse("AA207EA0-D06F-449A-A3B3-D78EBFD044DE"), GlobalVaribles.currloginUserFilter.Db)
        {
            InitializeComponent();
        }
        private void frm_Load(object sender, EventArgs e)
        {
            this.Text = bsFc.FunDesp;

            ToolStripButton tsbAdd = AddtsbButton("新增");
            tsbAdd.Click += new System.EventHandler(this.tsbAdd_Click);
            RefreshDgv();
        }
        private void tsbAdd_Click(object sender, EventArgs e)
        {
            bsDb objforadd = new bsDb();
            objforadd.bsD_Id = Guid.NewGuid();
            objforadd.AppName = GlobalVaribles.currAppObj.AppName;

            qyfAdd frm = new qyfAdd(AddOrEdit.Add, sqlConn, objforadd, bstable, bffs_byFormNo);
            frm.ShowDialog();
        }
    }
}
