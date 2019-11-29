using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dao.QyBllApp;
using QyTech.Core;
using QyTech.Json;
using QyTech.Core.BLL;
using QyExpress.Dao;

namespace QyExpress.Controllers.BllApp
{
    public class LtdUpInfoController : BllAppController
    {

        public override string GetAll(string sessionid, string fields = "", string where = "", string orderby = "")
        {
            // //管理员不合适
            //LtdRegInfo lri = EntityManager_Static.GetByPk<LtdRegInfo>(DbContext, "bsO_Id", LoginUser.bsO_Id);
            //if (lri.IsCheck==null ||lri.IsCheck==0)
            //{
            //    return jsonMsgHelper.Create(1, "", "必须完成企业信息核实才可以进行上报！");
            //}
            return base.GetAll(sessionid, fields, where, orderby);
        }
        public override string GetAllData(string sessionid, string fields = "", string where = "", string orderby = "")
        {
            //管理员不合适
            //LtdRegInfo lri = EntityManager_Static.GetByPk<LtdRegInfo>(DbContext, "bsO_Id", LoginUser.bsO_Id);
            //if (lri.IsCheck == null || lri.IsCheck == 0)
            //{
            //    return jsonMsgHelper.Create(1, "", "必须完成企业信息核实才可以进行上报！");
            //}
            if (where.ToLower().Contains("bso_id"))
            {
                return base.GetAllData(sessionid, fields, where, orderby);
            }
            else
            {
                Dictionary<string, string> dics = kvWhere2Dic(where.ToLower());
                string sqlwhere = "Cycle='" + dics["cycle"] + "'";
                if (dics["flowstatus"] != "")
                {
                    sqlwhere += " and FlowStatus ='" + dics["flowstatus"] + "'";
                }
                if (dics["ltdname"] != "")
                {
                    sqlwhere += " and ltdname like '%" + dics["ltdname"] + "%'";
                }
                if (dics["ltdchar"] != "")
                {
                    sqlwhere += " and bso_Id in (select bso_Id from LtdRegInfo where ltdchar ='" + dics["ltdchar"] + "')";
                }
                return base.GetAllData(sessionid, fields, sqlwhere, orderby);
            }
           
        }

        protected override string CheckLogical(string idValue)
        {
            List<LtdUpInfoRent> rents = EntityManager_Static.GetListNoPaging<LtdUpInfoRent>(DbContext, "LUI_Id=" + idValue + " and (DBHH is null or SBHH is null or FTYD is null)", "");
            if (rents.Count > 0)
            {
                return "租户信息中的水表信息，电表信息，分摊用地等不能为空！";
            }
            else
            {
                LtdUpInfo lui = EntityManager_Static.GetByPk<LtdUpInfo>(DbContext, "Id", Convert.ToInt32(idValue));
                rents = EntityManager_Static.GetListNoPaging<LtdUpInfoRent>(DbContext, "LUI_Id=" + idValue, "");
                Decimal sum = 0;
                string rentwaters = "";
                string rentelecs = "";

                foreach (LtdUpInfoRent rent in rents)
                {
                    sum += rent.FTYD.Value;
                    if (rent.DBHH != "无")
                        rentelecs += "," + rent.DBHH;
                    if (rent.SBHH != "无")
                        rentwaters += "," + rent.SBHH;

                }
                if (sum != lui.LandInfo_PJArea.Value)
                {
                    return "租户信息中的分摊用地之和必须与评价面积相等！";
                }
                //判断水表号和电表号的关系
                List<string> waters = new List<string>(lui.WaterMeters.Split(new string[] { ",", "，" }, StringSplitOptions.RemoveEmptyEntries));
                waters.Sort();
                List<string> eles = new List<string>(lui.ElecMeters.Split(new string[] { ",", "，" }, StringSplitOptions.RemoveEmptyEntries));
                eles.Sort();

                List<string> lstrentwaters = new List<string>(rentwaters.Split(new string[] { ",", "，" }, StringSplitOptions.RemoveEmptyEntries));
                lstrentwaters.Sort();
                List<string> lstrenteles = new List<string>(rentelecs.Split(new string[] { ",", "，" }, StringSplitOptions.RemoveEmptyEntries));
                lstrenteles.Sort();

                if (lstrenteles.Count == eles.Count)
                {
                    for (int e = 0; e < eles.Count; e++)
                    {
                        if (lstrenteles[e] != eles[e])
                        {
                            return "租户信息中的电表信息与地主企业的电表信息不一致！";
                        }
                    }
                }
                else
                {
                    return "租户信息中的电表信息数量与地主企业的电表信息不一致！";
                }
                if (lstrentwaters.Count == waters.Count)
                {
                    for (int e = 0; e < waters.Count; e++)
                    {
                        if (lstrentwaters[e] != waters[e])
                        {
                            return "租户信息中的水表信息与地主企业的水表信息不一致！";
                        }
                    }
                }
                else
                {
                    return "租户信息中的水表信息数量与地主企业的水表信息不一致！";
                }
            }
            return "";
        }

        public string ValidSubmit2Audio(string idValue)
        {
            try
            {
                string checkforminfo = CheckLogical(idValue);
                if (checkforminfo!="")
                    return checkforminfo;
                List<LtdUpPics> pics = EntityManager_Static.GetListNoPaging<LtdUpPics>(DbContext, "LUI_Id=" + idValue + " and (PicFile is null and isUse=1)", "");
                if (pics.Count > 0)
                    return "需要上传的图片必须上传才能提交！";
                else
                    return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Submit2Audit(string sessionid, string FName, string FValue, string keyvalues)
        {
            string check = "";
                check = ValidSubmit2Audio(FValue);
            if (check!="")
                return jsonMsgHelper.Create(1, "", "提交失败！(" + check + ")");
           
            return base.EditbyKeyValues(sessionid, FName, FValue, keyvalues);
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="sessionid"></param>
        /// <param name="idValue"></param>
        /// <param name="AuditDesp">审核意见</param>
        /// <returns></returns>
        public override string Audit(string sessionid, string idValue, int YesOrNo, string AuditDesp)
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
                string[] ids = idValue.Split(new char[] { ',' });
                foreach (string id in ids)
                {
                    try
                    {
                        LtdUpInfo obj = EntityManager_Static.GetByPk<LtdUpInfo>(DbContext, "Id", Convert.ToInt32(id));

                        LtdUpInfoAudit obj_audit = new LtdUpInfoAudit();
                        obj_audit.LUI_Id = Convert.ToInt32(id);
                        obj_audit.AuditbsO_Id = LoginUser.bsO_Id;
                        obj_audit.AuditbsO_Name = LoginUser.bsO_Name;
                        obj_audit.Auditor = LoginUser.NickName;
                        obj_audit.AuditDt = DateTime.Now;
                        if (YesOrNo == 1)
                        {
                            obj_audit.AutitType = QyTech.Core.Common.FlowStatus.已审核.ToString();
                            obj_audit.AuditDesp = AuditDesp.Trim() == "" ? "审核通过" : AuditDesp;
                        }
                        else
                        {
                            obj_audit.AutitType = QyTech.Core.Common.FlowStatus.已退回.ToString();
                            obj_audit.AuditDesp = AuditDesp;
                        }
                        string ret = EntityManager_Static.Add<LtdUpInfoAudit>(DbContext, obj_audit);

                        if (ret != "")
                        {
                            ltdName += "," + obj.LtdName;
                        }
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

        /// <summary>
        /// 获取当前用户的一份新的上传信息模板,
        /// </summary>
        /// <param name="sessionid"></param>
        ///  <param name="cycle">要复制的周期</param>
        /// <returns></returns>
        public string GetTemplate(string sessionid)
        {
            try
            {
                LtdUpInfo obj;
                int PreId = 0;
                //首先判断企业信息是否已核实，未核实信息，不能新建报表
                LtdRegInfo lri1 = EntityManager_Static.GetByPk<LtdRegInfo>(DbContext, "bsO_Id", LoginUser.bsO_Id);
                if (lri1.IsCheck==null || lri1.IsCheck!=1)
                {
                    return jsonMsgHelper.Create(1, "", "请首先核实企业信息！");
                }

                //获取新的周期
                List<string> cycle = EntityManager_Static.GetAllByStorProcedure<string>(DbContext, "bllSpGetTempleCycle", new object[] { LoginUser.bsO_Id.ToString() });
                if (cycle.Count == 0)
                    return jsonMsgHelper.Create(1, "", "创建失败！");
                if (cycle[0]=="1")
                {
                    return jsonMsgHelper.Create(1, "", "当前上报周期数据已存在，请修改数据！");
                }

                List<LtdUpInfo> objs = EntityManager_Static.GetListNoPaging<LtdUpInfo>(DbContext, "bsO_Id='" + LoginUser.bsO_Id.ToString() + "' and FlowStatus='已审核'", "UpDt desc");
                if (objs.Count > 0)
                {
                    PreId = objs[0].Id;
                    obj = Clone(objs[0]);
                }
                else
                {
                    LtdRegInfo lri= EntityManager_Static.GetByPk<LtdRegInfo>(DbContext, "bsO_Id",LoginUser.bsO_Id);

                    obj = new LtdUpInfo();
                    obj.UpDt = DateTime.Now;
                    obj.bsO_Id = LoginUser.bsO_Id;
                    obj.Cycle = cycle[0];
                    obj.LtdName = lri.LtdName;
                    obj.DKBH = lri.UseDKBH;
                    obj.Contacter = lri.Contacter;
                    obj.ContactTel = lri.ContactTel;
                    obj.FlowStatus = "未提交";
                    obj.IsGYQY = "是";
                    obj.Operator = LoginUser.NickName.Length>6?LoginUser.NickName.Substring(0,6):LoginUser.NickName;
                    string errmsg = "";
                    obj = EntityManager_Static.AddReturnEntity<LtdUpInfo>(DbContext, obj, out errmsg);
                    if (errmsg.Contains("IX_LtdUpInfo"))
                        return jsonMsgHelper.Create(1, obj, "当前上传周期已经存在数据，请修改!");
                }
                if (obj != null)
                {
                    //同时复制租户数据
                    string execSql = "exec splyAppendUpInfoExAfterGetTemple " + PreId.ToString() + "," + obj.Id.ToString();
                    EntityManager_Static.ExecuteSql(DbContext, execSql);
                    return jsonMsgHelper.Create(0, obj, "创建成功!");
                }
                else
                    return jsonMsgHelper.Create(1, null, "创建失败，请联系管理员!");
            }
            catch (Exception ex)
            {
                return jsonMsgHelper.Create(0, "",ex);
            }
        }


        /// <summary>
        /// 父子表保存
        /// </summary>
        /// <param name="sessionid"></param>
        /// <param name="tjson">主键数据，含主键值，为更改</param>
        /// <param name="fjson">子表数据，含外键值，可能更改或新增</param>
        /// <returns></returns>
        public string Save(string sessionid,string tjson,string fjson)
        {

            return jsonMsgHelper.Create(1, "", "未完成能！");
        }

        /// <summary>
        /// 复制上传信息和租户信息表
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        private LtdUpInfo Clone(LtdUpInfo template)
        {
            LtdUpInfo copy = EntityOperate.Copy<LtdUpInfo>(template);
            //copy.Id = -1;
            copy.UpDt = DateTime.Now;
            copy.AuditDesp = null;
            copy.AuditDt = null;
            copy.Auditor = null;
            copy.Operator = LoginUser.NickName;
            copy.Cycle = DateTime.Now.Year.ToString();
            copy.PreYearElecQuality = null;
            copy.ThisYearElecQuality = null;
            copy.PreYearWaterQuality = null;
            copy.ThisYearWaterQuality = null;
            string errmsg = "";
            LtdUpInfo obj = EntityManager_Static.AddReturnEntity<LtdUpInfo>(DbContext, copy, out errmsg);
            return obj;
        }


        /// <summary>
        /// 导出用地企业上传数据样式1
        /// </summary>
        /// <param name="sessionid"></param>
        /// <param name="Cycle">周期</param>
        /// <returns></returns>
        public ActionResult ExportOwner1(string sessionid, string Cycle)
        {
            QyTech.ExcelOper.QyExcelHelper excl = new QyTech.ExcelOper.QyExcelHelper("web");

            string downFileName;
            string templateFileName;
            templateFileName = "用地企业统计表1.xls";
            downFileName = "用地企业统计表1" + DateTime.Now.ToString("yyyyMMddHHmmsss") + ".xls";
            string saveToPath = templateFileName;
            try
            {
                List<rptLandLtdUpInfo> objs = EntityManager_Static.GetAllByStorProcedure<rptLandLtdUpInfo>(DbContext, "bllSp_rptLandLtdUpInfo", new object[] { Cycle, "已审核", "" });
                #region 导出的数据项  不用了
                //Dictionary<string, string> dicField2ExcelCol = new Dictionary<string, string>();
                //dicField2ExcelCol.Add("A", "地块编号");
                //dicField2ExcelCol.Add("B", "用地企业名称");
                //dicField2ExcelCol.Add("C", "表格填写单位");//?
                //dicField2ExcelCol.Add("D", "表格填写人员");//?
                //dicField2ExcelCol.Add("IsGYQY", "是否工业企业");
                //dicField2ExcelCol.Add("LicenceRegDt", "注册日期");
                //dicField2ExcelCol.Add("Addr1", "地块地址（所属镇）");
                //dicField2ExcelCol.Add("Addr2", "地块地址（所属村社区）");
                //dicField2ExcelCol.Add("Add3r", "详细地址");
                //dicField2ExcelCol.Add("Contacter", "联系人姓名");
                //dicField2ExcelCol.Add("ContactTel", "联系人手机");
                //dicField2ExcelCol.Add("ElecMeters", "所有电表户号");
                //dicField2ExcelCol.Add("PreYearElecQuality", (Cycle - 1).ToString() + "年度总用电量（万千瓦时）");
                //dicField2ExcelCol.Add("ThisYearElecQuality", Cycle.ToString() + "年度总用电量（万千瓦时）");
                //dicField2ExcelCol.Add("WaterMeters", "所有水表户号");
                //dicField2ExcelCol.Add("PreYearWaterQuality", (Cycle - 1).ToString() + "年度总水量（吨）");
                //dicField2ExcelCol.Add("ThisYearWaterQuality", Cycle.ToString() + "年度总水量（吨）");
                //dicField2ExcelCol.Add("LandInfo_PJArea", "评价面积（亩）");
                //dicField2ExcelCol.Add("LandInfo_CZArea", "持证面积（亩）");
                ////dicField2ExcelCol.Add("PjAreaChar_1", "评价面积使用性质");
                //dicField2ExcelCol.Add("PjAreaChar_1", "自用出租情况");//?
                //dicField2ExcelCol.Add("PjAreaChar_2_Self", "自用面积（亩）");
                //dicField2ExcelCol.Add("PjAreaChar_2_Rent", "出租面积（亩）");
                //dicField2ExcelCol.Add("PjAreaChar_3", "土地所有权人");//?
                //dicField2ExcelCol.Add("PjAreaChar_4", "土地使用税缴纳人");//?
                //dicField2ExcelCol.Add("AddScore_Research", "研发机构");
                //dicField2ExcelCol.Add("AddScore_Brand", "品牌");
                //dicField2ExcelCol.Add("AddScore_Quality", "质量");
                //dicField2ExcelCol.Add("AddScore_Standard", "标准");
                //dicField2ExcelCol.Add("AddScore_Credit", "守合同重信用");
                //dicField2ExcelCol.Add("AddScore_Safety", "安全生产标准化");
                //dicField2ExcelCol.Add("AddScore_EnviProtect", "环保信用");
                //dicField2ExcelCol.Add("AddScore_Tax", "诚信纳税");
                //dicField2ExcelCol.Add("AddScroe_Patent", "发明专利");
                //dicField2ExcelCol.Add("GradUD_Cost1", "上年度工业设备投入超1000万");
                //dicField2ExcelCol.Add("GradUD_Cost10", "上年度工业设备投入超亿");
                //dicField2ExcelCol.Add("GradUD_Cost100", "上年度工业设备投入超10亿");
                //dicField2ExcelCol.Add("GradUD_Leader", "是否科技领军人才企业");
                //dicField2ExcelCol.Add("GradUD_HiTech", "是否高新技术企业");
                //dicField2ExcelCol.Add("GradUD_Specialty", "是否专精特新企业");
                //dicField2ExcelCol.Add("GradUD_Safety", "是否安全环保红黄牌未摘牌企业");
                //dicField2ExcelCol.Add("GradUD_LandSupplyLess3Years", "是否供地未满三年");
                //dicField2ExcelCol.Add("GradUD_ReOrgnizeLess2Years", "是否通过兼并重组取得土地使用权未满两年");
                //dicField2ExcelCol.Add("GradUD_ Inappropriate", "是否镇区认定暂不适宜参加评价企业");



                #endregion
                string fields = "B,B_1,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,AA,AB,AC,AD,AE,AF,AG,AH,AI,AJ,AK,AL,AM,AN,AO,AP,AQ,AR,AS1";

                saveToPath = excl.ExportListToExcl<rptLandLtdUpInfo>(objs, downFileName, templateFileName, fields, "yyyy-MM-dd HH:mm:ss", true, 4, 2, "web");
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message);
            }
            return DownFile(sessionid, saveToPath, downFileName);

        }

        /// <summary>
        ///  导出用地企业上传数据样式2（租户信息）
        /// </summary>
        /// <param name="sessionid"></param>
        /// <param name="Cycle">周期</param>
        /// <returns></returns>
        public ActionResult ExportOwner2(string sessionid, string Cycle)
        {
            QyTech.ExcelOper.QyExcelHelper excl = new QyTech.ExcelOper.QyExcelHelper("web");
            string downFileName;
            string templateFileName;
            templateFileName = "用地企业统计表2.xls";
            downFileName = "用地企业统计表2"+ DateTime.Now.ToString("yyyyMMddHHmmsss") + ".xls";
            string saveToPath = templateFileName;
            try
            {
                List<rptLandLtdUpRent> objs = EntityManager_Static.GetAllByStorProcedure<rptLandLtdUpRent>(DbContext, "bllSp_rptLandLtdUpRent", new object[] { Cycle, "已审核", "" });
                #region 导出的数据项  不用了
                //Dictionary<string, string> dicField2ExcelCol = new Dictionary<string, string>();

                //dicField2ExcelCol.Add("DKBH", "地块编号");
                //dicField2ExcelCol.Add("LtdYDLX", "企业用地类型");
                //dicField2ExcelCol.Add("LtdName", "企业名称");
                //dicField2ExcelCol.Add("LicenseCode", "统一社会信用代码（组织机构代码）");
                //dicField2ExcelCol.Add("IsGY", "是否工业");
                //dicField2ExcelCol.Add("FTYD", "分摊用地（亩）");
                //dicField2ExcelCol.Add("DBHH", "该企业所有电表户号");
                //dicField2ExcelCol.Add("SBHH", "该企业所有水表户号");
                //dicField2ExcelCol.Add("BZ", "备注");
                #endregion
                string fields = "B,C,D,E,F,G,H,I,J";

                saveToPath = excl.ExportListToExcl<rptLandLtdUpRent>(objs, downFileName, templateFileName, fields, "yyyy-MM-dd HH:mm:ss", true, 3, 2, "web");
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message);
            }
            return DownFile(sessionid, saveToPath, downFileName);

        }

        /// <summary>
        ///  导出租赁企业上传数据
        /// </summary>
        /// <param name="sessionid"></param>
        /// <param name="Cycle">周期</param>
        /// <returns></returns>
        public ActionResult ExportRent(string sessionid, string Cycle)
        {
            QyTech.ExcelOper.QyExcelHelper excl = new QyTech.ExcelOper.QyExcelHelper("web");
            string downFileName;
            string templateFileName;
            templateFileName = "租赁企业统计表.xls";
            downFileName = "租赁企业统计表" + DateTime.Now.ToString("yyyyMMddHHmmsss") + ".xls";
            string saveToPath = templateFileName;
            try
            {
                List<rptRentLtdUpInfo> objs = EntityManager_Static.GetAllByStorProcedure<rptRentLtdUpInfo>(DbContext, "bllSp_rptRentLtdUpInfo", new object[] { Cycle, "已审核", "" });
                #region 导出的数据项 不用了
                //Dictionary<string, string> dicField2ExcelCol = new Dictionary<string, string>();
                //dicField2ExcelCol.Add("DKBH", "地块编号");
                //dicField2ExcelCol.Add("LtdName", "租赁企业名称");
                //dicField2ExcelCol.Add("NSRSBH", "租赁企业纳税人识别号");//?

                //dicField2ExcelCol.Add("RentLtd_LandHolderName", "地主企业名称");
                //dicField2ExcelCol.Add("RentLtd_LandHolderNSRSBH", "地主企业纳税人识别号");
                //dicField2ExcelCol.Add("Operator", "表格填写人员");
                //dicField2ExcelCol.Add("IsGYQY", "是否工业企业");
                //dicField2ExcelCol.Add("LicenceRegDt", "注册日期");
                //dicField2ExcelCol.Add("Addr", "经营地址");
                //dicField2ExcelCol.Add("Contacter", "联系人姓名");
                //dicField2ExcelCol.Add("ContactTel", "联系人手机");
                //dicField2ExcelCol.Add("ElecMeters", "所有电表户号");
                //dicField2ExcelCol.Add("PreYearElecQuality", (Cycle - 1).ToString() + "年度总用电量（万千瓦时）");
                //dicField2ExcelCol.Add("ThisYearElecQuality", Cycle.ToString() + "年度总用电量（万千瓦时）");
                //dicField2ExcelCol.Add("WaterMeters", "所有水表户号");
                //dicField2ExcelCol.Add("PreYearWaterQuality", (Cycle - 1).ToString() + "年度总水量（吨）");
                //dicField2ExcelCol.Add("ThisYearWaterQuality", Cycle.ToString() + "年度总水量（吨）");
                //dicField2ExcelCol.Add("RentLtd_LandHolderType", "地主类别");
                //dicField2ExcelCol.Add("RentLtd_PJArea_Land", "租用土地面积（亩）");
                //dicField2ExcelCol.Add("RentLtd_PJArea_House", "租用房屋面积（㎡）");
                //dicField2ExcelCol.Add("AddScore_Research", "研发机构");
                //dicField2ExcelCol.Add("AddScore_Brand", "品牌");
                //dicField2ExcelCol.Add("AddScore_Quality", "质量");
                //dicField2ExcelCol.Add("AddScore_Standard", "标准");
                //dicField2ExcelCol.Add("AddScore_Credit", "守合同重信用");
                //dicField2ExcelCol.Add("AddScore_Safety", "安全生产标准化");
                //dicField2ExcelCol.Add("AddScore_EnviProtect", "环保信用");
                //dicField2ExcelCol.Add("AddScore_Tax", "诚信纳税");
                //dicField2ExcelCol.Add("AddScroe_Patent", "发明专利");
                //dicField2ExcelCol.Add("GradUD_Cost1", "上年度工业设备投入超1000万");
                //dicField2ExcelCol.Add("GradUD_Cost10", "上年度工业设备投入超亿");
                //dicField2ExcelCol.Add("GradUD_Cost100", "上年度工业设备投入超10亿");
                //dicField2ExcelCol.Add("GradUD_Leader", "是否科技领军人才企业");
                //dicField2ExcelCol.Add("GradUD_HiTech", "是否高新技术企业");
                //dicField2ExcelCol.Add("GradUD_Specialty", "是否专精特新企业");
                //dicField2ExcelCol.Add("GradUD_Safety", "是否安全环保红黄牌未摘牌企业");
                //dicField2ExcelCol.Add("GradUD_LandSupplyLess3Years", "是否供地未满三年");
                //dicField2ExcelCol.Add("GradUD_ReOrgnizeLess2Years", "是否通过兼并重组取得土地使用权未满两年");
                //dicField2ExcelCol.Add("GradUD_ Inappropriate", "是否镇区认定暂不适宜参加评价企业");
                #endregion

                string fields = "B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,AA,AB,AC,AD,AE,AF,AG,AH,AI,AJ,AK,AL,AM,AN";
                saveToPath = excl.ExportListToExcl<rptRentLtdUpInfo>(objs, downFileName, templateFileName, fields, "yyyy-MM-dd HH:mm:ss", true, 4, 2, "web");
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message);
            }
            return DownFile(sessionid, saveToPath, downFileName);

        }

        private string GetSpaceString(object o ,int n=5)
        {
            string s="";
            if (o == null|| o.ToString().Trim()=="")
            {
                for(int i=0;i<(n>5?n:5);i++)
                s += "  ";
            }
            else
                s = o.ToString();

            return s;
        }

        /// <summary>
        /// 导出上传报表
        /// </summary>
        /// <param name="sessionid"></param>
        /// <param name="idValue"></param>
        /// <returns></returns>
        public ActionResult Export(string sessionid, int idValue)
        {
            //Microsoft.Office.Interop.Excel.Range a2 = cells.get_Range("A2");
            //a2.Characters[3, 11].Font.Bold = true;
            //a2.Characters[3, 11].Font.Underline = true;
            string templateFileName="";
            string saveToPath = "DefaultTemplate.xls";
            string downFileName ="数据有问题.xls";
            try
            {
                QyTech.ExcelOper.QyExcelHelper excl = new QyTech.ExcelOper.QyExcelHelper("web");
                LtdUpInfo obj = EntityManager_Static.GetByPk<LtdUpInfo>(DbContext, "Id", idValue);
                LtdRegInfo lri = EntityManager_Static.GetByPk<LtdRegInfo>(DbContext, "bsO_Id", obj.bsO_Id);

                if (lri.IsLandLtd == 1)
                {
                    templateFileName = "地主企业模板.xls";
                    downFileName = "用地企业上报资料" + DateTime.Now.ToString("yyMMddHHmmsss") + ".xls";
                }
                else
                {
                    templateFileName = "租赁企业模板.xls";
                    downFileName = "租赁企业上报资料" + DateTime.Now.ToString("yyMMddHHmmsss") + ".xls";
                }

                Dictionary<string, string> pos2Values = new Dictionary<string, string>();
                try
                {
                    #region 基本信息

                    pos2Values.Add(exChange("L1"), obj.DKBH);
                    pos2Values.Add(exChange("A3"), "（" + obj.Cycle.ToString() + "年度）");
                    pos2Values.Add(exChange("B5"), obj.LtdName);
                    pos2Values.Add(exChange("B8"), obj.IsGYQY == "是" ? "■是  □否" : "□是  ■否");
                    pos2Values.Add(exChange("K8"), obj.LicenceRegDt.ToString());
                    pos2Values.Add(exChange("B9"), "吴江区 " + obj.Addr1 + " 镇（区）" + obj.Addr2 + " 村（社区）)" + obj.Addr3 + " 。");
                    pos2Values.Add(exChange("B10"), obj.Contacter);
                    pos2Values.Add(exChange("K10"), obj.ContactTel);
                    pos2Values.Add(exChange("B11"), obj.ElecMeters);
                    pos2Values.Add(exChange("A12"), (Convert.ToInt16(obj.Cycle) - 1).ToString() + "年度总电量");
                    pos2Values.Add(exChange("B12"), obj.PreYearElecQuality.ToString() + " 万千瓦时");
                    pos2Values.Add(exChange("G12"), obj.Cycle + "年度总电量");
                    pos2Values.Add(exChange("K12"), obj.ThisYearElecQuality.ToString() + " 万千瓦时");
                    pos2Values.Add(exChange("B13"), obj.WaterMeters);
                    pos2Values.Add(exChange("A14"), (Convert.ToInt16(obj.Cycle) - 1).ToString() + "年度总水量");
                    pos2Values.Add(exChange("B14"), obj.PreYearWaterQuality.ToString() + "  吨");
                    pos2Values.Add(exChange("G14"), obj.Cycle + "年度总水量");
                    pos2Values.Add(exChange("K14"), obj.ThisYearWaterQuality.ToString() + "  吨");
                    #endregion

                    #region 资源要素信息
                    if (lri.IsLandLtd == 1)
                    {
                        pos2Values.Add(exChange("B17"), "评价面积 " + GetSpaceString(obj.LandInfo_PJArea.ToString()) + " 亩，其中持证面积" + GetSpaceString(obj.LandInfo_CZArea.ToString(),4) + "亩");

                        if (obj.PjAreaChar_1 == "全部自用")
                            pos2Values.Add(exChange("B18"), "        ■全部自用    □自用 + 出租   □全部出租  □其他");
                        else if (obj.PjAreaChar_1 == "自用 + 出租")
                            pos2Values.Add(exChange("B18"), "        □全部自用    ■自用 + 出租   □全部出租  □其他");
                        else if (obj.PjAreaChar_1 == "全部出租")
                            pos2Values.Add(exChange("B18"), "        □全部自用    □自用 + 出租   ■全部出租  □其他");
                        else
                            pos2Values.Add(exChange("B18"), "        □全部自用    □自用 + 出租   □全部出租  ■其他:  " + obj.PjAreaChar_1 + "。");

                        pos2Values.Add(exChange("B19"), "        其中自用 " + GetSpaceString(obj.PjAreaChar_2_Self.ToString()) + " 亩，出租 " + GetSpaceString(obj.PjAreaChar_2_Rent.ToString()) + " 亩");
                        pos2Values.Add(exChange("B20"), "        土地所有权人：" + GetSpaceString(obj.PjAreaChar_3,20) + "。");
                        pos2Values.Add(exChange("B21"), "        土地使用税缴纳人：" + GetSpaceString(obj.PjAreaChar_4,10) + "。");
                    }
                    else
                    {

                        if (obj.RentLtd_LandHolderType == "发展总公司")
                            pos2Values.Add(exChange("B17"), "■发展总公司       □村委会       □企业");
                        else if (obj.RentLtd_LandHolderType == "村委会")
                            pos2Values.Add(exChange("B17"), "□发展总公司       ■村委会       □企业");
                        else if (obj.RentLtd_LandHolderType == "企业")
                            pos2Values.Add(exChange("B17"), "□发展总公司       □村委会       ■企业");
                       
                        pos2Values.Add(exChange("B18"), obj.RentLtd_LandHolderName);
                        pos2Values.Add(exChange("K18"), obj.RentLtd_LandHolderNSRSBH);
                        if (obj.RentLtd_PJArea_Land != null && obj.RentLtd_PJArea_Land.Trim() != "")
                        {
                            pos2Values.Add(exChange("B19"), "■租用土地  ");
                            pos2Values.Add(exChange("D19"), GetSpaceString(obj.RentLtd_PJArea_Land) + " 亩。");
                        }
                        if (obj.RentLtd_PJArea_House != null && obj.RentLtd_PJArea_Land.Trim() != "")
                        {
                            pos2Values.Add(exChange("I19"), "■租用房屋");
                            pos2Values.Add(exChange("K19"), GetSpaceString(obj.RentLtd_PJArea_Land) + " ㎡。");
                        }
                    }
                    #endregion

                    #region 三、综合素质加分信息 从28行开始，动态列按一列计算
                    int Row = 28;
                    if (lri.IsLandLtd == 1)
                        Row = 28;
                    else
                        Row = 23;
                    if (obj.AddScore_Research == "国家级")
                        pos2Values.Add(exChange("B" + Row.ToString()), "■国家级      □省级     □市级     □无");
                    else if (obj.AddScore_Research == "省级")
                        pos2Values.Add(exChange("B" + Row.ToString()), "□国家级      ■省级     □市级     □无");
                    else if (obj.AddScore_Research == "市级")
                        pos2Values.Add(exChange("B" + Row.ToString()), "□国家级      □省级     ■市级     □无");
                    else 
                        pos2Values.Add(exChange("B" + Row.ToString()), "□国家级      □省级     □市级     ■无");

                    Row++;
                    if (obj.AddScore_Brand == "驰名商标")
                        pos2Values.Add(exChange("B" + Row.ToString()), "■驰名商标      □著名商标     □江苏名牌     □知名商标      □苏州名牌     □无");
                    else if (obj.AddScore_Brand == "著名商标")
                        pos2Values.Add(exChange("B" + Row.ToString()), "□驰名商标      ■著名商标     □江苏名牌     □知名商标      □苏州名牌     □无");
                    else if (obj.AddScore_Brand == "江苏名牌")
                        pos2Values.Add(exChange("B" + Row.ToString()), "□驰名商标      □著名商标     ■江苏名牌     □知名商标      □苏州名牌     □无");
                    else if (obj.AddScore_Brand == "知名商标")
                        pos2Values.Add(exChange("B" + Row.ToString()), "□驰名商标      □著名商标     □江苏名牌     ■知名商标      □苏州名牌     □无");
                    else if (obj.AddScore_Brand == "苏州名牌")
                        pos2Values.Add(exChange("B" + Row.ToString()), "□驰名商标      □著名商标     □江苏名牌     □知名商标      ■苏州名牌     □无");
                    else
                        pos2Values.Add(exChange("B" + Row.ToString()), "□驰名商标      □著名商标     □江苏名牌     □知名商标      □苏州名牌     ■无");

                    Row++;
                    if (obj.AddScore_Quality == "中国质量奖")
                        pos2Values.Add(exChange("B" + Row.ToString()), "■中国质量奖      □江苏省省长质量奖     □苏州市市长质量奖     □无");
                    else if (obj.AddScore_Quality == "江苏省省长质量奖")
                        pos2Values.Add(exChange("B" + Row.ToString()), "□中国质量奖      ■江苏省省长质量奖     □苏州市市长质量奖     □无");
                    else if (obj.AddScore_Quality == "苏州市市长质量奖")
                        pos2Values.Add(exChange("B" + Row.ToString()), "□中国质量奖      □江苏省省长质量奖     ■苏州市市长质量奖     □无");
                    else 
                        pos2Values.Add(exChange("B" + Row.ToString()), "□中国质量奖      □江苏省省长质量奖     □苏州市市长质量奖     ■无");


                    Row++;
                    if (obj.AddScore_Standard == "第一起草人国际标准")
                        pos2Values.Add(exChange("B" + Row.ToString()), "第一起草人 ■国际标准 □国家标准 □行业标准    参与制定□国际标准 □国家标准 □行业标准     □无");
                    else if (obj.AddScore_Standard == "第一起草人国家标准")
                        pos2Values.Add(exChange("B" + Row.ToString()), "第一起草人 □国际标准 ■国家标准 □行业标准    参与制定□国际标准 □国家标准 □行业标准     □无");
                    else if (obj.AddScore_Standard == "第一起草人行业标准")
                        pos2Values.Add(exChange("B" + Row.ToString()), "第一起草人 □国际标准 □国家标准 ■行业标准    参与制定□国际标准 □国家标准 □行业标准     □无");
                    else if (obj.AddScore_Standard == "参与制定国际标准")
                        pos2Values.Add(exChange("B" + Row.ToString()), "第一起草人 □国际标准 □国家标准 □行业标准    参与制定■国际标准 □国家标准 □行业标准     □无");
                    else if (obj.AddScore_Standard == "参与制定国家标准")
                        pos2Values.Add(exChange("B" + Row.ToString()), "第一起草人 □国际标准 □国家标准 □行业标准    参与制定□国际标准 ■国家标准 □行业标准     □无");
                    else if (obj.AddScore_Standard == "参与制定行业标准")
                        pos2Values.Add(exChange("B" + Row.ToString()), "第一起草人 □国际标准 □国家标准 □行业标准    参与制定□国际标准 □国家标准 ■行业标准     □无");
                    else 
                        pos2Values.Add(exChange("B" + Row.ToString()), "第一起草人 □国际标准 □国家标准 □行业标准    参与制定□国际标准 □国家标准 □行业标准     ■无");

                    Row++;
                    if (obj.AddScore_Credit == "国家级")
                        pos2Values.Add(exChange("B" + Row.ToString()), "■国家级      □省级     □市级     □无");
                    else if (obj.AddScore_Credit == "省级")
                        pos2Values.Add(exChange("B" + Row.ToString()), "□国家级      ■省级     □市级     □无");
                    else if (obj.AddScore_Credit == "市级")
                        pos2Values.Add(exChange("B" + Row.ToString()), "□国家级      □省级     ■市级     □无");
                    else 
                        pos2Values.Add(exChange("B" + Row.ToString()), "□国家级      □省级     □市级     ■无");

                    Row++;
                    if (obj.AddScore_Safety == "通过一级评审")
                        pos2Values.Add(exChange("B" + Row.ToString()), "■通过一级评审    □通过二级评审     □通过三级评审     □无");
                    else if (obj.AddScore_Safety == "通过二级评审")
                        pos2Values.Add(exChange("B" + Row.ToString()), "□通过一级评审    ■通过二级评审     □通过三级评审     □无");
                    else if (obj.AddScore_Safety == "通过三级评审")
                        pos2Values.Add(exChange("B" + Row.ToString()), "□通过一级评审    □通过二级评审     ■通过三级评审     □无");
                    else
                        pos2Values.Add(exChange("B" + Row.ToString()), "□通过一级评审    □通过二级评审     □通过三级评审     ■无");

                    Row++;
                    if (obj.AddScore_EnviProtect == "绿色企业")
                        pos2Values.Add(exChange("B" + Row.ToString()), "■绿色企业     □蓝色企业     □无");
                    else if (obj.AddScore_EnviProtect == "蓝色企业")
                        pos2Values.Add(exChange("B" + Row.ToString()), "□绿色企业     ■蓝色企业     □无");
                    else
                        pos2Values.Add(exChange("B" + Row.ToString()), "□绿色企业     □蓝色企业     ■无");

                    Row++;
                    if (obj.AddScore_Tax == "是")
                        pos2Values.Add(exChange("B" + Row.ToString()), "纳税信用是否A级    ■是  □否");
                    else if (obj.AddScore_Tax == "否")
                        pos2Values.Add(exChange("B" + Row.ToString()), "纳税信用是否A级    □是  ■否");

                    Row++;
                    if (obj.AddScroe_Patent == "有")
                        pos2Values.Add(exChange("B" + Row.ToString()), "近3年有无发明专利授权  ■有  □无");
                    else //if (obj.AddScroe_Patent == "无")
                        pos2Values.Add(exChange("B" + Row.ToString()), "近3年有无发明专利授权  □有  ■无");
                    #endregion

                    #region 四、提档降档信息
                    if (lri.IsLandLtd == 1)
                        Row = 39;
                    else
                        Row = 34;
                    if (obj.GradUD_Cost == "超1000万元")
                        pos2Values.Add(exChange("A" + Row.ToString()), "上年度工业设备投入是否  超1000万元 ■是/超亿元□是/超10亿元□是   □否");
                    else if (obj.GradUD_Cost == "超亿元")
                        pos2Values.Add(exChange("A" + Row.ToString()), "上年度工业设备投入是否  超1000万元 □是/超亿元■是/超10亿元□是   □否");
                    else if (obj.GradUD_Cost == "超10亿元")
                        pos2Values.Add(exChange("A" + Row.ToString()), "上年度工业设备投入是否  超1000万元 □是/超亿元□是/超10亿元■是   □否");
                    else if (obj.GradUD_Cost == "否")
                        pos2Values.Add(exChange("A" + Row.ToString()), "上年度工业设备投入是否  超1000万元 □是/超亿元□是/超10亿元□是   ■否");

                    Row++;
                    if (obj.GradUD_Leader == "是")
                        pos2Values.Add(exChange("A" + Row.ToString()), "是否科技领军人才企业                         ■是  □否");
                    else if (obj.GradUD_Leader == "否")
                        pos2Values.Add(exChange("A" + Row.ToString()), "是否科技领军人才企业                         □是  ■否");

                    Row++;
                    if (obj.GradUD_HiTech == "是")
                        pos2Values.Add(exChange("A" + Row.ToString()), "是否高新技术企业                             ■是  □否");
                    else if (obj.GradUD_HiTech == "否")
                        pos2Values.Add(exChange("A" + Row.ToString()), "是否高新技术企业                             □是  ■否");

                    Row++;
                    if (obj.GradUD_Specialty == "是")
                        pos2Values.Add(exChange("A" + Row.ToString()), "是否专精特新企业                             ■是  □否");
                    else if (obj.GradUD_Specialty == "否")
                        pos2Values.Add(exChange("A" + Row.ToString()), "是否专精特新企业                             □是  ■否");

                    Row++;
                    if (obj.GradUD_Safety == "是")
                        pos2Values.Add(exChange("A" + Row.ToString()), "是否上年度安全生产、环境保护红黄牌未摘牌企业    ■是  □否");
                    else if (obj.GradUD_Safety == "否")
                        pos2Values.Add(exChange("A" + Row.ToString()), "是否上年度安全生产、环境保护红黄牌未摘牌企业    □是  ■否");

                    Row++;
                    if (obj.GradUD_LandSupplyLess3Years == "是")
                        pos2Values.Add(exChange("A" + Row.ToString()), "是否供地未满三年企业                          ■是  □否");
                    else if (obj.GradUD_LandSupplyLess3Years == "否")
                        pos2Values.Add(exChange("A" + Row.ToString()), "是否供地未满三年企业                          □是  ■否");

                    Row++;
                    if (obj.GradUD_ReOrgnizeLess2Years == "是")
                        pos2Values.Add(exChange("A" + Row.ToString()), "是否通过兼并重组取得土地使用权未满两年企业      ■是  □否");
                    else if (obj.GradUD_ReOrgnizeLess2Years == "否")
                        pos2Values.Add(exChange("A" + Row.ToString()), "是否通过兼并重组取得土地使用权未满两年企业      □是  ■否");

                    Row++;
                    if (obj.GradUD_Inappropriate == "是")
                        pos2Values.Add(exChange("A" + Row.ToString()), "是否镇（区）认定暂不适宜参加评价企业            ■是  □否");
                    else if (obj.GradUD_Inappropriate == "否")
                        pos2Values.Add(exChange("A" + Row.ToString()), "是否镇（区）认定暂不适宜参加评价企业            □是  ■否");

                    #endregion

                }
                catch(Exception ex)
                {
                    LogHelper.Error(ex);
                }
                saveToPath = excl.ExportDiyToExcl(downFileName, templateFileName, pos2Values, "web");

                //租户信息
                if (lri.IsLandLtd == 1)
                {
                    //动态复制行，默认有一个空行
                    List<LtdUpInfoRent> rents = EntityManager_Static.GetListNoPaging<LtdUpInfoRent>(DbContext, "LUI_Id=" + idValue, "Id");
                    for(int i=0;i<rents.Count;i++)
                    {
                        LtdUpInfoRent rent = rents[i];
                        rents[i].IsGY = rents[i].IsGY == "是" ? "■是□否" : "□是■否";
                        if (rent.SelfOrRent=="自用")
                            rents[i].LtdName = "("+rent.SelfOrRent+")"+rents[i].LtdName;
                        else
                            rents[i].LtdName = "(" + rent.SelfOrRent+i.ToString() + ")" + rents[i].LtdName;
                    }
                    excl.Export2ExcelWithInsertRow(saveToPath, rents, "LtdName,LicenseCode,IsGY,FTYD,DBHH,SBHH,BZ", "1,2,4,6,8,11,12", 24, "A24:L24", "yyyy-MM-dd HH:mm:ss", "web");

                }
                return DownFile(sessionid, saveToPath, downFileName);
            }
            catch(Exception ex)
            {
                return DownFile(sessionid, saveToPath, downFileName);
                //Response.Redirect(" error / index ? message =");
            }
        }

        /// <summary>
        /// excel导出时判断转换位置（如：C12--》C,12）
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        private string exChange(string pos)
        {
            try
            {
                int sepindex = 0;
                foreach (char c in pos)
                {
                    if (c <= 58)
                        break;
                    sepindex++;
                }
                string col = pos.Substring(0, sepindex);
                string row = pos.Substring(sepindex);
                int icol = 0;
                for (int i = col.Length - 1; i >= 0; i--)
                {
                    char c = col[i];
                    icol += (c - 'A'+1) * (int)Math.Pow(26, i);

                }
                return icol.ToString() + "," + row;
            }
            catch { return "0,0"; }
        }
    }
}
