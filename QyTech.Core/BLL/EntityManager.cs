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

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]
namespace QyTech.Core.BLL
{

    public class intObject
    {
        public int intData { get; set; }
    }
    /// <summary>
    /// 实体操作类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class EntityManager
    {
        private static ILog log = LogManager.GetLogger("EntityManager");
        static String entityName = "";

        private ObjectContext db=null;

        public EntityManager(ObjectContext dblink)
        {
            db = dblink;
        }

        public int GetIntBySql(string sql)
        {
            try
            {
                List<intObject> objs = db.ExecuteStoreQuery<intObject>(sql).ToList<intObject>();
                return objs[0].intData;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return -9999;
            }
        }



       

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spName"></param>
        /// <param name="paramvalues"></param>
        /// <returns></returns>
        public List<T> GetAllByStorProcedure<T>(string spName, object[] paramvalues,int outParamIndexFrom1,out object result)
        {
            result = -999;
            try
            {
                string Sqls = "EXEC ["+spName+"] ";
                List<SqlParameter> paramArray = new List<SqlParameter>();
                
                int index=1;
                foreach (object item in paramvalues)
                {
                   if (index == outParamIndexFrom1)
                    {
                        SqlParameter param = new SqlParameter("@Param" + index.ToString(),  SqlDbType.Int);
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
                List<T> obj = db.ExecuteStoreQuery<T>(Sqls, paramArray.ToArray()).ToList<T>();

                result = paramArray[outParamIndexFrom1-1].Value;


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


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spName"></param>
        /// <param name="paramvalues">参数值列表，如果为null，则认为是out参数</param>
        /// <returns></returns>
        public List<T> GetAllByStorProcedure<T>(string spName, ref object[] paramvalues)
        {
            try
            {
                string Sqls = "EXEC [" + spName + "] ";
                List<SqlParameter> paramArray = new List<SqlParameter>();

                int index = 1;
                foreach (object item in paramvalues)
                {
                    if (item ==null)
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
                List<T> obj = db.ExecuteStoreQuery<T>(Sqls, paramArray.ToArray()).ToList<T>();

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


        /// <summary>
        /// 没有返回参数的执行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spName"></param>
        /// <param name="paramvalues"></param>
        /// <returns></returns>
        public List<T> GetAllByStorProcedure<T>(string spName, object[] paramvalues)
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
                List<T> obj = db.ExecuteStoreQuery<T>(Sqls, paramArray.ToArray()).ToList<T>();

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

        public List<T> GetPagingByStorProcedure<T>(string spName, string paramvalues, int pageNum, int numPerPage, out int totalCount)
        {
            totalCount = 0;
            try
            {

                List<T> list = db.ExecuteStoreQuery<T>("exec " + spName + " " + paramvalues).ToList<T>();
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
  

        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="t">要添加的实体对象</param>
        /// <returns></returns>
        public string Add<T>(T t)
        {
            try
            {
                entityName = db.GetType().GetProperties().Where(p => p.Name == t.GetType().Name || p.Name == t.GetType().Name + "s" || p.Name == t.GetType().Name + "es").Single().Name;
                db.AddObject(entityName, t);
                db.SaveChanges();
                return "";
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return LogHelper.Parse(ex); 
            }
        }

 
        public string Adds<T>(List<T> ts)
        {
            try
            {
                if (ts.Count > 0)
                {

                    entityName = db.GetType().GetProperties().Where(p => p.Name == ts[0].GetType().Name || p.Name == ts[0].GetType().Name + "s" || p.Name == ts[0].GetType().Name + "es").Single().Name;
                    foreach (T t in ts)
                    {
                        db.AddObject(entityName, t);
                    }
                    db.SaveChanges();
                }
                return "";
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return LogHelper.Parse(ex);
            }
        }
        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="t">要添加的实体对象</param>
        /// <returns></returns>
        public T AddReturnEntity<T>(T t, out string errmsg)
        {
            errmsg = "";
            try
            {

                entityName = db.GetType().GetProperties().Where(p => p.Name == t.GetType().Name || p.Name == t.GetType().Name + "s" || p.Name == t.GetType().Name + "es").Single().Name;
                db.AddObject(entityName, t);
                db.SaveChanges();
                return t;
            }
            catch (Exception ex)
            {
                errmsg = ex.Message;
                LogHelper.Error(ex);
                return default(T);
            }
        }
   
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public string Modify<T>( T t) where T : EntityObject
        {
            try
            {
                entityName = db.GetType().GetProperties().Where(p => p.Name == t.GetType().Name || p.Name == t.GetType().Name + "s" || p.Name == t.GetType().Name + "es").Single().Name;
                int i = db.UpdateEntity(t, entityName); //自定更新扩展方法
                db.SaveChanges();

                return "";
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return LogHelper.Parse(ex);
            }
        }

        /// <summary>
        /// 修改实体，默认使用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="pkName"></param>
        /// <param name="pkValue"></param>
        /// <returns></returns>
        public string ModifyByIdFName<T>(T t,string pkName,object pkValue) where T : EntityObject
        {
            try
            {
                T dbt = GetByPk<T>(pkName, pkValue);
                EntityOperate.Copy<T>(t, dbt, pkName);

                entityName = db.GetType().GetProperties().Where(p => p.Name == t.GetType().Name || p.Name == t.GetType().Name + "s" || p.Name == t.GetType().Name + "es").Single().Name;
                int i = db.UpdateEntity(dbt, entityName); //自定更新扩展方法
                db.SaveChanges();

                return "";
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return LogHelper.Parse(ex);
            }
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        /*public string Delete<T>( T t)
        {
            try
            {
                db.DeleteObject(t);
                db.SaveChanges();
                return "";
            }
            catch (Exception ex)
            {
                string errmsg = ExceptionMessage.Parse(ex);
                LogHelper.Error(ex);
                return errmsg;
            }
        }*/
        public string DeleteById<T>(string PkName, object PkValue) where T : EntityObject
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


                return ExecuteSql(CommandText);
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return LogHelper.Parse(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where">where 不能为null，危险条件</param>
        /// <returns></returns>
        public string DeleteBysqlwhere<T>(string where)
        {
            try
            {
                string tablename = typeof(T).Name;
                string CommandText = "delete  from " + tablename + " where " + where;
                db.ExecuteStoreCommand(CommandText);//返回影响的行数
                return "";
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return LogHelper.Parse(ex);
            }
        }

        public string ExecuteSql(string sql)
        {
            try
            {
                db.ExecuteStoreCommand(sql);//返回影响的行数
                return "";
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return LogHelper.Parse(ex);//返回影响的行数;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public int ExcuteStoreProcedure(string spName, string paras)
        {
            if (paras.ToLower().Contains("delete"))
            {
                return -9999;
            }

            string Sqls = "EXEC [" + spName + "] ";

            List<SqlParameter> paramArray = new List<SqlParameter>();
            string[] strs = paras.Split(new char[] { ',' });
            int index = 1;
            foreach (string item in strs)
            {
                Sqls += "@Param" + index.ToString() + ",";
                paramArray.Add(new SqlParameter("@Param" + index.ToString(), item));
                index++;
            }

            Sqls = Sqls.Substring(0, Sqls.Length - 1);
            int ret = db.ExecuteStoreCommand(Sqls, paramArray.ToArray());
            return ret;
        }

        public string  ExcuteStoreProcedure(string spName, object[] paramvalues)
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
                db.ExecuteStoreCommand(Sqls, paramArray.ToArray());
                return "";
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
                return LogHelper.Parse(ex);
            }
        }

        public T GetByGuidPkWithMaxDt<T>( string PkName, object PkValue, string orderby)
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

                String entityName = db.GetType().GetProperties().Where(p => p.Name == tablename || p.Name == tablename + "s" || p.Name == tablename + "es").Single().Name;
                var obj = db.ExecuteStoreQuery<T>(CommandText, entityName, System.Data.Objects.MergeOption.AppendOnly, null)
                       .SingleOrDefault<T>();
                return obj;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return default(T);
            }
        }
 
        public T GetByPk<T>( string PkName, object PkValue) where T : EntityObject
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
                
                String entityName = db.GetType().GetProperties().Where(p => p.Name == tablename || p.Name == tablename + "s" || p.Name == tablename + "es").Single().Name;
                var obj = db.ExecuteStoreQuery<T>(CommandText, entityName, System.Data.Objects.MergeOption.AppendOnly, null)
                       .SingleOrDefault<T>();
                return obj;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return default(T);
            }
        }
 
        public T GetBySql<T>(string conditionSql) where T : EntityObject
        {
            try
            {
                string tablename = typeof(T).Name;
                string CommandText = "select * from " + tablename + " where " + conditionSql;


                String entityName = db.GetType().GetProperties().Where(p => p.Name == tablename || p.Name == tablename + "s" || p.Name == tablename + "es").Single().Name;

                var obj = db.ExecuteStoreQuery<T>(CommandText, entityName, System.Data.Objects.MergeOption.AppendOnly, null)
                       .SingleOrDefault<T>();
                return obj;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return default(T);
            }
        }
  
        public List<T> GetListwithPaging<T>( string conditions, string orderbys, int pageNum, int numPerPage, out int totalCount) where T : class,new()
        {
            totalCount = 0;

            try
            {

                var blist = Pagination<T>.PagingWithCount(db, conditions, orderbys, numPerPage, pageNum, out totalCount);
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
       /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">条件</param>
        /// <param name="orderbys">排序</param>
        /// <returns></returns>
        public List<T> GetListNoPaging<T>( string sql, string orderbys) where T : EntityObject //class,new()
        {
            try
            {

                var blist = Pagination<T>.SelectAll(db, sql, orderbys);
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