using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using QyTech.Medicine.diabetes;

namespace MedicineTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            try
            {
                Analysis ana = new Analysis();
                string ret = ana.Predict(AppDomain.CurrentDomain.BaseDirectory+this.textBox1.Text);
                MessageBox.Show(ret);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try

            {
                string filename = AppDomain.CurrentDomain.BaseDirectory + this.textBox1.Text;
                var engine = IronPython.Hosting.Python.CreateEngine();
                engine.CreateScriptSourceFromString("print 'hello world!'").Execute();

                var scope = engine.CreateScope();
                var source = engine.CreateScriptSourceFromFile("predict_test.py");
                source.Execute(scope);
                var predict = scope.GetVariable<Func<object, object>>("ListReturnTest");
                var result1 = predict(filename);
                MessageBox.Show(result1.ToString());


                //var say_hello = scope.GetVariable<Func<object>>("say_hello");
                //say_hello();

                //var get_text = scope.GetVariable<Func<object>>("get_text");
                //var text = get_text().ToString();
                //Console.WriteLine(text);

                //var add = scope.GetVariable<Func<object, object, object>>("add");
                //var result1 = add(1, 2);
                //Console.WriteLine(result1);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Analysis ana = new Analysis();
                string strret = "";
                string errmsg = "";
                string ret = ana.TestFun(AppDomain.CurrentDomain.BaseDirectory + this.textBox1.Text);//,ref strret,ref errmsg);
                MessageBox.Show(ret);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Analysis ana = new Analysis();
                string ret = ana.TestFunInt(AppDomain.CurrentDomain.BaseDirectory + this.textBox1.Text);
                MessageBox.Show(ret);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            txtResult.Text = "";
            try
            {
                string databycomma = txt1.Text;
                databycomma += "," + txt2.Text;
                databycomma += "," + txt3.Text;
                databycomma += "," + txt4.Text;
                databycomma += "," + txt5.Text;
                databycomma += "," + txt6.Text;
                Analysis ana = new Analysis();
                string ret = ana.Predict_V1(databycomma);

                DispResult(ret);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txt1_TextChanged(object sender, EventArgs e)
        {
            txtResult.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            txtResult.Text = "";
            try
            {
                string databycomma = txt1.Text;
                databycomma += "," + txt2.Text;
                databycomma += "," + txt3.Text;
                databycomma += "," + txt4.Text;
                databycomma += "," + txt5.Text;
                databycomma += "," + txt6.Text;
                string ret = GetRemoteJson("http://122.112.245.147:8084/medicine/predict?c="+ databycomma);

                DispResult(ret);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DispResult(string ret)
        {
            if (ret == "0")
            {
                txtResult.Text = "正常";
                txtResult.ForeColor = Color.Green;
            }
            else if (ret == "1")
            {
                txtResult.Text = "检验结果提示您可能患有高脂血症! ";
                txtResult.Text += "\r\n";
                txtResult.Text += "\r\n";
                txtResult.Text += "如有疑问,请咨询值班医生。";
                txtResult.ForeColor = Color.Red;
            }
            else
                txtResult.Text = "？";
        }


        private static string GetRemoteJson(string url)
        {
            Uri Uri = new Uri(url);
            string ret = "";
            // Create an HttpClient instance    
            HttpClient client = new HttpClient();

            //远程获取数据  
            var task = client.GetAsync(url);
            var rep = task.Result;//在这里会等待task返回。  

            //读取响应内容  
            var task2 = rep.Content.ReadAsStringAsync();
            ret = task2.Result;//在这里会等待task返回。 

            return ret;
        }
    }
}
