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
using QyTech.Core.BLL;
using QyTech.DbUtils;

namespace QyTech.SoftConf.UIBLL
{
    public partial class frmbsOrg : qyfLayTreeEdit
    {
      
        public frmbsOrg() : base(GlobalVaribles.ObjContext_Base, null, GlobalVaribles.SqConn_Base, Guid.Parse("6c6e2474-0b65-49fa-8dd9-3c30140eb07b"), "")
        {
            InitializeComponent();


        }
        private void frmbsOrg_Load(object sender, EventArgs e)
        {
            this.Text = bsFc.FunDesp;
            //加载左侧数据
            List<qytvNode> nodes = BLL.commService.GetOrgs(DB_Base, strBaseWhere);
            qytvLeft.LoadData(nodes);

            qytvLeft.SetSelectNode(qytvLeft.Nodes[0].Text);
            //选择一个默认对象
            
            //加载右侧编辑内容
            //父类负责加载
        }
    }
}
