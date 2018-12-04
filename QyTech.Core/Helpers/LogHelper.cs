using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Reflection;
using log4net;
using System.Web.Routing;
using System.Web;
using System.Diagnostics;

namespace QyTech.Core
{

    /// <summary>
    /// 日志的记录类
    /// </summary>
    public class LogHelper 
    {
        public static ILog log = log4net.LogManager.GetLogger("QyTech.LogHelper");


        public static void Info(string methodname, string msg)
        {
            log = LogManager.GetLogger(methodname);
            log.Info(msg);
        }
        public static void Info(string msg)
        {
            string methodName = GetMethodName(2);
            Info(methodName, msg);
        }

        public static void Error(string msg)
        {
            string methodName = GetMethodName(2);

            log = LogManager.GetLogger(methodName);
            log.Error(msg);
        }
        public static void Error(string adddesp, string msg)
        {
            string methodName = GetMethodName(2);

            log = LogManager.GetLogger(methodName + "-" + adddesp);
            log.Error(msg);
        }
        public static void Error(Exception ex)
        {
            string methodName = GetMethodName(2);
            string msg = Parse(ex);
            //if (ex.InnerException != null)
            //{
            //    msg += "(" + ex.InnerException.Message + ")";
            //}
            Error(methodName, msg);
        }
        public static void Error(string adddesp, Exception ex)
        {
            string methodName = GetMethodName(2);
            string msg = adddesp+":---"+ Parse(ex); ;
            Error(methodName, msg);
        }
 


        public static void Fatal(string msg)
        {
            string methodName = GetMethodName(2);

            log = LogManager.GetLogger(methodName);
            log.Fatal(msg);
        }

        private static string GetMethodName(int layer)
        {
            var method = new StackFrame(layer).GetMethod(); // 这里忽略1层堆栈，也就忽略了当前方法GetMethodName，这样拿到的就正好是外部调用GetMethodName的方法信息
            var property = (
            from p in method.DeclaringType.GetProperties(
            BindingFlags.Instance |
            BindingFlags.Static |
            BindingFlags.Public |
            BindingFlags.NonPublic)
            where p.GetGetMethod(true) == method || p.GetSetMethod(true) == method
            select p).FirstOrDefault();
            return property == null ? method.Name : property.Name;
        }



        public static string Parse(Exception ex)
        {
            string errmsg = ex.Message;
            if (ex.InnerException != null)
                errmsg += "(明细:" + ex.InnerException.Message + ")";
            return errmsg;
        }
    }
}
