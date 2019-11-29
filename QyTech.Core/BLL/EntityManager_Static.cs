using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QyTech.Core.Models;
using System.Data.Objects.DataClasses;
using System.Data.Objects;
using System.Data.Entity;
using log4net;
using QyTech.Core;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;


namespace QyTech.Core.BLL
{

    /// <summary>
    /// 实体操作类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class EntityManager_Static
    {
        private static ILog log = LogManager.GetLogger("EntityManager");
        static String entityName = "";




        public static int GetIntBySql(ObjectContext dbcontext,string sql)
        {
            try
            {
                List<intObject> objs = dbcontext.ExecuteStoreQuery<intObject>(sql).ToList<intObject>();
                return objs[0].intData;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return -9999;
            }
        }


        public static string ExecuteSql(ObjectContext dbcontext, string sql)
        {
            try
            {
                dbcontext.ExecuteStoreCommand(sql);
                return "";
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return LogHelper.Parse(ex);
            }
        }


        public static List<T> GetAllByStorProcedure<T>(ObjectContext dbcontext, string spName, object[] paramvalues, int outParamIndexFrom1, out object result)
        {
            result = -999;
            try
            {
                string Sqls = "EXEC [" + spName + "] ";
                List<SqlParameter> paramArray = new List<SqlParameter>();

                int index = 1;
                foreach (object item in paramvalues)
                {
                    if (index == outParamIndexFrom1)
                    {
                        SqlParameter param = new SqlParameter("@Param" + index.ToString(), SqlDbType.Int);
                        param.Direction = ParameterDirection.Output;
                        paramArray.Add(param);
                        Sqls += "@Param" + index.ToString() + " out,";
                    }
                    else
                    {
                        paramArray.Add(new SqlParameter("@Param" + index.ToString(), item));
                        Sqls += "@Param" + index.ToString() + ",";
                    }
                    index++;
                }
                if (index == outParamIndexFrom1)
                {
                    SqlParameter param = new SqlParameter("@Param" + index.ToString(), SqlDbType.Int);
                    param.Direction = ParameterDirection.Output;
                    paramArray.Add(param);
                    Sqls += "@Param" + index.ToString() + " out,";
                }

                Sqls = Sqls.Substring(0, Sqls.Length - 1);
                //db.Database.ExecuteSqlCommand("EXEC [AddVote] @blockId,@titleId,@typeId,@num out", paramArray.ToArray());
                List<T> obj = dbcontext.ExecuteStoreQuery<T>(Sqls, paramArray.ToArray()).ToList<T>();

                result = paramArray[outParamIndexFrom1 - 1].Value;


                return obj;
            }
            catch (Exception ex)
            {
                string err = entityName + ":" + ex.Message;
                if (ex.InnerException != null)
                    err += " detail:" + ex.InnerException.Message;
                LogHelper.Error(ex);
                return new List<T>();
            }
        }


        public static List<T> GetAllByStorProcedure<T>(ObjectContext dbcontext, string spName, ref object[] paramvalues)
        {
            try
            {
                string Sqls = "EXEC [" + spName + "] ";
                List<SqlParameter> paramArray = new List<SqlParameter>();

                int index = 1;
                foreach (object item in paramvalues)
                {
                    if (item == null)
                    {
                        SqlParameter param = new SqlParameter("@Param" + index.ToString(), SqlDbType.Int);
                        param.Direction = ParameterDirection.Output;
                        paramArray.Add(param);
                        Sqls += "@Param" + index.ToString() + " out,";
                    }
                    else
                    {
                        paramArray.Add(new SqlParameter("@Param" + index.ToString(), item));
                        Sqls += "@Param" + index.ToString() + ",";
                    }
                    index++;
                }

                Sqls = Sqls.Substring(0, Sqls.Length - 1);
                List<T> obj = dbcontext.ExecuteStoreQuery<T>(Sqls, paramArray.ToArray()).ToList<T>();

                return obj;
            }
            catch (Exception ex)
            {
                string err = entityName + ":" + ex.Message;
                if (ex.InnerException != null)
                    err += " detail:" + ex.InnerException.Message;
                LogHelper.Error(ex);
                return new List<T>();
            }
        }


        public static List<T> GetAllByStorProcedure<T>(ObjectContext dbcontext, string spName, object[] paramvalues)
        {
            try
            {
                string Sqls = "EXEC [" + spName + "] ";
                List<SqlParameter> paramArray = new List<SqlParameter>();

                int index = 1;
                foreach (object item in paramvalues)
                {

                    paramArray.Add(new SqlParameter("@Param" + index.ToString(), item));
                    Sqls += "@Param" + index.ToString() + ",";

                    index++;
                }

                Sqls = Sqls.Substring(0, Sqls.Length - 1);
                //db.Database.ExecuteSqlCommand("EXEC [AddVote] @blockId,@titleId,@typeId,@num out", paramArray.ToArray());
                List<T> obj = dbcontext.ExecuteStoreQuery<T>(Sqls, paramArray.ToArray()).ToList<T>();

                return obj;
            }
            catch (Exception ex)
            {
                string err = entityName + ":" + ex.Message;
                if (ex.InnerException != null)
                    err += " detail:" + ex.InnerException.Message;
                LogHelper.Error(ex);
                return new List<T>();
            }
        }

        public static List<T> GetPagingByStorProcedure<T>(ObjectContext dbcontext, string spName, string paramvalues, int pageNum, int numPerPage, out int totalCount)
        {
            totalCount = 0;
            try
            {

                List<T> list = dbcontext.ExecuteStoreQuery<T>("exec " + spName + " " + paramvalues).ToList<T>();
                totalCount = list.Count;

                return list.Skip((pageNum - 1) * numPerPage).Take(numPerPage).ToList<T>();
            }
            catch (Exception ex)
            {
                string err = entityName + ":" + ex.Message;
                if (ex.InnerException != null)
                    err += " detail:" + ex.InnerException.Message;
                LogHelper.Error(ex);
                return new List<T>();
            }
        }

        public static string Add<T>(ObjectContext dbcontext, T t)
        {
            try
            {
                entityName = dbcontext.GetType().GetProperties().Where(p => p.Name == t.GetType().Name || p.Name == t.GetType().Name + "s" || p.Name == t.GetType().Name + "es").Single().Name;
                dbcontext.AddObject(entityName, t);
                dbcontext.SaveChanges();
                return "";
            }
            catch (Exception ex)
            {
               LogHelper.Error(ex);
                return LogHelper.Parse(ex);
            }
        }

        public static string Adds<T>(ObjectContext dbcontext, List<T> ts)
        {
            try
            {
                if (ts.Count > 0)
                {

                    entityName = dbcontext.GetType().GetProperties().Where(p => p.Name == ts[0].GetType().Name || p.Name == ts[0].GetType().Name + "s" || p.Name == ts[0].GetType().Name + "es").Single().Name;
                    foreach (T t in ts)
                    {
                        dbcontext.AddObject(entityName, t);
                    }
                    dbcontext.SaveChanges();
                }
                return "";
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return ex.Message;
            }
        }



        public static T AddReturnEntity<T>(ObjectContext dbcontext, T t, out string errmsg)
        {
            errmsg = "";
            try
            {

                entityName = dbcontext.GetType().GetProperties().Where(p => p.Name == t.GetType().Name || p.Name == t.GetType().Name + "s" || p.Name == t.GetType().Name + "es").Single().Name;
                dbcontext.AddObject(entityName, t);
                dbcontext.SaveChanges();
                return t;
            }
            catch (Exception ex)
            {
                errmsg = LogHelper.Parse(ex);
                LogHelper.Error(ex);
                return default(T);
            }
        }

     
        public static string Modify<T>(ObjectContext dbcontext, T t) where T : EntityObject
        {
            try
            {
                entityName = dbcontext.GetType().GetProperties().Where(p => p.Name == t.GetType().Name || p.Name == t.GetType().Name + "s" || p.Name == t.GetType().Name + "es").Single().Name;
                int i = dbcontext.UpdateEntity(t, entityName); //自定更新扩展方法
                dbcontext.SaveChanges();

                return "";
            }
            catch (Exception ex)
            {
               LogHelper.Error(ex);
                return LogHelper.Parse(ex); ;
            }
        }

        public static string Modify<T>(ObjectContext dbcontext, T t, string pkName, object pkValue) where T : EntityObject
        {
            try
            {
                T dbt = GetByPk<T>(dbcontext,pkName, pkValue);
                EntityOperate.Copy<T>(t, dbt, pkName);

                entityName = dbcontext.GetType().GetProperties().Where(p => p.Name == t.GetType().Name || p.Name == t.GetType().Name + "s" || p.Name == t.GetType().Name + "es").Single().Name;
                int i = dbcontext.UpdateEntity(dbt, entityName); //自定更新扩展方法
                dbcontext.SaveChanges();

                return "";
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return LogHelper.Parse(ex); ;
            }
        }
   
        public static string DeleteById<T>(ObjectContext dbcontext, string PkName, object PkValue) where T : EntityObject
        {
            try
            {
                string tablename = typeof(T).Name;
                string CommandText = "";
                Guid guid;

                if (Guid.TryParse(PkValue.ToString(), out guid)) //Guid)
                {
                    CommandText = "delete  from " + tablename + " where " + PkName + "='" + PkValue.ToString() + "'";
                }
                else
                {
                    int intid;

                    if (int.TryParse(PkValue.ToString(), out intid))
                        CommandText = "delete  from " + tablename + " where " + PkName + "=" + PkValue.ToString();
                    else
                    {
                        CommandText = "delete  from " + tablename + " where " + PkName + "='" + PkValue.ToString() + "'";
                    }
                }


                return ExecuteSql(dbcontext,CommandText);
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return LogHelper.Parse(ex); ;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where">where 不能为空，危险条件</param>
        /// <returns></returns>
        public static string DeleteBysqlwhere<T>(ObjectContext dbcontext, string where)
        {
            try
            {
                string tablename = typeof(T).Name;
                string CommandText = "delete  from " + tablename + " where " + where;
                dbcontext.ExecuteStoreCommand(CommandText);

                return "";
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return LogHelper.Parse(ex); 
            }
        }
        public static T GetByGuidPkWithMaxDt<T>(ObjectContext dbcontext, string PkName, object PkValue, string orderby)
        {
            try
            {
                string tablename = typeof(T).Name;
                string CommandText = "";
                if (PkValue is Guid)
                {
                    CommandText = "select top 1 * from " + tablename + " where " + PkName + "='" + PkValue.ToString() + "' order by " + orderby;
                }
                else if (PkValue is int)
                {
                    CommandText = "select top 1 * from " + tablename + " where " + PkName + "=" + PkValue.ToString() + " order by " + orderby;
                }
                else
                {
                    CommandText = "select top 1 * from " + tablename + " where " + PkName + "='" + PkValue.ToString() + "' order by " + orderby;
                }

                String entityName = dbcontext.GetType().GetProperties().Where(p => p.Name == tablename || p.Name == tablename + "s" || p.Name == tablename + "es").Single().Name;
                var obj = dbcontext.ExecuteStoreQuery<T>(CommandText, entityName, System.Data.Objects.MergeOption.AppendOnly, null)
                       .SingleOrDefault<T>();
                return obj;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return default(T);
            }
        }


        public static T GetByPk<T>(ObjectContext dbcontext, string PkName, object PkValue) where T : EntityObject
        {
            try
            {
                string tablename = typeof(T).Name;
                string CommandText = "";
                if (PkValue is Guid)
                {
                    CommandText = "select * from " + tablename + " where " + PkName + "='" + PkValue.ToString() + "'";
                }
                else if (PkValue is int)
                {
                    CommandText = "select * from " + tablename + " where " + PkName + "=" + PkValue.ToString();
                }
                else
                {
                    CommandText = "select * from " + tablename + " where " + PkName + "='" + PkValue.ToString() + "'";
                }

                String entityName = dbcontext.GetType().GetProperties().Where(p => p.Name == tablename || p.Name == tablename + "s" || p.Name == tablename + "es").Single().Name;
                var obj = dbcontext.ExecuteStoreQuery<T>(CommandText, entityName, System.Data.Objects.MergeOption.AppendOnly, null)
                       .SingleOrDefault<T>();
                return obj;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return default(T);
            }
        }


        public static T GetBySql<T>(ObjectContext dbcontext, string conditionSql) where T : EntityObject
        {
            try
            {
                string tablename = typeof(T).Name;
                string CommandText = "select * from " + tablename + " where " + conditionSql;


                String entityName = dbcontext.GetType().GetProperties().Where(p => p.Name == tablename || p.Name == tablename + "s" || p.Name == tablename + "es").Single().Name;

                var obj = dbcontext.ExecuteStoreQuery<T>(CommandText, entityName, System.Data.Objects.MergeOption.AppendOnly, null)
                       .SingleOrDefault<T>();
                return obj;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return default(T);
            }
        }


        public static List<T> GetListwithPaging<T>(ObjectContext dbcontext, string conditions, string orderbys, int pageNum, int numPerPage, out int totalCount) where T : class, new()
        {
            totalCount = 0;

            try
            {

                var blist = Pagination<T>.PagingWithCount(dbcontext, conditions, orderbys, numPerPage, pageNum, out totalCount);
                if (blist != null)
                {
                    return blist.ToList<T>();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return null;
            }
        }


        public static List<T> GetListNoPaging<T>(ObjectContext dbcontext, string sql, string orderbys) where T : EntityObject //class,new()
        {
            try
            {

                var blist = Pagination<T>.SelectAll(dbcontext, sql, orderbys);
                if (blist != null)
                {
                    return blist.ToList<T>();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return null;
            }
        }

    }
}