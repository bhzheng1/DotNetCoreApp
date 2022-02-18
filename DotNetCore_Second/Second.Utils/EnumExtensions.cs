using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Second.Utils
{
    public static class EnumExtensions {
        public static string ToDescription<TEnum>(this TEnum EnumValue) where TEnum : struct
        {
            return GetEnumDescription((Enum)(object)((TEnum)EnumValue));
        }
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }
    }
}
