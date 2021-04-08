using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelperClassLibrary
{
    public class UrlQueryString
    {
        public static void Create(string x)
        {
            var b = nameof(x);
            Console.WriteLine(b);
        }
        public static string Make(dynamic thing)
        {
            StringBuilder sb = new StringBuilder();
            object obj = thing;
            string[] propertyNames = obj.GetType().GetProperties().Select(prop => prop.Name).ToArray();
            Type[] propertyTypes = obj.GetType().GetProperties().Select(prop => prop.PropertyType).ToArray();

            sb.Append("?");

            for (int i = 0; i < propertyNames.Length; i++)
            {
                var prop = propertyNames[i];

                if (typeof(IEnumerable<string>).IsAssignableFrom(propertyTypes[i]) ||
                    typeof(IEnumerable<Guid>).IsAssignableFrom(propertyTypes[i]))
                {
                    object propValue = obj.GetType().GetProperty(prop)?.GetValue(obj, null);
                    foreach (var item in (IList)propValue)
                        sb.Append("&" + prop + "=" + Uri.EscapeDataString(Convert.ToString(item)));
                }
                else
                {
                    object propValue = obj.GetType().GetProperty(prop)?.GetValue(obj, null);
                    sb.Append("&" + prop + "=" + Uri.EscapeDataString(Convert.ToString(propValue)));
                }
            }

            return sb.ToString();
        }
    }
}
