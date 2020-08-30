using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Net.Http;
using System.Net;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

using System.Security.Cryptography;


namespace QyTech.MsgSms
{
    public class Sms_wj : IMsgSms_Huawei
    {


        public override void Send(string phones, string msg)
        {
            sender = "8819032763279"; //国内短信签名通道号或国际/港澳台短信通道号
            templateId = "cd0f95859ac9415db8236d9bdf26aa3a"; //模板ID

            //条件必填,国内短信关注,当templateId指定的模板类型为通用模板时生效且必填,必须是已审核通过的,与模板类型一致的签名名称
            //国际/港澳台短信不用关注该参数
            signature = "吴江企信申报平台"; //签名名称

            base.Send(phones, msg);
          
        }

        public override string SendMeetingNotice(string phones,string pubdep, string meetingtitle, string meetdt, string pubport, string id, string answer_phone="")
        {
            sender = "8819032763279"; //国内短信签名通道号或国际/港澳台短信通道号
            if (answer_phone.Length>0)
                templateId = "8fde93892e9c41d4ac76b1b499461f78"; //模板ID
            else
                templateId = "e02df614327b4c9f9eb0282f0c505eec"; //模板ID
            //条件必填,国内短信关注,当templateId指定的模板类型为通用模板时生效且必填,必须是已审核通过的,与模板类型一致的签名名称
            //国际/港澳台短信不用关注该参数
            signature = "吴江企信申报平台"; //签名名称

            return base.SendMeetingNotice(phones, pubdep,meetingtitle, meetdt, pubport, id, answer_phone);

        }

    }
}
