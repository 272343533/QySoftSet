using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QyTech.Auth.Dao;
using QyTech.SkinForm.Controls;

using log4net;


namespace QyTech.SkinForm.Component
{

    public partial class qyTvOrg : UserControl 
    {
        static ILog log = LogManager.GetLogger("qyOrg");

        public delegate void delAfterSelect(object sender, TreeViewEventArgs e);

        public event delAfterSelect AfterSelect;

        public delegate void delDel(bsOrganize org);
  
        public event delDel eventDel;
        public delegate void delSave(bsOrganize org);
        public event delSave eventSave;
        //private bool IfFlash_ = false;


        //public bool IfFlash {
        //    set { IfFlash_ = value; }
        //    get { return IfFlash_; }
        //}
        //public DataGridView DgvTT { get { return this.dgvTT; } }

        //private Point Position = new Point(0, 0);
        private List<bsOrganize> _Ts;
      
       private TreeNode currentSelectNode;

       private string NewName = "请修改";

        public qyTvOrg()
        {
            InitializeComponent();
        }
        public TreeView tvOrg{
            get { return this.qyTv; }
        
        }

        public TreeNode SelectedNode
        {
            get { return qyTv.SelectedNode; }
            set { qyTv.SelectedNode = value; }
        }
        public TreeNodeCollection Nodes
        {
            get { return qyTv.Nodes; }
        }
        private void tv_DragEnter(object sender, DragEventArgs e)
        {
            //设置拖放类别(复制，移动等)
            e.Effect = DragDropEffects.Copy;
        }

        private void tv_ItemDrag(object sender, ItemDragEventArgs e)
        {
            try
            {
                TreeNode tn = (TreeNode)e.Item;
                //if (CanDragTreeNode(tn))
                {
                    //启动拖放操作，设置拖放类型为Copy
                    qyTv.DoDragDrop(e.Item, DragDropEffects.Copy);
                }
            }
            catch (Exception ex)
            {
                log.Error("item drag:" + ex.Message);
            }
        }
        private void tv_DragDrop(object sender, DragEventArgs e)
        {

        }
        private bool CanDragTreeNode(TreeNode tn)
        {
            bool ret = false;

            try
            {
                if (tn != null && tn.Tag != null && tn.Tag is bsOrganize)
                {
                    ret = true;
                }
            }
            catch (Exception ex)
            {
                log.Error("CanDragTreeNodeg:" + ex.Message);
            }

            return ret;
        }

        public void RefreshTree(List<bsOrganize> Ts)
        {
            if (Ts == null)
                return;
            _Ts = Ts;
            
            if (_Ts.Count != 0)
            {
                qyTv.Nodes.Clear();
                if (_Ts.Count == 1)
                {
                    _Ts[0].PId = null;
                }
                AddRootTreeViewNodes();
            }
           // AddRootTreeViewNodes();
            qyTv.ExpandAll();

        }
     
        public void AddRootTreeViewNodes()
        {
            foreach (bsOrganize t in _Ts)
            {
                if ((t != null) && (t.PId == null))
                {
                    TreeNode newNode = new TreeNode(t.Name);
                    newNode.Checked = true;
                    newNode.Name = t.Name;
                    newNode.Tag = t;

                    qyTv.Nodes.Add(newNode);


                    AddChildTreeViewNodes(newNode);
                }

            }
        }
       
        private void AddChildTreeViewNodes(TreeNode parentTreeViewNode)
        {

            foreach (bsOrganize t in _Ts)
            {

                if (t.PId == ((bsOrganize)(parentTreeViewNode.Tag)).bsO_Id)
                {
                    TreeNode newNode = new TreeNode(t.Name);
                    newNode.Checked = true;
                    newNode.Name = t.Name;
                    newNode.Tag = t;

                    parentTreeViewNode.Nodes.Add(newNode);
                    AddChildTreeViewNodes(newNode);
                }
            }
        }
        private void tv_AfterCheck(object sender, TreeViewEventArgs e)
        {
            TreeNode tn = e.Node;
            foreach (TreeNode tnch in tn.Nodes)
            {
                tnch.Checked = tn.Checked;
            }
            this.currentSelectNode = e.Node;

            this.AfterSelect(sender, e);
        }

        private void tv_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //e.Node.Checked = true;
            this.currentSelectNode=e.Node;
            if (AfterSelect!=null)
                this.AfterSelect(sender, e);
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

            /*
            bsOrganize org=new bsOrganize();
            org.Name = NewName;
            if (currentSelectNode == null)
                org.PId = null;
            else
                org.PId = (currentSelectNode.Tag as bsOrganize).id;
            TreeNode tn = new TreeNode(NewName);
            tn.Tag = org;

            if (currentSelectNode == null)
            {
                qyTv.Nodes.Add(tn);
                qyTv.ExpandAll();
            }
            else
            {
                currentSelectNode.Nodes.Add(tn);
                currentSelectNode.Expand();
            }
            qyTv.LabelEdit =true;
         
            */
        }

        public void RemoveCurrentNode()
        {
            currentSelectNode.Remove();
        }
      

    

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            foreach (TreeNode tn in qyTv.Nodes)
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
