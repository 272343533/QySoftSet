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
using QyTech.SkinForm.Controls;
using QyTech.Core.BLL;
using System.Data.Objects;
using System.Data.SqlClient;

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
            LoadTreeData();
        }

        protected void LoadTreeData(string where="")
        {
            List<qytvNode> nodes = new List<qytvNode>();
            try
            {
                List<bsOrganize> dbns = EntityManager_Static.GetListNoPaging<bsOrganize>(DB_Base, where, "");
                foreach (bsOrganize s in dbns)
                {
                    qytvNode n = new qytvNode();
                    n.Id = s.bsO_Id.ToString();
                    if (s.PId == null)
                        n.PId = Guid.Empty.ToString();
                    else
                        n.PId = s.PId.ToString();
                    n.Name = s.Name + "-1";
                    n.Tag = s.bsoAttr;
                    nodes.Add(n);
                }
            }
            catch { }
            qytvLeftOrg.LoadData(nodes);
        }



        private void qytvLeftOrg_AfterSelect(object sender, TreeViewEventArgs e)
        {
            currLeftFPk = (e.Node.Tag as qytvNode).Id;
            CurrbsO_Name = e.Node.Text;
            RefreshDgv(dgvList, "bsO_Id='" + currLeftFPk + "'");
        }
    }
}
