using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Second.Utils
{
    public class JsEnumAttribute : Attribute
    {
        public string[] Groups { get; set; }

        public JsEnumAttribute(params string[] groups)
        {
            Groups = groups;
        }
    }

    public static class JsEnabledEnums
    {
        private static IList<JsEnumTypeInfo> _types = null;

        public static void LoadTypes()
        {
            if (_types != null) return;
            _types = new List<JsEnumTypeInfo>();
            var typesWithAttribute =
                from t in Assembly.GetExecutingAssembly().GetTypes().Where(x => x.IsEnum)
                let attributes = t.GetCustomAttributes(typeof(JsEnumAttribute), true)
                where attributes != null && attributes.Length > 0
                select new { Type = t, Attributes = attributes.Cast<JsEnumAttribute>() };

            foreach (var info in typesWithAttribute.Select(x => new { x.Type, x.Attributes.First().Groups }))
            {
                // if there are no groups then it defaults to everywhere
                if (info.Groups == null || info.Groups.Length == 0)
                {
                    _types.Add(new JsEnumTypeInfo
                    {
                        Type = info.Type,
                        Group = ""
                    });
                }
                else
                {
                    foreach (var group in info.Groups)
                    {
                        _types.Add(new JsEnumTypeInfo
                        {
                            Type = info.Type,
                            Group = string.IsNullOrEmpty(group) ? "" : group.ToLower()
                        });
                    }
                }

            }
        }

        public static Type[] GetTypes(string group)
        {
            LoadTypes();
            return _types
                .Where(x => string.IsNullOrEmpty(group) || x.Group == group.ToLower() || string.IsNullOrEmpty(x.Group))
                .Select(x => x.Type)
                .Distinct()
                .ToArray();
        }
    }

    internal class JsEnumTypeInfo
    {
        public string Group { get; set; }
        public Type Type { get; set; }
    }

    public static class CExtensions
    {
        public static string ToCamelCase(this string s)
        {
            return s.Substring(0, 1).ToLower() + s.Substring(1);
        }

        public static string ConvertEnumToJson(this Type e, string varName = null)
        {
            if (varName == null)
            {
                varName = e.Name.ToCamelCase();
            }

            var ret = varName + ": {";
            foreach (var val in Enum.GetValues(e))
            {
                ret += Enum.GetName(e, val).ToCamelCase() + ":" + (int)val + ",";
            }
            ret += "}";
            return ret;
        }

    }
}
