using System;
using System.Collections.Generic;

using System.Data;

using System.Windows.Forms;
using System.Data.Objects;
using System.Data.SqlClient;
using QyTech.Core.BLL;
using QyTech.Auth.Dao;
using QyTech.SkinForm;
using QyTech.SkinForm.Component;
using QyTech.SkinForm.Controls;
using QyTech.UICreate.Util;
using QyTech.DbUtils;

using QyTech.UICreate.UIUtils;

namespace QyTech.UICreate
{
    public partial class qyfLayoutListWithLeft : qyfLayoutListParent
    {
        public object CurrLeftPFk;//左侧对右侧的外键

        public qyfLayoutListWithLeft()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 根据配置bsFunConf/或者表名称进行设置相应内容
        /// </summary>
        /// <param name="em_Base">基本EM</param>
        /// <param name="em_App">应用EM</param>
        /// <param name="conn">应用EM的SqlConn</param>
        /// <param name="tname">表或视图对象</param>
        /// <param name="where"></param>
        /// <param name="orderby"></param>
        /// <summary>
        public qyfLayoutListWithLeft(ObjectContext db_Base, ObjectContext db_App, SqlConnection conn, Guid bsFC_Id, string where = "")
            :base(db_Base,db_App,conn,bsFC_Id,where)
        {
            InitializeComponent();
        }
        
    }

}
