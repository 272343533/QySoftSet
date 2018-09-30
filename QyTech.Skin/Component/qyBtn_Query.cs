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
    public partial class qyBtn_Search: qyBtn
    {
        public qyBtn_Search()
        {

            Text = "查询";
            Image = qyResources.search_16;
            ImageAlign = ContentAlignment.MiddleCenter;
        }
        
        private new void Click(object sender, EventArgs e)
        {
            ((Form)(this.Parent)).Close();
        }
    }
}
