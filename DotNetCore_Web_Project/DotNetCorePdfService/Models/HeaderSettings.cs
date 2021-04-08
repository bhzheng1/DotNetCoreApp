namespace DotNetCorePdfService.Models
{
    public class HeaderSettings
    {
        public HeaderSettings()
        {
            FrontBarColor = "#304048";
            BackBarColor = "#F37921";
            HeaderTitleColor = "#FFFFFF";
        }
        public string ReportTitle { get; set; }
        public string ReportFor { get; set; }
        public string ReportAsOf { get; set; }
        public string ReportBasis { get; set; }
        public string FrontBarColor { get; set; }
        public string BackBarColor { get; set; }
        public string HeaderTitleColor { get; set; }
    }
}