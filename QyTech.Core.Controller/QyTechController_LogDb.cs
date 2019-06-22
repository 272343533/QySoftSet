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
    public partial class QyTechController:Controller
    {

       #region 日志处理
        protected Guid AddLogTable(string SUID, bsTable bsT, Type dbtype, object tobj)
        {
            System.Reflection.PropertyInfo propertyInfo = dbtype.GetProperty(bsT.TPk);
            propertyInfo = dbtype.GetProperty(bsT.TPk);
            object svalue = propertyInfo.GetValue(tobj, null);

            return AddLogTable(SUID, bsT.TName, bsT.Desp, svalue.ToString());
        }
        protected Guid AddLogTable(string SUID, string tName, string tDesp, string IdValue)
        {
            bsLog_Table obj = new bsLog_Table();
            obj.bsLT_Id = Guid.NewGuid();
            try
            {

                obj.bsU_Id = LoginUser.bsU_Id;
                obj.bsU_Name = LoginUser.NickName;

                obj.AccType = SUID;
                obj.AppName = "";
                obj.logDt = DateTime.Now;
                obj.TName = tName;
                obj.TDesp = tDesp;
                obj.IdValue = IdValue.Length <= 50 ? IdValue : IdValue.Substring(0, 50);
                EManager_.Add<bsLog_Table>(obj);
            }
            catch (Exception ex) { LogHelper.Error(ex); }
            return obj.bsLT_Id;
        }

        protected void AddLogField(Guid bsLT_Id, string fName, string fDesp, string currValue, string preValue)
        {
            try
            {
                bsLog_Field obj = new bsLog_Field();
                obj.bsLF_Id = Guid.NewGuid();
                obj.bsLT_Id = bsLT_Id;
                obj.FName = fName;
                obj.FDesp = fDesp;
                obj.currFValue = currValue;
                obj.preFValue = preValue;

                EManager_.Add<bsLog_Field>(obj);
            }
            catch (Exception ex) { LogHelper.Error(ex); }
        }

        private void AddLogFun(string fName, string fDesp)
        {
            try
            {
                bsLog_Oper obj = new bsLog_Oper();
                obj.bsLO_Id = Guid.NewGuid();
                obj.logDt = DateTime.Now;

                obj.bsU_Id = LoginUser.bsU_Id;
                obj.bsU_Name = LoginUser.NickName;

                obj.FunName = fName;
                obj.FunDesp = fDesp;


                EManager_.Add<bsLog_Oper>(obj);
            }
            catch (Exception ex) { LogHelper.Error(ex); }
        }
        #endregion  
    }
}
