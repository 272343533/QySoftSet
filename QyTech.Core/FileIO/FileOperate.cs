using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace SunMvcExpress.Core.FileIO
{
    public class FileOperate
    {

        /// <summary>
        /// 两个文件合并到令一个文件的前面
        /// </summary>
        /// <param name="infileName">要合并的文件</param>
        /// <param name="outfileName">合并的目标文件</param>
        public static  void CombineToFile(String infileName, String outfileName)
        {
            int b;
            int n = infileName.Length;
            FileStream fileIn = new FileStream(infileName, FileMode.Open);

            using (FileStream fileOut = new FileStream(outfileName, FileMode.Append))//.Create))shi
            {
                try
                {
                    while ((b = fileIn.ReadByte()) != -1)
                        fileOut.WriteByte((byte)b);
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    fileIn.Close();
                }

            }
        }
    }
}
