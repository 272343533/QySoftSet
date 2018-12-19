using System;

using System.Web.Mvc;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;


namespace QyTech.Core.ExController
{

    public partial class QyTechController:Controller
    {

        /// <summary>
        /// 保存图片到Uploads文件夹，前端预览后上传预览的数据
        /// </summary>
        /// <param name="picString">逗号分隔的图片上传数据</param>
        /// <returns>逗号分隔的多个图片文件名</returns>
        protected string UpPicture(string sessionid,string picString)
        {
            LogHelper.Error(picString);
            string files = "";
            try
            {
                files = QyTech.Core.ExController.Helper.PicUpHelper.Save(picString);

                //var tmpArr = picString.Split(',');
                //for (int i = 0; i < tmpArr.Length - 1; i++)
                //{

                //    byte[] bytes = Convert.FromBase64String(tmpArr[i + 1]);
                //    MemoryStream ms = new MemoryStream(bytes);
                //    ms.Write(bytes, 0, bytes.Length);
                //    var img = Image.FromStream(ms, true);

                //    var path = System.AppDomain.CurrentDomain.BaseDirectory;
                //    var imagesPath = System.IO.Path.Combine(path, @"Uploads\");
                //    if (!System.IO.Directory.Exists(imagesPath))
                //        System.IO.Directory.CreateDirectory(imagesPath);

                //    string fileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                //    string srcFullname = imagesPath + fileName + "_1.jpg";
                //    string compressfullname = imagesPath + fileName + ".jpg";
                //    img.Save(srcFullname);

                //    bool ret = QyTech.Core.CommUtils.ImageUtril.CompressImage(srcFullname, compressfullname);
                //    if (ret)
                //        files +="|"+ fileName + ".jpg";
                //    else
                //        files+="|" + fileName + "_1.jpg";

                //    i = i + 1;
                //}
                //if (files.Length > 0)
                //    files = files.Substring(0);
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                
            }
            return files;
        }


    }
}
