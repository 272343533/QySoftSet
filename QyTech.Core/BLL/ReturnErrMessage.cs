using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QyTech.Core.BLL
{
    public class ExceptionMessage
    {
        public static string Parse(Exception ex)
        {
            string errmsg = ex.Message;
            if (ex.InnerException != null)
                errmsg += "(明细:" + ex.InnerException.Message+")";
            return errmsg;
        }
    }
}
