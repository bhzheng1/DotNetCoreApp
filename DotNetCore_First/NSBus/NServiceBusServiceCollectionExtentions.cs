using Microsoft.Extensions.DependencyInjection;

namespace NSBus
{
    public static class NServiceBusServiceCollectionExtensions
    {
        public static INServiceBusBuilder AddNServiceBus(this IServiceCollection services)
        {
            return new DefaultNServiceBusBuilder(services);
        }
    }
}