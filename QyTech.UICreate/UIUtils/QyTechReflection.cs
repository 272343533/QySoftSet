using System;
using System.Reflection;

namespace QyTech.UICreate.Util
{
    public class QyTechReflection
    {

        public static string GetObjectPropertyValue(object t, string propertyname)
        {
            if (t == null)
                return "";
            Type type = t.GetType();// typeof(T);

            PropertyInfo property = type.GetProperty(propertyname);

            if (property == null) return string.Empty;

            object o = property.GetValue(t, null);

            if (o == null) return string.Empty;

            return o.ToString();
        }
    }
}
