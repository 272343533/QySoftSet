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
using QyTech.DbUtils;

namespace QyTech.SoftConf.UIList
{
    public partial class frmApp
        : qyfLayoutList
    {
        public frmApp()
            :base(GlobalVaribles.ObjContext_Base, GlobalVaribles.ObjContext_App, GlobalVaribles.SqConn_Base,Guid.Parse("589590eb-38a9-422f-a0ff-abc71284d3ab"),"","")
        {
            InitializeComponent();
        }

        private void frmApp_Load(object sender, EventArgs e)
        {
            this.Text = bsFc.FunDesp;

            dgvList.CellClick += new DataGridViewCellEventHandler(dgvList_CellClick);
        }

        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1) return;
            CurrRowObj = SqlUtils.DataRow2EntityObject<bsTable>((dgvList.DataSource as DataTable).Rows[e.RowIndex]);

        }

    }
}
