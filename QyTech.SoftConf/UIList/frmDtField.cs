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
    public partial class frmDtField : qyfLayoutListWithLeft
    {

        public Guid PFk;

        public frmDtField():base(GlobalVaribles.EM_Base, GlobalVaribles.EM_Base, GlobalVaribles.SqConn_Base, new Guid("B7BF7641-CCDF-4726-B948-2F9F4B212A7F"), "bsT_Id in (select bsT_Id from bsTable where bsD_Name in (select DbName from bsDb where AppName='"+GlobalVaribles.currAppObj.AppName+"'", "FNo")
        {
            InitializeComponent();
        }

        private void frmDtField_Load(object sender, EventArgs e)
        {
            this.Text = bsFc.FunDesp;
            dgvList.AutoAddOperColumns = false;

            RefreshDgv(dgvList, "bsT_Id is null", "");
            dgvList.addDgvSaveButton();
            dgvList.addDgvDelButton();

            List < qytvNode > nodes = BLL.commService.GetbsTables();

            qytvDbTable.LoadData(nodes);
        }

        private void qyBtn_Refresh_Click(object sender, EventArgs e)
        {
            string sqlwhere = CreateWhere();
            RefreshDgv(dgvList,sqlwhere);
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
            if (tntag.Tag != "Db")
            {
                PFk = tntag.Id;

                TreeNode ptn = tn.Parent;
                qytvNode ptntag = ptn.Tag as qytvNode;

                strBaseWhere = "bsT_Id='" + tntag.Id + "'";
                if (txtName.Text.Trim()!="")
                    RefreshDgv(dgvList, CreateWhere());
                else
                    RefreshDgv(dgvList, "");
             }

        }


        private void tsbDelNoValidField_Click(object sender, EventArgs e)
        {
            if (-1==GlobalVaribles.EM_Base.ExecuteSql("exec bslybsFieldForDeleteNotValid '"+ PFk.ToString()+"'"))
            {
                MessageBox.Show("删除失败");
            }

        }

        private void tsbAddNewField_Click(object sender, EventArgs e)
        {
            GlobalVaribles.EM_Base.ExecuteSql("exec bslybsFieldForAddNew '" + PFk.ToString() + "'");

        }

        private void dgvList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (base.qyDgvList_CellClick(sender, e)==-1)
                return;

            if (dgvList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "删除")
            {
                DeleteRow(e.RowIndex);
            }
            else if (dgvList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "查看/编辑")
            {
                CurrRowObj = SqlUtils.DataRow2EntityObject<bsField>((dgvList.DataSource as DataTable).Rows[e.RowIndex]);
                EditRow(CurrRowObj);
            }
            else if (dgvList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "保存")
            {
                SaveRow(e.RowIndex);
            }
        }
    }
}
