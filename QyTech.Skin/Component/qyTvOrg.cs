using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using QyTech.SkinForm.Controls;

using log4net;


namespace QyTech.SkinForm.Component
{

    public partial class qyTvOrg : qyTreeView 
    {
        static ILog log = LogManager.GetLogger("qyOrg");

        public delegate void delAfterSelect(object sender, TreeViewEventArgs e);
        public delegate void delDel(bsOrganize org);
        public delegate void delSave(bsOrganize org);



        public event delDel eventDel;
        public event delSave eventSave;


        private List<bsOrganize> _Ts;
      
       private TreeNode currentSelectNode;

       private string NewName = "请修改";

        public qyTvOrg():base()
        {
           
        }
        
     
        private void tv_AfterCheck(object sender, TreeViewEventArgs e)
        {
            TreeNode tn = e.Node;
            foreach (TreeNode tnch in tn.Nodes)
            {
                tnch.Checked = tn.Checked;
            }
            this.currentSelectNode = e.Node;

        }


        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            foreach (TreeNode tn in Nodes)
            {
                SaveTree(tn);
            }
        
        }

        private void SaveTree(TreeNode ptn)
        {
            if (ptn.Checked && ptn.Tag != null)
            {
                bsOrganize org = (ptn.Tag as bsOrganize);
                if (ptn.Text != org.Name)
                {
                    org.Name = ptn.Text;
                }
                this.eventSave(org);
            }

            foreach (TreeNode tn in ptn.Nodes)
            {
                if (tn.Checked && tn.Tag != null)
                {
                    bsOrganize org = (tn.Tag as bsOrganize);
                    if (tn.Text != org.Name)
                    {
                        org.Name = tn.Text;
                    }
                    this.eventSave(org);
                }
               
               SaveTree(tn);

            }
        }

        private void tv_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            bsOrganize org = e.Node.Tag as bsOrganize;
            if (org.PId==null&& org.Name!=NewName)
                e.CancelEdit = true;
        }

     
    }

}
