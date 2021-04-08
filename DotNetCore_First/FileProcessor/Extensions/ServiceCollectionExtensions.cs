using FileProcessor.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FileProcessor.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTransientConfig<T>(this IServiceCollection self) where T : class
        {
            return self.AddTransient<T, T>(_ => _.GetRequiredService<ScopedConfigHolder>().Configuration.Get<T>());
        }
    }
}