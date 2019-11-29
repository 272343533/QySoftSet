using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dao.QyBllApp;
using QyTech.Core.Common;
using QyTech.Core.BLL;
using QyExpress.Dao;
using System.Data.Objects;
namespace QyExpress.BLL
{
    public class bllLtdRegInfo
    {
        /// <summary>
        /// 核实后应该项用户表里增加数据,或者使用bllSpCreateUserAfterAudit存储过程
        /// 在注册成功后也会调用
        /// </summary>
        public static string AddUserAfterAudited(ObjectContext objectcontext, LtdRegInfo lri)
        {

            // if (lri.IsCheck!=null && lri.IsCheck==1)

            string pwd = lri.NSRSBH.Length >= 6 ? lri.NSRSBH.Substring(lri.NSRSBH.Length - 6, 6) : lri.NSRSBH;
            pwd = QyTech.Core.Helpers.LockerHelper.MD5(pwd);
            string ret = EntityManager_Static.ExecuteSql(objectcontext, "exec bllSpCreateUserAfterAudit '" + lri.bsO_Id.ToString() + "','" + pwd + "'");
            return ret;
            #region 代码处理，已修改未存储过程处理

            //AddorModify addormodi = AddorModify.Modify;
            //Guid bsR_Id = Guid.Empty;
            //bsUser obj_user;
            //obj_user = EntityManager_Static.GetByPk<bsUser>(objectcontext,"bsO_Id",lri.bsO_Id);
            //if (obj_user == null)
            //{
            //    addormodi = AddorModify.Add;
            //    obj_user = new bsUser();
            //    obj_user.bsU_Id = Guid.NewGuid();
            //    obj_user.bsO_Id = lri.bsO_Id;
            //}
            //obj_user.LoginName = lri.NSRSBH;
            //string pwd = lri.NSRSBH.Length >= 6 ? lri.NSRSBH.Substring(lri.NSRSBH.Length - 6, 6) : lri.NSRSBH;
            //obj_user.LoginPwd =QyTech.Core.Helpers.LockerHelper.MD5(pwd);
            //obj_user.bsO_Name = lri.LtdName.Length>=100?lri.LtdName.Substring(0, 100):lri.LtdName;
            //obj_user.RegDt = DateTime.Now;
            //obj_user.NickName = lri.Contacter;
            //obj_user.ContactTel = lri.ContactTel;
            //obj_user.ValidDate = obj_user.RegDt.Value.Date.AddYears(10);
            //if (lri.IsLandLtd == 1)
            //{
            //    obj_user.UserType = "OWNER";
            //    bsR_Id = Guid.Parse("5CF1DCF0-44F0-41C5-B31A-BACA7115F0FA");
            //}
            //else if (lri.IsLandLtd == 0)
            //{
            //    obj_user.UserType = "TENANCY";
            //    bsR_Id = Guid.Parse("A01BE51C-B927-4570-BACD-852C550BDD36");
            //}
            //else
            //{
            //    obj_user.UserType = "userregi";
            //    bsR_Id = Guid.Parse("106C75E2-101B-4566-B4B7-DEB8B7BB1DE0");
            //}
            //obj_user.AccountStatus = "正常";
            //if (addormodi==AddorModify.Add)
            //    EntityManager_Static.Add<bsUser>(objectcontext, obj_user);
            //else
            //    EntityManager_Static.Modify<bsUser>(objectcontext, obj_user);

            ////增加相应的角色
            //addormodi = AddorModify.Modify;

            //bsUserRoleRel urr;
            //urr = EntityManager_Static.GetByPk<bsUserRoleRel>(objectcontext, "bsU_Id", obj_user.bsU_Id);
            //if (urr == null)
            //{
            //    addormodi = AddorModify.Add;
            //    urr = new bsUserRoleRel();
            //    urr.Id = Guid.NewGuid();
            //    urr.bsU_Id = obj_user.bsU_Id;
            // }
            // urr.bsR_Id = bsR_Id;
            //if (addormodi == AddorModify.Add)
            //    EntityManager_Static.Add<bsUserRoleRel>(objectcontext, urr);
            //else
            //    EntityManager_Static.Modify<bsUserRoleRel>(objectcontext, urr);
            #endregion

        }
    }
}