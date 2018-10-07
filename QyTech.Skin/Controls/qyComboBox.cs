using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QyTech.SkinForm.Controls
{
    public class qyComboBox:ComboBox
    {

        public void AddItems(List<string> items)
        {
            Items.Clear();
            foreach (string item in items)
            {
                Items.Add(item);
            }
        }
    }

   
}
