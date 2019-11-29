using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using log4net;





namespace QyTech.Json
{
 
    public class jsonMsgHelper
    {

        /// <summary>
        ///对象的所有数据全部转换
        /// </summary>
        /// <param name="flag">0:正确 1：错误</param>
        /// <param name="data">返回数据</param>
        /// <param name="msg">错误信息</param>
        /// <returns></returns>
        public static string Create(int flag, object data, Exception ex)
        {
            string msg= "操作失败,请登录页面再试一次！如果仍有问题，请与管理员联系！";
            if (ex.Message.Contains("唯一索引") || ex.Message.Contains("重复键"))
            {
                msg = "操作失败，数据已经存在！";
            }
            else if (System.Web.Configuration.WebConfigurationManager.AppSettings["currAppRunV"] == "Debug1.01")
                msg = QyTech.Core.LogHelper.Parse(ex);
            QyJsonData jd = new QyJsonData(flag, data, msg);
            string jsonstr = jd.Serialize();
            return jsonstr;
        }


        /// <summary>
        ///对象的所有数据全部转换
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
