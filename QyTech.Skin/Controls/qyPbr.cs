using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QyTech.SkinForm.Controls
{
  
    public partial class qyPbr : ProgressBar
    {
        public qyPbr()
        {

        }
        public int PercentValue {
            set {
                this.Value = value;

                string strText = value.ToString() + "%";
                Font font = new Font("Arial", (float)7, FontStyle.Regular);
                PointF pointF = new PointF(this.Width / 2 - (int)font.Size, this.Height / 2 - (int)font.Size);
                this.CreateGraphics().DrawString(strText, font, Brushes.Black, pointF);
            }
        }

        private new void MouseHover(object sender, EventArgs e)
        {
            string strText = Value.ToString() + "%";
            Font font = new Font("Arial", (float)7, FontStyle.Regular);
            PointF pointF = new PointF(this.Width / 2 - (int)font.Size, this.Height / 2 - (int)font.Size);
            this.CreateGraphics().DrawString(strText, font, Brushes.Black, pointF);

        }
    }
    
}
