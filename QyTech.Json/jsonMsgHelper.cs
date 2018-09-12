using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using log4net;





namespace QyTech.Json
{
    internal class  QyJsonData
    {
        public int code { get; set; }
        public object data { get; set; }
        public string msg { get; set; }



        public QyJsonData(int Code, object Data, string Msg)
        {
            data = QyTech.Json.JsonHelper.SerializeObject(Data); ;
            code = Code;
            msg = Msg;
        }
        public QyJsonData(int Code, object Data, string Msg, Type itemtype,List<string> keepProperty)
        {
            if (Data != null)
            {
                if (Data.GetType().FullName.Contains("System.Collections.Generic.List") && itemtype != null)
                    data = QyTech.Json.JsonHelper.SerializeObject(Data, itemtype, keepProperty);
                else
                    data = QyTech.Json.JsonHelper.SerializeObject(Data, itemtype, keepProperty); ;
            }
            else
            {
                data = "{}";
            }
            code = Code;
            msg = Msg;
        }
   


        public string Serialize()
        {
            return "{ \"code\": " + code.ToString() + ", \"msg\": \"" + msg.ToString() + "\", \"data\":" + data +"}";
        }

    
    }
    internal class JsonDataForPage
    {
        public JsonDataForPage(int Code, object Data, int CurrentPage, int PageSize, int TotalCount, int TotalPage, string Msg, Type itemtype, List<string> keepProperty)
        {
            code = Code;
            msg = Msg;

            if (Data.GetType().FullName.Contains("System.Collections.Generic.List") && itemtype != null)
                data = QyTech.Json.JsonHelper.SerializeObject(Data, itemtype, keepProperty);
            else
                data = QyTech.Json.JsonHelper.SerializeObject(Data, keepProperty);
            currentPage = CurrentPage;
            pageSize = PageSize;
            totalCount = TotalCount;
            totalPage = TotalPage;
            
        }
        public int code { get; set; }
        public object data { get; set; }
        public string msg { get; set; }

        public int currentPage { get; set; }
        public int pageSize { get; set; }
        public int totalCount { get; set; }
        public int totalPage { get; set; }


        public string Serialize()
        {

            return "{ \"code\": " + code.ToString() + ", \"msg\": \"" + msg.ToString() + "\", \"data\":{\"data\":" + data + ",\"currentPage\": " + currentPage.ToString() + ",\"pageSize\": " + pageSize.ToString() + ",\"totalCount\": " + totalCount.ToString() + ",\"totalPage\":" + totalPage.ToString() + "}}";
        }
    }
    // "data": {
    //    "data": [{…},{…}], 
    //    "currentPage": 0, 
    //    "pageSize": 25, 
    //    "totalCount": 8040, 
    //    "totalPage": 322
    //}
  

    public class jsonMsgHelper
    {

        /// <summary>
        ///对象的说有数据全部转换
        /// </summary>
        /// <param name="flag">0:正确 1：错误</param>
        /// <param name="data">返回数据</param>
        /// <param name="msg">错误信息</param>
        /// <returns></returns>
        public static string Create(int flag, object data, string msg)
        {
            QyJsonData jd = new QyJsonData(flag, data, msg);
            string jsonstr = jd.Serialize();
            return jsonstr;
        }

       
        /// <summary>
        ///
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="data"></param>
        /// <param name="msg"></param>
        /// <param name="itemtype"></param>
        /// <param name="listsetting">数据库中设定的字段内容</param>
        /// <returns></returns>
        public static string Create(int flag, object data, string msg, Type itemtype, List<string> keepProperty)
        {
            QyJsonData jd = new QyJsonData(flag, data, msg, itemtype, keepProperty);

            string jsonstr = jd.Serialize();

            return jsonstr;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="data"></param>
        /// <param name="msg"></param>
        /// <param name="itemtype"></param>
        /// <param name="dispFields">用户从前端选择的传递到要显示的字段列表，用，分隔</param>
        /// <returns></returns>
        public static string CreateWithStrField(int flag, object data, string msg, Type itemtype, string dispFields)
        {
            List<string> keepProperty = new List<string>();

            string[] strs = dispFields.Split(new char[] {',' });
            foreach (string str in strs)
            {
                if (str != "")
                {
                    keepProperty.Add(str);
                }
            }

            QyJsonData jd = new QyJsonData(flag, data, msg, itemtype, keepProperty);

            string jsonstr = jd.Serialize();

            return jsonstr;
        }

        

        /// <summary>
        /// 分页消息，采用标准格式过滤字段
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="data"></param>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="totalPage"></param>
        /// <param name="msg"></param>
        /// <param name="itemtype"></param>
        /// <param name="listsetting"></param>
        /// <returns></returns>
        public static string CreatePage(int flag, object data
            , int currentPage, int pageSize, int totalCount, int totalPage, string msg, Type itemtype, List<string> keepProperty = null)
        {

           
            JsonDataForPage jd = new JsonDataForPage(flag, data, currentPage, pageSize, totalCount, totalPage, msg, itemtype,keepProperty);


            string jsonstr = jd.Serialize();

            return jsonstr;
            
        }


        /// <summary>
        /// 分页消息，采用字符串过滤字段
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="data"></param>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="totalPage"></param>
        /// <param name="msg"></param>
        /// <param name="itemtype"></param>
        /// <param name="dispFields"></param>
        /// <returns></returns>
        public static string CreatePageWithStrField(int flag, object data
            , int currentPage, int pageSize, int totalCount, int totalPage, string msg, Type itemtype, string dispFields = "")
        {

            List<string> keepProperty = new List<string>();
            string[] strs = dispFields.Split(new char[] { ',' });
            foreach (string str in strs)
            {
                if (str != "")
                {
                    keepProperty.Add(str);
                }
            }

            JsonDataForPage jd = new JsonDataForPage(flag, data, currentPage, pageSize, totalCount, totalPage, msg, itemtype, keepProperty);


            string jsonstr = jd.Serialize();

            return jsonstr;

        }

    }
}
