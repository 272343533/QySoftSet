using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;
using System.Web.Script.Serialization;
using System.Data;
using System.Text;
using System.Collections;
using Newtonsoft.Json.Linq;

namespace QyTech.Json
{

    public class keyVal
    {
        public string key { get; set; }
        public string val { get; set; }
    }
    /// <summary>
    /// Json帮助类
    /// </summary>
    public class JsonHelper
    {
        static log4net.ILog  log = log4net.LogManager.GetLogger("JsonHelper");
        /// <summary>
        /// 序列化对象，直接序列化所有属性
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string SerializeObject(object o)
        {
            string json = null;
            //实例化一个使用Newtonsoft.Json 序列化的设置类
            JsonSerializerSettings jsetting = new JsonSerializerSettings();
            try
            {
                jsetting.MissingMemberHandling = MissingMemberHandling.Ignore;
                //设置对null值的处理 Include为保留，Ignore为忽略
                jsetting.NullValueHandling = NullValueHandling.Include;
                // 增加一个转换器，以便枚举值与枚举名间的转换
                jsetting.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                // 增加一个转换器，以方便时间格式的序列化和饭序列化
                var dateTimeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
                dateTimeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
                jsetting.Converters.Add(dateTimeConverter);
                // 排版返回的 json 数据，使其具有缩进格式，以方便裸眼查看
                jsetting.Formatting = Newtonsoft.Json.Formatting.Indented;
                //序列化
                json = JsonConvert.SerializeObject(o, jsetting);
            }
            catch (Exception ex)
            {

                return json;
            }

            //Formatting ft = Formatting.Indented;
            //string json = JsonConvert.SerializeObject(o,ft);
            return json;
        }

        /// <summary>
        /// 序列化对象为Json字符串
        /// </summary>
        /// <param name="o">操作对象</param>
        /// <param name="ItemName">需要序列化的属性</param>
        /// <returns>Json字符串 or null</returns>
        public static string SerializeObject(object o, List<string> ItemName)
        {
            string json = null;
            //实例化一个使用Newtonsoft.Json 序列化的设置类
            JsonSerializerSettings jsetting = new JsonSerializerSettings();
            try
            {
                string[] str;
                //设置序列化属性的清单
                if (ItemName == null || ItemName.Count == 0)
                {
                    ItemName = new List<string>();
                    Type type = o.GetType();
                    PropertyInfo[] _PropertyInfo = type.GetProperties();
                    List<string> listStr = new List<string>();
                    foreach (PropertyInfo p in _PropertyInfo)
                    {
                        //获取属性类型(a)
                        Type t = p.PropertyType;
                        if (t.Name.ToString() != "EntityState")//排除类型为System.Data.EntityState的属性
                        {
                            ////如果属性类型是数值型及string则进行保留
                            if (t.IsValueType || t == typeof(string))
                            {
                                ItemName.Add(p.Name);
                            }
                        }
                    }
                    str = ItemName.ToArray();
                }
                str = ItemName.ToArray();
                jsetting.ContractResolver = new LimitPropsContractResolver(str, true);

                jsetting.MissingMemberHandling = MissingMemberHandling.Ignore;
                //设置对null值的处理 Include为保留，Ignore为忽略
                jsetting.NullValueHandling = NullValueHandling.Include;
                // 增加一个转换器，以便枚举值与枚举名间的转换
                jsetting.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                // 增加一个转换器，以方便时间格式的序列化和饭序列化
                var dateTimeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
                dateTimeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
                jsetting.Converters.Add(dateTimeConverter);
                // 排版返回的 json 数据，使其具有缩进格式，以方便裸眼查看
                jsetting.Formatting = Newtonsoft.Json.Formatting.Indented;
                //序列化
                json = JsonConvert.SerializeObject(o, jsetting);

            }
            catch (Exception ex)
            {

                return json;
            }

            //Formatting ft = Formatting.Indented;
            //string json = JsonConvert.SerializeObject(o,ft);
            return json;
        }


        /// <summary>
        /// 序列化对象为Json字符串
        /// </summary>
        /// <param name="o">操作对象</param>
        /// <param name="ItemName">需要序列化的属性</param>
        /// <returns>Json字符串 or null</returns>
        public static string SerializeObject<T>(T o, List<string> ItemName)
        {
            string json = null;
            try
            {
                //实例化一个使用Newtonsoft.Json 序列化的设置类
                JsonSerializerSettings jsetting = new JsonSerializerSettings();
                jsetting.MissingMemberHandling = MissingMemberHandling.Ignore;

                string[] str;
                if (ItemName == null || ItemName.Count == 0)
                {
                    ItemName = new List<string>();
                    Type type = o.GetType();
                    PropertyInfo[] _PropertyInfo = type.GetProperties();
                    List<string> listStr = new List<string>();

                    foreach (PropertyInfo p in _PropertyInfo)
                    {
                        //获取属性类型(a)
                        Type t = p.PropertyType;
                        if (t.FullName.Contains("System") && !t.Name.Contains("Entity"))
                        //if (t.Name.ToString() != "EntityState")//排除类型为System.Data.EntityState的属性
                        {
                            ////如果属性类型是数值型及string则进行保留
                            if (t.IsValueType || t == typeof(string))
                            {
                                ItemName.Add(p.Name);
                            }
                        }
                    }
                    str = ItemName.ToArray();
                }
                str = ItemName.ToArray();
                jsetting.ContractResolver = new LimitPropsContractResolver(str, true);


                //设置对null值的处理 Include为保留，Ignore为忽略
                jsetting.NullValueHandling = NullValueHandling.Include;
                //设置序列化属性的清单
                jsetting.ContractResolver = new LimitPropsContractResolver(str, true);
                // 增加一个转换器，以便枚举值与枚举名间的转换
                jsetting.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                // 增加一个转换器，以方便时间格式的序列化和饭序列化
                var dateTimeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
                dateTimeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
                jsetting.Converters.Add(dateTimeConverter);
                // 排版返回的 json 数据，使其具有缩进格式，以方便裸眼查看
                jsetting.Formatting = Newtonsoft.Json.Formatting.Indented;
                //序列化
                json = JsonConvert.SerializeObject(o, jsetting);
            }
            catch (Exception ex)
            {

                return json;
            }

            //Formatting ft = Formatting.Indented;
            //string json = JsonConvert.SerializeObject(o,ft);
            return json;
        }

        /// <summary>
        /// 序列化数组
        /// </summary>
        /// <param name="o"></param>
        /// <param name="listitemtype">项类型</param>
        /// <param name="ItemName"></param>
        /// <returns></returns>
        public static string SerializeObject(object o, Type listitemtype, List<string> ItemName)
        {
            string json = null;
            //实例化一个使用Newtonsoft.Json 序列化的设置类
            JsonSerializerSettings jsetting = new JsonSerializerSettings();
            try
            {
                string[] str;
                if (ItemName == null || ItemName.Count == 0)
                {
                    ItemName = new List<string>();
                    PropertyInfo[] _PropertyInfo = listitemtype.GetProperties();
                    List<string> listStr = new List<string>();
                    foreach (PropertyInfo p in _PropertyInfo)
                    {
                        //获取属性类型(a)
                        Type t = p.PropertyType;
                        if (t.FullName.Contains("System") && !t.Name.Contains("Entity"))// !t.Name.Contains("EntityState") && !t.Name.Contains("EntityReference"))//排除类型为System.Data.EntityState的属性
                        {
                            ////如果属性类型是数值型及string则进行保留
                            //导航含items所以if不能有，------------------------------2017-01-19
                            //if (t.IsValueType || t == typeof(string))
                            //{
                            ItemName.Add(p.Name);
                            //}
                        }
                    }
                }
                str = ItemName.ToArray();
                jsetting.ContractResolver = new LimitPropsContractResolver(str, true);

                jsetting.MissingMemberHandling = MissingMemberHandling.Ignore;
                //设置对null值的处理 Include为保留，Ignore为忽略
                jsetting.NullValueHandling = NullValueHandling.Include;
                // 增加一个转换器，以便枚举值与枚举名间的转换
                jsetting.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                // 增加一个转换器，以方便时间格式的序列化和饭序列化
                var dateTimeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
                dateTimeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
                jsetting.Converters.Add(dateTimeConverter);
                // 排版返回的 json 数据，使其具有缩进格式，以方便裸眼查看
                jsetting.Formatting = Newtonsoft.Json.Formatting.Indented;
                //序列化
                json = JsonConvert.SerializeObject(o, jsetting);
                json = json.Replace("\r\n", "");
            }
            catch (Exception ex)
            {

                return json;
            }

            //Formatting ft = Formatting.Indented;
            //string json = JsonConvert.SerializeObject(o,ft);
            return json;
        }
        /// <summary>
        /// 序列化集合对象为Json字符串
        /// </summary>
        /// <param name="o">操作对象</param>
        /// <returns>Json字符串 or null</returns>
        public static string SerializeObjects<T>(List<T> o)
        {
            string json = null;
            try
            {
                //实例化一个使用Newtonsoft.Json 序列化的设置类
                JsonSerializerSettings jsetting = new JsonSerializerSettings();
                ////获取操作对象类型
                Type type = o[0].GetType();
                //通过上述类型反射得到操作对象的公共属性（p）
                PropertyInfo[] _PropertyInfo = type.GetProperties();
                //定义数组，保存将要序列化的属性字段
                List<string> listStr = new List<string>();
                foreach (PropertyInfo p in _PropertyInfo)
                {
                    //获取属性类型(a)
                    Type t = p.PropertyType;
                    if (t.Name.ToString() != "EntityState")//排除类型为System.Data.EntityState的属性
                    {
                        //如果属性类型是数值型及string则进行保留
                        if (t.IsValueType || t == typeof(string))
                        {
                            listStr.Add(p.Name);
                        }
                    }
                }
                string[] str = listStr.ToArray();

                jsetting.MissingMemberHandling = MissingMemberHandling.Ignore;
                //设置对null值的处理 Include为保留，Ignore为忽略
                jsetting.NullValueHandling = NullValueHandling.Include;
                //设置序列化属性的清单
                jsetting.ContractResolver = new LimitPropsContractResolver(str, true);
                // 增加一个转换器，以便枚举值与枚举名间的转换
                jsetting.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                // 增加一个转换器，以方便时间格式的序列化和饭序列化
                var dateTimeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
                dateTimeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
                jsetting.Converters.Add(dateTimeConverter);
                // 排版返回的 json 数据，使其具有缩进格式，以方便裸眼查看
                jsetting.Formatting = Newtonsoft.Json.Formatting.Indented;
                //序列化
                json = JsonConvert.SerializeObject(o, jsetting);
            }
            catch (Exception ex)
            {

                return json;
            }

            //Formatting ft = Formatting.Indented;
            //string json = JsonConvert.SerializeObject(o,ft);
            return json;
        }
        /// <summary>
        /// 序列化对象为Json字符串
        /// </summary>
        /// <param name="o">操作对象</param>
        /// <returns>Json字符串 or null</returns>
        public static string SerializeObjects<T>(List<T> o, Type tp)
        {
            string json = null;
            try
            {
                //实例化一个使用Newtonsoft.Json 序列化的设置类
                JsonSerializerSettings jsetting = new JsonSerializerSettings();
                ////获取操作对象类型
                Type type = o[0].GetType();
                PropertyInfo[] _PropertyInfo = type.GetProperties();
                List<string> listStr = new List<string>();
                foreach (PropertyInfo p in _PropertyInfo)
                {
                    //获取属性类型(a)
                    Type t = p.PropertyType;
                    if (t.Name.ToString() != "EntityState")//排除类型为System.Data.EntityState的属性
                    {
                        //如果属性类型是数值型及string则进行保留
                        if (t.IsValueType || t == typeof(string) || t == tp)
                        {
                            listStr.Add(p.Name);
                        }
                    }
                }
                string[] str = listStr.ToArray();

                jsetting.MissingMemberHandling = MissingMemberHandling.Ignore;

                //设置对null值的处理 Include为保留，Ignore为忽略
                jsetting.NullValueHandling = NullValueHandling.Include;
                //设置序列化属性的清单
                jsetting.ContractResolver = new LimitPropsContractResolver(str, true);
                // 增加一个转换器，以便枚举值与枚举名间的转换
                jsetting.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                // 增加一个转换器，以方便时间格式的序列化和饭序列化
                var dateTimeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
                dateTimeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
                jsetting.Converters.Add(dateTimeConverter);
                // 排版返回的 json 数据，使其具有缩进格式，以方便裸眼查看
                jsetting.Formatting = Newtonsoft.Json.Formatting.Indented;
                //序列化
                json = JsonConvert.SerializeObject(o, jsetting);
            }
            catch (Exception ex)
            {

                return json;
            }

            //Formatting ft = Formatting.Indented;
            //string json = JsonConvert.SerializeObject(o,ft);
            return json;
        }


        /// <summary>
        /// 序列化对象为Json字符串
        /// </summary>
        /// <param name="o">操作对象</param>
        /// <returns>Json字符串 or null</returns>
        public static string SerializeObjects<T>(List<T> o, List<string> ItemName)
        {
            string json = null;
            try
            {
                //实例化一个使用Newtonsoft.Json 序列化的设置类
                JsonSerializerSettings jsetting = new JsonSerializerSettings();

                string[] str;
                if (ItemName == null || ItemName.Count == 0)
                {
                    ItemName = new List<string>();
                    Type type = o[0].GetType();
                    PropertyInfo[] _PropertyInfo = type.GetProperties();
                    List<string> listStr = new List<string>();

                    foreach (PropertyInfo p in _PropertyInfo)
                    {
                        //获取属性类型(a)
                        Type t = p.PropertyType;
                        if (t.Name.ToString() != "EntityState")//排除类型为System.Data.EntityState的属性
                        {
                            ////如果属性类型是数值型及string则进行保留
                            if (t.IsValueType || t == typeof(string))
                            {
                                ItemName.Add(p.Name);
                            }
                        }
                    }
                    str = ItemName.ToArray();
                }
                str = ItemName.ToArray();
                jsetting.ContractResolver = new LimitPropsContractResolver(str, true);

                //设置序列化属性的清单
                jsetting.ContractResolver = new LimitPropsContractResolver(str, true);

                jsetting.MissingMemberHandling = MissingMemberHandling.Ignore;
                //设置对null值的处理 Include为保留，Ignore为忽略
                jsetting.NullValueHandling = NullValueHandling.Include;

                // 增加一个转换器，以便枚举值与枚举名间的转换
                jsetting.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                // 增加一个转换器，以方便时间格式的序列化和饭序列化
                var dateTimeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
                dateTimeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
                jsetting.Converters.Add(dateTimeConverter);
                // 排版返回的 json 数据，使其具有缩进格式，以方便裸眼查看
                jsetting.Formatting = Newtonsoft.Json.Formatting.Indented;
                //序列化
                json = JsonConvert.SerializeObject(o, jsetting);
            }
            catch (Exception ex)
            {

                return json;
            }

            //Formatting ft = Formatting.Indented;
            //string json = JsonConvert.SerializeObject(o,ft);
            return json;
        }

        /// <summary>
        /// 序列化简单对象,没有属性限制。一般适用于自定义对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o">要序列化的对象</param>
        /// <param name="errinfo">错误信息,正确返回则为默认为null</param>
        /// <param name="isFormat">是否排版返回json数据</param>
        /// <param name="isIgnoreNullValue">Null值是否忽略</param>
        /// <param name="FormatDt">日期类型格式化字符串</param>
        /// <returns>Json字符串,返回null为出错</returns>
        public static string SerializeObjectsNoLimit<T>(List<T> o, out string errinfo, bool isFormat = true, bool isIgnoreNullValue = false, string FormatDt = "yyyy-MM-dd HH:mm:ss")
        {
            string json = null;
            try
            {
                //实例化一个使用Newtonsoft.Json 序列化的设置类
                JsonSerializerSettings jsetting = new JsonSerializerSettings();

                //设置对null值的处理 Include为保留，Ignore为忽略
                jsetting.NullValueHandling = isIgnoreNullValue ? NullValueHandling.Ignore : NullValueHandling.Include;
                // 增加一个转换器，以便枚举值与枚举名间的转换
                jsetting.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                // 增加一个转换器，以方便时间格式的序列化和饭序列化
                var dateTimeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
                dateTimeConverter.DateTimeFormat = FormatDt;
                jsetting.Converters.Add(dateTimeConverter);
                // 排版返回的 json 数据，使其具有缩进格式，以方便裸眼查看
                jsetting.Formatting = isFormat ? Newtonsoft.Json.Formatting.Indented : Newtonsoft.Json.Formatting.None;
                //序列化
                json = JsonConvert.SerializeObject(o, jsetting);
                errinfo = null;
            }
            catch (Exception ex)
            {
                errinfo = ex.Message;
                return null;
            }

            return json;
        }
        /// <summary>
        /// 解析含格式化的JSON字符串生成对象实体
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json字符串</param>
        /// <returns>对象实体</returns>
        public static T DeserializeFormtJsonToObject<T>(string json) where T : class
        {
            JsonSerializer serializer = new JsonSerializer();
            json = json.Replace("\\n", "");
            json = json.Replace("\\r", "");
            json = json.Replace("\"{", "{");
            json = json.Replace("}\"", "}");
            json = json.Replace("\\\"", "\"");
            StringReader sr = new StringReader(json);
            object o = serializer.Deserialize(new JsonTextReader(sr), typeof(T));
            T t = o as T;
            return t;
        }

        /// <summary>
        /// 解析JSON字符串生成对象实体
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json字符串(eg.{"ID":"112","Name":"石子儿"})</param>
        /// <returns>对象实体</returns>
        public static T DeserializeJsonToObject<T>(string json) where T : class
        {
            try
            {
                JsonSerializer serializer = new JsonSerializer();
                StringReader sr = new StringReader(json);
                object o = serializer.Deserialize(new JsonTextReader(sr), typeof(T));
                T t = o as T;
                return t;
            }
            catch(Exception ex)
            {
                log.Error(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 解析JSON数组生成对象实体集合
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json数组字符串(eg.[{"ID":"112","Name":"石子儿"}])</param>
        /// <returns>对象实体集合</returns>
        public static List<T> DeserializeJsonToList<T>(string json) where T : class
        {
            //JsonSerializer serializer = new JsonSerializer();
            //StringReader sr = new StringReader(json);
            //object o = serializer.Deserialize(new JsonTextReader(sr), typeof(List<T>));
            //List<T> list = o as List<T>;

            JavaScriptSerializer Serializer = new JavaScriptSerializer();
            List<T> list = Serializer.Deserialize<List<T>>(json);
            return list;
        }

        public static List<keyVal> DeserializeJsonToKeyValList(string json)
        {
            List<keyVal> kvlist = new List<keyVal>();
            JArray item = (JArray)JsonConvert.DeserializeObject(json);
            for (int j = 0; j < item.Count; j++)
            {
                JObject jsonobj = (JObject)item[j];
                QyTech.Json.keyVal kv = new QyTech.Json.keyVal();
                string[] strkv = jsonobj.ToString().Replace("\r\n", "").Replace("{", "").Replace("}", "").Replace("\"", "").Replace(" ", "").Split(new char[] { ':' });
                kv.key = strkv[0];
                kv.val = strkv[1];

                kvlist.Add(kv);
            }
            return kvlist;
        }
        /// <summary>
        /// 反序列化JSON到给定的匿名对象.
        /// </summary>
        /// <typeparam name="T">匿名对象类型</typeparam>
        /// <param name="json">json字符串</param>
        /// <param name="anonymousTypeObject">匿名对象</param>
        /// <returns>匿名对象</returns>
        public static T DeserializeAnonymousType<T>(string json, T anonymousTypeObject)
        {
            T t = JsonConvert.DeserializeAnonymousType(json, anonymousTypeObject);
            return t;
        }




        #region 对DataTable的序列话

        public string SerializeDataTableToJsonWithJsonNet(DataTable table)
        {
            string JsonString = string.Empty;
            JsonString = JsonConvert.SerializeObject(table);
            return JsonString;
        }


        public string SerializeDataTableToJsonWithJavaScriptSerializer(DataTable table)
        {
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            foreach (DataRow row in table.Rows)
            {
                childRow = new Dictionary<string, object>();
                foreach (DataColumn col in table.Columns)
                {
                    childRow.Add(col.ColumnName, row[col]);
                }
                parentRow.Add(childRow);
            }
            return jsSerializer.Serialize(parentRow);

        }
        public DataTable DeserializerTableWithJavaScriptSerializer(string strJson)
        {
            DataTable dt = new DataTable();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            // var obj = serializer.DeserializeObject(strJson);//反序列化
            ArrayList arralList = serializer.Deserialize<ArrayList>(strJson);//反序列化ArrayList类型
            if (arralList.Count > 0)//反序列化后ArrayList个数不为0
            {
                foreach (Dictionary<string, object> row in arralList)
                {
                    if (dt.Columns.Count == 0)//新建的DataTable中无任何信息，为其添加列名及类型
                    {
                        foreach (string key in row.Keys)
                        {
                            dt.Columns.Add(key, row[key].GetType());//添加dt的列名
                        }
                    }
                    DataRow dr = dt.NewRow();
                    foreach (string key in row.Keys)//讲arrayList中的值添加到DataTable中
                    {

                        dr[key] = row[key];//添加列值
                    }
                    dt.Rows.Add(dr);//添加一行
                }
            }

            return dt;
        }

        public string SerializeDataTableToJson(DataTable table)
        {
            var JsonString = new StringBuilder();
            if (table.Rows.Count > 0)
            {
                JsonString.Append("[");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    JsonString.Append("{");
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        if (j < table.Columns.Count - 1)
                        {
                            JsonString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\",");
                        }
                        else if (j == table.Columns.Count - 1)
                        {
                            JsonString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\"");
                        }
                    }
                    if (i == table.Rows.Count - 1)
                    {
                        JsonString.Append("}");
                    }
                    else
                    {
                        JsonString.Append("},");
                    }
                }
                JsonString.Append("]");
            }
            return JsonString.ToString();
        }


        #endregion

    }
}