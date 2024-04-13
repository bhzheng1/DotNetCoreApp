using MassTransit;
using MassTransit_Contracts;

namespace MassTransit_Publisher;
public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddInMemoryMassTransit(this IServiceCollection services)
    {
        return services.AddMassTransit(
            x =>
            {
                x.SetKebabCaseEndpointNameFormatter();
                x.AddRequestClient<SubmitOrder>();
                x.UsingInMemory((context, cfg) =>
                {
                    //this will add extra topic in azure service bus
                    cfg.Message<Hello>(x => { x.SetEntityName("test-hello"); });
                    cfg.AutoStart = true;
                });
            });
    }
    public static IServiceCollection AddAzureServiceBusMassTransit(this IServiceCollection services)
    {
        return services.AddMassTransit(
            x =>
            {
                x.SetKebabCaseEndpointNameFormatter();
                x.AddRequestClient<SubmitOrder>();
                x.UsingAzureServiceBus((context, cfg) =>
                {
                    //cfg.Host("Endpoint=sb://mss-dev-east-demo-sbn.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=FcS3S3pvO486i3E3rajPZEGUm1LEzxvanvyiswIcS9s=");

                    //this will add extra topic in azure service bus
                    cfg.Message<Hello>(x => { x.SetEntityName("test-hello"); });
                    cfg.AutoStart = true;
                });
            });
    }
}