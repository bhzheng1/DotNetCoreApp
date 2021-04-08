using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace One.API.Client
{
    public static class QueryString
    {
        public static string Make(dynamic thing)
        {
            var sb = new StringBuilder();
            object obj = thing;
            
            var propertyNames = obj.GetType().GetProperties().Select(prop => prop.Name).ToArray();
            var propertyTypes = obj.GetType().GetProperties().Select(prop => prop.PropertyType).ToArray();
            if (propertyNames.Length == 0) return string.Empty;
            sb.Append("?");

            for (int i = 0; i < propertyNames.Length; i++)
            {
                var prop = propertyNames[i];

                if (typeof(IEnumerable<string>).IsAssignableFrom(propertyTypes[i]) || typeof(IEnumerable<Guid>).IsAssignableFrom(propertyTypes[i]))
                {
                    object propValue = obj.GetType().GetProperty(prop).GetValue(obj, null);
                    foreach (var item in (IList)propValue)
                        sb.Append("&" + prop + "=" + Uri.EscapeDataString(Convert.ToString(item)));
                }
                else
                {
                    object propValue = obj.GetType().GetProperty(prop).GetValue(obj, null);
                    sb.Append("&" + prop + "=" + Uri.EscapeDataString(Convert.ToString(propValue)));
                }
            }

            return sb.ToString();
        }
    }
}