using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Reflection;
using log4net;
using QyTech.Core.Helpers;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using QyTech.Core;
using System.Data.Objects;
using QyTech.Core.BLL;
using QyTech.Json;
using QyTech.Core.Common;
using System.Collections;
using QyTech.Core.ExController.Bll;

using QyExpress.Dao;
using QyTech.ExcelOper;

namespace QyTech.Core.ExController
{
    public partial class QyTechController
    {

        /// <summary>
        /// 获取数据列表，字段区分大小写
        /// </summary>
        /// <param name="fields">要显示的字段，为空则表示显示全部</param>
        /// <param name="where">keyvalue方式的json串</param>
        /// <param name="orderby">不含order by的完整排序sql</param>
        /// <returns>json数据</returns>
        //[HttpPost]
        public virtual string GetAll(string sessionid, string fields="", string where="", string orderby="")
        {
             try
            {
                AddLogTable("获取", bsT.TName, bsT.Desp, where);

                //ObjectClassFullName
                if (where.Length > 0)
                    where = AjustWhereSql(where);
                //if (orderby == "")
                //    orderby = bsFC.OrderBySql;
                object objs = dataManager.GetObjects(where, orderby);
                Type type = Type.GetType(objClassFullName);//.Replace(strForReplaceObject, objNameSpace + "." + objClassName));

                if (type != null && objs != null)
                {
                    if (fields == null || fields.Equals(""))
                    {
                        List<string> dispitems = null;
                        if (bsFC != null)
                        {
                           try
                            {
                                dispitems = bllbsUserFields.GetUserNeedDispListItemDesps(EManagerApp_, LoginUser.bsU_Id, bsFC);
                            }
                            catch (Exception ex)
                            {
                                LogHelper.Error(ex.Message);
                            }
                        }
                        return jsonMsgHelper.Create(0, objs, "", type, dispitems);
                    }
                    else
                    {
                        return jsonMsgHelper.CreateWithStrField(0, objs, "", type, fields);
                    }
                }
                else
                {
                    //提示错误
                   //return jsonMsgHelper.CreateWithStrField(1, "","类型错误！", type, fields);
                   return jsonMsgHelper.Create(1, "", "类型错误！");

                }

            }
            catch (Exception ex)
            {
                LogHelper.Error("Add:" ,ex.Message);
                return jsonMsgHelper.Create(1, "", "获取失败！");
            }
        }

        /// <summary>
        /// 获取列表数组，需要前端控制分页
        /// </summary>
        /// <param name="sessionid"></param>
        /// <param name="fields"></param>
        /// <param name="where"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public virtual string GetAllData(string sessionid, string fields = "", string where = "", string orderby = "")
        {
            AddLogTable("获取", bsT.TName, bsT.Desp, where);
            if (where.Length > 0)
                where = AjustWhereSql(where);
            //如果表中有bsO_Id字段，还要获取数据的部门权限

            //if (orderby == "" && (bsFC.OrderBySql!="" && bsFC.OrderBySql!=null))
            //    orderby = bsFC.OrderBySql;
            try
            {
                object objs = dataManager.GetObjects(where, orderby);
                Type type = Type.GetType(objClassFullName);//.Replace(strForReplaceObject, objNameSpace + "." + objClassName));

                if (type != null && objs != null)
                {
                    if (fields == null || fields.Equals(""))
                    {
                        List<string> dispitems = null;
                        if (bsFC != null)
                        {
                            try
                            {
                                dispitems = bllbsUserFields.GetUserNeedDispListItemDesps(EManagerApp_, LoginUser.bsU_Id, bsFC);
                            }
                            catch (Exception ex)
                            {
                                LogHelper.Error(ex.Message);
                            }
                        }
                        if (objs != null)
                        {
                            if (objs.GetType().FullName.Contains("System.Collections.Generic.List") && type != null)
                                return QyTech.Json.JsonHelper.SerializeObject(objs, type, dispitems);
                            else
                                return QyTech.Json.JsonHelper.SerializeObject(objs, type, dispitems);
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    //提示错误
                    //return jsonMsgHelper.CreateWithStrField(1, "","类型错误！", type, fields);
                    return null;

                }

            }
            catch (Exception ex)
            {
                LogHelper.Error("getalldata:", ex.Message);
                return null;
            }

        }


        /// <summary>
        /// 非存储过程获取分页数据
        /// </summary>
        /// <param name="fields">要显示的字段，为空则表示显示全部</param>
        /// <param name="where">不含where的完整sql条件</param>
        /// <param name="orderby">不含order by的完整排序sql</param>
        /// <returns>json数据</returns>
        public virtual string GetAllWithPaging(string sessionid, string fields="", string where="", string orderby="", int currentPage = 1, int pageSize = 20)
        {
            AddLogTable("分页获取", bsT.TName, bsT.Desp, where);
            try
            {
               
                if (where!="")
                    where = AjustWhereSql(where);
                
                int totalCount = 100;
                int totalPage = (int)Math.Ceiling(1.0 * totalCount / pageSize);

                object objs = dataManager.GetObjectsWithPaging(where, orderby, out totalCount, out totalPage, currentPage, pageSize);

                Type type = Type.GetType(objClassFullName);//.Replace(strForReplaceObject, objNameSpace + "." + objClassName));

                if (fields == null || fields.Equals(""))
                {
                    List<string> dispitems = null;
                    if (bsFC != null)
                    {
                        try
                        {
                            dispitems = bllbsUserFields.GetUserNeedDispListItemDesps(EManagerApp_, LoginUser.bsU_Id, bsFC);
                        }
                        catch (Exception ex)
                        {
                            LogHelper.Error(ex.Message);
                        }
                    }
                        return jsonMsgHelper.CreatePage(0, objs, currentPage, pageSize, totalCount, totalPage, "", type, dispitems);
                }
                else
                {
                    return jsonMsgHelper.CreatePageWithStrField(0, objs, currentPage, pageSize, totalCount, totalPage, "", type, fields);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error( ex);
                return jsonMsgHelper.Create(1, "", "获取失败");
            }
        }





        ///// <summary>
        ///// 获取数据列表，字段区分大小写
        ///// </summary>
        ///// <param name="fields">要显示的字段，为空则表示显示全部</param>
        ///// <param name="where">keyvalue方式的json串</param>
        ///// <param name="orderby">不含order by的完整排序sql</param>
        ///// <returns>json数据</returns>
        ////[HttpPost]
        //public virtual string GetAllWithUser(string bsU_Id, string fields = "", string where = "", string orderby = "")
        //{
        //    //ObjectClassFullName
        //    if (where.Length > 0)
        //        where = AjustWhereSql(where);
        //    //if (orderby == "")
        //    //    orderby = bsFC.OrderBySql;
        //    try
        //    {
        //        object objs = dataManager.GetObjects(where, orderby);
        //        Type type = Type.GetType(objClassFullName);//.Replace(strForReplaceObject, objNameSpace + "." + objClassName));

        //        if (type != null && objs != null)
        //        {
        //            if (fields == null || fields.Equals(""))
        //            {
        //                List<string> dispitems = null;
        //                try
        //                {
        //                    dispitems = bllbsUserFields.GetUserNeedDispListItemDesps(EManagerApp_, LoginUser.bsU_Id, bsFC);
        //                }
        //                catch { }
        //                return jsonMsgHelper.Create(0, objs, "", type, dispitems);
        //            }
        //            else
        //            {
        //                return jsonMsgHelper.CreateWithStrField(0, objs, "", type, fields);
        //            }
        //        }
        //        else
        //        {
        //            //提示错误
        //            //return jsonMsgHelper.CreateWithStrField(1, "","类型错误！", type, fields);
        //            return jsonMsgHelper.Create(1, "", "类型错误！");

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.Error("Add:", ex.Message);
        //        return jsonMsgHelper.Create(1, "", ex.Message);
        //    }
        //}



        ///// <summary>
        ///// 非存储过程获取分页数据
        ///// </summary>
        ///// <param name="fields">要显示的字段，为空则表示显示全部</param>
        ///// <param name="where">不含where的完整sql条件</param>
        ///// <param name="orderby">不含order by的完整排序sql</param>
        ///// <returns>json数据</returns>
        //public virtual string GetAllWithPagingWithUser(string bsU_Id, string fields = "", string where = "", string orderby = "", int currentPage = 1, int pageSize = 20)
        //{
        //    try
        //    {

        //        if (where != "")
        //            where = AjustWhereSql(where);

        //        int totalCount = 100;
        //        int totalPage = (int)Math.Ceiling(1.0 * totalCount / pageSize);

        //        object objs = dataManager.GetObjectsWithPaging(where, orderby, out totalCount, out totalPage, currentPage, pageSize);

        //        Type type = Type.GetType(objClassFullName);//.Replace(strForReplaceObject, objNameSpace + "." + objClassName));

        //        if (fields == null || fields.Equals(""))
        //        {
        //            List<string> dispitems = bllbsUserFields.GetUserNeedDispListItemDesps(EManagerApp_, LoginUser.bsU_Id, bsFC);
        //            return jsonMsgHelper.CreatePage(0, objs, currentPage, pageSize, totalCount, totalPage, "", type, dispitems);
        //        }
        //        else
        //        {
        //            return jsonMsgHelper.CreatePageWithStrField(0, objs, currentPage, pageSize, totalCount, totalPage, "", type, fields);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.Error("GetAllWithPaging:", ex.Message);
        //        return jsonMsgHelper.Create(1, "", ex.Message);
        //    }
        //}


    }
}
