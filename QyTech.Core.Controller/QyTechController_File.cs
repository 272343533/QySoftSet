
using System.Text;
using System.Web.Mvc;
using System.Web;

using System.IO;


namespace QyTech.Core.ExController
{

    public partial class QyTechController:Controller
    {

        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="saveToPath">要下载文件所在位置（一般是导出后保存的文件名）</param>
        /// <param name="downFileName">下载后文件名称（一般如果是导出后可能带日期标记）</param>
        /// <returns></returns>
        protected ActionResult DownFile(string sessionid, string serverfilePath, string downFileName)
        {
            LogHelper.Info("ReturnFile", serverfilePath + " down(ws) to:" + downFileName + "浏览器：" + HttpContext.Request.Browser.Browser);
        
            if (HttpContext.Request.Browser.Browser == "InternetExplorer" || HttpContext.Request.Browser.Browser == "IE")
            {
                return File(serverfilePath, "application/octet-stream; charset=utf-8", Url.Encode(downFileName));
            }
            else if (HttpContext.Request.Browser.Browser == "Chrome")
            {
                //return File(saveToPath, "application/octet-stream; charset=utf-8", downFileName);
                return File(serverfilePath, "application/msexcel; charset=utf-8", downFileName);
            }
            else
            {
                return File(serverfilePath, "application/octet-stream; charset=utf-8", HttpContext.Request.Browser.Browser + downFileName);
            }
        }

    }
}
