using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net.Http;
using System.Net;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
using System.Security.Cryptography;


using QyTech.MsgSms;

namespace MsgSmsTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender1, EventArgs e)
        {
            SmsFactory_Huawei fac = new SmsFactory_Huawei();
            IMsgSms_Huawei msg = fac.Create("qdz");
            msg.Send("13302127187", "已退回");


            //SendDefault("+8613302127187", "已退回");
            //string ret = Send("+8613302127187", "已退回");
            //MessageBox.Show(ret);
        }
        public string Send(string phones, string msg)
        {
            //必填,请参考"开发准备"获取如下数据,替换为实际值
            string apiAddress = "https://api.rtc.huaweicloud.com:10443/sms/batchSendSms/v1"; //APP接入地址+接口访问URI
            string appKey = "TZC38O80Ob5e7CzASzhQ7CXyeqko"; //APP_Key
            string appSecret = "1TlS52vw4m691IAi7eW5vQ1zSTXN"; //APP_Secret

            string sender = "8819032763279"; //国内短信签名通道号或国际/港澳台短信通道号
            string templateId = "cd0f95859ac9415db8236d9bdf26aa3a"; //模板ID

            //条件必填,国内短信关注,当templateId指定的模板类型为通用模板时生效且必填,必须是已审核通过的,与模板类型一致的签名名称
            //国际/港澳台短信不用关注该参数
            string signature = "吴江企信申报平台"; //签名名称

            //必填,全局号码格式(包含国家码),示例:+8615123456789,多个号码之间用英文逗号分隔
            string receiver = "+8613302127187"; //短信接收人号码

            //选填,短信状态报告接收地址,推荐使用域名,为空或者不填表示不接收状态报告
            string statusCallBack = "";
        

            
        /*
         * 选填,使用无变量模板时请赋空值 string templateParas = "";
         * 单变量模板示例:模板内容为"您的验证码是${NUM_6}"时,templateParas可填写为"[\"369751\"]"
         * 双变量模板示例:模板内容为"您有${NUM_2}件快递请到${TXT_32}领取"时,templateParas可填写为"[\"3\",\"人民公园正门\"]"
         * 查看更多模板变量规则:常见问题>业务规则>短信模板内容审核标准
         */
        string templateParas = "[\"已退回\"]"; //模板变量

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
                Console.WriteLine(response.StatusCode); //打印响应结果码
                var res = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(res); //打印响应信息
                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Message);
                return "Fail";
            }
        }

        /// <summary>
        /// 构造X-WSSE参数值
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="appSecret"></param>
        /// <returns></returns>
        static string buildWSSEHeader(string appKey, string appSecret)
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
        static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

        public void SendDefault(string phones, string msg)
        {
            //必填,请参考"开发准备"获取如下数据,替换为实际值
            string apiAddress = "https://api.rtc.huaweicloud.com:10443/sms/batchSendSms/v1"; //APP接入地址+接口访问URI
            string appKey = "TZC38O80Ob5e7CzASzhQ7CXyeqko"; //APP_Key
            string appSecret = "1TlS52vw4m691IAi7eW5vQ1zSTXN"; //APP_Secret
            string sender = "10690549122901566"; //国内短信签名通道号或国际/港澳台短信通道号
            string templateId = "cdbc29d8dd8041c9bc892bd4d55250cf"; //模板ID

            //条件必填,国内短信关注,当templateId指定的模板类型为通用模板时生效且必填,必须是已审核通过的,与模板类型一致的签名名称
            //国际/港澳台短信不用关注该参数
            string signature = "华为云短信测试"; //签名名称

            //必填,全局号码格式(包含国家码),示例:+8615123456789,多个号码之间用英文逗号分隔
            string receiver = "+8613302127187"; //短信接收人号码

            //选填,短信状态报告接收地址,推荐使用域名,为空或者不填表示不接收状态报告
            string statusCallBack = "";

            /*
             * 选填,使用无变量模板时请赋空值 string templateParas = "";
             * 单变量模板示例:模板内容为"您的验证码是${NUM_6}"时,templateParas可填写为"[\"369751\"]"
             * 双变量模板示例:模板内容为"您有${NUM_2}件快递请到${TXT_32}领取"时,templateParas可填写为"[\"3\",\"人民公园正门\"]"
             * 查看更多模板变量规则:常见问题>业务规则>短信模板内容审核标准
             */
            string templateParas = "[\"369751\"]"; //模板变量

            try
            {
                //为防止因HTTPS证书认证失败造成API调用失败,需要先忽略证书信任问题
                //.NET Framework 4.7.1及以上版本,请采用如下代码
               
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
                Console.WriteLine(response.StatusCode); //打印响应结果码
                var res = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(res); //打印响应信息
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                Console.WriteLine(e.Message);
            }
        }
    }
}

