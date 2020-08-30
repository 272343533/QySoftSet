using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QyTech.Core.ExController;
using qyExpress.Dao;
using QyTech.Core.BLL;
using QyTech.Core;
using QyTech.Core.Common;
using QyTech.Json;
using QyTech.BLL;


namespace QyExpress.Controllers.api
{
    public class bsNavigationController : apiController
    {
        public override string Add(string sessionid, string strjson)
        {
            if (strjson == null || strjson.Equals(""))
            {
                return jsonMsgHelper.Create(1, null, "参数为空，无法增加");
            }
            bsNavigation rowdataobj;
            try
            {
                rowdataobj = JsonHelper.DeserializeJsonToObject<bsNavigation>(strjson);
                string ret = EManager.Add<bsNavigation>(rowdataobj);

                if (ret.ToString() == "")
                    return jsonMsgHelper.Create(0, "", "新增成功！");
                else
                    return jsonMsgHelper.Create(1, "", new Exception(ret));
            }
            catch (Exception ex)
            {
                LogHelper.Error("Add:" + ex.Message);
                return jsonMsgHelper.Create(1, "", ex);
            }
        }

        public override string Edit(string sessionid, string strjson)
        {
            if (strjson == null || strjson.Equals(""))
            {
                return jsonMsgHelper.Create(1, null, "参数为空，无法增加");
            }
            bsNavigation rowdataobj;
            try
            {
                rowdataobj = JsonHelper.DeserializeJsonToObject<bsNavigation>(strjson);
                bsNavigation dbobj = EManager.GetByPk<bsNavigation>("bsN_Id", rowdataobj.bsN_Id);
                dbobj = EntityOperate.Copy<bsNavigation>(rowdataobj);

                string ret = EManager.Modify<bsNavigation>(rowdataobj);

                if (ret.ToString() == "")
                    return jsonMsgHelper.Create(0, "", "编辑成功！");
                else
                    return jsonMsgHelper.Create(1, "", new Exception(ret));
            }
            catch (Exception ex)
            {
                LogHelper.Error("Add:" + ex.Message);
                return jsonMsgHelper.Create(1, "", ex);
            }
        }

        public override string GetOne(string sessionid, string idValue)
        {
            if (idValue == null || idValue.Equals(""))
            {
                return jsonMsgHelper.Create(1, "", "参数必须输入");
            }
            try
            {
               
                Guid idV = Guid.Parse(idValue);
                bsNavigation dbobj = EManager.GetByPk<bsNavigation>("bsN_Id", idV);

                if (dbobj != null)
                    return jsonMsgHelper.Create(0, dbobj, "成功获取！");
                else
                    return jsonMsgHelper.Create(1, null, "获取失败！");
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return jsonMsgHelper.Create(1, "", "获取失败");
            }
        }

        public override string Delete(string sessionid, string idValue)
        {
            if (idValue == null || idValue.Equals(""))
            {
                return jsonMsgHelper.Create(1, "", "参数必须输入");
            }
            try
            {

                Guid idV = Guid.Parse(idValue);
                string ret= EManager.DeleteById<bsNavigation>("bsN_Id", idV);

                if (ret.ToString() == "")
                    return jsonMsgHelper.Create(0, "", "删除成功！");
                else
                    return jsonMsgHelper.Create(1, "", new Exception(ret));
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return jsonMsgHelper.Create(1, "", "获取失败");
            }
        }

        /// <summary>
        ///  配置导航使用  重写了getall,需要吗？//2018-10-18
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="where"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public override string GetAll(string sessionid, string fields,string where="", string orderby="")
        {
            //1.应该与用户账号相关，默认的getall没有考虑账号，

            //2.应该对有些表，根据权限获取数据 zhwsun 2018-10-08 根据用户权限在结合where结果进行过滤
            ObjectClassFullName = ObjectClassFullName.Replace(StrForReplaceObject, objNameSpace + "." + "bsNavigation");

          

            return base.GetAll(sessionid,fields, where, orderby);
        }

        /// <summary>
        /// 重写的gettree
        /// </summary>
        /// <param name="where">不需要</param>
        /// <param name="orderby">不需要</param>
        /// <returns></returns>
        public string getUserTree(string sessionid, string where="", string orderby="")
        {
            bllbsNavigation bobj = new bllbsNavigation();
            List<qytvNode> nodes= bobj.GetNaviNodes(EManager,LoginUser);
            string json = jsonMsgHelper.Create(0, nodes, "");
            return json;
        }

       
    }
}
