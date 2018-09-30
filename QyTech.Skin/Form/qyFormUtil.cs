using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]
namespace QyTech.SkinForm
{
      
    public static class qyFormUtil
    {
         [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;

        public static void MouseMoveForm(IntPtr handle)
        {
            ReleaseCapture();
            SendMessage(handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }




        public static void FormInitSetting(Form frmobj)
        {
            frmobj.BackColor = SystemColors.GradientInactiveCaption;
            frmobj.StartPosition = FormStartPosition.CenterScreen;
        }

        /// <summary>
        /// 应该可以用withTitle完全替代，至少母线来说
        /// </summary>
        /// <param name="frm"></param>
        public static void ShowForm(Form frm)
        {
            qyFormContainer pfrm = new qyFormContainer(frm);
            pfrm.Show();
        }
        public static void ShowDialogForm(Form frm)
        {
            qyFormContainer pfrm = new qyFormContainer(frm);
            pfrm.ShowDialog();
        }
    }
}
