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

namespace QyTech.SoftConf.UIList
{
    public partial class frmFunConf : qyfLayoutListWithLeft
    {
        public frmFunConf():base(GlobalVaribles.EM_Base, GlobalVaribles.EM_Base, GlobalVaribles.SqConn_Base, Guid.Parse("595C5BE8-7F0B-42C3-B707-E47E6A3B870E"), "bsN_Id in (select bsN_Id from bsNavigation where bsA_Id='"+GlobalVaribles.currAppObj.AppId.ToString()+"')", "FunCode")
        {
            InitializeComponent();
        }

        private void frmFunConf_Load(object sender, EventArgs e)
        {
            this.Text = bsFc.FunDesp;


            List<qytvNode> nodes = BLL.commService.GetNavigations(strBaseWhere);
            qytvDbTable.LoadData(nodes);


            RefreshDgv(dgvList, "bsN_Id is  null", "");
        }

        private void qyBtn_Refresh_Click(object sender, EventArgs e)
        {
            string sqlwhere = CreateWhere();
            RefreshDgv(dgvList, sqlwhere);
        }

        private string CreateWhere()
        {
            if (txtName.Text.Trim() != "")
                return "FunDesp like '%" + txtName.Text.Trim() + "%'";
            else
                return "";
        }

        private void qytvDbTable_AfterSelect(object sender, TreeViewEventArgs e)
        {

            TreeNode tn = e.Node;
            qytvNode tntag = tn.Tag as qytvNode;

            strBaseWhere = "bsN_Id='" + tntag.Id + "'";

            string sqlwhere = CreateWhere();
            RefreshDgv(dgvList, sqlwhere);
        }
       

        private void dgvList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (base.qyDgvList_CellClick(sender, e) == -1)
                return;
            if (dgvList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "删除")
            {
                DeleteRow(e.RowIndex);
            }
            else if (dgvList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "查看/编辑")
            {
                CurrRowObj = QyTech.DbUtils.SqlUtils.DataRow2EntityObject<bsFunConf>((dgvList.DataSource as DataTable).Rows[e.RowIndex]);
                EditRow(CurrRowObj);
            }
            else if (dgvList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "保存")
            {
                SaveRow(e.RowIndex);
            }
        }
    }
}
