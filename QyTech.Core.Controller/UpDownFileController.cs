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
        public string PictureUpLoad(string picString)
        {
            string files = "";
            try
            {
                var tmpArr = picString.Split(',');
                for (int i = 0; i < tmpArr.Length - 1; i++)
                {

                    byte[] bytes = Convert.FromBase64String(tmpArr[i + 1]);
                    MemoryStream ms = new MemoryStream(bytes);
                    ms.Write(bytes, 0, bytes.Length);
                    var img = Image.FromStream(ms, true);

                    var path = System.AppDomain.CurrentDomain.BaseDirectory;
                    var imagesPath = System.IO.Path.Combine(path, @"Uploads\");
                    if (!System.IO.Directory.Exists(imagesPath))
                        System.IO.Directory.CreateDirectory(imagesPath);

                    string fileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                    string srcFullname = imagesPath + fileName + "_1.jpg";
                    string compressfullname = imagesPath + fileName + ".jpg";
                    img.Save(srcFullname);

                    bool ret = QyTech.Core.CommUtils.ImageUtril.CompressImage(srcFullname, compressfullname);
                    if (ret)
                        files += "," + fileName + ".jpg";
                    else
                        files += "," + fileName + "_1.jpg";

                    i = i + 1;
                }
                if (files.Length > 0)
                    files = files.Substring(0);//不去掉开始的逗号更好，匹配删除的时候，防止重名，
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);

            }
            return files;
        }


        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Upload(FormCollection form)
        {
            if (Request.Files.Count == 0)
            {
                //Request.Files.Count 文件数为0上传不成功
                return View();
            }
            var file = Request.Files[0];
            if (file.ContentLength == 0)
            {
                //文件大小大（以字节为单位）为0时，做一些操作
                return View();
            }
            else
            {
                //文件大小不为0
                file = Request.Files[0];
                //保存成自己的文件全路径,newfile就是你上传后保存的文件,
                //服务器上的UpLoadFile文件夹必须有读写权限
                string target = Server.MapPath("/") + ("/Mock/Learning/");//取得目标文件夹的路径
                string filename = file.FileName;//取得文件名字
                string path = target + filename;//获取存储的目标地址
                file.SaveAs(path);
            }
            return View();
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

     
        
    }
}
