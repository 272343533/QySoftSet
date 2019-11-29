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
    public class qyGroupFactory 
    {
        public enum qyGroupType { Textbox,Checkbox,MulTextbox, Combox}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ControlType">控件类型，枚举最好了</param>
        /// <returns></returns>
        public static IaddComponent Create(qyGroupType ControlType)
        {

            if (ControlType == qyGroupType.Textbox)
            {
                return new qyGroupTextbox();
            }
            else if (ControlType == qyGroupType.MulTextbox)
            {
                return new qyGroupTextbox();
            }
            else if (ControlType == qyGroupType.Checkbox)
            {
                return new qyGroupTextbox();
            }
            else if (ControlType == qyGroupType.Combox)
            {
                return new qyGroupTextbox();
            }
            else
            {
                ///逐步增加的应该反射得到
                return null;
            }
        }

    }
}
