using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using QyTech.Core.BLL;

using QyTech.Auth.Dao;
using QyTech.SkinForm;
using QyTech.SkinForm.Component;
using QyTech.SkinForm.Controls;

namespace QyTech.UICreate.Util
{
    public class qyDgvListUtil
    {
        // public static event qyControlEvents.delegateProgressHandler ProgressChangeddEvent;

        public static DataTable RefreshDgv(qyDgv qyDgvList, SqlConnection sqlConn, string tName, string where, string orderby = "")
        {
            DataTable dtList = null;
            try
            {
                dtList = QyTech.DbUtils.SqlUtils.GetDataTable(sqlConn, tName, where, orderby);
                qyDgvList.DataSource = dtList;
                return dtList;
            }
            catch (Exception ex)
            {
                throw new Exception("RefreshDgv:" + ex.Message);
            }
        }


     

        public static void ResetDgvHeader(qyDgv qyDgvList,List<bsFunField> bffs)
        {

            for (int i = 0; i < bffs.Count; i++)
            {
                try
                {
                    qyDgvList.Columns[bffs[i].FName].Visible = bffs[i].VisibleInList == null ? true : (bool)bffs[i].VisibleInList;
                    qyDgvList.Columns[bffs[i].FName].HeaderText = bffs[i].FDesp;
                    qyDgvList.Columns[bffs[i].FName].Width = (int)bffs[i].FWidthInList;
                    if ((bffs[i].FrozeInList!=null && (bool)bffs[i].FrozeInList))
                        qyDgvList.Columns[bffs[i].FName].Frozen = true;


                }
                catch (Exception ex) { throw ex; }
            }
        }

        

        private static void ProgressChangeddEvent(qyPbr qyPbr_Refresh, int value, int Total)
        {

            qyPbr_Refresh.Value = value;
            if (value == Total)
            {
                qyPbr_Refresh.Visible = false;
            }
            else
            {
                qyPbr_Refresh.Visible = true;
                qyPbr_Refresh.Maximum = (int)Total;
                qyPbr_Refresh.Minimum = 0;
            }
        }
    }
}
