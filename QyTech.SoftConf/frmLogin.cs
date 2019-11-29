using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using QyExpress.Dao;
using QyTech.Utils;
using QyTech.Core.BLL;
using System.Net.Http;
using System.Threading;
using QyTech.Json;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace QyTech.SoftConf
{
    public partial class frmLogin : Form
    {
        System.Data.Objects.ObjectContext dbcontext_;

        //LoginUser luser;

        //public LoginUser CurrentLoginUser { get { return luser; } }
        //private const string Uri = "http://122.114.190.250/arc_gis/countries?format=json";  
        BinaryWriter bw;
        BinaryReader br;
        string filename = "mydata";
        string passwordkey = "abcdefghigklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRST";

        string strpwd = "";

        Dictionary<string, User> users = new Dictionary<string, User>();

        public frmLogin(System.Data.Objects.ObjectContext dbcontext)
        {
            InitializeComponent();

            dbcontext_ = dbcontext;
        }

       
        private void exButton2_Click(object sender, EventArgs e)
        {
            GlobalVaribles.LoginStatus = Core.Common.LoginStatus.Exit;
            this.Close();
        }

        private void exButton1_Click(object sender, EventArgs e)
        {

            string username = this.UserName.Text.Trim();
            string password = this.PassWord.Text.Trim();

            bsUser luser = Core.Common.InnerAccout.IsInnerAccount(username, strpwd);
            if (luser == null)
            {
                Dictionary<string, string> paras = new Dictionary<string, string>();
                paras.Add("loginname", username);
                paras.Add("loginpwd", password);
                QyJsonData qyjson = HttpRequestUtils.PostRemoteJsonQy(GlobalVaribles.ServerUrl+"/api/bsuser/login", paras);
                if (qyjson.code == 1)
                {
                    MessageBox.Show(qyjson.msg);
                    return;
                }
                GlobalVaribles.SessionId = qyjson.msg;
                luser = QyTech.Json.JsonHelper.DeserializeJsonToObject<bsUser>(qyjson.data.ToString());
            }
            if (luser != null)
            {
                GlobalVaribles.currloginUser = luser;
                this.DialogResult = DialogResult.OK;
                GlobalVaribles.LoginStatus = Core.Common.LoginStatus.Success;

                this.pbrLoad.Minimum = 0;
                this.pbrLoad.Maximum = 10;
                this.lblLoad.Visible = true;
                this.pbrLoad.Visible = true;
                while (!GlobalVaribles.MainFormLoadFinished && this.pbrLoad.Value <= this.pbrLoad.Maximum)
                {
                    this.pbrLoad.Value = this.pbrLoad.Value >= this.pbrLoad.Maximum - 1 ? this.pbrLoad.Maximum - 1 : this.pbrLoad.Value + 1;
                    this.pbrLoad.Refresh();
                    this.lblLoad.Refresh();
                    System.Threading.Thread.Sleep(1000);
                }
                this.pbrLoad.Value = this.pbrLoad.Maximum;

                //保存账号到本地

             
                User user = new User();
                FileStream fs = new FileStream("data.bin", FileMode.Create);
                BinaryFormatter bf = new BinaryFormatter();
                user.Username = username;
                if (this.RemeberPassword.Checked)       //  如果单击了记住密码的功能
                {   //  在文件中保存密码
                    user.Password = password;
                }
                else
                {   //  不在文件中保存密码
                    user.Password = "";
                }

                //  选在集合中是否存在用户名 
                if (users.ContainsKey(user.Username))
                {
                    users.Remove(user.Username);
                }
                users.Add(user.Username, user);
                //要先将User类先设为可以序列化(即在类的前面加[Serializable])
                bf.Serialize(fs, users);
                //user.Password = this.PassWord.Text;
                fs.Close();
                //try
                //{
                //    bw = new BinaryWriter(new FileStream(filename, FileMode.OpenOrCreate));
                //    string content = username + "@@@@" + strpwd;
                //    content = String.Format("{0, -50}", content);
                //    byte[] bytes = System.Text.Encoding.Default.GetBytes(content);
                //    StringBuilder ps = new StringBuilder();
                //    for (int i = 0; i < bytes.Length; i++)
                //    {
                //        int c = bytes[i] ^ passwordkey[i];
                //        ps.Append(c + " ");
                //    }

                //    bw.Write(ps.ToString());
                //    bw.Close();

                //}
                //catch (IOException ex)
                //{

                //}

            }
            else
            {
                MessageBox.Show("用户名或密码不正确", "提示", MessageBoxButtons.OK);
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            panel1.Parent = pictureBox1;

            //  读取配置文件寻找记住的用户名和密码
            FileStream fs = new FileStream("data.bin", FileMode.OpenOrCreate);

            if (fs.Length > 0)
            {
                BinaryFormatter bf = new BinaryFormatter();
                users = bf.Deserialize(fs) as Dictionary<string, User>;
                foreach (User user in users.Values)
                {
                    this.UserName.Items.Add(user.Username);
                }

                for (int i = 0; i < users.Count; i++)
                {
                    if (this.UserName.Text != "")
                    {
                        if (users.ContainsKey(this.UserName.Text))
                        {
                            this.PassWord.Text = users[this.UserName.Text].Password;
                            this.RemeberPassword.Checked = true;
                        }
                    }
                }
            }
            fs.Close();
            //  用户名默认选中第一个
            if (this.UserName.Items.Count > 0)
            {
                this.UserName.SelectedIndex = this.UserName.Items.Count - 1;
            }



            //try
            //{
            //    br = new BinaryReader(new FileStream(filename,FileMode.Open));
            //    try
            //    {
            //        string passwd = br.ReadString();
            //        string[] ps = passwd.Split(' ');
            //        StringBuilder sb = new StringBuilder();
            //        for (int i = 0; i < ps.Length - 1; i++)
            //        {
            //            sb.Append((char)(passwordkey[i] ^ int.Parse(ps[i]))); //异或解密， 转换成char
            //        }
            //        passwd = sb.ToString();

            //        string[] strs = passwd.Split(new string[] { "@@@@" }, 2, StringSplitOptions.RemoveEmptyEntries);
            //        this.tbUserName.Text = strs[0];

            //        this.tbPassword.Text = strs[1].Trim();
            //        strpwd = this.tbPassword.Text;
            //        br.Close();
            //    }
            //    catch { br.Close(); }
            //}
            //catch (IOException ex)
            //{
            //    return;
            //}
        }

        private void tbPassword_TextChanged(object sender, EventArgs e)
        {
            //strpwd = QyTech.Core.Helpers.LockerHelper.MD5(this.PassWord.Text.Trim());

        }

        private void UserName_SelectedValueChanged(object sender, EventArgs e)
        {
            //  首先读取记住密码的配置文件
            FileStream fs = new FileStream("data.bin", FileMode.OpenOrCreate);

            if (fs.Length > 0)
            {
                BinaryFormatter bf = new BinaryFormatter();

                users = bf.Deserialize(fs) as Dictionary<string, User>;

                for (int i = 0; i < users.Count; i++)
                {
                    if (this.UserName.Text != "")
                    {
                        if (users.ContainsKey(UserName.Text) && users[UserName.Text].Password != "")
                        {
                            this.PassWord.Text = users[UserName.Text].Password;
                            this.RemeberPassword.Checked = true;
                        }
                        else
                        {
                            this.PassWord.Text = "";
                            this.RemeberPassword.Checked = false;
                        }
                    }
                }
            }

            fs.Close();

        }
    }


    [Serializable]
    public class User
    {

        //public User(string username, string password)
        //{
        //    this.userName = username;
        //    this.passWord = password;
        //}

        private string userName;
        public string Username
        {
            get { return userName; }
            set { userName = value; }
        }

        private string passWord;
        public string Password
        {
            get { return passWord; }
            set { passWord = value; }
        }
    }
}
