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
    public partial class frmNaviagtion : qyfLayoutListWithLeft
    {
        public frmNaviagtion():base(GlobalVaribles.EM_Base, GlobalVaribles.EM_Base, GlobalVaribles.SqConn_Base, Guid.Parse("376FEC88-7431-4358-AF33-123597BEDD65"), "bsA_Id='"+GlobalVaribles.currAppObj.AppId.ToString()+"'", "NaviNo")
        {
            InitializeComponent();
        }

        private void frmNaviagtion_Load(object sender, EventArgs e)
        {
            this.Text = bsFc.FunDesp;

            qytvDbTable.AllowDrop = true;
             List<qytvNode> nodes = BLL.commService.GetNavigations(strBaseWhere);
            qytvDbTable.LoadData(nodes);


            RefreshDgv(dgvList,"", "");
        }

        private void qyBtn_Refresh_Click(object sender, EventArgs e)
        {
            strWhere = CreateWhere();
            RefreshDgv(dgvList, strWhere);
        }

        private string CreateWhere()
        {
            if (txtName.Text.Trim() != "")
                return "NaviName like '%" + txtName.Text.Trim() + "%'";
            else
                return "";
        }

        private void qytvDbTable_AfterSelect(object sender, TreeViewEventArgs e)
        {

            TreeNode tn = e.Node;
            qytvNode tntag = tn.Tag as qytvNode;
            strBaseWhere = "bsN_Id='" + tntag.Id + "' or pId='"+tntag.Id+"'";

            strWhere = CreateWhere();
            RefreshDgv(dgvList,strWhere);
            ResetDgvHeader(dgvList);
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
                CurrRowObj = QyTech.DbUtils.SqlUtils.DataRow2EntityObject<bsNavigation>((dgvList.DataSource as DataTable).Rows[e.RowIndex]);
                EditRow(CurrRowObj);
            }
            else if (dgvList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "保存")
            {
                SaveRow(e.RowIndex);
            }


        }

        private void qytvDbTable_eventDragDroped(TreeNode tn, TreeNode ptn)
        {
            //完成后台的保存操作
            qytvNode tnobj = tn.Tag as qytvNode;
            qytvNode ptnobj= ptn.Tag as qytvNode;

            bsNavigation dbobj = GlobalVaribles.EM_Base.GetByPk<bsNavigation>("bsN_Id", tnobj.Id);
            dbobj.pId = ptnobj.Id;
            GlobalVaribles.EM_Base.Modify<bsNavigation>(dbobj);
        }

        private void dgvList_DataSourceChanged(object sender, EventArgs e)
        {

        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            bsNavigation navObj = new bsNavigation();
            navObj.bsN_Id = Guid.NewGuid();
            navObj.bsA_Id = GlobalVaribles.currAppObj.AppId;
            qyfAdd frm = new qyfAdd(AddOrEdit.Add, sqlConn, navObj, bstable, bffs);
            frm.ShowDialog();
        }

    }
}
