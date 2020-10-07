using System;
using System.Web;
using System.Web.Mvc;
using QyTech.Core.ExController;
using qyExpress.Dao;
using QyTech.Core.BLL;
using QyTech.Core;
using QyTech.Core.Common;
using QyTech.Json;
using System.Collections;
using System.Collections.Generic;
using QyTech.Core.ExController.Bll;
using Dao.QyBllApp;

namespace QyExpress.Controllers.BllApp
{
    public class tA1企业基本信息表Controller : BllAppController
    {
        public override string GetAll(string sessionid, string fields = "", string where = "", string orderby = "")
        {
            tA1企业基本信息表 obj = new tA1企业基本信息表();
            return base.GetAll(sessionid, fields, where, orderby);
        }
        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="sessionid"></param>
        /// <param name="idValues"></param>
        /// <param name="AuditDesp">审核意见</param>
        /// <returns></returns>
        public override string Audit(string sessionid, string idValues, string result, string desp)
        {
            try
            {
                string ret="";
                string ltdName = "";
                //string check = "";
                //if (YesOrNo == 1)
                //{
                //    check = ValidSubmit2Audio(idValue);
                //    return jsonMsgHelper.Create(1, "", "审核失败！(" + check + ")");
                //}
                string[] ids = idValues.Split(new char[] { ',' });
                foreach (string id in ids)
                {
                    try
                    {
                        bnbaseInfo_audit obj_audit = new bnbaseInfo_audit();
                        obj_audit.ltd_Id = Convert.ToInt32(id);//可以保证tA1表中的主键值与bsLtdInfo中的id值一致。
                        tA1企业基本信息表 obj = EntityManager_Static.GetByPk<tA1企业基本信息表>(DbContext, "Id", id);
                        obj_audit.ltd_name = obj.纳税人名称;
                        obj_audit.audit_bsO_Id = LoginUser.bsO_Id;
                        obj_audit.audit_bsO_Name = LoginUser.bsO_Name;
                        obj_audit.auditor = LoginUser.NickName;
                        obj_audit.audit_dt = DateTime.Now;
                        if (result == "已审核")
                        {
                            obj_audit.audit_result = QyTech.Core.Common.FlowStatus.已审核.ToString();
                            obj_audit.audit_desp = desp.Trim() == "" ? "审核通过" : desp;
                        }
                        else
                        {
                            obj_audit.audit_result = QyTech.Core.Common.FlowStatus.已退回.ToString();
                            obj_audit.audit_desp = desp;
                        }
                        
                        ret = EntityManager_Static.Add<bnbaseInfo_audit>(DbContext, obj_audit);

                        //发送短信
                        if (ret != "")
                        {
                            ltdName += "," + ret;// obj.LtdName;
                        }
                        //else
                        //{
                        //    if (obj_audit.audit_result == "已退回")
                        //    {
                        //        try
                        //        {
                        //            QyTech.MsgSms.SmsFactory_Huawei fac = new QyTech.MsgSms.SmsFactory_Huawei();
                        //            QyTech.MsgSms.IMsgSms_Huawei msg = fac.Create(WebSiteParams.currSoftCustCode.ToLower());
                        //            if (msg != null)
                        //                msg.Send(obj.ContactTel, obj_audit.AutitType);
                        //        }
                        //        catch (Exception ex) { LogHelper.Error(ex.Message); }
                        //    }
                        //}


                    }
                    catch (Exception ex)
                    {
                        LogHelper.Error(id, ex);
                    }
                }
                if (ret != "")
                    return jsonMsgHelper.Create(1, "", ret);
                else
                    return jsonMsgHelper.Create(0, "", "审核成功！");
            }
            catch (Exception ex)
            {
                return jsonMsgHelper.Create(1, "", ex);
            }
        }

    }
}
