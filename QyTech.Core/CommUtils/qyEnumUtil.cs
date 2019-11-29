using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QyTech.Core.CommUtils
{
    public class qyEnumUtil
    {
        public static string GetNameByValue<T>( int value)
        {
            return Enum.GetName(typeof(T), value);
        }

        public static T GetEnumByName<T>(string strvalue)
        {
            return (T)Enum.Parse(typeof(T), strvalue);
        }

        public static int GetValueByName<T>(string strvalue)
        {
            return (int)Enum.Parse(typeof(T), strvalue);
        }
    }
}
