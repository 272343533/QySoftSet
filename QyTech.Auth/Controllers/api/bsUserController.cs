using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QyTech.Core.ExController;
using QyTech.Auth.Dao;
using QyTech.Core.BLL;
using QyTech.Core;
using QyTech.Core.Common;
using QyTech.Json;
using System.Security.Cryptography;
namespace QyTech.Auth.Controllers.api
{
    public class bsUserController : AuthController
    {
        public string Login(string username, string password)
        {
            try
            {
                log.Info("login:" + username + "--" + password + ".");
                if ((username != null && password != null))
                {
                    bsUser obj = EM_Base.GetBySql<bsUser>("LoginName='" + username + "' and LoginPwd='" + MD5(password) + "'");

                    
                    if (obj != null)
                    {
                        if (obj.ValidDate <= DateTime.Now)
                        {
                            return jsonMsgHelper.Create(1, null, "账号已经过期，请联系管理员", null, null);
                        }
                        else
                        {
                            return jsonMsgHelper.CreateWithStrField(0, obj, "", obj.GetType(), "bsU_Id,bsO_Id,LoginName,NickName,ValidDate");
                        }
                    }
                    else
                        return jsonMsgHelper.Create(1, null, "账号或密码错误，请重新输入", null, null);
                }
                else
                {
                    return jsonMsgHelper.Create(1, null, "账号和密码不能为空", null, null);
                }
            }
            catch (Exception ex)
            {
                return jsonMsgHelper.Create(1, null, ex.Message, null, null);
            }
        }

        public static string MD5(string source)
        {
            byte[] result = System.Text.Encoding.Default.GetBytes(source);    //tbPass为输入密码的文本框
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            return BitConverter.ToString(output).Replace("-", "");

        }
        
    }
}
