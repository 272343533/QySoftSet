using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Net.Http;
using QyTech.Json;
using System.Threading;

namespace QyTech.Utils
{
    public class HttpRequestUtils
    {


        public static T GetRemoteJsonQy<T>(string url) where T : class
        {
            QyJsonData qyjson = GetRemoteJsonQy(url);
            if (qyjson.code == 1)
                return default(T);

            T t = JsonHelper.DeserializeJsonToObject<T>(qyjson.data.ToString());
            return t;
        }
        public static QyJsonData GetRemoteJsonQy(string url)
        {
            string ret = GetRemoteJson(QyTech.SoftConf.GlobalVaribles.ServerUrl + url);
            QyJsonData qyjson = JsonHelper.DeserializeJsonToObject<QyJsonData>(ret.Replace("\r\n", ""));
            return qyjson;
        }
        private static string GetRemoteJson(string url)
        {
            Uri Uri = new Uri(url);
            string ret = "";
            // Create an HttpClient instance    
            HttpClient client = new HttpClient();

            //远程获取数据  
            var task = client.GetAsync(url);
            var rep = task.Result;//在这里会等待task返回。  

            //读取响应内容  
            var task2 = rep.Content.ReadAsStringAsync();
            ret = task2.Result;//在这里会等待task返回。 

            return ret;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="paras">参数名和参数值的keyvalue集合</param>
        /// <returns>结构类型</returns>
        public static QyJsonData PostRemoteJsonQy(string url, Dictionary<string, string> paras)
        {
            string ret = PostRemoteJson(url, paras);
            QyJsonData qyjson = JsonHelper.DeserializeJsonToObject<QyJsonData>(ret.Replace("\r\n", ""));
            return qyjson;
        }

        public static T PostRemoteJsonQy<T>(string url, Dictionary<string, string> paras) where T:class
        {
            string ret = PostRemoteJson(QyTech.SoftConf.GlobalVaribles.ServerUrl+ url, paras);
            QyJsonData qyjson = JsonHelper.DeserializeJsonToObject<QyJsonData>(ret);
            if (qyjson.code == 1)
                return default(T);

            T t= JsonHelper.DeserializeJsonToObject<T>(qyjson.data.ToString());
            return t;
        }

        private static string PostRemoteJson(string url, Dictionary<string, string> paras)
        {
            Dictionary<string, string> dicparams = new Dictionary<string, string>();
            if (url.Contains("bsuser/LoginWithUserType"))
            {
                dicparams.Add("usertype", "manager");
            }
            else
            {
                dicparams.Add("sessionid", QyTech.SoftConf.GlobalVaribles.SessionId);

            }
            foreach (string key in paras.Keys)
            {
                dicparams.Add(key, paras[key]);
            }
            Uri Uri = new Uri(url);
            string ret = "";

            // Create an HttpClient instance    
            HttpClient client = new HttpClient();
            

            //提交操作
            if (dicparams != null)
            {
                var content = new FormUrlEncodedContent(dicparams);
                var task = client.PostAsync(url, content);
                var rep = task.Result;//在这里会等待task返回。 
                var task2 = rep.Content.ReadAsStringAsync();
                ret = task2.Result;//在这里会等待task返回。 
            }
            else
            {
                var task = client.PostAsync(url, null);
                var rep = task.Result;//在这里会等待task返回。  
                var task2 = rep.Content.ReadAsStringAsync();
                ret = task2.Result;//在这里会等待task返回。 
            }
            //读取响应内容  
            return ret;
        }
    }
}
