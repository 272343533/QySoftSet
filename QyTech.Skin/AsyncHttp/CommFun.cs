using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Net.Http;
using System.Threading;
using log4net;

namespace QyTech.AsyncHttp
{
    public class CommFun
    {
        public static string GetRemoteJson(string url)
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

        public static string PostRemoteJson(string url,Dictionary<string,string> paras)
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

       
            
    }
}
