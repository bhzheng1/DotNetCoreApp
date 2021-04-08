using Microsoft.Extensions.DependencyInjection;
using NSBus;

namespace One.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static INServiceBusBuilder AddNServiceBus(this IServiceCollection services)
        {
            return new DefaultNServiceBusBuilder(services);
        }
    }
}