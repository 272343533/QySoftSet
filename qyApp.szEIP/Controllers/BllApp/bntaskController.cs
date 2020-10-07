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
    public class bntaskController : BllAppController
    {
       
        public override string Save(string sessionid, string strjson)
        {
            LogHelper.Info(sessionid);
            LogHelper.Info(strjson);
            strjson = strjson.Replace("|||amp;", "&");
            strjson = strjson.Replace("|||lt;", "<");
            strjson = strjson.Replace("|||gt;", ">");
            strjson = strjson.Replace("|||quot;", "'");
            //strjson = strjson.Replace("|||#039;", "'");
            LogHelper.Info(strjson);

            return base.Save(sessionid, strjson);
        }


        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="sessionid"></param>
        /// <param name="fkIdValue"></param> 
        /// <param name="idValues"></param>
        /// <param name="AuditDesp">审核意见</param>
        /// <returns></returns>
        public override string AuditWithFk(string sessionid,string fkIdValue, string idValues, string result, string desp)
        {
            try
            {
                //string check = "";
                //if (YesOrNo == 1)
                //{
                //    check = ValidSubmit2Audio(idValue);
                //    return jsonMsgHelper.Create(1, "", "审核失败！(" + check + ")");
                //}
                string ltdName = "";
                string[] ids = idValues.Split(new char[] { ',' });
                foreach (string id in ids)
                {
                    try
                    {
                        
                        bntask_audit obj_audit = new bntask_audit();
                        obj_audit.bntask_Id = Convert.ToInt32(fkIdValue);
                        obj_audit.ltd_Id = Convert.ToInt32(id);
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
                        string ret = EntityManager_Static.Add<bntask_audit>(DbContext, obj_audit);

                            //发送短信
                            //if (ret != "")
                            //{
                            //    ltdName += "," + obj.LtdName;
                            //}
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
                if (ltdName != "")
                    return jsonMsgHelper.Create(1, ltdName.Substring(1), "部分数据没有审核成功，请刷新数据！");
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
