using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Reflection;
using log4net;
using QyTech.Core.Helpers;
using qyExpress.Dao;
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
        /// <returns>json数据</returns>
        public string GetFunQueryCondition(string sessionid)
        {
            try
            {
                FunQueryCondition obj = new FunQueryCondition();

                List<bsFunQuery> ffs = EManager.GetListNoPaging<bsFunQuery>("bsFC_Id='" + bsFC.bsFC_Id.ToString() + "'", "");
                if (ffs.Count > 0)
                {
                    //不应该使用bsUserFieldRel，这个表实际是用于用户策略使用的，只应用于列表显示时的数据展示，与Form方式无关
                    //应该使用bsFunField //还要获取该功能项的需要显示的项的列表
                    foreach (bsFunQuery item in ffs)
                    {
                        FunQueryItem objqi = new FunQueryItem();
                        objqi.Name = item.FName;
                        objqi.NameTip = item.QueryName;
                        objqi.InputType = (FDataQueryType)Enum.Parse(typeof(FDataQueryType), "QT_" + item.QueryType);
                        objqi.DataEx = item.DataEx;
                        if (objqi.InputType == FDataQueryType.QT_date)
                        {
                            objqi.DefaultValue = DateTime.Now.ToString("yyyy-MM-dd");
                        }
                        else if (objqi.InputType == FDataQueryType.QT_datetime)
                        {
                            objqi.DefaultValue = DateTime.Now.ToString("yyyy-MM-dd 00:00:00");
                        }
                        else if (objqi.InputType == FDataQueryType.QT_time)
                        {
                            objqi.DefaultValue = DateTime.Now.ToString("00:00:00");
                        }
                        else if (objqi.InputType == FDataQueryType.QT_datetime_range)
                        {
                            objqi.DefaultValue = DateTime.Now.ToString("yyyy-MM-dd 00:00:00") + ";" + DateTime.Now.ToString("yyyy-MM-dd 23:59:59");
                        }
                        else if (objqi.InputType == FDataQueryType.QT_daterange)
                        {
                            objqi.DefaultValue = DateTime.Now.ToString("yyyy-MM-dd") + ";" + DateTime.Now.ToString("yyyy-MM-dd");
                        }
                        else if (objqi.InputType == FDataQueryType.QT_time_range)
                        {
                            objqi.DefaultValue = DateTime.Now.ToString("00:00:00") + ";" + DateTime.Now.ToString("23:59:59");
                        }
                        objqi.Size = (int)item.Size;

                        if (item.ItemPos == "left")
                        {
                            obj.Left.Add(objqi);
                        }
                        else if (item.ItemPos == "right")
                            obj.Right.Add(objqi);
                        else if (item.ItemPos == "top")
                            obj.Top.Add(objqi);
                        else if (item.ItemPos == "bottom")
                            obj.Bottom.Add(objqi);
                    }
                }
                if (obj.Top.Count > 0)
                    obj.TopHeight = 100;
                if (obj.Bottom.Count > 0)
                    obj.BottomHeight = 100;
                if (obj.Left.Count > 0)
                    obj.LeftWidth = 300;
                if (obj.Right.Count > 0)
                    obj.RightWidth = 300;


                return jsonMsgHelper.Create(0, obj, "");
            }
            catch (Exception ex)
            {
                LogHelper.Error("Add:" + ex.Message);
                return jsonMsgHelper.Create(1, "", ex.Message);
            }
        }



        /// <summary>
        /// 获取界面操作按钮数据
        /// </summary>
        /// <returns>json数据</returns>
        public string GetFunOperations(string sessionid)
        {
            try
            {
                List<FunOperation>  objs = GetFunOperations(EManager_, bsFC.bsFC_Id, ItemPos.top);
                return jsonMsgHelper.Create(0, objs, "");
            }
            catch (Exception ex)
            {
                LogHelper.Error("GetFunOperations:" + ex.Message);

                return jsonMsgHelper.Create(1, "", ex.Message);
            }
        }




        /// <summary>
        ///  获取查询列表的表格头配置
        /// </summary>
        /// <returns>json数据</returns>
        public string GetFunQueryListItemConf(string sessionid)
        {
            try
            {
                List<listReadDataItemSet> objs = bllbsUserFields.GetQueryListDispItemDesps(EManager_, LoginUser.bsU_Id, bsFC.bsFC_Id);

                //然后获取行操作内容
                List<FunOperation> operobjs = GetFunOperations(EManager_, bsFC.bsFC_Id, ItemPos.rowend);
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
                LogHelper.Error("GetFunQueryListItemConf:" + ex.Message);

                return jsonMsgHelper.Create(1, "", ex.Message);
            }
        }

        /// <summary>
        /// 获取查询Form的查询配置
        /// </summary>
        /// <returns></returns>
        public string GetFunQueryFormItemConf(string sessionid)
        {
            try
            {
                List<formReadDataItemSet> objs = bllbsUserFields.GetQuerFormDispItemDesps(EManager_, LoginUser.bsU_Id, bsFC.bsFC_Id);

                return jsonMsgHelper.Create(0, objs, "");
            }
            catch (Exception ex)
            {
                LogHelper.Error("GetFunQueryFormItemConf:" + ex.Message);

                return jsonMsgHelper.Create(1, "", ex.Message);
            }
        }

        /// <summary>
        ///  获取编辑Form界面的字段设置
        /// </summary>
        /// <returns>json数据</returns>
        public string GetFunEditListItemConf(string sessionid)
        {
            try
            {
                List<listWriteDataItemSet> objs = bllbsUserFields.GetEditListDispItemDesps(EManager_, LoginUser.bsU_Id, bsFC.bsFC_Id);

                //然后获取行操作内容
                List<FunOperation> operobjs = GetFunOperations(EManager_, bsFC.bsFC_Id, ItemPos.rowend);
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
                LogHelper.Error("GetFunEditListItemConf:" + ex.Message);
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




        /// <summary>
        /// 获取指定位置的操作的接口
        /// </summary>
        /// <param name="EManager"></param>
        /// <param name="bsFC_Id"></param>
        /// <param name="ItemPos">行末，表上</param>
        /// <returns></returns>
        private static List<FunOperation> GetFunOperations(EntityManager EManager, Guid bsFC_Id, ItemPos ItemPos)
        {
            List<FunOperation> objs = new List<FunOperation>();

            List<bsFunOper> ffs;
            if (ItemPos == ItemPos.none)
            {
                ffs = EManager.GetListNoPaging<bsFunOper>("bsFC_Id='" + bsFC_Id.ToString() + "'", "");
            }
            else
            {
                ffs = EManager.GetListNoPaging<bsFunOper>("bsFC_Id='" + bsFC_Id.ToString() + "' and btnPos='" + ItemPos.ToString().ToLower() + "'", "");
            }

            if (ffs.Count > 0)
            {
                //不应该使用bsUserFieldRel，这个表实际是用于用户策略使用的，只应用于列表显示时的数据展示，与Form方式无关
                //应该使用bsFunField //还要获取该功能项的需要显示的项的列表
                foreach (bsFunOper item in ffs)
                {
                    FunOperation obj = new FunOperation();
                    obj.btnNameTip = item.OperName;
                    //修改表结构所以暂时注释掉以下4行 zhwsun 2018-10-06
                    //obj.btnType = item.BtnType;
                    //obj.btnSize = item.btnSize;
                    //obj.btnClickType=item.btnClickGetPost;
                    //obj.btnClass = item.btnClass;
                    obj.btnClickUrl = item.UrlServerEx + item.LinkUrl;
                    if (obj.btnClickType.ToLower() == "get")
                        obj.getPagePostUrl = item.UrlServerEx + item.PostUrl;

                    obj.btnPos = (ItemPos)Enum.Parse(typeof(ItemPos), item.btnPositon);
                    obj.pageType = (PageType)Enum.Parse(typeof(PageType), item.NewPageType); ;
                    objs.Add(obj);
                }
            }
            return objs;
        }
    }
}
