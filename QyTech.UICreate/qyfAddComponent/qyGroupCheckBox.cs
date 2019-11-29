using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

using System.Data;
using QyExpress.Dao;
using System.Data.SqlClient;

namespace QyTech.UICreate.qyfAddComponent
{
    public class qyGroupCheckBox : QyTech.UICreate.IaddComponent
    {
        public override Control Create(GroupBox gbContainer, string labText, string FName, object FValue, int x, int y, object querytag = null, int labwidth = 90, int textwidth = 150, bool Enabled = true)
        {

            Label l = new Label();
            l.Name = "lbl_" + FName;
            l.Text = labText;
            l.Location = new System.Drawing.Point(x, y);//(300,...)
            l.Width = labwidth;
            l.TextAlign = ContentAlignment.MiddleRight;
            gbContainer.Controls.Add(l);

            CheckBox tb = new CheckBox();
            tb.Location = new System.Drawing.Point(x + 100, y);
            tb.Width = textwidth;

            //tb.Text = labText;
            //tb.RightToLeft = RightToLeft.Yes;// = ContentAlignment.MiddleLeft;
            tb.Name = FName;
            tb.Checked = FValue.ToString() == "True" ? true : false; ;
            tb.Tag = querytag;
            gbContainer.Controls.Add(tb);
            return tb;
        }

    }
}
