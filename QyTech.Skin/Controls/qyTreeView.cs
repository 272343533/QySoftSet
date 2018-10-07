using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
namespace QyTech.SkinForm.Controls
{
    public class qyTreeView : TreeView
    {

        public delegate void delDragHandler(TreeNode tn, TreeNode ptn);
        public event delDragHandler eventDragDroped;

        private TreeNode preSelectTreeNode;
        private List<qytvNode> _Ts;


        private Point Position = new Point(0, 0);


        private bool NodeCheckedWithParent_ = false;
        /// <summary>
        /// 设置父子节点的复选框是否联动
        /// </summary>
        public bool NodeCheckedWithParent
        {
            get { return NodeCheckedWithParent_; }
            set { NodeCheckedWithParent_ = value; }
        }

        public qyTreeView():base()
        {
            Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

            AfterSelect += new TreeViewEventHandler(QyTreeView_AfterSelect);

            ItemDrag += new ItemDragEventHandler(tv_ItemDrag);
            DragDrop += new DragEventHandler(tv_DragDrop);
            DragEnter += new DragEventHandler(tv_DragEnter);

            AfterCheck += new TreeViewEventHandler(tv_AfterCheck);
        }


        #region 设置选中节点
        public bool SetSelectNode(string nodeText)
        {
            foreach(TreeNode tn in Nodes)
            {
                if (tn.Text == nodeText)
                {
                   SelectedNode = tn;
                   return true;
                }
                bool findflag=SetSelectNode(tn, nodeText);
                if (findflag)
                {
                    return true;
                }
            }
            return false;
        }
        private bool SetSelectNode(TreeNode tn,string nodeText)
        {
            foreach (TreeNode node in tn.Nodes)
            {
                if (node.Text == nodeText)
                {
                    SelectedNode = tn;
                    return true;
                }
                bool findflag = SetSelectNode(tn, nodeText);
                if (findflag)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        protected void QyTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode tn = e.Node;
            tn.BackColor = Color.SkyBlue;
            if (preSelectTreeNode != null)
            {
                preSelectTreeNode.BackColor = Color.White;

            }
            preSelectTreeNode = e.Node;

            //for (int TreeNode tn0 in this.Nodes) {
            //    foreach (TreeNode tn2 in this.Nodes[0].Nodes)
            //    {
            //        if (tn2.Text == tn.Text)
            //            tn2.ForeColor = Color.Red;
            //        else
            //            tn2.ForeColor = Color.Black;

            //    }
            //}
        }




        #region 加载树
        /// <summary>
        /// 填充树
        /// </summary>
        /// <param name="Ts">要求根的PId=Id</param>
        public void LoadData(List<qytvNode> Ts,bool checkbox=false)
        {
            if (Ts == null)
                return;
            _Ts = Ts;
            CheckBoxes = checkbox;
            if (_Ts.Count != 0)
            {
                Nodes.Clear();
                AddRootTreeViewNodes();
            }
            // AddRootTreeViewNodes();
            ExpandAll();
            Refresh();
        }

        public void AddRootTreeViewNodes()
        {
            foreach (qytvNode t in _Ts)
            {
                if (t != null && t.PId!=null)//(t != null) && (t.PId == null))
                {
                    if (t.PId == Guid.Empty.ToString())//t.Id)
                    {
                        TreeNode newNode = new TreeNode(t.Name);
                        //newNode.Checked = true;
                        newNode.Name = t.Name;
                        newNode.Tag = t;

                        Nodes.Add(newNode);


                        AddChildTreeViewNodes(newNode);
                    }
                }

            }
        }

        private void AddChildTreeViewNodes(TreeNode parentTreeViewNode)
        {

            foreach (qytvNode t in _Ts)
            {

                if (t.PId!=t.Id && t.PId == ((qytvNode)(parentTreeViewNode.Tag)).Id)
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
        #endregion

        #region 复选框

        //用一个节点集合刷新树，表示选中
        public void RefreshChecked(List<qytvNode> checknodes)
        {
            foreach (TreeNode tn in Nodes)
            {
                if (tnInchecknodes(tn,ref checknodes))
                {
                    tn.Checked = true;
                }
                else
                { 
                    foreach(TreeNode subnode in tn.Nodes)
                    {
                        RefreshChecked(subnode, ref checknodes);
                    }
                }
            }
        }
        public void RefreshChecked(TreeNode tn, ref List<qytvNode> checknodes)
        {
            foreach (TreeNode tn1 in tn.Nodes)
            {
                if (tnInchecknodes(tn, ref checknodes))
                {
                    tn.Checked = true;
                }
                else
                {
                    foreach (TreeNode subnode in tn.Nodes)
                    {
                        RefreshChecked(subnode, ref checknodes);
                    }
                }
            }
        }
        private bool tnInchecknodes(TreeNode tn,ref List<qytvNode> checknodes)
        {
            bool findflag = false;
            for(int i = checknodes.Count - 1; i >= 0; i--)
            {
                if ((tn.Tag as qytvNode).Id == checknodes[i].Id)
                {
                    findflag = true;
                    checknodes.RemoveAt(i);
                    break;
                }
            }

            return findflag;
        }


        //获得选中的节点
        public List<qytvNode> GetCheckedNode()
        {
            List<qytvNode> checkednodes = new List<qytvNode>();
            foreach (TreeNode tn in Nodes)
            {
                GetCheckedNode(tn, ref checkednodes);
            }
            return checkednodes;
        }

        public void GetCheckedNode(TreeNode tn, ref List<qytvNode> checkednodes)
        {
            if (tn.Checked)
            {
                checkednodes.Add(tn.Tag as qytvNode);
            }

            foreach (TreeNode tn1 in tn.Nodes)
            {
                GetCheckedNode(tn1, ref checkednodes);
            }

        }


        //选择父节点，联动子节点选中
        private void tv_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (NodeCheckedWithParent_)
            {
                foreach (TreeNode tn in e.Node.Nodes)
                {
                    tn.Checked = e.Node.Checked;
                }
            }
        }

        #endregion  

        #region 拖拽

        private void tv_DragEnter(object sender, DragEventArgs e)
        {
            //设置拖放类别(复制，移动等)
            if (e.Data.GetDataPresent(typeof(TreeNode)))
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
             //e.Effect = DragDropEffects.Copy;
        }

        private void tv_ItemDrag(object sender, ItemDragEventArgs e)
        {
            try
            {
                //启动拖放操作，Move
                //DoDragDrop(e.Item, DragDropEffects.Copy);
                DoDragDrop(e.Item, DragDropEffects.Move);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void tv_DragDrop(object sender, DragEventArgs e)
        {
            TreeNode myNode = null;
            if (e.Data.GetDataPresent(typeof(TreeNode)))
            {
                myNode = (TreeNode)(e.Data.GetData(typeof(TreeNode)));
            }
            else
            {
                MessageBox.Show("error");
            }
            Position.X = e.X;
            Position.Y = e.Y;
            Position = PointToClient(Position);
            TreeNode DropNode = GetNodeAt(Position);

           // 将被拖拽节点从原来位置删除。
            myNode.Remove();
            // 1.目标节点不是空。2.目标节点不是被拖拽接点的子节点。3.目标节点不是被拖拽节点本身
            if (DropNode != null && DropNode.Parent != myNode && DropNode != myNode)
            {
                // 在目标节点下增加被拖拽节点
                DropNode.Nodes.Add(myNode);
                if (eventDragDroped != null)
                    eventDragDroped(myNode, DropNode);
            }
            else if (DropNode == null) // 如果目标节点不存在，即拖拽的位置不存在节点，那么就将被拖拽节点放在根节点之下
            {
                Nodes.Add(myNode);
                if (eventDragDroped != null)
                    eventDragDroped(myNode, null);
            }
        }

        #endregion
    }
    public class qytvNode
    {
        public string Id;
        public string Name;
        public string PId;
        public string Tag;
    }
}
