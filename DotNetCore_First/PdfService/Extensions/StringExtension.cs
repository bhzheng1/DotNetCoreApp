using System.Drawing;
using System.Text.RegularExpressions;

namespace DotNetCorePdfService.Services
{
    public static class StringExtension
    {
        public static bool IsValidFormatHtmlColor(this string inputColor)
        {
            //regex from http://stackoverflow.com/a/1636354/2343
            if (Regex.Match(inputColor, "^#(?:[0-9a-fA-F]{3}){1,2}$").Success)
                return true;

            var result = Color.FromName(inputColor);
            return result.IsKnownColor;
        }
    }
}