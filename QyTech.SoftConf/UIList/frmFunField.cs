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
    public partial class frmFunField : qyfLayoutListWithLeft
    {

        public frmFunField() : base(GlobalVaribles.EM_Base, GlobalVaribles.EM_Base, GlobalVaribles.SqConn_Base, Guid.Parse("F98AD688-66F8-439D-A6E1-66B81640721D"), "bsFC_Id in (select bsFC_Id from bsFunConf where bsN_Id in (select bsN_Id from bsNavigation where bsA_Id='" + GlobalVaribles.currAppObj.AppId.ToString() + "'))", "FNo")
        {
            InitializeComponent();

        }

        private void frmFunField_Load(object sender, EventArgs e)
        {
            this.Text = bsFc.FunDesp;

             RefreshDgv(dgvList, "bsFC_Id is null", "");
            dgvList.addDgvSaveButton();
            dgvList.CellClick += new DataGridViewCellEventHandler(dgvList_CellClick);

            List<qytvNode> nodes = BLL.commService.GetFunConfs(strBaseWhere);
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
                return "FName like '%" + txtName.Text.Trim() + "%'";
            else
                return "";
        }

        private void qytvDbTable_AfterSelect(object sender, TreeViewEventArgs e)
        {

            TreeNode tn = e.Node;
            qytvNode tntag = tn.Tag as qytvNode;

            strBaseWhere = "bsFC_Id='" + tntag.Id + "'";
            CurrLeftPFk = tntag.Id;

            string sqlwhere = CreateWhere();
            RefreshDgv(dgvList, sqlwhere);
        }


        private void tsbDels_Click(object sender, EventArgs e)
        {
            base.DeleteRows();
        }


        private void tsbSetQueryField_Click(object sender, EventArgs e)
        {
            GlobalVaribles.EM_Base.ExecuteSql("exec bslyAddFunFieldToFunQuery '" + currRowTPkId.ToString() + "'");
        }

        private void tsbAddNewField_Click(object sender, EventArgs e)
        {
            GlobalVaribles.EM_Base.ExecuteSql("exec bslyAppendFunAllFields2FCField '" + CurrLeftPFk.ToString() + "'");
        }


        private void ChangeOrder(string ListOrEdit,int rowindex, string upordown)
        {
            int ToRow = rowindex;
            bsFunField RowObj1;
            bsFunField RowObj2;
            Guid toRowTpkId;// urrRowTPkId;
            if (upordown == "up")
            {
                if (rowindex == 0)
                    return;
                ToRow = rowindex - 1;
           }
            else
            {
                if (rowindex == dtList.Rows.Count-1)
                    return;
                ToRow = rowindex + 1;
            }
            toRowTpkId = Guid.Parse(dgvList.Rows[ToRow].Cells["bsFF_Id"].Value.ToString());//["NoInList"]
            RowObj1 = EM_Base.GetByPk<bsFunField>("bsFF_Id", currRowTPkId);
            RowObj2 = EM_Base.GetByPk<bsFunField>("bsFF_Id", toRowTpkId);

            if (ListOrEdit == "list")
            {
                int tmp = (int)RowObj1.NoInList;
                RowObj1.NoInList = RowObj2.NoInList;
                RowObj2.NoInList = tmp;
            }
            else
            {
                int tmp = (int)RowObj1.NoInForm;
                RowObj1.NoInForm = RowObj2.NoInForm;
                RowObj2.NoInForm = tmp;
            }
            EM_Base.Modify<bsFunField>(RowObj1);
            EM_Base.Modify<bsFunField>(RowObj2);

        }

        private void tsmiListDown_Click(object sender, EventArgs e)
        {
            ChangeOrder("list", currRowIndex, "down");

        }




        private void tsmiListLeft_Click(object sender, EventArgs e)
        {
            ChangeOrder("list", currRowIndex, "up");

        }

        private void tsmiListRight_Click(object sender, EventArgs e)
        {
            ChangeOrder("list", currRowIndex, "down");

        }

        private void tsmiEditUp_Click(object sender, EventArgs e)
        {
            ChangeOrder("edit", currRowIndex, "up");

        }

        private void tsmiEditDown_Click(object sender, EventArgs e)
        {
            ChangeOrder("edit", currRowIndex, "down");

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
                CurrRowObj = QyTech.DbUtils.SqlUtils.DataRow2EntityObject<bsFunField>((dgvList.DataSource as DataTable).Rows[e.RowIndex]);
                EditRow(CurrRowObj);
            }
            else if (dgvList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "保存")
            {
                SaveRow(e.RowIndex);
            }
        }
    }
}
