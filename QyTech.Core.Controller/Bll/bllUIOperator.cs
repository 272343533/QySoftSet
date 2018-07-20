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



    public class bllUIOperator
    {


        public static FunQueryCondition GetFunQueryConditions(EntityManager EManager, string controllername, string actionname)
        {
            Guid fcid = bllbsUserFields.GetFunConfId(EManager, controllername,actionname);

            return GetFunQueryConditions(EManager, fcid);
        }
        public static FunQueryCondition GetFunQueryConditions(EntityManager EManager, RouteData routeData)
        {
            Guid fcid = bllbsUserFields.GetFunConfId(EManager, routeData);

            return  GetFunQueryConditions(EManager, fcid);
        }
        public static FunQueryCondition GetFunQueryConditions(EntityManager EManager, Guid bsFC_Id)
        {

           List<bsFunQuery> ffs = EManager.GetListNoPaging<bsFunQuery>("bsFC_Id='" + bsFC_Id.ToString() + "'", "");

           FunQueryCondition obj = new FunQueryCondition();

           if (ffs.Count > 0)
            {
                //不应该使用bsUserFieldRel，这个表实际是用于用户策略使用的，只应用于列表显示时的数据展示，与Form方式无关
                //应该使用bsFunField //还要获取该功能项的需要显示的项的列表
                foreach (bsFunQuery item in ffs)
                {
                    FunQueryItem objqi = new FunQueryItem();
                    objqi.Name = item.FName;
                    objqi.NameTip = item.OperName;
                    objqi.InputType = (FDataQueryType)Enum.Parse(typeof(FDataQueryType), "QT_" + item.OperType);
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

            return obj;
        }




        public static List<FunOperation> GetFunOperations(EntityManager EManager, string controllername, string actionname)
        {
            Guid fcid = bllbsUserFields.GetFunConfId(EManager, controllername, actionname);

            return GetFunOperations(EManager, fcid, ItemPos.top);
        }

        public static List<FunOperation> GetFunOperations(EntityManager EManager, RouteData routeData)
        {
            Guid fcid = bllbsUserFields.GetFunConfId(EManager, routeData);

            return GetFunOperations(EManager, fcid,ItemPos.top);
        }
        public static List<FunOperation> GetFunOperations(EntityManager EManager, Guid bsFC_Id, ItemPos ItemPos)
        {

            List<FunOperation> objs = new List<FunOperation>();

            List<bsFunOper> ffs;

            if (ItemPos == ItemPos.none)
            {
                ffs = EManager.GetListNoPaging<bsFunOper>("bsFC_Id='" + bsFC_Id.ToString() + "'", "");
            }
            else
            {
                ffs = EManager.GetListNoPaging<bsFunOper>("bsFC_Id='" + bsFC_Id.ToString() + "' and btnPos='"+ItemPos.ToString().ToLower()+"'", "");
           
            }

            if (ffs.Count > 0)
            {
                //不应该使用bsUserFieldRel，这个表实际是用于用户策略使用的，只应用于列表显示时的数据展示，与Form方式无关
                //应该使用bsFunField //还要获取该功能项的需要显示的项的列表
                foreach (bsFunOper item in ffs)
                {
                    FunOperation obj = new FunOperation();
                    obj.btnNameTip = item.OperName;
                    obj.btnType = item.BtnType;
                    obj.btnSize = item.btnSize;
                    obj.btnClickType=item.btnClickGetPost;
                    obj.btnClickUrl =item.UrlServerEx+ item.LinkUrl;
                    if (obj.btnClickType.ToLower() == "get")
                        obj.getPagePostUrl = item.UrlServerEx + item.PostUrl;
                    obj.btnClass = item.btnClass;

                    obj.btnPos = (ItemPos)Enum.Parse(typeof(ItemPos), item.btnPos);
                    obj.pageType = (PageType)Enum.Parse(typeof(PageType), item.NewPageType); ;
                    objs.Add(obj);
                }
            }
            return objs;
        }


    }
}
