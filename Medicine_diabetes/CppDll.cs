using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;


namespace QyTech.Medicine.diabetes
{
    class CppDll
    {

        //[DllImport(@"PredictTest", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto)]
        //public static extern String list_return(System.Text.StringBuilder filepath);

        //C:\Program Files (x86)\Microsoft Visual Studio\Shared\Anaconda3_64\Scripts
        [DllImport(@"PredictDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int predict(string filename, StringBuilder result, StringBuilder errmsg);


        [DllImport(@"PredictDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Predict_V1(string strbycomma, StringBuilder result, StringBuilder errmsg);
        //[DllImport("PredictDLL.dll", CallingConvention = CallingConvention.StdCall)]
        //public static extern IntPtr list_return(IntPtr par1);



        //1.StdCall->cdecl
        //2.stringbuidler->string
        //3.改为 指针
        //4.改为ref string  报错：与托管参数不匹配
        [DllImport(@"PredictDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int fun(string filename, StringBuilder result, StringBuilder errmsg);



        [DllImport(@"PredictDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int funint(string filepath);

    }
}
