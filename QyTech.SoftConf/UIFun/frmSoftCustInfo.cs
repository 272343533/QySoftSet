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
using QyTech.SoftConf;
using QyTech.SkinForm.Controls;
using QyTech.Core.BLL;
using QyTech.Core.Common;


namespace QyTech.SoftConf.UIList
{
    public partial class frmSoftCustInfo : qyfLayListWithLeftTree
    {
        public frmSoftCustInfo()
            :base(GlobalVaribles.ObjContext_Base, GlobalVaribles.ObjContext_App, GlobalVaribles.SqConn_Base
                 ,Guid.Parse("DBAA027D-2057-4F1B-B4FB-300EF90EB04F"), GlobalVaribles.currloginUserFilter.Cust)
        {
            InitializeComponent();
        }
        private void frm_Load(object sender, EventArgs e)
        {
            this.Text = bsFc.FunDesp;

            List<qytvNode> nodes = BLL.commService.GetAppNames(DB_Base, "");
            qytvLeft.LoadData(nodes);
            qytvLeft.SetSelectNode("系统配置");



            ToolStripButton tsbAdd = AddtsbButton("新增");
            tsbAdd.Click += new System.EventHandler(this.tsbAdd_Click);

            ToolStripButton tsbCustIni = AddtsbButton("角色账号初始化");
            tsbCustIni.Click += new System.EventHandler(this.tsbCustInit_Click);

            //下面连个合起来角色权限就够了
            ToolStripButton tsbCustNavs = AddtsbButton("功能权限");//需要同时把权限给系统角色
            tsbCustNavs.Click += new System.EventHandler(this.tsbCustNavs_Click);
            ToolStripButton tsbCustRoleRigh = AddtsbButton("获取角色权限");//需要同时把权限给系统角色
            tsbCustRoleRigh.Click += new System.EventHandler(this.tsbCustRoleRight_Click);

            ///就差部门权限，可是部门是自己建立的，所以不应该在这里，应该是建立的时候本来就有，管理员账号的部门单独取所有，不再保存具体的部门

            if (qytvLeft.Nodes.Count > 0)
                qytvLeft.SelectedNode = qytvLeft.Nodes[0];
        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            bsSoftCustInfo objforadd = new bsSoftCustInfo();
            objforadd.bsS_Id = Guid.NewGuid();
            objforadd.AppName =currLeftText;
  
            qyfAdd frm = new qyfAdd(AddOrEdit.Add, sqlConn, objforadd, bstable, bffs_byFormNo);
            frm.ShowDialog();
        }

        /// <summary>
        /// 初始化角色，账号及相应权限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbCustInit_Click(object sender, EventArgs e)
        {
            if (CurrRowObj == null)
            {
                MessageBox.Show("先选择客户");
                return;
            }
            string str = QyTech.DbUtils.SqlUtils.ExceuteSp(sqlConn, "bslyInitSoftCust", "'"+currRowTPkId.ToString() +"',7");
            MessageBox.Show(str);

        }

        /// <summary>
        /// 给客户管理员权限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbCustNavs_Click(object sender, EventArgs e)
        {
            if (CurrRowObj==null)
            {
                MessageBox.Show("先选择客户");
                return;
            }
            bsTable bstable = EntityManager_Static.GetByPk<bsTable>(DB_Base, "TName", "bsSoftRelFuns");
            UIBLL.frmRights frmobj = new UIBLL.frmRights(DB_Base, GlobalVaribles.currloginUser, (CurrRowObj as bsSoftCustInfo), RightType.CustFuns, bstable);
            frmobj.ShowDialog();
        }

        

        private void tsbCustRoleRight_Click(object sender, EventArgs e)
        {
            if (CurrRowObj == null)
            {
                MessageBox.Show("先选择客户");
                return;
            }
            string str = QyTech.DbUtils.SqlUtils.ExceuteSp(sqlConn, "bslyInitSoftCust", "'" + currRowTPkId.ToString() + "',2");
            MessageBox.Show(str);

        }
        
    }
}
