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
using QyTech.Core.BLL;
using QyTech.SkinForm.Controls;

namespace QyTech.SoftConf.UIList
{
    public partial class frmNaviagtion : qyfLayoutListWithLeft
    {
        public frmNaviagtion()
            :base(GlobalVaribles.ObjContext_Base, GlobalVaribles.ObjContext_App, GlobalVaribles.SqConn_Base, Guid.Parse("376FEC88-7431-4358-AF33-123597BEDD65"), "bsA_Id='"+GlobalVaribles.currAppObj.AppId.ToString()+"'", "NaviNo")
        {
            InitializeComponent();
        }


        private void frmNaviagtion_Load(object sender, EventArgs e)
        {
            this.Text = bsFc.FunDesp;

            qytvDbTable.AllowDrop = true;
             List<qytvNode> nodes = BLL.commService.GetNavigations(DB_Base, strBaseWhere);
            qytvDbTable.LoadData(nodes);

        }

        private void qytvDbTable_AfterSelect(object sender, TreeViewEventArgs e)
        {
            qytvNode tntag = e.Node.Tag as qytvNode;

            strBaseWhere = "bsN_Id='" + tntag.Id + "' or pId='"+tntag.Id+"'";
            CurrLeftPFk = tntag.Id;


            RefreshDgv();
         }
        
        private void dgvList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1) return;
            CurrRowObj = QyTech.DbUtils.SqlUtils.DataRow2EntityObject<bsNavigation>((dgvList.DataSource as DataTable).Rows[e.RowIndex]);
        }

        private void qytvDbTable_eventDragDroped(TreeNode tn, TreeNode ptn)
        {
            //完成后台的保存操作
            qytvNode tnobj = tn.Tag as qytvNode;
            qytvNode ptnobj= ptn.Tag as qytvNode;

            bsNavigation dbobj = EntityManager.GetByPk<bsNavigation>(DB_Base, "bsN_Id", tnobj.Id);
            dbobj.pId = ptnobj.Id;
            EntityManager.Modify<bsNavigation>(DB_Base, dbobj);
        }


        private void tsbAdd_Click(object sender, EventArgs e)
        {
            bsNavigation navObj = new bsNavigation();
            navObj.bsN_Id = Guid.NewGuid();
            navObj.bsA_Id = GlobalVaribles.currAppObj.AppId;
            navObj.pId = Guid.Parse(CurrLeftPFk.ToString());
            qyfAdd frm = new qyfAdd(AddOrEdit.Add, sqlConn, navObj, bstable, bffs_byFormNo);
            frm.ShowDialog();
        }

    }
}
