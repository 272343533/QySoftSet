using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using qyExpress.Dao;
using QyTech.Core.BLL;
using QyTech.Core;
namespace QyTech.Core.ExController.Bll
{
    public class bsSessionManager
    {
        private static int ValidMinutes = 120;
        public static bool Add(EntityManager EM, string sessionid,Guid bsU_Id)
        {
            try
            {
                bsSession obj_session = EM.GetByPk<bsSession>("SessionId", sessionid);
                if (obj_session == null)
                {
                    obj_session = new bsSession();
                    obj_session.Id = Guid.NewGuid();
                    obj_session.StartDt = DateTime.Now;
                    obj_session.SessionId = sessionid;
                    obj_session.bsU_Id = bsU_Id;
                    obj_session.InValidDt = DateTime.Now.AddMinutes(ValidMinutes);//30分钟内有效
                    EM.Add<bsSession>(obj_session);
                }
                else
                {
                    obj_session.bsU_Id = bsU_Id;
                    obj_session.InValidDt = DateTime.Now.AddMinutes(ValidMinutes);//30分钟内有效
                    EM.Modify<bsSession>(obj_session);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return false;
            }
        }

        public static bsUser GetLoginUser(EntityManager EM, string sessionid)
        {
            try
            {
                bsSession obj_seesion = EM.GetByPk<bsSession>("SessionId", sessionid);
                if (obj_seesion.InValidDt<DateTime.Now)
                {
                    EM.DeleteById<bsSession>("Id",obj_seesion.Id);
                    return null;
                }
                else
                {
                    obj_seesion.InValidDt = DateTime.Now.AddMinutes(ValidMinutes);
                    EM.Modify<bsSession>(obj_seesion);
                }
                return EM.GetByPk<bsUser>("bsU_Id", obj_seesion.bsU_Id); ;
            }
            catch { return null; }
        }


        public static List<string> GetUrlWithoutSession()
        {
            List<string> urls = new List<string>();
            urls.Add("bsuser/login".ToLower());
            urls.Add("bsuser/loginwithusertype".ToLower());
            urls.Add("bsuser/register".ToLower());
            urls.Add("UpDownFile/Download".ToLower());
            urls.Add("UpDownFile/Upload".ToLower());
            urls.Add("UpDownFile/PictureUpLoad".ToLower());
            urls.Add("ltdNotice/PublishInLogin".ToLower());
            urls.Add("bnannounce/getall".ToLower());
            urls.Add("bclink/getall".ToLower());
            urls.Add("bsuser/register".ToLower());

            return urls;
        }
    }
}