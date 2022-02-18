using System.Collections.Generic;
using System.Xml;

namespace Second.Utils
{
    public class XmlHelper
    {
        public static bool UpdatePaylineSQL(string xmlPath, string sql, string tab)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlPath);
                XmlNodeList nodeList = doc.SelectNodes("/Filters/Tabs/tab/sql");
                foreach (XmlNode node in nodeList)
                {
                    XmlAttribute nameAttribute = node.ParentNode.Attributes["name"];
                    if (nameAttribute != null)
                    {
                        if (nameAttribute.Value == tab)
                        {
                            node.InnerText = sql;
                        }
                    }
                }
                doc.Save(xmlPath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static IList<FilterUnit> GetPortfolioAndTextSearchFilterList(string xmlPath)
        {
            List<FilterUnit> _filterLists = FilterHelper.ReadXmlToFilterUnitCollection(xmlPath, "filter");
            _filterLists.AddRange(FilterHelper.ReadXmlToFilterUnitCollection(xmlPath, "tab"));
            return _filterLists;
        }
    }
}
