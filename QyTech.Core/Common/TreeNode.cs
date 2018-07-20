using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QyTech.Core.Common
{
    public class TreeNode
    {
        public string id { get; set; }
        public string name { get; set; }
        public string pId { get; set; }
        public string type { get; set; }
        public bool checkFlag { get; set; }

        public bool addBtnFlag { get; set; }
        public bool removeBtnFlag { get; set; }
        public bool editBtnFlag { get; set; }

        public bool openFlag { get; set; }

    }
}
