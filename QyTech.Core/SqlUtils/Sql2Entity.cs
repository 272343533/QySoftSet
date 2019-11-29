using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;

using System.Web;
using QyTech.Core.Models;
using System.Data.Objects.DataClasses;
using System.Data.Objects;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;









namespace QyTech.Core.SqlUtils
{
    /// <summary>
    /// 反射用到，不要更改位置名称等
    /// </summary>
    public class Sql2Entity
    {
      
        public static List<T> DataTable2Entity<T>(DataTable dt) where T : class,new()
        {
            Type type = typeof(T);
            List<T> list = new List<T>();

            foreach (DataRow row in dt.Rows)
            {
                PropertyInfo[] pArray = type.GetProperties();
                T entity = new T();
                foreach (PropertyInfo p in pArray)
                {
                    if (row[p.Name] is Int64)
                    {
                        p.SetValue(entity, Convert.ToInt32(row[p.Name]), null);
                        continue;
                    }
                    p.SetValue(entity, row[p.Name], null);
                }
                list.Add(entity);
            }
            return list;
        }

        // 调用： 
        //List<User> userList = TableToEntity<User>(YourDataTable);  
        //public static IList<T> FillList<T>(System.Data.IDataReader reader)
        //{
        //    IList<T> lst = new List<T>();
        //    while (reader.Read())
        //    {
        //        T RowInstance = Activator.CreateInstance<T>();
        //        foreach (PropertyInfo Property in typeof(T).GetProperties())
        //        {
        //            foreach (
        //                BindingFieldAttribute FieldAttr in Property.GetCustomAttributes(

        //                typeof(BindingFieldAttribute), true))
        //            {
        //                try
        //                {

        //                    int Ordinal = reader.GetOrdinal(FieldAttr.FieldName);

        //                    if (reader.GetValue(Ordinal) != DBNull.Value)
        //                    {

        //                        Property.SetValue(RowInstance,

        //                            Convert.ChangeType(reader.GetValue(Ordinal),

        //                            Property.PropertyType), null);

        //                    }

        //                }

        //                catch
        //                {

        //                    break;

        //                }

        //            }

        //        }

        //        lst.Add(RowInstance);

        //    }

        //    return lst;

        //}

        /// <summary>
        /// 反射用到，不要更改位置名称等
        /// </summary>
        public static T DataRow2Entity<T>(DataRow r)
        {
            T t = default(T);
            t = Activator.CreateInstance<T>();
            PropertyInfo[] ps = t.GetType().GetProperties();
            foreach (var item in ps)
            {
                if (r.Table.Columns.Contains(item.Name))
                {
                    object v = r[item.Name];
                    if (v.GetType() == typeof(System.DBNull))
                        v = null;
                    item.SetValue(t, v, null);
                }
            }
            return t;
        }


        public static string SqlTypeMap2OType(string sqlType)
        {
            if (sqlType == "int")
                return "int";
            else if (sqlType == "decimal")
                return "decimal";
            else if (sqlType == "datetime")
                return "datetime";
            else if (sqlType == "uniqueidentifier")
                return "Guid";
            return "string";
        }
    }
}
