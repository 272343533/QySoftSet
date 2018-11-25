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
using QyTech.SkinForm.Controls;
using QyTech.Core.BLL;
using System.Data.Objects;
using System.Data.SqlClient;
using QyTech.Core.Common;


namespace QyTech.UICreate.qyfLayout
{
    public partial class qyfLayListWithLeftOrg : qyfLayListParent
    {

        protected string CurrbsO_Name;

        public qyfLayListWithLeftOrg()
        {
            InitializeComponent();
        }

        public qyfLayListWithLeftOrg(ObjectContext db_Base, ObjectContext db_App, SqlConnection conn, Guid bsFC_Id, string where = "")
           : base(db_Base, db_App, conn, bsFC_Id, where)
        {
            InitializeComponent();
        }

        private void qyfLayListWithLeftOrg_Load(object sender, EventArgs e)
        {
            LoadTreeData(strBaseWhere);
            if (qytvLeftOrg.Nodes.Count > 0)
                qytvLeftOrg.SelectedNode = qytvLeftOrg.Nodes[0];
        }

        protected void LoadTreeData(string where)
        {
            List<qytvNode> nodes = new List<qytvNode>();
            try
            {
                List<bsOrganize> dbns = EntityManager_Static.GetListNoPaging<bsOrganize>(DB_Base, where, "");
                foreach (bsOrganize s in dbns)
                {
                    qytvNode n = new qytvNode();
                    n.id = s.bsO_Id.ToString();
                    if (s.PId == null)
                        n.pId = Guid.Empty.ToString();
                    else
                        n.pId = s.PId.ToString();
                    n.name = s.Name + "-1";
                    n.type = s.bsoAttr;
                    nodes.Add(n);
                }
            }
            catch { }
            qytvLeftOrg.LoadData(nodes);
        }



        private void qytvLeftOrg_AfterSelect(object sender, TreeViewEventArgs e)
        {
            currLeftFPk = (e.Node.Tag as qytvNode).id;
            CurrbsO_Name = e.Node.Text;
            RefreshDgv(dgvList, "bsO_Id='" + currLeftFPk + "'");
        }
    }
}
