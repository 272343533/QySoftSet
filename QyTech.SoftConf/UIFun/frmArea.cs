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
    public partial class frmArea : qyfLayList
    {
        public frmArea()
            :base(GlobalVaribles.ObjContext_Base, GlobalVaribles.ObjContext_App, GlobalVaribles.SqConn_Base,
                 Guid.Parse("da7682c6-1cad-499d-9cbc-42dc79f62bb4"), GlobalVaribles.currloginUserFilter.Area)
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
            bsAreaRegistration objforadd = new bsAreaRegistration();
            objforadd.bsA_Id = Guid.NewGuid();
            objforadd.AppName = GlobalVaribles.currAppObj.AppName;

            qyfAdd frm = new qyfAdd(AddOrEdit.Add, sqlConn, objforadd, bstable, bffs_byFormNo);
            frm.ShowDialog();
        }
    }
}
