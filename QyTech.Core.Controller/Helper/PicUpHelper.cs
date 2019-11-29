using System;
using System.Text;
using System.Web.Mvc;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;


namespace QyTech.Core.ExController.Helper
{
    public class PicUpHelper
    {
        public static string Save(string picString,string subdir="")
        {
            string files = "";

            var tmpArr = picString.Split(',');
            for (int i = 0; i < tmpArr.Length - 1; i++)
            {

                byte[] bytes = Convert.FromBase64String(tmpArr[i + 1]);
                MemoryStream ms = new MemoryStream(bytes);
                ms.Write(bytes, 0, bytes.Length);
                var img = Image.FromStream(ms, true);

                var path = System.AppDomain.CurrentDomain.BaseDirectory;
                var imagesPath = System.IO.Path.Combine(path, @"Uploads\"+ subdir);
                if (!System.IO.Directory.Exists(imagesPath))
                    System.IO.Directory.CreateDirectory(imagesPath);

                string fileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                string srcFullname = imagesPath + fileName + "_1.jpg";
                string compressfullname = imagesPath + fileName + ".jpg";
                img.Save(srcFullname);

                bool ret = QyTech.Core.CommUtils.ImageUtril.CompressImage(srcFullname, compressfullname);
                if (ret)
                {
                    files += "|" + fileName + ".jpg";
                    System.IO.File.Delete(srcFullname);
                }
                else
                {
                    files += "|" + fileName + "_1.jpg";
                }

                i = i + 1;
            }
            if (files.Length > 0)
                files = files.Substring(1);
            //如果只有一个文件，则去掉前面的
            //if (tmpArr.Length<=3)
            //    files = files.Substring(1);

            return files;
        }
        
    }
}
