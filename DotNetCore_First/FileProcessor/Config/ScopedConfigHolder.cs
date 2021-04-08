using Microsoft.Extensions.Configuration;

namespace FileProcessor.Config
{
    public class ScopedConfigHolder
    {
        public IConfiguration Configuration { get; set; }
    }
}
