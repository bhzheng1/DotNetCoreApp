using System;
using System.IO;
using System.Linq;

namespace Second.Utils
{
    public static class Test
    {
        static void String2Linq(string s)
        {
            var strFilter = s ?? "strFilter%3D%5B%7B%22Name%22%3A%22fy%22%2C%22Val%22%3A%222022%22%2C%22DisplayName%22%3A%22Fiscal%20Year%22%2C%22Ops%22%3A1%7D%2C%7B%22Name%22%3A%22council%22%2C%22Val%22%3A%22202201%2C202205%22%2C%22DisplayName%22%3A%22Council-NPARS%22%2C%22Ops%22%3A1%7D%2C%7B%22Name%22%3A%22DivisionID%22%2C%22Val%22%3A%221%20%3A%20DMID%22%2C%22DisplayName%22%3A%22Division%22%2C%22Ops%22%3A1%7D%2C%7B%22Name%22%3A%22NPARSStatusId%22%2C%22Val%22%3A%221%20%3A%20Pending%2C2%20%3A%20PO%20Rec%2C3%20%3A%20PO%20Not%20Rec%2C4%20%3A%20BC%20Rec%2C5%20%3A%20BC%20Not%20Rec%2C8%20%3A%20DIV%20Not%20Rec%2C10%20%3A%20GMO%20Disapproved%2C12%20%3A%20GMS%20Disapproved%2C18%20%3A%20Pending%20Sup%2C31%20%3A%20DIV%20Not%20Rec%20Sup%2C32%20%3A%20GMO%20Approved%20Sup%22%2C%22DisplayName%22%3A%22NPARS%20Status%22%2C%22Ops%22%3A1%7D%5D%26strNewSearch%3Dyes";
            var strFilterGrid = System.Net.WebUtility.UrlDecode(strFilter);
            var strArray = strFilterGrid.Split('&');
            strFilterGrid = strArray[0].Replace("strFilter=", "");
            var filterLst = strFilterGrid.JSONDeserialize<FilterUnit>();
            var path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var xmlPath = Path.Combine(path, "FilterXml.xml");

            var xmlLst = XmlHelper.GetPortfolioAndTextSearchFilterList(xmlPath);
            System.Linq.Expressions.Expression<Func<Student, bool>> exp =
                    filterLst.FilterInCollections<Student>(xmlLst);

            var stu = Student.studentList.Where(exp.Compile()).ToList();
        }
    }
}
