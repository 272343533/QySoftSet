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
using QyExpress.Dao;
using QyTech.Core.BLL;
using QyTech.Json;
using QyTech.Core.Common;

using QyTech.SoftConf.UIDb;

namespace QyTech.SoftConf
{
    public partial class frmMain : Form
    {
        System.Data.Objects.ObjectContext DB_Base = GlobalVaribles.ObjContext_Base;

        Dictionary<string, Form> forms = new Dictionary<string, Form>();
        frmApp frmappobj;
  
        public frmMain()
        {
            InitializeComponent();
            GlobalVaribles.mdiform = this;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

            #region 启动 登录
            //WindowState = FormWindowState.Minimized;
            //ShowInTaskbar = false;
            //Hide();
            System.Threading.Thread thrlogin = new System.Threading.Thread(new System.Threading.ThreadStart(LoginValid));
            thrlogin.Start();
            #endregion

            #region 登录需要做的与权限无关的工作
            //创建所有窗体对象
            forms.Add("frmApp", frmappobj);
            #endregion


            #region 全局工作完成后
            GlobalVaribles.MainFormLoadFinished = true;
            ShowInTaskbar = true;
            while (GlobalVaribles.LoginStatus !=LoginStatus.Success)
            {
                if (GlobalVaribles.LoginStatus == LoginStatus.Exit)
                    this.Close();
                System.Threading.Thread.Sleep(1000);
            }
             #endregion

            #region 登录成功后的操作，与登录权限相关的
            this.WindowState = FormWindowState.Maximized;
            if (!InnerAccout.IsInnerAccount(GlobalVaribles.currloginUser))
            {
                panLeft.Width = 0;
                导航视图ToolStripMenuItem.Visible = false;

                应用程序ToolStripMenuItem.Visible = false;


                数据字典ToolStripMenuItem.Visible = false;


                功能维护ToolStripMenuItem.Visible = false;

                部门与权限ToolStripMenuItem.Visible = true;

                资源ToolStripMenuItem.Visible = false;

                初始化系统ToolStripMenuItem.Visible = false;
            
            }
            else
            {
                if (InnerAccout.expressAdminUser.bsU_Id == GlobalVaribles.currloginUser.bsU_Id)
                {
                    应用管理ToolStripMenuItem.Visible = true;
                    区域ToolStripMenuItem.Visible = true;
                    导航ToolStripMenuItem.Visible = false;
                    软件客户ToolStripMenuItem.Visible = false;
                    操作日志查询ToolStripMenuItem.Visible = false;
                    数据表日志查询ToolStripMenuItem.Visible = false;

                    数据库科ToolStripMenuItem.Visible = true;
                    数据表ToolStripMenuItem.Visible = true;
                    数据字段ToolStripMenuItem.Visible = true;
                    数据接口ToolStripMenuItem.Visible = true;
                    导入导出列维护ToolStripMenuItem.Visible = false;

                    功能配置ToolStripMenuItem.Visible = true;
                    功能字段ToolStripMenuItem.Visible = true;
                    功能查询ToolStripMenuItem.Visible = true;
                    功能接口ToolStripMenuItem.Visible = true;
                    功能操作ToolStripMenuItem.Visible = true;

                    部门与权限ToolStripMenuItem.Visible = false;

                    资源ToolStripMenuItem.Visible = true;

                    初始化系统ToolStripMenuItem.Visible = false;

                }
                else
                {
                    应用管理ToolStripMenuItem.Visible = true;
                    区域ToolStripMenuItem.Visible = true;
                    导航ToolStripMenuItem.Visible = true;
                    软件客户ToolStripMenuItem.Visible = true;
                    操作日志查询ToolStripMenuItem.Visible = true;
                    数据表日志查询ToolStripMenuItem.Visible = true;

                    数据库科ToolStripMenuItem.Visible = true;
                    数据表ToolStripMenuItem.Visible = true;
                    数据字段ToolStripMenuItem.Visible = true;
                    数据接口ToolStripMenuItem.Visible = true;
                    导入导出列维护ToolStripMenuItem.Visible = true;

                    功能配置ToolStripMenuItem.Visible = true;
                    功能字段ToolStripMenuItem.Visible = true;
                    功能查询ToolStripMenuItem.Visible = true;
                    功能接口ToolStripMenuItem.Visible = true;
                    功能操作ToolStripMenuItem.Visible = true;

                    部门与权限ToolStripMenuItem.Visible = true;

                    资源ToolStripMenuItem.Visible = true;

                    初始化系统ToolStripMenuItem.Visible = true;
                }

            }

            this.Text += "("+GlobalVaribles.currloginUser.NickName+")";

            刷新ToolStripMenuItem_Click(刷新ToolStripMenuItem, null);

            #endregion
        }
        private void LoginValid()
        {
            if (GlobalVaribles.currloginUser == null)
            {
                frmLogin frmlogin = new frmLogin(DB_Base);
                frmlogin.ShowDialog();
            }
        }
        private void 数据表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GlobalVaribles.currAppObj == null)
            {
                MessageBox.Show("首先选择应用");
                return;
            }

            frmDtTable frmdttable = new frmDtTable();
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
            if ((tn.Tag as qytvNode).type == "appname")
            {
                GlobalVaribles.currAppObj = EntityManager_Static.GetByPk<QyExpress.Dao.bsAppName>(DB_Base, "AppName", tn.Text);
                GlobalVaribles.currSoftCutomer = null;
            }
            else if((tn.Tag as qytvNode).type == "customer")
            {
                GlobalVaribles.currAppObj = EntityManager_Static.GetByPk<QyExpress.Dao.bsAppName>(DB_Base, "AppName", tn.Parent.Text);
                GlobalVaribles.currSoftCutomer = EntityManager_Static.GetByPk<QyExpress.Dao.bsSoftCustInfo>(DB_Base, "bsS_Id", (tn.Tag as qytvNode).id);
            }
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
            //if (GlobalVaribles.currAppObj == null)
            //{
            //    MessageBox.Show("首先选择应用");
            //    return;
            //}

            //frmFunInterface obj = new frmFunInterface();
            //obj.MdiParent = this;
            //obj.Show();
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
            //设置窗体集合，保证只有一个窗体，然后可以使用单例和工厂模式结合完成这个功能，显示a，其它的都hide？
            //增加观察者模式，可以完成当应用修改时，其它都进行刷新。
            if (frmappobj == null || frmappobj.IsDisposed)
            {
                frmappobj = new frmApp();
                frmappobj.MdiParent = this;
                frmappobj.Show();
            }
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

        private void 软件客户ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmSoftCustInfo frmobj = new frmSoftCustInfo();
            frmobj.MdiParent = this;
            frmobj.Show();
        }

        private void 区域ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmArea frmobj = new frmArea();
            frmobj.MdiParent = this;
            frmobj.Show();
        }

        private void 数据库科ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDb frmobj = new frmDb();
            frmobj.MdiParent = this;
            frmobj.Show();
        }

        private void 导入导出列维护ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmExcelColMap frm = new frmExcelColMap();
            frm.MdiParent = this;
            frm.Show();
        }

        private void 软件客户ToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            frmSoftCustInfo frm = new frmSoftCustInfo();
            frm.MdiParent = this;
            frm.Show();
        }

        private void 组织机构ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GlobalVaribles.currSoftCutomer == null)
            {
                MessageBox.Show("请先选择软件客户");
                return;
            }
            UIBLL.frmbsOrg frm = new UIBLL.frmbsOrg();
            frm.MdiParent = this;
            frm.Show();
        }

        private void 角色管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GlobalVaribles.currSoftCutomer == null)
            {
                MessageBox.Show("请先选择软件客户");
                return;
            }
            UIBLL.frmbsRole frm = new UIBLL.frmbsRole();
            frm.MdiParent = this;
            frm.Show();
        }

        private void 用户管理ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (GlobalVaribles.currSoftCutomer == null)
            {
                MessageBox.Show("请先选择软件客户");
                return;
            }
            UIBLL.frmbsUser frm = new UIBLL.frmbsUser();
            frm.MdiParent = this;
            frm.Show();
        }

        private void 数据接口ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDtInterface frm = new frmDtInterface();
            frm.MdiParent = this;
            frm.Show();
        }

        private void 初始化系统ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("恢复到空项目状态！");//应该使用存储过程删除所有数据库中无效的数据
        }

        private void 刷新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (InnerAccout.IsInnerAccount(GlobalVaribles.currloginUser))
            {
                ////////刷新应用树
                //////string url = GlobalVaribles.ServerUrl + "/softconf/bsAppName/getall";
                //////QyJsonData qyjson = QyTech.FormRequest.HttpRequest.GetRemoteJsonQy(url);
                //////List<bsAppName> apps = JsonHelper.DeserializeJsonToList<bsAppName>(qyjson.data.ToString());
                ////////json数据转对象处理
                GlobalVaribles.currloginUserFilter = new InnerAccountFilter(GlobalVaribles.currloginUser);
                List<qytvNode> nodes = BLL.commService.GetAppCustomers(DB_Base, GlobalVaribles.currloginUserFilter.App);
                qytvAppName.LoadData(nodes);
                if (qytvAppName.Nodes.Count > 0)
                {
                    if (qytvAppName.Nodes[0].Nodes.Count > 0)
                    {
                        qytvAppName.SelectedNode = qytvAppName.Nodes[0].Nodes[0];
                    }
                }
            }
            else
            {
                //bsSoftCustInfo sci= GlobalVaribles.currloginUser.bsOrganize.bsSoftCustInfo; //目前的对象是序列化出来的，不能.bsOrganize.bsSoftCustInfo
                //根据bsU_Id 获取到所属的bsSoftCustInfo对象和app对象
                bsSoftCustInfo sci = EntityManager_Static.GetBySql<bsSoftCustInfo>(DB_Base, " bsS_Id= (select bsS_Id from bsOrganize where bsO_Id='" + GlobalVaribles.currloginUser.bsO_Id.ToString() + "')");
                GlobalVaribles.currAppObj = EntityManager_Static.GetByPk<bsAppName>(DB_Base, "AppName", sci.AppName);
                GlobalVaribles.currSoftCutomer = sci;
            }
        }

        
    }
}
