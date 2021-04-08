using System;
using TimeZoneNames;

namespace DotNetCorePdfService.Models
{
    public class FootSettings
    {
        public FootSettings()
        {
            CreatedDayTime = GetDayTime();
        }
        public string ReportId { get; set; }
        public string CreatedDayTime { get; set; }
        public int PageNumber { get; set; }
        public int TotalPage { get; set; }

        public string GetDayTime()
        {
            var timeZoneId = TimeZoneInfo.Local.Id;
            var timeZoneAbb = TimeZoneInfo.Local.IsDaylightSavingTime(DateTime.Now)
                ? TZNames.GetAbbreviationsForTimeZone(timeZoneId, "en-US").Daylight
                : TZNames.GetAbbreviationsForTimeZone(timeZoneId, "en-US").Standard;

            var now = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            var k = DateTime.Now.ToString("'(GMT' K')'");
            return now + " " + timeZoneAbb + " " + " " + k;
        }
    }
}