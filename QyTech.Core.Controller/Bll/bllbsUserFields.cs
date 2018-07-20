using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Reflection;
using log4net;
using QyTech.Core.Helpers;
using QyTech.Auth.Dao;
using QyTech.Core;
using System.Data.Objects;
using QyTech.Core.BLL;
using QyTech.Json;
using QyTech.Core.Common;
using System.Collections;
using System.Web.Routing;

namespace QyTech.Core.ExController.Bll
{



    public class bllbsUserFields
    {


        /// <summary>
        /// 获取用户要显示的查询列表的表头描述内容，Guid都不显示
        /// </summary>
        /// <returns></returns>
        public static List<listReadDataItemSet> GetQueryListDispItemDesps(EntityManager EManager, Guid loginUserId, Guid bsFC_Id)
        {

            List<bsFunField> ffs = EManager.GetAllByStorProcedure<bsFunField>("splyGetUserFunConfFields", new object[] { loginUserId, bsFC_Id });

            List<listReadDataItemSet> objs = new List<listReadDataItemSet>();
            if (ffs.Count > 0)
            {
                foreach (bsFunField item in ffs)
                {
                    listReadDataItemSet obj = new listReadDataItemSet();
                    obj.columnType = ColumnType.col_field;
                    obj.FName = item.FName;
                    obj.FDesp = item.FDesp;
                    obj.FVisible = (bool)item.VisibleInList;
                    obj.FWidth = (int)item.FWidthInList;
                    objs.Add(obj);
                }
            }
            return objs;
        }

        /// <summary>
        /// 获取用户要显示的查询列表的表头描述内容，Guid都不显示
        /// </summary>
        /// <returns></returns>
        public static List<formReadDataItemSet> GetQuerFormDispItemDesps(EntityManager EManager, Guid loginUserId, Guid bsFC_Id)
        {

            List<bsFunField> ffs = EManager.GetAllByStorProcedure<bsFunField>("splyGetUserFunConfFields", new object[] { loginUserId, bsFC_Id });

            List<formReadDataItemSet> objs = new List<formReadDataItemSet>();
            if (ffs.Count > 0)
            {
                foreach (bsFunField item in ffs)
                {
                    formReadDataItemSet obj = new formReadDataItemSet();
                    obj.FName = item.FName;
                    obj.FDesp = item.FDesp;
                    obj.FVisible = (bool)item.VisibleInForm;
                    obj.FWidth = (int)item.FWidthInForm;
                    objs.Add(obj);
                }
            }
            return objs;
        }


        /// <summary>
        /// 获取列表方式的表头数据
        /// </summary>
        /// <returns></returns>
        public static List<listWriteDataItemSet> GetEditListDispItemDesps(EntityManager EManager, Guid loginUserId, Guid bsFC_Id)
        {

            List<bsFunField> ffs = EManager.GetAllByStorProcedure<bsFunField>("splyGetUserFunConfFields", new object[] { loginUserId, bsFC_Id });

            List<listWriteDataItemSet> objs = new List<listWriteDataItemSet>();
            if (ffs.Count > 0)
            {

                //不应该使用bsUserFieldRel，这个表实际是用于用户策略使用的，只应用于列表显示时的数据展示，与Form方式无关
                //应该使用bsFunField //还要获取该功能项的需要显示的项的列表
                foreach (bsFunField item in ffs)
                {
                    listWriteDataItemSet obj = new listWriteDataItemSet();
                    obj.columnType = ColumnType.col_field;
                    obj.FName = item.FName;
                    obj.FDesp = item.FDesp;
                    obj.FVisible = (bool)item.VisibleInList;

                    obj.FEditable = item.FEditable =="0" ? false : true;

                    obj.FDataVerity=new FDataInputVerity();
                    obj.FDataVerity.fdatatype=(FDataType)Enum.Parse(typeof(FDataType), "DT_" + item.OType);

                    obj.FEditType = (FDataEditType)Enum.Parse(typeof(FDataEditType), "ET_" + item.FEditType);
                    obj.FWidth = (int)item.FWidthInList;
                    obj.FRequired = item.FIsNull.HasValue ? !(bool)item.FIsNull : false;
                    objs.Add(obj);
                }
            }
            return objs;
        }

        /// <summary>
        /// 获取列表方式的表头数据
        /// </summary>
        /// <returns></returns>
        public static List<formWriteDataItemSet> GetEditFormDispItemDesps(EntityManager EManager, Guid loginUserId, Guid bsFC_Id)
        {

            List<bsFunField> ffs = EManager.GetAllByStorProcedure<bsFunField>("splyGetUserFunConfFields", new object[] { loginUserId, bsFC_Id });

            List<formWriteDataItemSet> objs = new List<formWriteDataItemSet>();
            if (ffs.Count > 0)
            {

                //不应该使用bsUserFieldRel，这个表实际是用于用户策略使用的，只应用于列表显示时的数据展示，与Form方式无关
                //应该使用bsFunField //还要获取该功能项的需要显示的项的列表
                foreach (bsFunField item in ffs)
                {
                    formWriteDataItemSet obj = new formWriteDataItemSet();
                    obj.FName = item.FName;
                    obj.FDesp = item.FDesp;
                    obj.FVisible = (bool)item.VisibleInList;

                    obj.FDataVerity = new FDataInputVerity();
                    obj.FDataVerity.fdatatype = (FDataType)Enum.Parse(typeof(FDataType), "DT_" + item.OType);

                    obj.FEditType = (FDataEditType)Enum.Parse(typeof(FDataEditType), "ET_" + item.FEditType);
                    obj.FWidth = (int)item.FWidthInList;
                    obj.FRequired = item.FIsNull.HasValue ? !(bool)item.FIsNull : false;
                    objs.Add(obj);
                }
            }
            return objs;
        }

        /// <summary>
        /// 获取用户要显示的字段信息
        /// </summary>
        /// <returns></returns>
        private static List<DataItemSet> GetUserNeedDispItemDesps(EntityManager EManager, Guid loginUserId, bsFunConf bsFC, string ListOrForm)
        {

            List<bsFunField> ffs = EManager.GetAllByStorProcedure<bsFunField>("splyGetUserFunConfFields", new object[] { loginUserId, bsFC.bsFC_Id });

            List<DataItemSet> objs = new List<DataItemSet>();
            if (ffs.Count > 0)
            {

                //不应该使用bsUserFieldRel，这个表实际是用于用户策略使用的，只应用于列表显示时的数据展示，与Form方式无关
                //应该使用bsFunField //还要获取该功能项的需要显示的项的列表
                foreach (bsFunField item in ffs)
                {
                    DataItemSet obj = new DataItemSet();
                    if (ListOrForm.ToLower() == "list")
                    {
                        if ((bool)item.VisibleInList || bsFC.TPk == item.FName)
                        {
                            obj.FName = item.FName;
                            //obj.FDesp = item.FDesp;
                            //obj.FVisible = (bool)item.VisibleInList;
                            //obj.FDatatype = (FDataType)Enum.Parse(typeof(FDataType), "DT_" + item.OType);
                            //obj.FEditType = (FDataEditType)Enum.Parse(typeof(FDataEditType), "ET_" + item.FEditType);
                            //obj.FWidth = (int)item.FWidthInList;
                            //obj.FRequired = item.FIsNull.HasValue ? !(bool)item.FIsNull : false;
                        }
                    }
                    else
                    {
                        if ((bool)item.VisibleInForm)// || item.bsTable.TPk == item.FName)
                        {
                            obj.FName = item.FName;
                        }
                    }
                    objs.Add(obj);


                }
            }
            return objs;
        }


      
        public static Guid GetFunConfId(EntityManager EManager, RouteData routeData)
        {
            Guid fcid;
            if (routeData.Values.Count == 2)
            {
                fcid = GetFunConfId(EManager, routeData.Values["controller"].ToString(), routeData.Values["action"].ToString());
            }
            else
            {
                //根据动态路由修改 2018-05-19
                string[] routes = routeData.Values["dynamicroute"].ToString().Split(new char[] { '/' });
                fcid = GetFunConfId(EManager, routes[0], routes[1]);
            }
            return fcid;
        }
        public static Guid GetFunConfId(EntityManager EManager, string controller, string action)
        {
            bsFunInterface obj = EManager.GetBySql<bsFunInterface>("LinkController='" + controller + "' and LinkAction='" + action + "'");
            if (obj == null)
            {
                obj = EManager.GetBySql<bsFunInterface>("LinkController='" + controller + "' and LinkAction='getall'");
            }

            if (obj != null)
            {
                return obj.bsFC_Id;
            }
            else
                return Guid.Empty;
        }


        public static List<DataItemSet> GetUserNeedDispListItemDesps(EntityManager EManager, Guid loginUserId, bsFunConf bsfc)
        {
            if (((int)bsfc.FunLayout&1)==1)
                return GetUserNeedDispItemDesps(EManager, loginUserId, bsfc, "List");
            else
                return GetUserNeedDispItemDesps(EManager, loginUserId, bsfc, "Form");
        }

        //public static List<DataItemSet> GetUserNeedDispListItemDesps(EntityManager EManager, Guid loginUserId, string controllername,string actionname)
        //{
        //    Guid fcid = GetFunConfId(EManager, controllername, actionname);

        //    return GetUserNeedDispItemDesps(EManager, loginUserId, fcid, "List");
        //}


        ////public static List<DataItemSet> GetUserNeedDispListItemDesps(EntityManager EManager, Guid loginUserId, RouteData RouteData)
        ////{
        ////    return GetUserNeedDispListItemDesps(EManager, loginUserId, RouteData.Values["controller"].ToString(), RouteData.Values["action"].ToString());
        ////}

        ////public static List<DataItemSet> GetUserNeedDispFormItemDesps(EntityManager EManager, Guid loginUserId, RouteData RouteData)
        ////{
        ////    Guid fcid = GetFunConfId(EManager, RouteData.Values["controller"].ToString(), RouteData.Values["action"].ToString());

        ////    return GetUserNeedDispItemDesps(EManager, loginUserId, fcid, "Form");
        ////}
    }
}
