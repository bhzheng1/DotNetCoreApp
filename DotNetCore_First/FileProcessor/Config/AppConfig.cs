using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace FileProcessor.Config
{
    public class AppConfig
    {
        public string ApiEndpoint { get; set; }
        public string MonitorPath { get; set; }
        public string ArchivePath { get; set; }
        public IEnumerable<IConfigurationSection> Handlers { get; set; }
    }
}