using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCorePdfService.Models
{
    public class GeneratePdfRequest
    {
        public string LogoBase64String { get; set; }
        public string ImageExtension { get; set; }
        public string ReportTitle { get; set; }
        public string ReportFor { get; set; }
        public string ReportAsOf { get; set; }
        public string ReportBasis { get; set; }
        public string FrontBarColor { get; set; }
        public string BackBarColor { get; set; }
        public string HeaderTitleColor { get; set; }
        public string ReportId { get; set; }
        public string HtmlString { get; set; }
    }
    public class GeneratePdfFromHtmlRequest
    {
        public bool Landscape { get; set; } = true;
        public string HtmlString { get; set; }
    }
}
