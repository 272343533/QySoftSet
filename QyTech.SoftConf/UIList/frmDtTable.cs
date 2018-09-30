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
        public frmDtTable():base(GlobalVaribles.EM_Base, GlobalVaribles.EM_Base, GlobalVaribles.SqConn_Base, Guid.Parse("717519BD-7AAB-4445-8686-1033FAC8EBE7"), "bsD_Id in ( select bsD_Id from bsDb where AppName='" + GlobalVaribles.currAppObj.AppName+"')", "No")
        {
            InitializeComponent();
        }

        private void frmDtTable_Load(object sender, EventArgs e)
        {
            this.Text = bsFc.FunDesp;

            RefreshDgv(dgvList,"", "");

            LoadTreeUnTable();
        }


        private void LoadTreeUnTable()
        {
            List<tmpTreeNode> dbts = GlobalVaribles.EM_Base.GetAllByStorProcedure<tmpTreeNode>("splyGetDbAndTable", new object[] { GlobalVaribles.currAppObj.AppName, 0 });
            List<qytvNode> nodes = new List<qytvNode>();
            foreach (tmpTreeNode s in dbts)
            {
                qytvNode n = new qytvNode();
                n.Id = s.Id;
                n.PId = (Guid)s.PId;
                n.Name = s.Name;
                n.Tag = s.Tag;
                nodes.Add(n);
            }

            qytvDbTable.Nodes.Clear();
            qytvDbTable.LoadData(nodes);
        }
        private void qyBtn_Refresh_Click(object sender, EventArgs e)
        {
            string sqlwhere = CreateWhere();
            RefreshDgv(dgvList, sqlwhere);
        }
        private string CreateWhere()
        {
            if (txtName.Text.Trim() != "")
                return "TName like '%" + txtName.Text.Trim() + "%'";
            else
                return "";
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
                    GlobalVaribles.EM_Base.ExecuteSql("exec bsly1AppendCreatedTableInfoToBsTable '" + ptntag.Id + "','" + tn.Text + "','" + tn.Text + "',0");
                }
            }
        }

        private void dgvList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void 刷新ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            LoadTreeUnTable();
        }

        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (base.qyDgvList_CellClick(sender, e) == -1)
                return;
            if (dgvList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "删除")
            {
                DeleteRow(e.RowIndex);
            }
            else if (dgvList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "查看/编辑")
            {
                CurrRowObj = SqlUtils.DataRow2EntityObject<bsTable>((dgvList.DataSource as DataTable).Rows[e.RowIndex]);
                EditRow(CurrRowObj);
            }
            else if (dgvList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "保存")
            {
                SaveRow(e.RowIndex);
            }
        }
    }
}
