using System;
using System.Collections.Generic;
using System.Reflection;


namespace ConsoleApp1.ReflectionSamples
{
    public class MethodReflection
    {
        public static void MethodPropertyReflection() {
            Type d1 = MethodBase.GetCurrentMethod().DeclaringType;
            var o = Activator.CreateInstance(d1);
            PropertyInfo[] properties = d1.GetProperties();
            foreach (var item in properties)
            {
                if (item.PropertyType.GetGenericTypeDefinition() == typeof(IDictionary<,>))
                {
                    var valueType = item.PropertyType.GetGenericArguments()[1];
                    if (valueType == typeof(string))
                    {
                        object value = item.GetValue(o, null);
                        foreach (var i in (IDictionary<int, string>)value)
                        {
                            Console.WriteLine(i.Value);
                        }
                    }

                }
            }

            Console.WriteLine(d1.Name);
        }
    }
}
