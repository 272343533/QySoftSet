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
            :base(GlobalVaribles.ObjContext_Base, GlobalVaribles.ObjContext_App, GlobalVaribles.SqConn_Base,
                 Guid.Parse("376FEC88-7431-4358-AF33-123597BEDD65"), BLL.commService.NavigationWhere)//, "NaviNo")
        {
            InitializeComponent();
        }


        private void frmNaviagtion_Load(object sender, EventArgs e)
        {
            this.Text = bsFc.FunDesp;

            qytvDbTable.AllowDrop = true;
            refreshTree();
        }

        private void qytvDbTable_AfterSelect(object sender, TreeViewEventArgs e)
        {
            qytvNode tntag = e.Node.Tag as qytvNode;

            strBaseWhere = "bsN_Id='" + tntag.Id + "' or pId='"+tntag.Id+"'";
            CurrLeftPFk = tntag.Id;


            RefreshDgv();
         }
        private void qytvDbTable_eventDragDroped(TreeNode tn, TreeNode ptn)
        {
            //完成后台的保存操作
            qytvNode tnobj = tn.Tag as qytvNode;
            qytvNode ptnobj = ptn.Tag as qytvNode;

            bsNavigation dbobj = EntityManager_Static.GetByPk<bsNavigation>(DB_Base, "bsN_Id", tnobj.Id);
            dbobj.pId = Guid.Parse(ptnobj.Id);
            EntityManager_Static.Modify<bsNavigation>(DB_Base, dbobj);
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

        private void tsbRefreshTree_Click(object sender, EventArgs e)
        {
            refreshTree();
        }
        private void refreshTree()
        {
            List<qytvNode> nodes = BLL.commService.GetNavigations(DB_Base, strBaseWhere);
            qytvDbTable.LoadData(nodes);
        }

        private void tsbInitFunConf_Click(object sender, EventArgs e)
        {
            string sqls = "exec bslyUpdateNavi2FunConf '" + currRowTPkId.ToString() + "'";


            int ret = QyTech.DbUtils.SqlUtils.ExceuteSql(GlobalVaribles.SqConn_Base, sqls);
            if (ret == -1)
            {
                MessageBox.Show("初始化失败");
            }
        }
    }
}
