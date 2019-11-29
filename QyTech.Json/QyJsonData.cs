using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QyTech.Json
{

    public class QyJsonData//internal class QyJsonData
    {
        public int code { get; set; }
        public object data { get; set; }
        public string msg { get; set; }

        public QyJsonData()
        {
        }
        public QyJsonData(int Code, object Data, string Msg)
        {
            if (Data != null)
            {
                string fullName = Data.GetType().FullName;
                data = QyTech.Json.JsonHelper.SerializeObject(Data);
            }
            else
                Data = "";
            code = Code;
            msg = Msg;
        }

        public QyJsonData(int Code, object Data, string Msg, Type itemtype, List<string> keepProperty)
        {
            if (Data != null)
            {
                if (Data.GetType().FullName.Contains("System.Collections.Generic.List") && itemtype != null)
                    data = QyTech.Json.JsonHelper.SerializeObject(Data, itemtype, keepProperty);
                else
                    data = QyTech.Json.JsonHelper.SerializeObject(Data, itemtype, keepProperty); 
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
            List<string> props = new List<string>();
            props.Add("code");
            props.Add("msg");
            props.Add("data");
            string json="{ \"code\": " + code.ToString() + ", \"msg\": \"" + msg.ToString() + "\", \"data\":" + data + "}";

            //不用下面的，因为data部分已经是序列化，再次序列化格式不对。转义字符被再次转义
            //json = JsonHelper.SerializeObject<QyJsonData>(this, props);
            return json;

        }


    }
    public class JsonDataForPage//internal class JsonDataForPage
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
        public string data { get; set; }
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
}
