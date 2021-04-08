using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace One.API.Utility
{
    public class HostSettings
    {
        public string ApiHostUrl { get; set; }
        public string WatermarkHostUrl { get; set; }
        public string GeoLocationUrl { get; set; }
        public string GeoLocationLicenseKey { get; set; }
    }
}
