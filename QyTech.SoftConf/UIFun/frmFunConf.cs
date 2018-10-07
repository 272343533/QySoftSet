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
    public partial class frmFunConf : qyfLayListParent
    {
        public frmFunConf()
            :base(GlobalVaribles.ObjContext_Base, GlobalVaribles.ObjContext_App, GlobalVaribles.SqConn_Base,
                 Guid.Parse("595C5BE8-7F0B-42C3-B707-E47E6A3B870E"), BLL.commService.NavigationWhere)//, "FunCode")
        {
            InitializeComponent();

            
        }

        private void frmFunConf_Load(object sender, EventArgs e)
        {
            this.Text = bsFc.FunDesp;


            List<qytvNode> nodes = BLL.commService.GetNavigations(DB_Base, strBaseWhere);
            qytvDbTable.LoadData(nodes);



        }

        /// <summary>
        /// 有过有自定义的qyadd，需要重载下面的方法
        /// </summary>
        protected override void CreateAddForm()
        {
                subfrmAdd = new UIAdd.qyfFunConfAdd();
        }


        private void qytvDbTable_AfterSelect(object sender, TreeViewEventArgs e)
        {

            TreeNode tn = e.Node;
            qytvNode tntag = tn.Tag as qytvNode;

            strBaseWhere = "bsN_Id='" + tntag.Id + "'";

            RefreshDgv();
        }
       
    }
}
