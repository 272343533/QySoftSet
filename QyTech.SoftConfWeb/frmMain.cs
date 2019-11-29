using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using QyTech.SoftConf.UIList;
using QyTech.SkinForm.Controls;
using QyTech.Auth.Dao;
using QyTech.Core.BLL;
namespace QyTech.SoftConf
{
    public partial class frmMain : Form
    {

        frmDtTable frmdttable;

        System.Data.Objects.ObjectContext DB_Base = GlobalVaribles.ObjContext_Base;


        public frmMain()
        {
            InitializeComponent();
            GlobalVaribles.mdiform = this;
        }




        private void frmMain_Load(object sender, EventArgs e)
        {
            //刷新应用树
            List<qytvNode> nodes = BLL.commService.GetAppNames(DB_Base, "");
            qytvAppName.LoadData(nodes);

            if (qytvAppName.Nodes.Count>0)
                qytvAppName.SelectedNode = qytvAppName.Nodes[0];
        }

        private void 数据表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GlobalVaribles.currAppObj == null)
            {
                MessageBox.Show("首先选择应用");
                return;
            }
        
                frmdttable = new frmDtTable();
                frmdttable.MdiParent = this;
       
            frmdttable.Show();
        }

        private void 数据字段ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GlobalVaribles.currAppObj == null)
            {
                MessageBox.Show("首先选择应用");
                return;
            }

            frmDtField obj = new frmDtField();
            obj.MdiParent = this;
            obj.Show();
        }

        private void qytvAppName_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode tn = e.Node;
            //qytvNode tntag = tn.Tag as qytvNode;
            GlobalVaribles.currAppObj = EntityManager.GetByPk<QyTech.Auth.Dao.bsAppName>(DB_Base, "AppName", tn.Text);

        }

        private void 功能配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GlobalVaribles.currAppObj == null)
            {
                MessageBox.Show("首先选择应用");
                return;
            }

            frmFunConf obj = new frmFunConf();
            obj.MdiParent = this;
            obj.Show();
        }

        private void 导航ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GlobalVaribles.currAppObj == null)
            {
                MessageBox.Show("首先选择应用");
                return;
            }

            frmNaviagtion obj = new frmNaviagtion();
            obj.MdiParent = this;
            obj.Show();
        }

        private void 功能字段ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GlobalVaribles.currAppObj == null)
            {
                MessageBox.Show("首先选择应用");
                return;
            }

            frmFunField obj = new frmFunField();
            obj.MdiParent = this;
            obj.Show();
        }

        private void 功能接口ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GlobalVaribles.currAppObj == null)
            {
                MessageBox.Show("首先选择应用");
                return;
            }

            frmFunInterface obj = new frmFunInterface();
            obj.MdiParent = this;
            obj.Show();
        }

        private void 功能操作ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GlobalVaribles.currAppObj == null)
            {
                MessageBox.Show("首先选择应用");
                return;
            }

            frmFunOper obj = new frmFunOper();
            obj.MdiParent = this;
            obj.Show();
            
        }

        private void 功能查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GlobalVaribles.currAppObj == null)
            {
                MessageBox.Show("首先选择应用");
                return;
            }

            frmFunQuery obj = new frmFunQuery();
            obj.MdiParent = this;
            obj.Show();
        }

        private void 应用管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmApp frmobj = new frmApp();
            frmobj.MdiParent = this;
            frmobj.Show();
        }

        private void 导航视图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            导航视图ToolStripMenuItem.Checked = !导航视图ToolStripMenuItem.Checked;
            if (导航视图ToolStripMenuItem.Checked)
            {
                panLeft.Width = 230;
            }
            else
                panLeft.Width = 0;
        }

        private void qytvAppName_DoubleClick(object sender, EventArgs e)
        {
            导航视图ToolStripMenuItem_Click(null, null);
        }
    }
}
