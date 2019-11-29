using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dao.QyBllApp;
using QyTech.Core.BLL;
using QyTech.Core;
using QyExpress.BLL;
using QyTech.Json;
using QyTech.Core.ExController.Bll;
namespace QyExpress.Controllers.BllApp
{
    public class LtdRegInfoController : BllAppController
    {


        /// <summary>
        /// 用户是否已核实企业信息
        /// </summary>
        /// <param name="sessionid"></param>
        /// <returns></returns>
        public string IsCheck(string sessionid)
        {
            LtdRegInfo lri = EManager_App.GetByPk<LtdRegInfo>("bsO_Id", LoginUser.bsO_Id);
            if (lri.IsCheck.HasValue && lri.IsCheck.Value == 1)
                return jsonMsgHelper.Create(0, lri.IsCheck.Value, "已核实");
            else
                return jsonMsgHelper.Create(1, lri.IsCheck.Value, "请首先核实企业数据，然后进行体检上报！");
        }

        public override string GetAll(string sessionid, string fields = "", string where = "", string orderby = "")
        {
            return base.GetAll(sessionid, fields, where, "LtdName");
        }

        public override string GetAllData(string sessionid, string fields = "", string where = "", string orderby = "")
        {
            return base.GetAllData(sessionid, fields, where, "LtdName");
        }

        /// <summary>
        /// 企业注册
        /// </summary>
        /// <param name="strjson">执照文件之外的其它数据</param>
        /// <param name="licensefileString">营业执照图片</param>
        /// <returns></returns>
        public string Register(string strjson, string licensefileString, string landcertificate)
        {
            if (strjson == null || strjson.Equals(""))
            {
                return jsonMsgHelper.Create(1, null, "参数为空，无法注册");
            }

            try
            {
                string ret = "";
                string licensefile = "";
                string landcertificatefile = "";
                if (licensefileString != null && licensefileString != "")
                    licensefile = base.UpPicture("", licensefileString);
                else
                    return jsonMsgHelper.Create(1, "", "必须上传营业执照！");
                if (landcertificate != null && landcertificate != "")
                    landcertificatefile = base.UpPicture("", landcertificate);

                LtdRegInfo lri = JsonHelper.DeserializeJsonToObject<LtdRegInfo>(strjson);
                LtdRegInfo dbobj = EntityManager_Static.GetByPk<LtdRegInfo>(DbContext, "NSRSBH", lri.NSRSBH);
                if (dbobj != null)
                {
                    return jsonMsgHelper.Create(1, "", "该纳税人识别号已存在，请核实信息！");
                }
                else
                {
                    //dbobj.UseDKBH = "";放在了触发器中//	update LtdRegInfo set UseDKBH='WJKFQ'+Right('0000'+CONVERT(varchar(5),@Id),5) where Id=@Id
                    lri.LicenseFile = licensefile;
                    lri.landcertificate = landcertificatefile;
                    lri.FlowStatus = "已提交";
                    lri.LtdChar = "新注册";
                    lri.CurrCheckStatus = "未进行体检";
                    lri.IsCheck = 0;
                    lri.bsO_Id = Guid.NewGuid();
                    lri.bsS_Code = WebSiteParams.currSoftCustCode;

                    ret = EntityManager_Static.Add<LtdRegInfo>(DbContext, lri);
                    if (ret.Contains("IX_LtdRegInfo"))
                    {
                        return jsonMsgHelper.Create(1, "", "企业名称重复，请核实企业名称！");
                    }
                    else if (ret != "")
                    {
                        return jsonMsgHelper.Create(1, "", new Exception(ret));
                    }
                    //完成权限操作
                    string rightret = bllLtdRegInfo.AddUserAfterAudited(DbContext, lri);
                    if (rightret != "")
                    {
                        LogHelper.Error(rightret);
                        //这里不能返回失败，用户体验不好
                        return jsonMsgHelper.Create(1, "", new Exception(rightret));
                    }
                    else
                        return jsonMsgHelper.Create(0, "", "注册成功!");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("Add:" + ex.Message);
                return jsonMsgHelper.Create(1, "", ex);
            }
        }

        public string RegisterNew(string strjson)
        {
            if (strjson == null || strjson.Equals(""))
            {
                return jsonMsgHelper.Create(1, null, "参数为空，无法注册");
            }

            try
            {
                string ret = "";

                LtdRegInfo lri = JsonHelper.DeserializeJsonToObject<LtdRegInfo>(strjson);
                if (lri.LicenseFile==null || lri.LicenseFile.Trim()=="")
                {
                    return jsonMsgHelper.Create(1, "", "营业执照必须上传！");

                }
                LtdRegInfo dbobj = EntityManager_Static.GetByPk<LtdRegInfo>(DbContext, "NSRSBH", lri.NSRSBH);
                if (dbobj != null)
                {
                    return jsonMsgHelper.Create(1, "", "该纳税人识别号已存在，请核实信息！");
                }
                else
                {
                    //dbobj.UseDKBH = "";放在了触发器中//	update LtdRegInfo set UseDKBH='WJKFQ'+Right('0000'+CONVERT(varchar(5),@Id),5) where Id=@Id
                    lri.FlowStatus = "已提交";
                    lri.LtdChar = "新注册";
                    lri.CurrCheckStatus = "未进行体检";
                    lri.IsCheck = 0;
                    lri.bsO_Id = Guid.NewGuid();
                    lri.bsS_Code = WebSiteParams.currSoftCustCode;
                    ret = EntityManager_Static.Add<LtdRegInfo>(DbContext, lri);
                    if (ret.Contains("IX_LtdRegInfo"))
                    {
                        return jsonMsgHelper.Create(1, "", "企业名称重复，请核实企业名称！");
                    }
                    else if (ret != "")
                    {
                        return jsonMsgHelper.Create(1, "", new Exception(ret));
                    }
                    //完成权限操作
                    string rightret = bllLtdRegInfo.AddUserAfterAudited(DbContext, lri);
                    if (rightret != "")
                    {
                        LogHelper.Error(rightret);
                        //这里不能返回失败，用户体验不好
                        return jsonMsgHelper.Create(1, "", new Exception(rightret));
                    }
                    else
                        return jsonMsgHelper.Create(0, "", "注册成功!");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("Add:" + ex.Message);
                return jsonMsgHelper.Create(1, "", ex);
            }
        }


        public override string EditbyKeyValuesDefaultTpk(string sessionid, string keyvalues)
        {
            return base.EditbyKeyValues(sessionid, "bsO_Id", LoginUser.bsO_Id.ToString(), keyvalues);
        }

        ///// <summary>
        ///// 审核
        ///// </summary>
        ///// <param name="sessionid"></param>
        ///// <param name="idValue"></param>
        ///// <param name="AuditDesp">审核意见</param>
        ///// <returns></returns>
        //public override string Audit(string sessionid, string idValue, int YesOrNo, string AuditDesp)
        //{

        //    try
        //    {
        //        LtdRegInfo obj = EntityManager_Static.GetByPk<LtdRegInfo>(DbContext, "Id", Convert.ToInt32(idValue));

        //        obj.AuditDt = DateTime.Now;
        //        obj.Auditor = LoginUser.NickName;
        //        string preValue = obj.FlowStatus;
        //        if (YesOrNo == 1)
        //            obj.FlowStatus = QyTech.Core.Common.FlowStatus.已审核.ToString();
        //        else
        //            obj.FlowStatus = QyTech.Core.Common.FlowStatus.已退回.ToString();
        //        obj.AuditDesp = AuditDesp;
        //        string ret = EntityManager_Static.Modify<LtdRegInfo>(DbContext, obj, "Id", obj.Id);

        //        //完成权限操作
        //        ret=bllLtdRegInfo.AddUserAfterAndited(DbContext, obj);
        //        if (ret!="")
        //            LogHelper.Error(ret);
        //        EntityManager_Static.Modify<LtdRegInfo>(DbContext, obj);

        //        //增加日志
        //        Guid ltd_Id = AddLogTable("审核", "LtdRegInfo", "企业注册", obj.Id.ToString());
        //        AddLogField(ltd_Id, "FlowStatus", "审核状态", obj.FlowStatus, preValue);

        //        if (ret == "")
        //            return jsonMsgHelper.Create(0, "", obj.FlowStatus + "！");
        //        else
        //            return jsonMsgHelper.Create(0, "", "审核失败！(" + ret + ")");
        //    }
        //    catch (Exception ex)
        //    {
        //        return jsonMsgHelper.Create(0, "", ex);
        //    }
        //}


        public string SetLongLatiPosition(string sessionid, string bsO_IdValue, string LongLati)
        {
            try
            {
                Dictionary<string, string> dicKV = new Dictionary<string, string>();
                dicKV.Add("LongLatitude", LongLati);
                bsT = GetbsTablebyName("LtdRegInfo");
                ObjectClassName = bsT.TName;
                return EditbyKeyValues("bsO_Id", bsO_IdValue, dicKV);
            }
            catch (Exception ex)
            {
                return jsonMsgHelper.Create(0, null, ex);
            }
        }




        /// <summary>
        /// 登录后的企业信息核实
        /// </summary>
        /// <param name="sessionid"></param>
        /// <param name="strjson"></param>
        /// <param name="licensefileString">营业执照文件</param>
        /// <param name="landcertificate">土地证文件</param>
        /// <returns></returns>
        public string CheckLtdInfo(string sessionid, string strjson, string licensefileString, string landcertificate)
        {
            if (strjson == null || strjson.Equals(""))
            {
                return jsonMsgHelper.Create(1, null, "参数为空，无法注册");
            }

            try
            {
                string ret = "";
                string licensefile = "";
                string landcertfile = "";
                if (licensefileString != null && licensefileString != "")
                {
                    licensefile = base.UpPicture(sessionid, licensefileString);
                }
                if (landcertificate != null && landcertificate != "")
                {
                    landcertfile = base.UpPicture(sessionid, landcertificate);
                }
                LtdRegInfo lri = JsonHelper.DeserializeJsonToObject<LtdRegInfo>(strjson);
                LtdRegInfo dbobj = EntityManager_Static.GetByPk<LtdRegInfo>(DbContext, "NSRSBH", lri.NSRSBH);

                if (dbobj != null)
                {
                    if (dbobj.FlowStatus == "已审核")
                        return jsonMsgHelper.Create(1, "", "已经审核，不能更改！");
                    else
                    {
                        dbobj.LtdName = lri.LtdName;
                        dbobj.LtdAddr = lri.LtdAddr;
                        dbobj.Contacter = lri.Contacter;
                        dbobj.ContactTel = lri.ContactTel;
                        dbobj.IsLandLtd = lri.IsLandLtd;

                        dbobj.RegDt = DateTime.Now;
                        if (licensefile != "")
                            dbobj.LicenseFile = licensefile;
                        if (landcertificate != "")
                            dbobj.landcertificate = landcertfile;

                        ret = EntityManager_Static.Modify<LtdRegInfo>(DbContext, dbobj);
                    }
                }
                if (ret == "")
                    return jsonMsgHelper.Create(0, "", "保存成功！");
                else
                    return jsonMsgHelper.Create(1, "", "保存失败！");
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message);
                return jsonMsgHelper.Create(1, "", ex);
            }
        }


        /// <summary>
        /// 登录后的企业信息核实
        /// </summary>
        /// <param name="sessionid"></param>
        /// <param name="keyvalues">只包含 主键和编辑项的无引号的json串</param>
        /// <param name="licensefileString">营业执照文件</param>
        /// <param name="landcertificate">土地证文件</param>
        /// <returns></returns>
        public string CheckbyKeyValues(string sessionid, string keyvalues, string licensefileString, string landcertificate)
        {
            if (keyvalues == null || keyvalues.Equals(""))
            {
                return jsonMsgHelper.Create(1, null, "参数为空，无法注册");
            }

            try
            {

                Dictionary<string, string> dicKv = base.json2dicKV(keyvalues);
                LtdRegInfo lri = EManager_App.GetByPk<LtdRegInfo>("bsO_Id", LoginUser.bsO_Id.ToString());
                //if (lri.FlowStatus == "已审核")
                //{
                //    return jsonMsgHelper.Create(1, "", "已经审核，不能更改！");
                //}

                try
                {
                    string licensefile = "";
                    string landcertfile = "";
                    if (licensefileString != null && licensefileString != "")
                    {
                        licensefile = base.UpPicture(sessionid, licensefileString);
                    }
                    if (landcertificate != null && landcertificate != "")
                    {
                        landcertfile = base.UpPicture(sessionid, landcertificate);
                    }
                    if (licensefile != "")
                        dicKv.Add("LicenseFile", licensefile);
                    if (landcertfile != "")
                        dicKv.Add("landcertificate", landcertfile);
                }
                catch { }
                //dicKv.Add("FlowStatus", "未提交");
                dicKv.Add("IsCheck", "0");

                //return base.EditbyKeyValues("bsO_Id", dicKv["bsO_Id"], dicKv);
                return base.EditbyKeyValues("bsO_Id", LoginUser.bsO_Id.ToString(), dicKv);
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message);
                return jsonMsgHelper.Create(1, "", ex);
            }
        }



        /// <summary>
        /// 提交核实信息
        /// </summary>
        /// <param name="sessionid"></param>
        /// <returns></returns>
        public string SubmitLtdInfo(string sessionid)
        {
            LtdRegInfo lri = EManager_App.GetByPk<LtdRegInfo>("bsO_Id", LoginUser.bsO_Id.ToString());

            if (lri.LicenseFile == null || lri.LicenseFile == "" || lri.Contacter == null || lri.Contacter == "" || lri.LongLatitude == null || lri.LongLatitude == "")
            {
                if (lri.LongLatitude == null || lri.LongLatitude.Trim() == "")
                    return jsonMsgHelper.Create(1, "请定位信息并保存", "核实失败！");
                else if (lri.Contacter == null || lri.Contacter.Trim() == "")
                    return jsonMsgHelper.Create(1, "请维护企业信息信息并保存", "核实失败！");
                else if (lri.LicenseFile == null || lri.LicenseFile.Trim() == "")
                    return jsonMsgHelper.Create(1, "请上传营业执照并保存", "核实失败！");
                else
                    return jsonMsgHelper.Create(1, "请定位并维护企业信息信息", "核实失败！");
            }
            Dictionary<string, string> dicKv = new Dictionary<string, string>();
            dicKv.Add("IsCheck", "1");
            dicKv.Add("FlowStatus", "已提交");
            return base.EditbyKeyValues("bsO_Id", LoginUser.bsO_Id.ToString(), dicKv);
        }


        public string DelLandCertPics(string sessionid, string filename)
        {
            LtdRegInfo dbobj = EntityManager_Static.GetByPk<LtdRegInfo>(DbContext, "bsO_Id", LoginUser.bsO_Id);
            dbobj.landcertificate = ("|" + dbobj.landcertificate).Replace("|" + filename, "");
            if (dbobj.landcertificate.Length > 0)
                dbobj.landcertificate = dbobj.landcertificate.Substring(1);
            string ret = EntityManager_Static.Modify<LtdRegInfo>(DbContext, dbobj);

            if (ret == "")
                return jsonMsgHelper.Create(0, "", "删除成功！");
            else
                return jsonMsgHelper.Create(1, "", "删除失败！");
        }

    }
}
