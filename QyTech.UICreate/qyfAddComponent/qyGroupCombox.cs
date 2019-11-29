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
    class qyGroupCombox : QyTech.UICreate.IaddComponent
    {
        public override Control Create(GroupBox gbContainer, string labText, string FName, object FValue, int x, int y, object querytag = null, int labwidth = 90, int textwidth = 150, bool Enabled = true)//, string items="")
        {

            Label l = new Label();
            l.Text = labText;
            l.Location = new System.Drawing.Point(x, y);
            l.Width = labwidth;
            l.TextAlign = ContentAlignment.MiddleRight;
            gbContainer.Controls.Add(l);

            ComboBox cb = new ComboBox();
            cb.Location = new System.Drawing.Point(x + 100, y);
            cb.Width = textwidth;
            cb.Name = FName;
           // cb.Tag = itemstag;    缺少参数
            cb.Text = FValue.ToString();
            int index = 0;
            //if (items.Trim() != "")
            //{
            //    string[] sitems = items.Split(new char[] { ',', ';' });
            //    cb.Items.Clear();
            //    //cb.Items.Add("");
            //    foreach (string s in sitems)
            //    {
            //        cb.Items.Add(s);
            //    }
            //    for (int i = 0; i < cb.Items.Count; i++)
            //        if (cb.Items[i].ToString() == FValue.ToString())
            //        { index = i; break; }

            //    cb.SelectedIndex = index;
            //}

            gbContainer.Controls.Add(cb);
            return cb;
        }

    }
}
