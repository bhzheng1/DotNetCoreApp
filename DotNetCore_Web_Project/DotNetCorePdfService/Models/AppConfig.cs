using Microsoft.Extensions.Configuration;

namespace DotNetCorePdfService.Models
{
    public class AppConfig
    {
        public IConfigurationSection ApiService { get; set; }
    }
}
