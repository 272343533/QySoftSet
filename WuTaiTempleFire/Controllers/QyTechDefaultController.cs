using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WuTaiTempleFire.Controllers
{
    public class QyTechDefaultController : WuTaiTempleController
    {
        //
        // GET: /QyTechDefault/

        public ActionResult Index()
        {
            return View();
        }


        public override string GetAll(string fields = "", string where = "", string orderby = "")
        {
            return base.GetAll(fields, where, orderby);
        }
    }
}
