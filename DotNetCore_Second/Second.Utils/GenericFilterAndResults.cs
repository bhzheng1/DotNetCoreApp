using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Linq.Expressions;
using System.Xml.Serialization;
using System.Text.Json;

namespace Second.Utils
{
    [Serializable]
    public class GroupUnit : IEquatable<GroupUnit>
    {
        public GroupUnit()
        {
            GroupOps = string.Empty;
            Name = string.Empty;
            Ops = FilterOps.NA;
            Val = string.Empty;
            Sql = string.Empty;
            DisplayName = string.Empty;
            IncludeNull = bool.FalseString;
        }

        public GroupUnit(string groupsOps, string name, FilterOps ops, string val, string sql, string includeNull)
        {
            GroupOps = groupsOps;
            Name = name;
            Ops = ops;
            Val = val;
            Sql = Sql;
            IncludeNull = includeNull;
        }

        public string GroupOps { get; set; } // (Percentile > ... GroupOps(or/and) Score < ...)
        public string Name { get; set; }
        public FilterOps Ops { get; set; }
        public string Val { get; set; }
        public string Sql { get; set; }
        public string DisplayName { get; set; }

        public string IncludeNull { get; set; }

        public bool Equals(GroupUnit other)
        {
            if (other is null)
                return false;

            return GroupOps == other.GroupOps && Name == other.Name && Val == other.Val;
        }

        public override bool Equals(object obj) => Equals(obj as GroupUnit);
        public override int GetHashCode() => (Name, Val).GetHashCode();
    }

    [Serializable]
    public class FilterUnit : IEquatable<FilterUnit>
    {
        public FilterUnit()
        {
            Name = string.Empty;
            Ops = FilterOps.NA;
            Val = string.Empty;
            valueList = new List<string>();
            DisplayName = string.Empty;
            Sql = string.Empty;
            UnitGoup = new List<GroupUnit>();
        }
        public FilterUnit(string name, FilterOps ops, string val, List<string> valList, string displayname, string sql)
        {
            Name = name;
            Ops = ops;
            Val = val;
            valueList = valList;
            DisplayName = displayname;
            Sql = sql;
            UnitGoup = new List<GroupUnit>();
        }

        public FilterUnit(string name, FilterOps ops, string val, List<string> valList, string displayname, string sql, List<GroupUnit> UnitGroup)
        {
            Name = name;
            Ops = ops;
            Val = val;
            valueList = valList;
            DisplayName = displayname;
            Sql = sql;
            UnitGoup = UnitGroup;
        }

        public string Name { get; set; }
        public FilterOps Ops { get; set; }

        public string Val { get; set; } //This field might be absolete
        public List<string> valueList { get; set; }
        public string DisplayName { get; set; }
        public string Sql { get; set; }
        public string TypeAheadEnabled { get; set; }
        public string TypeAheadSingleValue { get; set; }
        public string TypeAheadSql { get; set; }

        public string ShowTextSearch { get; set; }
        public string PortfolioSearch { get; set; }
        public string PortfolioValue { get; set; }
        public string PortfolioSql { get; set; }
        public string BudgetpoolSearch { get; set; }

        public string BudgetpoolRelnotcomm { get; set; }
        public string BudgetpoolOpencomm { get; set; }
        public string BudgetpoolAward { get; set; }

        public List<GroupUnit> UnitGoup;


        public bool Equals(FilterUnit other)
        {
            if (other is null)
                return false;

            return Name == other.Name && Val == other.Val;
        }

        public override bool Equals(object obj) => Equals(obj as FilterUnit);
        public override int GetHashCode() => (Name, Val).GetHashCode();

    }

    public static class FilterHelper
    {
        public static List<FilterUnit> ReadXmlToFilterUnitCollection(string path, string tableName)
        {
            List<FilterUnit> filterLst = new List<FilterUnit>();
            DataSet objDataSet = new DataSet();
            objDataSet.ReadXml(path);
            DataTable pfTable = objDataSet.Tables[tableName];
            foreach (DataRow r in pfTable.Rows)
            {
                FilterUnit fu = new FilterUnit();
                fu.Name = r.Field<string>("name");
                if (fu.Name == "COVID")
                {
                    var pftabsql = pfTable.AsEnumerable().Select(x => new { tabname = x.Field<string>("name"), tabsql = x.Field<string>("sql") }).ToList();
                    fu.Sql = pftabsql.Where(x => x.tabname == "TPL").Select(x => x.tabsql).FirstOrDefault() + "&& IsCovidGrant == true";
                }
                else
                {
                    fu.Sql = r.Field<string>("sql");
                }


                filterLst.Add(fu);
            }
            return filterLst;
        }
        public static string GetPendingPaylineCriteria(string xmlPath)
        {
            DataSet objDataSet = new DataSet();
            objDataSet.ReadXml(xmlPath);
            DataTable pfTable = objDataSet.Tables["tab"];
            var pftabsql = pfTable.AsEnumerable().Select(x => new { tabname = x.Field<string>("name"), tabsql = x.Field<string>("sql") }).ToList();
            var Sql = pftabsql.Where(x => x.tabname == "TPL").Select(x => x.tabsql).FirstOrDefault();
            return Sql;
        }
        public static FilterUnit GetFilterUnitByName(this IList<FilterUnit> xmlLst, string name, string excludedPattern)
        {
            string pat = excludedPattern; // @"(\w+)\s+(In)\s+(\{\d+\})";

            IList<FilterUnit> newLst = new List<FilterUnit>();
            foreach (var l in xmlLst)
            {
                Regex r = new Regex(pat, RegexOptions.IgnoreCase);
                Match m = r.Match(l.Sql);
                if (!m.Success)
                {
                    newLst.Add(l);
                }
            }

            return newLst.Where(x => x.Name.ToLower() == name.ToLower()).FirstOrDefault();
        }

        public static FilterUnit GetFilterUnitByNameWithSqlPattern(this IList<FilterUnit> xmlLst, string name, string pattern)
        {
            string pat = pattern; // @"(\w+)\s+(In)\s+(\{\d+\})";

            IList<FilterUnit> newLst = new List<FilterUnit>();
            foreach (var l in xmlLst)
            {
                Regex r = new Regex(pat, RegexOptions.IgnoreCase);
                Match m = r.Match(l.Sql);
                if (m.Success)
                {
                    newLst.Add(l);
                }
            }

            return newLst.Where(x => x.Name.ToLower() == name.ToLower()).FirstOrDefault();
        }

        public static string SqlStrings<T>(this List<FilterUnit> filterLst, IList<FilterUnit> xmlLst)
        {
            Type unitType = typeof(T);
            var unitSql = string.Empty;
            string pat = @"(\w+)\s+(In)\s+(\{(.+)\})";
            filterLst = filterLst.Where(f => f.Name != "BudgetNodeID" && f.Name != "GrantTag_Id" && f.Name != "CAN").ToList();
            StringBuilder sqlAll = new StringBuilder();
            foreach (var f in filterLst)
            {
                if (f.UnitGoup == null || f.UnitGoup.Count == 0)
                {
                    var xmlUnit = xmlLst.GetFilterUnitByName(f.Name, pat);

                    if (xmlUnit != null && xmlUnit.Sql != null && xmlUnit.Sql.Trim() != string.Empty)
                    {

                        if (f.Name.ToLower().Contains("date"))
                        {
                            var year = DateTime.Parse(f.Val).Year;
                            var month = DateTime.Parse(f.Val).Month;
                            var day = DateTime.Parse(f.Val).Day;
                            unitSql = string.Format(xmlUnit.Sql, year, year, month, year, month, day);
                        }
                        else if (f.Name.ToLower().Contains("expedited"))
                        {
                            string gUnitSql = string.Empty;

                            if (f.Val.ToLower() == "yes")
                                gUnitSql = string.Format(xmlUnit.Sql, "true");
                            else
                                gUnitSql = string.Format(xmlUnit.Sql, "false");


                            if (gUnitSql != string.Empty) gUnitSql = "(" + gUnitSql + ")";
                            unitSql = gUnitSql;
                        }
                        else if (xmlUnit.Sql.Contains(".Contains")) //if(f.Name.ToLower().Contains("grantno"))
                        {
                            string gUnitSql = string.Empty;

                            var valLst = f.Val.Split(',').Select(x => x.Trim()).ToList();

                            if (valLst.Count() == 1)
                            {
                                unitSql = string.Format(xmlUnit.Sql, f.Val);
                            }
                            else
                            {
                                foreach (var itm in valLst)
                                {
                                    if (gUnitSql.Length > 0)
                                        gUnitSql = gUnitSql + " Or ";

                                    gUnitSql += string.Format(xmlUnit.Sql, itm);
                                }

                                gUnitSql = " ( " + gUnitSql + " ) ";

                                unitSql = gUnitSql;
                            }


                        }
                        else
                        {
                            if (f.Val.Contains(":"))
                            {
                                var searchTxt = f.Val;
                                var strarray = f.Val.Split(',');
                                for (var i = 0; i < strarray.Length; i++)
                                {
                                    strarray[i] = strarray[i].Substring(0, searchTxt.IndexOf(':')).Replace(@"(/\s +/ g)", "");
                                }
                                searchTxt = string.Join(",", strarray);
                                f.Val = searchTxt;
                            }



                            unitSql = string.Format(xmlUnit.Sql, f.Val);
                        }

                    }
                }
                else //case like percentile/score
                {
                    string gUnitSql = string.Empty;
                    foreach (var g in f.UnitGoup)
                    {
                        var xmlUnit = xmlLst.GetFilterUnitByName(g.Name, pat);
                        if (g.Val != null && g.Val.Trim() != string.Empty)
                        {
                            var valLst = g.Val.Split(',', '-').Select(x => x.Trim()).ToArray();
                            gUnitSql += string.Format(g.GroupOps + " " + xmlUnit.Sql + " ", valLst); // g.Val);
                        }

                        if (g.DisplayName == "Percentile" && g.IncludeNull == "True")
                        {
                            gUnitSql = "((" + gUnitSql + ")" + " Or " + "( Pctile == null )) ";

                        }

                    }

                    if (gUnitSql != string.Empty) gUnitSql = "(" + gUnitSql + ")";
                    unitSql = gUnitSql;
                }
                if (!string.IsNullOrEmpty(unitSql))
                {
                    if (sqlAll.Length > 0)
                    {
                        sqlAll.Append(" and ");
                    }
                    sqlAll.Append(unitSql);
                }

                unitSql = string.Empty;
            }

            return sqlAll.ToString();
        }

        public static Expression<Func<Student, bool>> FilterInCollections<T>(this List<FilterUnit> filterLst, IList<FilterUnit> xmlLst)
        {
            Type unitType = typeof(T);
            MethodCallExpression unitExp = null;
            string pat = @"(\w+)\s+(In)\s+(\{(.+)\})";
            var param = Expression.Parameter(typeof(Student), "p");
            BinaryExpression bex = Expression.Equal(Expression.Constant(1), Expression.Constant(1));
            filterLst = filterLst.Where(f => f.Name != "BudgetNodeID" && f.Name != "GrantTag_Id").ToList();
            filterLst = filterLst.Where(f => f.Name != "ResearchAreaId").ToList();
            foreach (var f in filterLst)
            {
                if (f.UnitGoup == null || f.UnitGoup.Count == 0)
                {
                    var xmlUnit = xmlLst.GetFilterUnitByNameWithSqlPattern(f.Name, pat);
                    if (xmlUnit != null && xmlUnit.Sql != null && xmlUnit.Sql.Trim() != string.Empty)
                    {
                        string sName = string.Empty;
                        string[] arr;
                        List<string> valLst = null;

                        if (f.Sql.Length > 0)
                            xmlUnit.Sql = f.Sql;
                        //take from xml sql for portfolio
                        if (f.Val == null || f.Val.Trim() == string.Empty)
                        {
                            sName = xmlUnit.Sql.StringGetName();

                            arr = xmlUnit.Sql.Split(new string[] { "in", "In" }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(x => x.Trim()).ToArray(); //eg. Mechanism in {"a", "b"}

                            if (arr != null && arr[0] != null && arr[1] != null)
                            {
                                sName = arr[0];
                                valLst = arr[1].StringListDeserialize();
                            }
                        }
                        else //otherwise take from filter unit: for text seatrch
                        {
                            if (f.Val.Contains(":"))
                            {
                                var searchTxt = f.Val;
                                var strarray = f.Val.Split(',').Select(p => p.Trim()).ToList();
                                for (var i = 0; i < strarray.Count; i++)
                                {
                                    strarray[i] = strarray[i].Substring(0, strarray[i].IndexOf(':')).Replace(@"(/\s +/g)", "");
                                }
                                searchTxt = string.Join(",", strarray);
                                f.Val = searchTxt;
                            }

                            sName = f.Name;
                            if (sName.ToLower().Contains("rfapa"))
                                valLst = f.Val.Replace("-", "").Split(',', '-').Select(x => x.Trim()).ToList();
                            else
                                valLst = f.Val.Split(',', '-').Select(x => x.Trim()).ToList();
                        }


                        unitExp = FilterByInCollection<Student>(param, valLst, sName);     //string.Format(xmlUnit.Sql, f.Val);


                    }
                }

                if (unitExp != null)
                {
                    bex = Expression.AndAlso(bex, unitExp);
                }

                unitExp = null;
            }

            var exp = Expression.Lambda<Func<Student, bool>>(bex, param);
            return exp;
        }

        public static string Serialize(this FilterUnit[] filter)
        {
            string result1 = "";

            using (StringWriter sw = new StringWriter())
            {
                XmlSerializer xml = new XmlSerializer(typeof(FilterUnit[]));
                xml.Serialize(sw, filter);
                result1 = sw.ToString();
            }

            return result1;
        }

        public static List<FilterUnit> Deserialize(this string xmlStr)
        {
            XDocument xDoc = XDocument.Parse(xmlStr);
            List<FilterUnit> lst = (from node in xDoc.Descendants("FilterUnit")
                                    select new FilterUnit
                                    {
                                        Name = node.Descendants("Name").FirstOrDefault().Value,
                                        Val = node.Descendants("Val").FirstOrDefault().Value
                                    }).ToList();

            return lst;
        }

        public static string JSONSerialize(this FilterUnit[] filter)
        {
            string result1 = "";

            using (StringWriter sw = new StringWriter())
            {
                result1 = JsonSerializer.Serialize(filter);
            }

            return result1;
        }
        public static string StringGetName(this string str)
        {
            string s = string.Empty;
            var strArr = str.Split(new string[] { "In", "in" }, StringSplitOptions.RemoveEmptyEntries);
            if (strArr != null && strArr[0] != null)
                s = strArr[0].Trim();

            return s;
        }
        public static List<string> StringListDeserialize(this string str)
        {
            //string pat = @"(\w+)\s+(In)\s+(\{(.+)\})";
            List<string> lst = new List<string>();
            Regex r = new Regex(@"(""\w+"")", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
            Match m = r.Match(str);
            while (m.Success)
            {
                var v = m.Value;
                v = v.Trim(new char[] { '\"' });
                lst.Add(v);
                m = m.NextMatch();
            }
            return lst;
        }
        public static List<FilterUnit> JSONDeserialize123(this string jsonStr)
        {
            List<FilterUnit> lst = new List<FilterUnit>();
            lst = JsonSerializer.Deserialize<List<FilterUnit>>(jsonStr);
            return lst;
        }

        public static List<T> JSONDeserialize<T>(this string jsonStr)
        {
            List<T> lst = new List<T>();
            lst = JsonSerializer.Deserialize<List<T>>(jsonStr);
            return lst;
        }

        public static MethodCallExpression FilterByInCollection<Student>(ParameterExpression param, List<string> codes, string name)
        {
            if (codes == null) return null;
            MethodCallExpression body;
            //var propertyType = typeof(Student).GetProperty(name).PropertyType;
            var methodInfo = typeof(List<string>).GetMethod("Contains", new Type[] { typeof(string) });
            var value = Expression.Constant(codes);
            Expression propertyExp = Expression.Property(param, name);
            var type = propertyExp.Type;

            if (type == typeof(int))
            {
                List<int> fValue = new List<int>();
                //fValue = codes.Select(int.Parse).ToList();
                fValue = codes.Select(s => int.TryParse(s, out int n) ? n : (int?)null)
                         .Where(n => n.HasValue)
                         .Select(n => n.Value)
                         .ToList();
                var me = Expression.Property(param, name);
                var ce = Expression.Constant(fValue);
                body = Expression.Call(typeof(Enumerable), "Contains", new[] { me.Type }, ce, me);
            }
            else if (type == typeof(decimal)) //if (name == "AppId") 
            {
                List<decimal> fValue = new List<decimal>();
                //fValue = codes.Select(decimal.Parse).ToList();  
                fValue = codes.Select(s => decimal.TryParse(s, out decimal n) ? n : (decimal?)null)
                         .Where(n => n.HasValue)
                         .Select(n => n.Value)
                         .ToList();
                var me = Expression.Property(param, name);
                var ce = Expression.Constant(fValue);
                body = Expression.Call(typeof(Enumerable), "Contains", new[] { me.Type }, ce, me);

            }
            else
                body = Expression.Call(value, methodInfo, propertyExp);

            return body;

        }

        public static Expression<Func<Student, bool>> LamdaExpress(this List<FilterUnit> filter)
        {
            var param = Expression.Parameter(typeof(Student), "p");
            BinaryExpression bex = Expression.Equal(Expression.Constant(1), Expression.Constant(1));
            foreach (var fu in filter)
            {
                BinaryExpression binaryExpression;
                string sn = fu.Name; //.ToLower();
                FilterOps op = fu.Ops;
                string sv = fu.Val; //.ToLower();
                //if (sv.Equals("true") || sv.Equals("false"))
                //{
                //    binaryExpression =
                //                Expression.MakeBinary(
                //    ExpressionType.Equal,
                //    Expression.Property(param, fu.Name),
                //    Expression.Constant(sv == "true" ? true : false));

                //    bex = Expression.And(bex, binaryExpression);
                //}
                //else if (sn.Equals("grantno") || sn.Equals("title"))
                if (op == FilterOps.Contains)
                {
                    var expContains = ExpBinary<Student>(param, sn, "Contains", sv, true);
                    bex = Expression.And(bex, expContains);
                }
                else if (op == FilterOps.Equals)
                {
                    var p = Expression.Property(param, sn);
                    var pType = p.Type;
                    //var gClass = pType.GetGenericTypeDefinition();
                    //decimal? d = sU.Val.ToNullableDecimal();
                    var d = pType.CustomParse(sv);

                    ConstantExpression cExp = Expression.Constant(d);
                    var ccExp = Expression.Convert(cExp, p.Type);


                    //if (sv.Equals("true") || sv.Equals("false"))
                    //{
                    //    cExp = Expression.Constant(sv == "true" ? true : false);
                    //}
                    //else
                    //{
                    //    cExp = Expression.Constant(fu.Val);
                    //}
                    binaryExpression =
                                Expression.MakeBinary(ExpressionType.Equal, Expression.Property(param, fu.Name), ccExp);

                    bex = Expression.And(bex, binaryExpression);
                }
                else if (op == FilterOps.In)
                {

                }
                else if (op == FilterOps.NA || string.IsNullOrEmpty(sn))
                {
                    BinaryExpression grpBinExp = null;
                    foreach (var sUnit in fu.UnitGoup)
                    {
                        var sU = sUnit;
                        var sGrpOps = sU.GroupOps;
                        //var lcExp = ExpBinary<Student>(param, sU.Name, "Equals", sU.Val, true);
                        var p = Expression.Property(param, sU.Name);
                        var pType = p.Type;
                        //var gClass = pType.GetGenericTypeDefinition();
                        //decimal? d = sU.Val.ToNullableDecimal();
                        var d = pType.CustomParse(sU.Val);
                        var cExp = Expression.Constant(d);
                        var ccExp = Expression.Convert(cExp, p.Type);




                        var lcExp = Expression.MakeBinary(ExpressionType.Equal, Expression.Property(param, sU.Name), ccExp);

                        //If Group Ops is not specified, it is the first one on the group.
                        if (grpBinExp == null && string.IsNullOrEmpty(sU.GroupOps)) grpBinExp = lcExp;
                        else grpBinExp = Expression.Or(grpBinExp, lcExp);

                    }

                    bex = Expression.And(bex, grpBinExp);
                }

            }

            var exp = Expression.Lambda<Func<Student, bool>>(bex, param);
            return exp;
        }

        static Expression<Func<T, bool>> ExpContains<T>(string propertyName, string propertyValue, string opStr)
        {
            var parameterExp = Expression.Parameter(typeof(T), "p");
            var propertyExp = Expression.Property(parameterExp, propertyName);
            MethodInfo method = typeof(string).GetMethod(opStr, new[] { typeof(string) });
            var proValue = Expression.Constant(propertyValue, typeof(string));

            var containsMethodExp = Expression.Call(propertyExp, method, proValue);



            return Expression.Lambda<Func<T, bool>>(containsMethodExp, parameterExp);
        }

        static BinaryExpression ExpBinary<T>(ParameterExpression param, string propertyName, string opStr, string propertyValue, bool isTrue)
        {
            var parameterExp = param; //Expression.Parameter(typeof(T), "p");
            var propertyExp = Expression.Property(parameterExp, propertyName);
            MethodInfo method = typeof(string).GetMethod(opStr, new[] { typeof(string) });
            var proValue = Expression.Constant(propertyValue, typeof(string));
            var boolVal = Expression.Constant(isTrue, typeof(bool));
            var containsMethodExp = Expression.Call(propertyExp, method, proValue);

            return Expression.MakeBinary(ExpressionType.Equal, containsMethodExp, boolVal);

        }

        static BinaryExpression ExpGroup<T>(ParameterExpression param, string propertyName1, string propertyValue1, string propertyName2, string propertyValue2)
        {
            var parameterExp = param;
            var propertyExp1 = Expression.Property(parameterExp, propertyName1);
            MethodInfo method1 = typeof(string).GetMethod("Equals", new[] { typeof(string) });
            var proValue1 = Expression.Constant(propertyValue1, typeof(string));
            var boolVal1 = Expression.Constant(true, typeof(bool));
            var containsMethodExp1 = Expression.Call(propertyExp1, method1, proValue1);
            var binExp1 = Expression.MakeBinary(ExpressionType.Equal, containsMethodExp1, boolVal1);

            var propertyExp2 = Expression.Property(parameterExp, propertyName2);
            MethodInfo method2 = typeof(string).GetMethod("Equals", new[] { typeof(string) });
            var proValue2 = Expression.Constant(propertyValue2, typeof(string));
            var boolVal2 = Expression.Constant(true, typeof(bool));
            var containsMethodExp2 = Expression.Call(propertyExp2, method2, proValue2);
            var binExp2 = Expression.MakeBinary(ExpressionType.Equal, containsMethodExp2, boolVal2);

            var Exp = Expression.And(binExp1, binExp2);

            return Exp;
        }

        public static decimal? ToNullableDecimal(this string s)
        {
            decimal value;
            if (!decimal.TryParse(s, out value))
                return null;
            return value;
        }

        public static T ChangeType<T>(this object obj)
        {
            return (T)Convert.ChangeType(obj, typeof(T));
        }

        public static object CustomParse(this Type type, string s)
        {
            if (type == typeof(decimal?) || type == typeof(decimal))
            {
                //var d = s.ChangeType<decimal?>();
                return s.ToNullableDecimal();
            }
            else if (type == typeof(bool?) || type == typeof(bool))
            {
                if (s.ToLower().Equals("true")) return true;
                else if (s.ToLower().Equals("false")) return true;
                else return null;
            }
            else if (type == typeof(int?) || type == typeof(int))
            {
                return (int)s.ToNullableDecimal();
            }

            return s;

        }
    }

    public class GenericFilterAndResults<T>
    {
        private int _count;
        private IDictionary<string, dynamic> _filter;
        private IList<T> _resultList;

        public GenericFilterAndResults()
        {
            _filter = new Dictionary<string, dynamic>();
        }
        public GenericFilterAndResults(IDictionary<string, dynamic> filter)
        {
            _filter = filter;
            ResultList = new List<T>();
        }

        public IDictionary<string, dynamic> Filter
        {
            get
            {
                if (_filter == null)
                {
                    _filter = new Dictionary<string, dynamic>();
                }
                return _filter;
            }
            set
            {
                _filter = value;
            }
        }
        public int Count
        {
            get
            {
                return _count < 0 ? 0 : _count;
            }
            set
            {
                _count = value;
            }
        }

        public IList<T> ResultList
        {
            get
            {
                if (_resultList == null)
                {
                    _resultList = new List<T>();
                }
                return _resultList;
            }
            set
            {
                _resultList = value;
            }
        }

    }
}
