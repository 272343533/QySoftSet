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
using QyTech.DbUtils;

namespace QyTech.SoftConf.UIList
{
    public partial class frmApp
        : qyfLayList
    {
        public frmApp()
            :base(GlobalVaribles.ObjContext_Base, GlobalVaribles.ObjContext_App, GlobalVaribles.SqConn_Base,
                 Guid.Parse("589590eb-38a9-422f-a0ff-abc71284d3ab"), GlobalVaribles.currloginUserFilter.App)
        {
            InitializeComponent();
        }

        private void frmApp_Load(object sender, EventArgs e)
        {
            this.Text = bsFc.FunDesp;
            ToolStripButton tsbAdd = AddtsbButton("新增");
            tsbAdd.Click += new System.EventHandler(this.tsbAdd_Click);

            ToolStripButton tsbNavigatioin = AddtsbButton("导航");
            tsbNavigatioin.Click += new System.EventHandler(this.tsbNavigation_Click);

            RefreshDgv();
        }
        private void tsbAdd_Click(object sender, EventArgs e)
        {
            bsAppName objforadd = new bsAppName();
            objforadd.AppId = Guid.NewGuid();
            
            qyfAdd frm = new qyfAdd(AddOrEdit.Add, sqlConn, objforadd, bstable, bffs_byFormNo);
            frm.ShowDialog();
        }

        private void tsbNavigation_Click(object sender, EventArgs e)
        {
            MessageBox.Show("将系统配置下的页面加入到对应的项目中，暂时不选择，按照默认方式进行");

            string sql = "exec bslyInitAppNam '" + GlobalVaribles.currAppObj.AppName + "'";
            QyTech.DbUtils.SqlUtils.ExceuteSql(GlobalVaribles.SqConn_Base, sql);

        }
    }
}
