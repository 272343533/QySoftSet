using System;
using System.Collections.Generic;

using System.Data;

using System.Windows.Forms;

using System.Data.SqlClient;
using QyTech.Core.BLL;
using QyTech.Auth.Dao;
using QyTech.SkinForm;
using QyTech.SkinForm.Component;
using QyTech.SkinForm.Controls;
using QyTech.UICreate.Util;
using QyTech.DbUtils;

namespace QyTech.UICreate.UIUtils
{
    public class qyLayoutUtil
    {
        public static void FormLoad(Form frm)
        {
            frm.WindowState = FormWindowState.Maximized;
        }
    }
}
