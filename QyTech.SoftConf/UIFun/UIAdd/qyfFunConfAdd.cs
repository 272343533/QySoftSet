using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using QyTech.UICreate;
namespace QyTech.SoftConf.UIAdd
{
    public partial class qyfFunConfAdd : qyfAdd
    {
        ComboBox cbo_bsT_Id;
        ComboBox cbo_PPK;
        ComboBox cbo_NotNullField;
        ComboBox cbo_OrderBySql;
        ComboBox cbo_TPk;
        

        public qyfFunConfAdd()
        {
            InitializeComponent();
        }

        private void qyfFunConf_Load(object sender, EventArgs e)
        {
            //添加需要的事件
            //1。 当表名修改后，对应的字段信息也要修改
            //可是还没创建，事件怎么写呢
            try
            {
                cbo_bsT_Id = dicControls["bsT_Id"] as ComboBox;
                cbo_bsT_Id.SelectedIndexChanged += new EventHandler(TName_SelectChange);
                cbo_NotNullField = dicControls["NotNullField"] as ComboBox;
                cbo_PPK = dicControls["PFk"] as ComboBox;
                cbo_OrderBySql = dicControls["OrderBySql"] as ComboBox;
                cbo_TPk = dicControls["TPk"] as ComboBox; ;

            }
            catch { }
        }




        private void TName_SelectChange(object sender,EventArgs e)
        {
            try
            {
                string bsT_Id = cbo_bsT_Id.Tag.ToString().Split(new char[] { ',' })[cbo_bsT_Id.SelectedIndex];
                int dhPos = cbo_bsT_Id.Text.LastIndexOf(".");
                HiddenFieldsValue["TName"] = cbo_bsT_Id.Text.Substring(dhPos+1);

                List<string> fields = DbUtils.SqlUtils.GetTableAllFields(sqlConn, bsT_Id);
                AddItems(cbo_NotNullField, fields);
                AddItems(cbo_PPK, fields);
                AddItems(cbo_OrderBySql, fields);
                AddItems(cbo_TPk, fields);

                //cbo_NotNullField.DataSource = fields.GetRange(0, fields.Count);
                //cbo_PPK.DataSource = fields.GetRange(0, fields.Count);
                //cbo_OrderBySql.DataSource = fields;
                //cbo_TPk.DataSource = fields.GetRange(0, fields.Count);
            }
            catch
            { }
        }

        private void AddItems(ComboBox cb,List<string> items)
        {
            cb.Items.Clear();
            foreach(string item in items)
            {
                cb.Items.Add(item);
            }
        }
    }
}
