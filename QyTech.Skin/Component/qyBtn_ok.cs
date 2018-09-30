using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QyTech.SkinForm.Controls;

namespace QyTech.SkinForm.Component
{
    public partial class qyBtn_ok: qyBtn
    {
        public qyBtn_ok()
        {

            Image = qyResources.ok_16;
            Text = "确定";
            ImageAlign = ContentAlignment.MiddleCenter;
        }
        
    }
}
