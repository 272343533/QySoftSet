using System;
using System.Text;
using System.Web.Mvc;
using System.Web;

using System.IO;


namespace QyTech.Core.ExController
{

    public partial class QyTechController:Controller
    {

        /// <summary>
        /// 文件下载,用语带数据导出后的下载
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



        public virtual string Uploads(string subpath)
        {
            LogHelper.Info(subpath);
            subpath = CheckSubPath(subpath);

            LogHelper.Info(subpath);
            if (Request.Files.Count == 0)
            {
                //Request.Files.Count 文件数为0上传不成功
                return "请选择文件上传！";
            }
            string filenames = "";
            LogHelper.Info("1000");
            for (int i = 0; i < Request.Files.Count; i++)
            {
                // 文件上传后的保存路径
                string filePath = Server.MapPath("~/Uploads/" +subpath);
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                LogHelper.Info("2000");
                var file = Request.Files[i];
                if (file.ContentLength == 0)
                {
                    //文件大小大（以字节为单位）为0时，做一些操作
                    return "文件大小为0，不能上传！";
                }
                else
                {
                    //文件大小不为0
                    LogHelper.Info("3000");
                    file = Request.Files[i];
                    //保存成自己的文件全路径,newfile就是你上传后保存的文件,
                    //服务器上的UpLoadFile文件夹必须有读写权限
                    string target = Server.MapPath("~/Uploads/" + subpath);
                    //取得目标文件夹的路径
                    LogHelper.Info("4000:" + target);
                    string filename = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", System.Globalization.DateTimeFormatInfo.InvariantInfo) + file.FileName.Substring(file.FileName.LastIndexOf('.'));
                    ;//取得文件名字
                    string path = target + filename;//获取存储的目标地址
                    LogHelper.Info("5000:" + path);
                    file.SaveAs(path);
                    LogHelper.Info("6000");
                    filenames += "|" + subpath + filename;
                }
            }
            LogHelper.Info("10000");
            if (filenames.Length > 1)
                filenames = filenames.Substring(1);
            return filenames;
        }
        public virtual string Upload(string subpath)
        {
            string filename = "";
            subpath = CheckSubPath(subpath);

            if (Request.Files.Count != 0)
            {
                HttpPostedFileBase fileData = Request.Files[0];
                try
                {
                    // 文件上传后的保存路径
                    string filePath = Server.MapPath("~/Uploads/" + subpath);
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    string fileName = Path.GetFileName(fileData.FileName);// 原始文件名称
                    string fileExtension = Path.GetExtension(fileName); // 文件扩展名
                    string saveName = Guid.NewGuid().ToString() + fileExtension; // 保存文件名称
                    filename = filePath + saveName;
                    fileData.SaveAs(filename);

                    filename = filename.Replace(@"\", @"/");
                    int indexer = filename.IndexOf("/Uploads/");
                    return filename.Substring(indexer);
                }
                catch (Exception ex)
                {
                    LogHelper.Error(ex);
                }
            }
            else
            {

                filename = "请选择文件！";
            }
            return filename;
        }



        public virtual string UploadsWithName(string subpath)
        {
            LogHelper.Info(subpath);
            subpath = CheckSubPath(subpath);

            LogHelper.Info(subpath);
            if (Request.Files.Count == 0)
            {
                return "请选择文件上传！";
            }
            string filenames = "";
            for (int i = 0; i < Request.Files.Count; i++)
            {
                // 文件上传后的保存路径
                string filePath = Server.MapPath("~/Uploads/" + subpath);
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                LogHelper.Info("2000");
                var fileData = Request.Files[i];
                if (fileData.ContentLength == 0)
                {
                    return "文件大小为0，不能上传！";
                }
                else
                {
                    //文件大小不为0
                    LogHelper.Info("3000");
                    fileData = Request.Files[i];
                    //保存成自己的文件全路径,newfile就是你上传后保存的文件,
                    //服务器上的UpLoadFile文件夹必须有读写权限
                    string target = Server.MapPath("~/Uploads/" + subpath);
                    //取得目标文件夹的路径
                    LogHelper.Info("4000:" + target);
                    string fileName = Path.GetFileName(fileData.FileName); ;//取得文件名字
                    string path = target + fileName;//获取存储的目标地址
                    LogHelper.Info("5000:" + path);
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                    fileData.SaveAs(path);
                    LogHelper.Info("6000");
                    filenames += "|" + subpath + fileName;
                }
            }
            LogHelper.Info("10000");
            if (filenames.Length > 1)
                filenames = filenames.Substring(1);
            return filenames;
        }

        public virtual string UploadWithName(string subpath)
        {
            string filename = "";
            subpath = CheckSubPath(subpath);

            if (Request.Files.Count != 0)
            {
                HttpPostedFileBase fileData = Request.Files[0];
                try
                {
                    // 文件上传后的保存路径
                    string filePath = Server.MapPath("~/Uploads/" + subpath);
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    string fileName = Path.GetFileName(fileData.FileName);// 原始文件名称
                    filename = filePath + fileName;
                    if (System.IO.File.Exists(filename))
                    {
                        System.IO.File.Delete(filename);
                    }
                    fileData.SaveAs(filename);

                    filename = filename.Replace(@"\", @"/");
                    int indexer = filename.IndexOf("/Uploads/");
                    return filename.Substring(indexer);
                }
                catch (Exception ex)
                {
                    LogHelper.Error(ex);
                }
            }
            else
            {

                filename = "请选择文件！";
            }
            return filename;
        }

        private string CheckSubPath(string subpath)
        {
            subpath = subpath.Replace(@"\", @"/");
            subpath = subpath.Replace(@"//", @"/");

            if (subpath.Length > 0)
            {
                if (subpath.Substring(0, 1) == "/")
                    subpath = subpath.Substring(1);
                if (subpath.Substring(subpath.Length - 1, 1) != "/")
                    subpath = subpath+"/";
            }

            return subpath;
        }
    }
}
