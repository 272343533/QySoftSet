using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QyTech.SkinForm.Controls
{
    public class qyControlEvents
    {
        public delegate void delegateProgressHandler(int value, int maxvalue);
        public static event qyControlEvents.delegateProgressHandler ProgressChangeddEvent;
    }
}