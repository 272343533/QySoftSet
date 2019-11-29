using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;

namespace QyTech.Medicine.diabetes
{
    public class Analysis
    {
        public string Predict(string filepath)
        {
            //判断文件是否存在，格式是否正确为csv文件
            if (!File.Exists(filepath))
                return "";
            StringBuilder ret = new StringBuilder();
            StringBuilder msg = new StringBuilder();

            int retvalue=CppDll.predict(filepath, ret, msg);

            if (retvalue == 0)
                return ret.ToString();
            else
                return msg.ToString();

        }

        public string Predict_V1(string databycomma)
        {
            StringBuilder ret = new StringBuilder();
            StringBuilder msg = new StringBuilder();


            QyTech.Core.LogHelper.Info(databycomma);
            int retvalue = CppDll.Predict_V1(databycomma, ret, msg);
            QyTech.Core.LogHelper.Info(retvalue.ToString()+":"+ret.ToString()+"-"+msg.ToString());

            if (retvalue == 0)
                return ret.ToString();
            else
                return msg.ToString();

        }

        //public string Predict(string filepath)
        //{
        //    //判断文件是否存在，格式是否正确为csv文件
        //    if (!File.Exists(filepath))
        //        return "";
        //    return CppDll.list_return(filepath);

        //}

        //public string Predict1(string filepath)
        //{
        //    IntPtr ptrIn = Marshal.StringToHGlobalAnsi(filepath);
        //    IntPtr ptrRet = CppDll.list_return(ptrIn);
        //    string result = Marshal.PtrToStringAnsi(ptrRet);
        //    return result;
        //}


        public string TestFun(string data)
        {
            try
            {
                //////判断文件是否存在，格式是否正确为csv文件
                //if (!File.Exists(filepath))
                //    return "";
                //StringBuilder res = new StringBuilder(100, 200);
                ////res.Append(result);
                //StringBuilder errmsg = new StringBuilder(100,200);
                ////errmsg.Append(errMsg);

                //int intret= CppDll.fun(filepath, res, errmsg);
                //result = res.ToString();
                //errMsg = errmsg.ToString();
                //return intret.ToString();

                //IntPtr ptrIn = Marshal.StringToHGlobalAnsi(filepath);
                //IntPtr ptrRet = CppDll.fun(ptrIn);
                //string result = Marshal.PtrToStringAnsi(ptrRet);
                //return result;

                StringBuilder ret = new StringBuilder();
                StringBuilder msg = new StringBuilder();


                QyTech.Core.LogHelper.Info(data);
                int retvalue = CppDll.fun(data, ret, msg);
                QyTech.Core.LogHelper.Info(retvalue.ToString() + ":" + ret.ToString() + "-" + msg.ToString());

                if (retvalue == 0)
                    return ret.ToString();
                else
                    return msg.ToString();
            }


            catch (Exception ex)
            {
                return "aaa";
            }
        }

        public string TestFunInt(string filepath)
        {
            try
            {
                ////判断文件是否存在，格式是否正确为csv文件
                //if (!File.Exists(filepath))
                //    return "";
                StringBuilder sb = new StringBuilder();
                sb.Append(filepath);
                int a1 = CppDll.funint(filepath);

                return a1.ToString();

                //IntPtr ptrIn = Marshal.StringToHGlobalAnsi(filepath);
                //IntPtr ptrRet = CppDll.funint(ptrIn);
                //string result = Marshal.PtrToStringAnsi(ptrRet);
                //return result;
            }


            catch (Exception ex)
            {
                return "aaa";
            }
        }
    }

 
}
