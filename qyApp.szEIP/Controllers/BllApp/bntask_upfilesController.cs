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
    public class bntask_upfilesController : BllAppController
    {
        public override string Upload(string subpath)
        {
            LogHelper.Error("upload");
            return base.Upload("bntask/"+ subpath+"/");
        }
        public override string UploadWithName(string subpath)
        {
            LogHelper.Error("uploadWithName");
            return base.UploadWithName("bntask/" + subpath + "/");
        }
        public override string Uploads(string subpath)
        {
            return base.Uploads("bntask/" + subpath + "/");
        }
        public override string UploadsWithName(string subpath)
        {
            return base.UploadsWithName("bntask/" + subpath + "/");
        }
    }
}
