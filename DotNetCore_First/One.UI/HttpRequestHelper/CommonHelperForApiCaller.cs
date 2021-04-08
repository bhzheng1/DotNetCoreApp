using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace One.UI.HttpRequestHelper
{
    public class CommonHelperForApiCaller : ICommonHelperForApiCaller
    {
        public string ParameterBuilder(dynamic dynamicObj)
        {
            StringBuilder sb = new StringBuilder();
            object obj = dynamicObj;
            string[] propertyNames = obj.GetType().GetProperties().Select(prop => prop.Name).ToArray();
            Type[] propertyTypes = obj.GetType().GetProperties().Select(prop => prop.PropertyType).ToArray();

            sb.Append("?");

            for (int i = 0; i < propertyNames.Length; i++)
            {
                var prop = propertyNames[i];

                if (propertyTypes[i] == typeof(List<string>) || propertyTypes[i] == typeof(List<Guid>))
                {
                    object propValue = obj.GetType().GetProperty(prop).GetValue(obj, null);
                    foreach (var item in (IList)propValue)
                    {
                        string propValueInStr = Convert.ToString(item);
                        sb.Append("&" + prop + "=" + Convert.ToString(propValueInStr));
                    }

                }
                else
                {
                    object propValue = obj.GetType().GetProperty(prop).GetValue(obj, null);
                    string propValueInStr = Convert.ToString(propValue);
                    sb.Append("&" + prop + "=" + Convert.ToString(propValue));

                }
            }
            return sb.ToString();

        }
    }
}
