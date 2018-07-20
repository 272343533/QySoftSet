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
using QyTech.Core.ExController.Bll;


namespace QyTech.Core.ExController
{
    public partial class QyTechController
    {

        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <param name="controllername">可省略，省略时取路由控制器</param>
        /// <param name="actionname">可省略，省略时取getall</param>
        /// <returns>json数据</returns>
        public string GetFunQueryCondition()//string controllername, string actionname)
        {
            try
            {
                FunQueryCondition obj;
                //if (actionname != null)
                //{
                //    if (controllername == null || controllername == "")
                //        obj= bllUIOperator.GetFunQueryConditions(EManager_, RouteData.Values["controller"].ToString(), actionname);
                //    else
                //        obj= bllUIOperator.GetFunQueryConditions(EManager_, controllername, actionname);
                //}
                //else
                //    obj= bllUIOperator.GetFunQueryConditions(EManager_, RouteData.Values["controller"].ToString(),"getall");

                obj = bllUIOperator.GetFunQueryConditions(EManager_, bsFC_Id);

                return jsonMsgHelper.Create(0, obj, "");
            }
            catch (Exception ex)
            {
                log.Error("Add:" + ex.Message);
                return jsonMsgHelper.Create(1, "", ex.Message);
            }
        }

        /// <summary>
        /// 获取界面操作按钮数据
        /// </summary>
        /// <param name="controllername">可省略，省略时取路由控制器</param>
        /// <param name="actionname">可省略，省略时取getall</param>
        /// <returns>json数据</returns>
        public string GetFunOperations()//string controllername, string actionname)
        {
            try
            {
                List<FunOperation> objs;
                //if (actionname != null)
                //{
                //    if (controllername == null || controllername == "")
                //        objs = bllUIOperator.GetFunOperations(EManager_, RouteData.Values["controller"].ToString(), actionname);
                //    else
                //        objs = bllUIOperator.GetFunOperations(EManager_, controllername, actionname);
                //}
                //else
                //    objs = bllUIOperator.GetFunOperations(EManager_, RouteData.Values["controller"].ToString(), "getall");
                objs = bllUIOperator.GetFunOperations(EManager_, bsFC_Id, ItemPos.top);
                return jsonMsgHelper.Create(0, objs, "");
            }
            catch (Exception ex)
            {
                log.Error("Add:" + ex.Message);
                return jsonMsgHelper.Create(1, "", ex.Message);
            }
        }


        /// <summary>
        ///  获取查询列表的表格头配置
        /// </summary>
        /// <returns>json数据</returns>
        public string GetFunQueryListItemConf()
        {
            try
            {
                List<listReadDataItemSet> objs = bllbsUserFields.GetQueryListDispItemDesps(EManager_, LoginUser.bsU_Id, bsFC_Id);

                //然后获取行操作内容
                List<FunOperation> operobjs = bllUIOperator.GetFunOperations(EManager_, bsFC_Id, ItemPos.rowend);
                if (operobjs.Count > 0)
                {
                    listReadDataItemSet dis = new listReadDataItemSet();
                    dis.FDesp = "操作";
                    dis.columnType = ColumnType.col_operation;
                    dis.RowOpers = new List<FunOperation>();
                    foreach (FunOperation fo in operobjs)
                    {
                        dis.RowOpers.Add(fo);
                    }
                    objs.Add(dis);
                }

                return jsonMsgHelper.Create(0, objs, "");
            }
            catch (Exception ex)
            {
                log.Error("Add:" + ex.Message);
                return jsonMsgHelper.Create(1, "", ex.Message);
            }
        }

        /// <summary>
        /// 获取查询Form的查询配置
        /// </summary>
        /// <returns></returns>
        public string GetFunQueryFormItemConf()
        {
            try
            {
                List<formReadDataItemSet> objs = bllbsUserFields.GetQuerFormDispItemDesps(EManager_, LoginUser.bsU_Id, bsFC_Id);

                return jsonMsgHelper.Create(0, objs, "");
            }
            catch (Exception ex)
            {
                log.Error("Add:" + ex.Message);
                return jsonMsgHelper.Create(1, "", ex.Message);
            }
        }

        /// <summary>
        ///  获取查询列表的表格头配置
        /// </summary>
        /// <returns>json数据</returns>
        public string GetFunEditListItemConf()
        {
            try
            {
                List<listWriteDataItemSet> objs = bllbsUserFields.GetEditListDispItemDesps(EManager_, LoginUser.bsU_Id, bsFC_Id);

                //然后获取行操作内容
                List<FunOperation> operobjs = bllUIOperator.GetFunOperations(EManager_, bsFC_Id, ItemPos.rowend);
                if (operobjs.Count > 0)
                {
                    listWriteDataItemSet dis = new listWriteDataItemSet();
                    dis.FDesp = "操作";
                    dis.columnType = ColumnType.col_operation;
                    dis.RowOpers = new List<FunOperation>();
                    foreach (FunOperation fo in operobjs)
                    {
                        dis.RowOpers.Add(fo);
                    }
                    objs.Add(dis);
                }

                return jsonMsgHelper.Create(0, objs, "");
            }
            catch (Exception ex)
            {
                log.Error("Add:" + ex.Message);
                return jsonMsgHelper.Create(1, "", ex.Message);
            }
        }

        ///////// <summary>
        /////////  获取列表的表格头的内容
        ///////// </summary>
        ///////// <param name="controllername">表格项的控制器，可省略</param>
        ///////// <param name="actionname">表格项的action，可省略</param>
        ///////// <returns>json数据</returns>
        //////public string GetFunListItemConf(string controllername, string actionname)
        //////{
        //////    List<DataItemSet> objs;


        //////    //首先获取列数据
        //////    if (controllername == null || controllername == "")
        //////    {
        //////        if (actionname == null)
        //////            objs = bllbsUserFields.GetUserNeedDispListItemDesps(EManager_, LoginUser.bsU_Id,bsFC_Id);
        //////        else
        //////            objs = bllbsUserFields.GetUserNeedDispListItemDesps(EManager_, LoginUser.bsU_Id, RouteData.Values["controller"].ToString(), actionname);

        //////    }
        //////    else
        //////        if (actionname == null || actionname == "")
        //////            objs = bllbsUserFields.GetUserNeedDispListItemDesps(EManager_, LoginUser.bsU_Id, controllername, "getall");
        //////        else
        //////            objs = bllbsUserFields.GetUserNeedDispListItemDesps(EManager_, LoginUser.bsU_Id, controllername, actionname);

        //////    //然后获取行操作内容
        //////    List<FunOperation> operobjs = bllUIOperator.GetFunOperations(EManager_, bsFC_Id, ItemPos.rowend);
        //////    if (operobjs.Count > 0)
        //////    {
        //////        DataItemSet dis = new DataItemSet();
        //////        dis.FDesp = "操作";
        //////        dis.columnType = ColumnType.col_operation;
        //////        dis.RowOpers = new List<FunOperation>();
        //////        foreach (FunOperation fo in operobjs)
        //////        {
        //////            dis.RowOpers.Add(fo);
        //////        }
        //////        objs.Add(dis);
        //////    }

        //////    return jsonMsgHelper.Create(0, objs, "");
        //////}

    }
}
