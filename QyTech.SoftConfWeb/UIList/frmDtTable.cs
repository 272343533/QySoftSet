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
using QyTech.DbUtils;


namespace QyTech.SoftConf.UIList
{
    public partial class frmDtTable : qyfLayoutListWithLeft
    {
        public frmDtTable()
            :base(GlobalVaribles.ObjContext_Base, GlobalVaribles.ObjContext_App, GlobalVaribles.SqConn_Base, Guid.Parse("717519BD-7AAB-4445-8686-1033FAC8EBE7"), "bsD_Id in ( select bsD_Id from bsDb where AppName='" + GlobalVaribles.currAppObj.AppName+"')", "No")
        {
            InitializeComponent();
        }

        private void frmDtTable_Load(object sender, EventArgs e)
        {

            this.Text = bsFc.FunDesp;

            dgvList.CellClick += new DataGridViewCellEventHandler(dgvList_CellClick);

            List<qytvNode> nodes = BLL.commService.GetNotinBsTables(DB_Base);
            qytvDbTable.LoadData(nodes);
        }

        private void 导入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (qytvDbTable.SelectedNode!=null)
            {
                TreeNode tn = qytvDbTable.SelectedNode;
                qytvNode tntag = tn.Tag as qytvNode;
                if (tntag.Tag != "Db")
                {
                    TreeNode ptn = tn.Parent;
                    qytvNode ptntag = ptn.Tag as qytvNode;
                    //库id，表名，主键，描述，类型，是否重建
                    string sqls = "exec bsly1AppendCreatedTableInfoToBsTable '" + ptntag.Id + "','" + tn.Text + "','" + tn.Text + "',0";
                    QyTech.DbUtils.SqlUtils.ExceuteSql(GlobalVaribles.SqConn_Base,sqls);
                }
            }
        }


        private void 刷新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<qytvNode> nodes = BLL.commService.GetNotinBsTables(DB_Base);
            qytvDbTable.LoadData(nodes);
        }

        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1) return;
            CurrRowObj = SqlUtils.DataRow2EntityObject<bsTable>((dgvList.DataSource as DataTable).Rows[e.RowIndex]);
            
        }
    }
}
