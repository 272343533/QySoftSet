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
    public class bnannounceController : BllAppController
    {
        public override string Upload(string subpath)
        {
            return base.Upload("announce/" + subpath + "/");
        }

        public override string UploadWithName(string subpath)
        {
            LogHelper.Error("announce：uploadWithName");
            return base.UploadWithName("announce/" + subpath + "/");
        }
        public override string Uploads(string subpath)
        {
            return base.Uploads("announce/" + subpath + "/");
        }
        public override string UploadsWithName(string subpath)
        {
            return base.UploadsWithName("announce/" + subpath + "/");
        }

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
    }
}
