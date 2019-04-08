using System;
using System.Text;
using System.Web.Mvc;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;


namespace QyTech.Core.ExController
{

    public partial class UpDownFileController : Controller
    {

        /// <summary>
        /// 保存图片到Uploads文件夹，前端预览后上传预览的数据
        /// </summary>
        /// <param name="picString">图片上传数据</param>
        /// <returns>图片文件名</returns>
        //[HttpPost]
        public string PictureUpLoad(string picString, string subpath = "")
        {
            string files = "";
            try
            {
                files = QyTech.Core.ExController.Helper.PicUpHelper.Save(picString, subpath);
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);

            }
            return files;
        }
       

        /// <summary>
        /// 上传文件,local可以，但是服务器上不行
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public string Upload_Post1(string subpath)
        {
            subpath = Request["subpath"];
            if (Request.Files.Count == 0)
            {
                //Request.Files.Count 文件数为0上传不成功
                return "请选择文件上传！";
            }
            string filenames = "";
            for (int i = 0; i < Request.Files.Count;i++)
            {
                var file = Request.Files[i];
                if (file.ContentLength == 0)
                {
                    //文件大小大（以字节为单位）为0时，做一些操作
                    return "文件大小为0，不能上传！";
                }
                else
                {
                    //文件大小不为0
                    file = Request.Files[i];
                    //保存成自己的文件全路径,newfile就是你上传后保存的文件,
                    //服务器上的UpLoadFile文件夹必须有读写权限
                    string target = Server.MapPath("/") + ("/Uploads/");//取得目标文件夹的路径
                    string filename = subpath + DateTime.Now.ToString("yyyyMMddHHmmss_ffff", System.Globalization.DateTimeFormatInfo.InvariantInfo)+file.FileName.Substring(file.FileName.LastIndexOf('.'));
                    ;//取得文件名字
                    string path = target + filename;//获取存储的目标地址
                    file.SaveAs(path);
                    filenames += "," + filename;
                }
            }
            return filenames;
        }

        public string Upload_Default(string subpath)
        {
            LogHelper.Info(subpath);
            subpath = Request["subpath"];
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
                string filePath = Server.MapPath("~/Uploads/" + subpath);
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
                    LogHelper.Info("4000:"+target);
                    string filename = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", System.Globalization.DateTimeFormatInfo.InvariantInfo) + file.FileName.Substring(file.FileName.LastIndexOf('.'));
                    ;//取得文件名字
                    string path = target + filename;//获取存储的目标地址
                    LogHelper.Info("5000:"+ path);
                    file.SaveAs(path);
                    LogHelper.Info("6000");
                    filenames += "|" + subpath+filename;
                }
            }
            LogHelper.Info("10000");
            if (filenames.Length > 1)
                filenames = filenames.Substring(1);
            return filenames;
        }
        public string Upload_Direct(string subpath)
        {
            string filename = "";
            
            if (Request.Files.Count!=0)
            {
                HttpPostedFileBase fileData = Request.Files[0];
                try
                {
                    // 文件上传后的保存路径
                    string filePath = Server.MapPath("~/Uploads/"+subpath);
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    string fileName = Path.GetFileName(fileData.FileName);// 原始文件名称
                    string fileExtension = Path.GetExtension(fileName); // 文件扩展名
                    string saveName = Guid.NewGuid().ToString() + fileExtension; // 保存文件名称
                    filename = filePath + saveName;
                    fileData.SaveAs(filename);

                    return filename;
                }
                catch (Exception ex)
                {
                    LogHelper.Error(ex);
                }
            }
            else
            {

                filename="请选择文件！";
            }
            return filename;
        }



        /// <summary>
        /// 下载文件：<a href="/DownloadFile/Download?filePath=@ViewBag.Value&fileName='小王子.pdf'">下载</a>
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public ActionResult Download(string filePath, string fileName)
        {
            Encoding encoding;
            string outputFileName = null;
            fileName = fileName.Replace("'", "");

            string browser = Request.UserAgent.ToUpper();
            if (browser.Contains("MS") == true && browser.Contains("IE") == true)
            {
                outputFileName = HttpUtility.UrlEncode(fileName);
                encoding = Encoding.Default;
            }
            else if (browser.Contains("FIREFOX") == true)
            {
                outputFileName = fileName;
                encoding = Encoding.GetEncoding("GB2312");
            }
            else
            {
                outputFileName = HttpUtility.UrlEncode(fileName);
                encoding = Encoding.Default;
            }
            FileStream fs = new FileStream(filePath, FileMode.Open);
            byte[] bytes = new byte[(int)fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();
            Response.Charset = "UTF-8";
            Response.ContentType = "application/octet-stream";
            Response.ContentEncoding = encoding;
            Response.AddHeader("Content-Disposition", "attachment; filename=" + outputFileName);
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
            return new EmptyResult();
        }

     
        public  string TestFile()
        {
            return "ok";
        }
    }
}
