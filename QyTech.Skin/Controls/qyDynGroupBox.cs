using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace QyTech.SkinForm.Controls
{
    public class qyDynGroupBox:GroupBox
    {

        int[] Item_LocationYs;
        int controlcount = 0;
        public qyDynGroupBox()
        {
            VScrollBar rightbar = new VScrollBar();
            rightbar.Dock = DockStyle.Right;
            rightbar.Top = 0;
            rightbar.Size = new Size(8, this.Height);
            rightbar.Scroll += new ScrollEventHandler(vScrollBar_Scroll);
            this.Controls.Add(rightbar);
        }
      

        private void vScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            int index = 0;
            foreach (Control gbox in Controls)
            {
                if (gbox is VScrollBar) continue;
                gbox.Location = new System.Drawing.Point(gbox.Location.X, Item_LocationYs[index++] - e.NewValue);
            }
        }

        private new void ControlAdded(object sender, ControlEventArgs e) {
            Item_LocationYs[controlcount++] = e.Control.Location.Y;
        }
    }
}
