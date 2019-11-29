using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QyTech.Core.Common;

namespace QyTech.Core.CommUtils
{
    public class qyTreeViewUtil
    {
        public static List<qytvNode> GetChecked(List<qytvNode> nodes)
        {
            List<qytvNode> checkednodes = new List<qytvNode>();
            foreach (qytvNode node in nodes)
            {
                if (node.checkFlag)
                    checkednodes.Add(node);
            }
            return checkednodes;
        }


        public static List<qytvNode> RefreshCheckedById(List<qytvNode> initnodes,List<qytvNode> checknodes)
        {
            foreach (qytvNode node in initnodes)
            {
                if (tnInchecknodesById(node, ref checknodes))
                {
                    node.checkFlag = true;
                }
            }
            return initnodes;
        }
        private static bool tnInchecknodesById(qytvNode tn, ref List<qytvNode> checknodes)
        {
            bool findflag = false;
            for (int i = checknodes.Count - 1; i >= 0; i--)
            {
                if (tn.id == checknodes[i].id)
                {
                    findflag = true;
                    checknodes.RemoveAt(i);
                    break;
                }
            }

            return findflag;
        }

        public static List<qytvNode> RefreshCheckedByName(List<qytvNode> initnodes, List<qytvNode> checknodes)
        {
            List<qytvNode> nodes = new List<qytvNode>();
            foreach (qytvNode node in initnodes)
            {
                if (tnInchecknodesByName(node, ref checknodes))
                {
                    node.checkFlag = true;
                }
                nodes.Add(node);
            }
            return nodes;
        }
        private static bool tnInchecknodesByName(qytvNode tn, ref List<qytvNode> checknodes)
        {
            bool findflag = false;
            for (int i = checknodes.Count - 1; i >= 0; i--)
            {
                if (tn.name == checknodes[i].name)
                {
                    findflag = true;
                    checknodes.RemoveAt(i);
                    break;
                }
            }

            return findflag;
        }

    }
}
