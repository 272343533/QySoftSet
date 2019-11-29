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
namespace QyExpress.Controllers.BllApp
{
    public class LtdUpPicsController : BllAppController
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionid"></param>
        /// <param name="fields"></param>
        /// <param name="where">[{LUI_Id:列表上的主键}]</param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public override string GetAllData(string sessionid, string fields = "", string where = "", string orderby = "")
        {
            return base.GetAllData(sessionid, fields, where, orderby);
        }

        /// <summary>
        /// 图片信息上传
        /// </summary>
        /// <param name="sessionid"></param>
        /// <param name="IdValue">主键</param>
        /// <param name="PicFile">图片文件</param>
        /// <param name="PicMemo">图片备注</param>
        /// <returns></returns>
        public string EditPics(string sessionid,string IdValue, string PicFile, string PicMemo)
        {
            try
            {
                Dictionary<string, string> dicKvs = new Dictionary<string, string>();
                dicKvs.Add("PicMemo", PicMemo);
                dicKvs.Add("PicFile", PicFile);

                return base.EditbyKeyValues("Id", IdValue, dicKvs);
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message);
                return jsonMsgHelper.Create(1, "", ex);
            }
        }


        
        public string DelPics(string sessionid,string IdValue,string filename)
        {
            LtdUpPics dbobj = EntityManager_Static.GetByPk<LtdUpPics>(DbContext, "Id", Convert.ToInt32(IdValue));
            dbobj.PicFile = ("|" + dbobj.PicFile).Replace("|" + filename, "");
            if (dbobj.PicFile.Length>0)
                dbobj.PicFile=dbobj.PicFile.Substring(1);

            string ret = EntityManager_Static.Modify<LtdUpPics>(DbContext, dbobj);

            if (ret == "")
                return jsonMsgHelper.Create(0, "", "删除成功！");
            else
                return jsonMsgHelper.Create(1, "", "删除失败！");
        }

    }
}
