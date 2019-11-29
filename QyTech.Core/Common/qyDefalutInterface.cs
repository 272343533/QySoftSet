using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QyTech.Core.CommUtils;
namespace QyTech.Core.Common
{
    public class qyDefalutInterface
    {
        public static List<qytvNode> GetSysPreDefineTInterface()
        {
            List<qytvNode> nodes = new List<qytvNode>();
            string[] names = Enum.GetNames(typeof(DaoDefaultInterfaceName));
            foreach (string name in names)
            {
                qytvNode node = new qytvNode();
                node.id = qyEnumUtil.GetValueByName<DaoDefaultInterfaceName>(name).ToString();
                node.name = name;
                node.pId = "";
                node.type = "";

                nodes.Add(node);
            }
            return nodes;
        }
    }
}
