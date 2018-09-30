using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Resources;
using System.Drawing;

namespace QyTech.SkinForm
{
    public enum ImageSource { add_16, cancel_16 , exit_16 ,save_16}
    public enum StringSource { AuthDaoDb }
    
    public class qyResources
    {
        //public static Image  GetImage(ImageSource image)
        //{
        //    Assembly myAssem = Assembly.GetEntryAssembly();
        //    ResourceManager rm = new ResourceManager("QyTech.SkinForm.Resources", myAssem);
        //    return (Image)rm.GetObject(image.ToString());
            
        //}

        //public static string GetString(StringSource text)
        //{
        //    Assembly myAssem = Assembly.GetEntryAssembly();
        //    ResourceManager rm = new ResourceManager("QyTech.SkinForm.Resources", myAssem);
        //    return rm.GetString(text.ToString());
        //}

        public static Image add_16
        {
            get { return Resources.add_16; }
        }
        public static Image ok_16
        {
            get { return Resources.ok_16; }
        }
        public static Image cancel_16
        {
            get { return Resources.cancel_16; }
        }

        public static Image Excel_16
        {
            get { return Resources.Excel_16; }

        }
        public static Image search_16
        {
            get { return Resources.search_16; }
        }
        public static Image exit_16
        {
            get { return Resources.exit_16; }
        }

        

        public static Image Alarm_16
        {
            get { return Resources.Alarm_16; }
        }

        public static Image caution_16
        {
            get { return Resources.caution_16; }
        }
        public static Image colorize_16
        {
            get { return Resources.colorize_16; }
        }
        public static Image del_16
        {
            get { return Resources.del_16; }
        }

        public static Image edit_16
        {
            get { return Resources.edit_16; }


        }
        public static Image position_16
        {
            get { return Resources.position_16; }
        }
        public static Image print_16
        {
            get { return Resources.print_16; }
        }
        public static Image print_16_Red
        {
            get { return Resources.print_16_Red; }
        }
        public static Image save_16_black
        {
            get { return Resources.save_16_black; }
        }
        public static Image save_16
        {
            get { return Resources.save_16; }
        }
        public static Image weixing
        {
            get { return Resources.weixing; }
        }
    }
}
