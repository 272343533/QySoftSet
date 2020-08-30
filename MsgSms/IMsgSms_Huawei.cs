using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using System.Net.Http;
using System.Net;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

using System.Security.Cryptography;
using log4net;
using Newtonsoft.Json;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]
namespace QyTech.MsgSms
{
    public class IMsgSms_Huawei
    {
        private static ILog log = log4net.LogManager.GetLogger("IMsgSms_Huawei");


        //属于应用，不需更改
        protected string apiAddress = "https://api.rtc.huaweicloud.com:10443/sms/batchSendSms/v1"; //APP接入地址+接口访问URI
        protected string appKey = "TZC38O80Ob5e7CzASzhQ7CXyeqko"; //APP_Key
        protected string appSecret = "1TlS52vw4m691IAi7eW5vQ1zSTXN"; //APP_Secret


        //签名通道号，模板id
        protected string sender = "8819032763279"; //国内短信签名通道号或国际/港澳台短信通道号
        protected string templateId = "cd0f95859ac9415db8236d9bdf26aa3a"; //模板ID
      

        //条件必填,国内短信关注,当templateId指定的模板类型为通用模板时生效且必填,必须是已审核通过的,与模板类型一致的签名名称
        //国际/港澳台短信不用关注该参数
        protected string signature = "吴江企信申报平台"; //签名名称


 
        public virtual void Send(string phones, string msg)
        {
            //必填,全局号码格式(包含国家码),示例:+8615123456789,多个号码之间用英文逗号分隔
            string receiver = "+86" + phones.Replace(",", ",+86"); //短信接收人号码

            //选填,短信状态报告接收地址,推荐使用域名,为空或者不填表示不接收状态报告
            string statusCallBack = "http://122.112.245.147:8083/M/bllapp/MsgStatus/OnSmsStatusReport";

            /*
             * 选填,使用无变量模板时请赋空值 string templateParas = "";
             * 单变量模板示例:模板内容为"您的验证码是${NUM_6}"时,templateParas可填写为"[\"369751\"]"
             * 双变量模板示例:模板内容为"您有${NUM_2}件快递请到${TXT_32}领取"时,templateParas可填写为"[\"3\",\"人民公园正门\"]"
             * 查看更多模板变量规则:常见问题>业务规则>短信模板内容审核标准
             */
            string templateParas = "[\"" + msg + "\"]"; //模板变量

            try
            {
                //为防止因HTTPS证书认证失败造成API调用失败,需要先忽略证书信任问题
                //.NET Framework 4.7.1及以上版本,请采用如下代码
                //var sslHandler = new HttpClientHandler()
                //{
                //    ServerCertificateCustomValidationCallback = (message, cert, chain, err) => { return true; }
                //};
                //HttpClient client = new HttpClient(sslHandler, true);
                //低于.NET Framework 4.7.1版本,请采用如下代码
                HttpClient client = new HttpClient();
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);

                //请求Headers
                client.DefaultRequestHeaders.Add("Authorization", "WSSE realm=\"SDP\",profile=\"UsernameToken\",type=\"Appkey\"");
                client.DefaultRequestHeaders.Add("X-WSSE", buildWSSEHeader(appKey, appSecret));
                //请求Body
                var body = new Dictionary<string, string>() {
                    {"from", sender},
                    {"to", receiver},
                    {"templateId", templateId},
                    {"templateParas", templateParas},
                    {"statusCallback", statusCallBack},
                    //{"signature", signature} //使用国内短信通用模板时,必须填写签名名称
                };

                HttpContent content = new FormUrlEncodedContent(body);

                var response = client.PostAsync(apiAddress, content).Result;
                //Console.WriteLine(response.StatusCode); //打印响应结果码
                log.Info("IMsgSms_Huawei_Send response.StatusCode:" + receiver + ":"+ response.StatusCode);
                var res = response.Content.ReadAsStringAsync().Result;
                //Console.WriteLine(res); //打印响应信息
                log.Info("IMsgSms_Huawei_Send response.Content.ReadAsStringAsync().Result:" + receiver + ":"+ res);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Message);
                log.Error("IMsgSms_Huawei_Send " + receiver+":"+ ex.Message+"-"+ex.StackTrace);

            }
        }


        public virtual string SendMeetingNotice(string phones,string pubdep, string meetingtitle, string meetdt, string pubport,string id,string answer_phone)
        {
            //必填,全局号码格式(包含国家码),示例:+8615123456789,多个号码之间用英文逗号分隔
            string receiver = "+86"+ phones.Replace(",", ",+86");// "+86" + phones; //短信接收人号码

            //选填,短信状态报告接收地址,推荐使用域名,为空或者不填表示不接收状态报告
            string statusCallBack = "http://122.112.245.147:8084/bllApp/MsgStatus/OnSmsStatusReport";
            string extend = "meeting";
            /*
             * 选填,使用无变量模板时请赋空值 string templateParas = "";
             * 单变量模板示例:模板内容为"您的验证码是${NUM_6}"时,templateParas可填写为"[\"369751\"]"
             * 双变量模板示例:模板内容为"您有${NUM_2}件快递请到${TXT_32}领取"时,templateParas可填写为"[\"3\",\"人民公园正门\"]"
             * 查看更多模板变量规则:常见问题>业务规则>短信模板内容审核标准
             */

            string t1 = "";
            string t2 = "";
            string t3 = "";

            if (meetingtitle.Length <=20)
            {
                t1 = meetingtitle + "\"";
                t2 = ",\"\"";
                t3 = ",\"";
            }
            else if (meetingtitle.Length <= 40)
            {
                t1 = meetingtitle.Substring(0, 20)+ "\"" ;
                t2 = ",\"" + meetingtitle.Substring(20) + "\"";
                t3 = ",\"";
            }
            else
            {
                t1 =  meetingtitle.Substring(0, 20) + "\"";
                t2 = ",\"" + meetingtitle.Substring(20,20) + "\"";
                t3 = ",\"" + meetingtitle.Substring(40) ;
            }
            

            string templateParas = "[\"" + pubdep + "\",\"" + t1+t2+t3+ "\",\"" + meetdt + "\",\"" + id + "\"]"; //模板变量
            if (answer_phone.Length>0)
            {
                   templateParas = "[\"" + pubdep + "\",\"" + t1 + t2 + t3 + "\",\"" + meetdt + "\",\"" + id + "\",\"" + answer_phone + "\"]"; //模板变量
            }
            try
            {
                //为防止因HTTPS证书认证失败造成API调用失败,需要先忽略证书信任问题
                //.NET Framework 4.7.1及以上版本,请采用如下代码
                //var sslHandler = new HttpClientHandler()
                //{
                //    ServerCertificateCustomValidationCallback = (message, cert, chain, err) => { return true; }
                //};
                //HttpClient client = new HttpClient(sslHandler, true);
                //低于.NET Framework 4.7.1版本,请采用如下代码
                HttpClient client = new HttpClient();
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);

                //请求Headers
                client.DefaultRequestHeaders.Add("Authorization", "WSSE realm=\"SDP\",profile=\"UsernameToken\",type=\"Appkey\"");
                client.DefaultRequestHeaders.Add("X-WSSE", buildWSSEHeader(appKey, appSecret));
                //请求Body
                var body = new Dictionary<string, string>() {
                    {"from", sender},
                    {"to", receiver},
                    {"templateId", templateId},
                    {"templateParas", templateParas},
                    {"statusCallback", statusCallBack},
                    { "extend", extend }
                    //{"signature", signature} //使用国内短信通用模板时,必须填写签名名称
                };

                HttpContent content = new FormUrlEncodedContent(body);

                var response = client.PostAsync(apiAddress, content).Result;
                log.Info("IMsgSms_Huawei_Send response.StatusCode:" + response.StatusCode);
                var res = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(res); //打印响应信息
                log.Info("IMsgSms_Huawei_SendMeetingNotice response.Content.ReadAsStringAsync().Result:" + res);


                Huawei_send_Response hsr = (Huawei_send_Response)JsonConvert.DeserializeObject<Huawei_send_Response>(res);
                //string createTime, string smsMsgId, string status, string extend = "", string to = ""
                foreach (Huawei_send_result item in hsr.result)
                {
                    string urlparams = "createTime=" + item.createTime + "&smsMsgId=" + item.smsMsgId + "&status=" + item.status + "&extend=" + extend + "&to=" + item.originTo;
                    string url = "http://122.112.245.147:8084/bllApp/MsgStatus/OnSmsSendReport?" + urlparams;
                    log.Info(url);
                    HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
                    req.Method = "GET";
                    req.Timeout = 3 * 1000;
                    req.Credentials = CredentialCache.DefaultCredentials;
                    using (WebResponse wr = req.GetResponse())
                    {
                        //在这里对接收到的页面内容进行处理
                        HttpWebResponse hwr = (HttpWebResponse)wr;

                        // 从Internet资源返回数据流
                        //Stream responseStream = 
                        hwr.GetResponseStream();
                        //if (responseStream != null)
                        //{
                        //    // 读取数据流
                        //    StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.GetEncoding("utf-8"));

                        //    // 读取数据
                        //    string result = reader.ReadToEnd();
                        //    log.Info("Send->Write_SendInfo:" + result);
                        //    reader.Close();
                        //    responseStream.Close();
                        //    req.Abort();
                        //    hwr.Close();
                        //}
                    }
                }
                
                return response.StatusCode.ToString();
                    

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Message);
                log.Error("IMsgSms_Huawei_SendMeetingNotice:" + ex.Message + "-" + ex.StackTrace);
                return "发送错误!";
            }
        }

        /// <summary>
        /// 构造X-WSSE参数值
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="appSecret"></param>
        /// <returns></returns>
        public static string buildWSSEHeader(string appKey, string appSecret)
        {
            string now = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ"); //Created
            string nonce = Guid.NewGuid().ToString().Replace("-", ""); //Nonce

            byte[] material = Encoding.UTF8.GetBytes(nonce + now + appSecret);
            byte[] hashed = SHA256Managed.Create().ComputeHash(material);
            string hexdigest = BitConverter.ToString(hashed).Replace("-", "");
            string base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(hexdigest)); //PasswordDigest

            return String.Format("UsernameToken Username=\"{0}\",PasswordDigest=\"{1}\",Nonce=\"{2}\",Created=\"{3}\"",
                            appKey, base64, nonce, now);
        }

        //低于.NET Framework 4.7.1版本,启用如下方法
        public static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }


        public static void OnSmsStatusReport(string data)
        {
            try
            {
                var keyValues = HttpUtility.ParseQueryString(data); //解析状态报告数据

                /**
                 * Example: 此处已解析status为例,请按需解析所需参数并自行实现相关处理
                 * 
                 * 'smsMsgId': 短信唯一标识
                 * 'total': 长短信拆分条数
                 * 'sequence': 拆分后短信序号
                 * 'source': 状态报告来源
                 * 'updateTime': 资源更新时间
                 * 'status': 状态码
                 */
                string status = keyValues.Get("status"); // 状态报告枚举值
                                                         // 通过status判断短信是否发送成功
                if ("DELIVRD".Equals(status.ToUpper()))
                {
                    log.Info("Send sms success. smsMsgId: " + keyValues.Get("smsMsgId"));
                }
                else
                {
                    // 发送失败,打印status和orgCode
                    log.Error("Send sms failed. smsMsgId: " + keyValues.Get("smsMsgId"));
                    log.Error("Failed status: " + status);
                }
            }
            catch(Exception ex)
            {
                log.Error(ex.Message);
            }
        }




        public class Huawei_send_Response
        {
            public Huawei_send_result[] result;
            public string code;
            public string description;

            
        }
        public class Huawei_send_result
        {
            public string originTo;
            public string createTime;
            public string from;
            public string smsMsgId;
            public string status;
        }
    }



  
}
