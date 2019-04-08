using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Reflection;
using log4net;
using QyTech.Core.Helpers;
using QyExpress.Dao;
using QyTech.Core;
using System.Data.Objects;
using QyTech.Core.BLL;
using QyTech.Json;
using QyTech.Core.Common;
using System.Collections;
using QyTech.Core.ExController.Bll;

namespace QyTech.Core.ExController.Bll
{
    /// <summary>
    /// 通用此类访问数据库操作类EntityManager中的获取数据的方法
    /// add，edit，delete方法不在这里，直接再Dao中泛型调用
    /// </summary>
    public class DataManager
    {
        private EntityManager EManager_;
        private string objClassFullName;//包含namespace

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eManager">数据库访问对象</param>
        /// <param name="objclassfullname">获取类</param>
        public DataManager(EntityManager eManager,string objclassfullname)
        {
            EManager_ = eManager;
            objClassFullName = objclassfullname;
        }


        /// <summary>
        /// 通用获取数据列表
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public object GetObjects(string where = "", string orderby = "")
        {
             try
            {
                Type typeEm = typeof(EntityManager);

                Type dbtype = Type.GetType(objClassFullName);

                object dbobj = dbtype.Assembly.CreateInstance(dbtype.FullName);
                //MethodInfo[] mis=typeEm.GetMethods();
                MethodInfo miObj = typeEm.GetMethod("GetListNoPaging",new Type[] { typeof(String), typeof(String)}).MakeGenericMethod(dbtype);//获取泛型类方法,不能有重名的，否则找不到，2018-10-06有一个错误，就是一个实例，一个静态，报错了
                object objs = miObj.Invoke(EManager_, new object[] { where, orderby });

                return objs;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return null;
            }
        }

        /// <summary>
        /// 根据where条件数据和排序条件获取数据列表
        /// </summary>
        /// <param name="spname"></param>
        /// <param name="wheres"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public object GetObjects(string spname, object[] wheres, string orderby)
        {
            object dbobj, objs;
            Type dbtype;
            MethodInfo miObj;
            Type typeEm = typeof(EntityManager);
            dbtype = Type.GetType(objClassFullName);


            dbobj = dbtype.Assembly.CreateInstance(dbtype.FullName);
            miObj = typeEm.GetMethod("GetAllByStorProcedure").MakeGenericMethod(dbtype);

            Object[] myArgs = new Object[wheres.Length + 5];
            int index = 0;
            for (int i = 0; i < wheres.Length; i++)
            {
                myArgs[index++] = wheres[index++];
            }
            myArgs[index++] = orderby;
            myArgs[index++] = null;


            objs = miObj.Invoke(EManager_, myArgs);

            return objs;
        }

     
        /// <summary>
        /// 通用根据存储过程和参数数组获取对象列表，具体数据由参数决定
        /// </summary>
        /// <param name="spname"></param>
        /// <param name="paramvalues"></param>
        /// <returns></returns>
        public object GetObjects(string spname, ref object[] paramvalues)
        {
            object dbobj, objs;
            Type dbtype;
            MethodInfo miObj;
            Type typeEm = typeof(EntityManager);
            dbtype = Type.GetType(objClassFullName);


            dbobj = dbtype.Assembly.CreateInstance(dbtype.FullName);
            miObj = typeEm.GetMethod("GetAllByStorProcedure").MakeGenericMethod(dbtype);

            Object[] myArgs = new Object[paramvalues.Length + 5];
            int index = 0;
            for (int i = 0; i < paramvalues.Length; i++)
            {
                myArgs[index++] = paramvalues[index++];
            }
            objs = miObj.Invoke(EManager_, myArgs);

            return objs;
        }


        /// <summary>
        /// 非存储过程分页获取对象列表
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderby"></param>
        /// <param name="totalCount"></param>
        /// <param name="totalPage"></param>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public object GetObjectsWithPaging(string where, string orderby
           , out int totalCount, out int totalPage, int currentPage = 1, int pageSize = 20)
        {
            object dbobj, objs;
            Type dbtype;
            MethodInfo miObj;
            Type typeEm = typeof(EntityManager);
            //dbtype = Type.GetType(objClassFullName.Replace(strForReplaceObject, objNameSpace + "." + objClassName));
            dbtype = Type.GetType(objClassFullName);


            dbobj = dbtype.Assembly.CreateInstance(dbtype.FullName);
            miObj = typeEm.GetMethod("GetListwithPaging").MakeGenericMethod(dbtype);
            totalCount = 0;
            Object[] myArgs = new Object[5] { where, orderby, currentPage, pageSize, totalCount };
            objs = miObj.Invoke(EManager_, myArgs);

            totalCount = (int)myArgs[4];
            totalPage = (int)Math.Ceiling(1.0 * totalCount / pageSize);

            return objs;
        }


      /// <summary>
      /// 
      /// </summary>
      /// <param name="spname"></param>
      /// <param name="wheres"></param>
      /// <param name="orderby"></param>
      /// <param name="totalCount"></param>
      /// <param name="totalPage"></param>
      /// <param name="currentPage"></param>
      /// <param name="pageSize"></param>
      /// <returns></returns>
        public object GetObjectsWithStorePaging(string spname, object[] wheres, string orderby
        , out int totalCount, out int totalPage, int currentPage = 1, int pageSize = 20)
        {
            object dbobj, objs;
            Type dbtype;
            MethodInfo miObj;
            Type typeEm = typeof(EntityManager);
            dbtype = Type.GetType(objClassFullName);


            dbobj = dbtype.Assembly.CreateInstance(dbtype.FullName);
            miObj = typeEm.GetMethod("GetAllByStorProcedure").MakeGenericMethod(dbtype);

            Object[] myArgs = new Object[wheres.Length + 5];
            int index = 0;
            for (int i = 0; i < wheres.Length; i++,index++)
            {
                myArgs[index] = wheres[i];
            }
            myArgs[index++] = orderby;
            myArgs[index++] = currentPage;
            myArgs[index++] = pageSize;
            myArgs[index++] = null;


            objs = miObj.Invoke(EManager_, myArgs);

            totalCount = (int)myArgs[4];
            totalPage = (int)Math.Ceiling(1.0 * totalCount / pageSize);

            return objs;
        }

        public object GetObjectsWithCommonStorePaging(string tname, string wheres, string orderbyField,bool isAscDire,out int totalCount, out int totalPage, int currentPage = 1, int pageSize = 20)
        {
            object dbobj, objs;
            Type dbtype;
            MethodInfo miObj;
            Type typeEm = typeof(EntityManager);
            dbtype = Type.GetType(objClassFullName);

            dbobj = dbtype.Assembly.CreateInstance(dbtype.FullName);
            miObj = typeEm.GetMethod("GetAllByStorProcedure").MakeGenericMethod(dbtype);

            Object[] myArgs = new Object[8];
            myArgs[0] = tname;
            myArgs[1] = "";
            myArgs[2] = wheres;
            myArgs[3] = orderbyField;
            myArgs[4] = pageSize;
            myArgs[5] = currentPage;
            myArgs[6] = 0;
            myArgs[7] = isAscDire?0:1;
            
         
    //        @TableName          VARCHAR(4000),           -- 表名
    //@SelectField        VARCHAR(4000),           -- 要显示的字段名(不要加select)
    //@WhereConditional   VARCHAR(4000),           -- 查询条件(注意: 不要加 where)
    //@SortExpression     VARCHAR(255),            -- 排序索引字段名
    //@PageSize           INT = 20,                -- 页大小
    //@PageIndex          INT = 1,                 -- 页码
    //@RecordCount        INT OUTPUT,              -- 返回记录总数
    //@SortDire           VARCHAR(5) = 'DESC' 

            objs = miObj.Invoke(EManager_, myArgs);

            totalCount = (int)myArgs[4];
            totalPage = (int)Math.Ceiling(1.0 * totalCount / pageSize);

            return objs;
        }
    }
}
