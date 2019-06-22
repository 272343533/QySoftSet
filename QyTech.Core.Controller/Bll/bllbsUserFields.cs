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
using System.Web.Routing;

namespace QyTech.Core.ExController.Bll
{

    /// <summary>
    /// 返回用户操作的界面的各项信息，暂时先不管，先把dao接口弄完 noted by zhwusn on 2018-10-12
    /// </summary>
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

                    obj.FEditable = item.FEditable ==null ? false : (bool)item.FEditable;

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
        private static List<string> GetUserNeedDispItemDesps(EntityManager EManager, Guid loginUserId, bsFunConf bsFC, string ListOrForm)
        {

            List<bsFunField> ffs = EManager.GetAllByStorProcedure<bsFunField>("splyGetUserFunConfFields", new object[] { loginUserId, bsFC.bsFC_Id });

            List<string> objs = new List<string>();
            if (ffs.Count > 0)
            {

                //不应该使用bsUserFieldRel，这个表实际是用于用户策略使用的，只应用于列表显示时的数据展示，与Form方式无关
                //应该使用bsFunField //还要获取该功能项的需要显示的项的列表
                foreach (bsFunField item in ffs)
                {
                    if (ListOrForm.ToLower() == "list")
                    {
                        if ((bool)item.VisibleInList || bsFC.TPk == item.FName)
                        {
                            objs.Add(item.FName);
                        }
                    }
                    else
                    {
                        //modiftied by zhwsun on 2018-10-09 原来不知为什么把主键去掉了，又给加上了
                        if ((bool)item.VisibleInForm || item.bsTable.TPk == item.FName)
                        {
                            objs.Add(item.FName);
                        }
                    }
                }
            }
            return objs;
        }


        /// <summary>
        /// 根据路由信息判断是哪个FunConf或bsTable
        /// </summary>
        /// <param name="EManager"></param>
        /// <param name="routeData"></param>
        /// <param name="AccObj"></param>
        /// <returns></returns>
        public static UrlType GetAccessObject(EntityManager EManager, RouteData routeData,ref object AccObj)
        {
            UrlType retype = UrlType.None;
            //要考虑：1是否包含area，2是否是动态路由
            
            string url = "";
            //如何判断是否包括动态路由，下面的条件合适吗？2018-05-19
            //modified by zhwsun on 2018-10-17
            if (!routeData.Values.ContainsKey("dynamicroute"))
            {
                if (!routeData.DataTokens.ContainsKey("area"))
                    url = routeData.Values["controller"].ToString() +"/"+ routeData.Values["action"].ToString();
                else
                    url = routeData.DataTokens["area"]+"/"+ routeData.Values["controller"].ToString() + "/" + routeData.Values["action"].ToString();
            }
            else
            {
                //根据动态路由修改 2018-05-19
                string[] routes = routeData.Values["dynamicroute"].ToString().Split(new char[] { '/' });
                url = routeData.DataTokens["area"]+"/"+ routes[0] + "/" + routes[1];

            }
            vwlyRouteUrl urlobj = EManager.GetBySql<vwlyRouteUrl>("RouteUrl='/"+url+"'");
            if (urlobj != null)
            {
                if (urlobj.IType.ToLower() == "ui")
                {
                    retype = UrlType.UI;
                    AccObj = EManager.GetByPk<bsTable>("bsT_Id", urlobj.Id);
                }
                else
                {
                    retype = UrlType.DAO;

                    bsTInterface tiobj = EManager.GetByPk<bsTInterface>("bsTI_Id", urlobj.Id);
                    AccObj = EManager.GetByPk<bsTable>("bsT_Id", tiobj.bsT_Id);
                }
            }
            return retype;
        }


        /// <summary>
        /// 获取用户需要显示的窗体或浏览的字段列表
        /// </summary>
        /// <param name="EManager"></param>
        /// <param name="loginUserId"></param>
        /// <param name="bsfc"></param>
        /// <returns></returns>
        public static List<string> GetUserNeedDispListItemDesps(EntityManager EManager, Guid loginUserId, bsFunConf bsfc)
        {
            if (((int)bsfc.FunLayout&1)==1)
                return GetUserNeedDispItemDesps(EManager, loginUserId, bsfc, "List");
            else
                return GetUserNeedDispItemDesps(EManager, loginUserId, bsfc, "Form");
        }

        public static Dictionary<string,string> GetbsTFieldsOType(EntityManager EManager, Guid loginUserId, bsTable bst)
        {
            List<bsField> bfs = EManager.GetListNoPaging<bsField>("bsT_Id='"+bst.bsT_Id.ToString()+"'","");
            //bfs=bst.bsField.ToList<bsField>();
            Dictionary<string, string> dicobjs = new Dictionary<string, string>();
            if (bfs.Count > 0)
            {
                foreach (bsField item in bfs)
                { 
                    dicobjs.Add(item.FName,item.OType);
                }
            }
            return dicobjs;
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
