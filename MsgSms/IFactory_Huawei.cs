using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;



namespace QyTech.MsgSms
{
    public class SmsFactory_Huawei
    {
        public IMsgSms_Huawei Create(string flag)
        {
            if (flag.ToLower() == "wj")
            {
                return new Sms_wj();
            }
            else if (flag.ToLower() == "qdz")
            {
                return new Sms_qdz();
            }
            else if (flag.ToLower() == "szz")
            {
                return new Sms_szz();
            }
            else
                return null;
        }
    }


}