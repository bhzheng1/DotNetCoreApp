using MassTransit;
using MassTransit_Consumer.Consumers;
using MassTransit_Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace MassTransit_Consumer;
public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddInMemoryMassTransit<T>(this IServiceCollection services)
    {
        return services.AddMassTransit(
            x =>
            {
                x.SetKebabCaseEndpointNameFormatter();
                // x.AddConsumer<HelloConsumer>(typeof(HelloConsumerDefinition));
                // x.AddConsumer<HelloConsumer2>();
                // x.AddConsumer<SubmitOrderConsumer>();
                x.AddConsumersFromNamespaceContaining<T>();

                x.UsingInMemory((context, cfg) =>
                {
                    cfg.AutoStart = true;
                    cfg.ConfigureEndpoints(context);
                });
            });
    }

    public static IServiceCollection AddRabbitMQMassTransit<T>(this IServiceCollection services)
    {
        return services.AddMassTransit(
            x =>
            {
                x.SetKebabCaseEndpointNameFormatter();
                // x.AddConsumer<HelloConsumer>(typeof(HelloConsumerDefinition));
                // x.AddConsumer<HelloConsumer2>();
                // x.AddConsumer<SubmitOrderConsumer>();
                x.AddConsumersFromNamespaceContaining<T>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", "/", h =>
                    {
                        h.Username("admin");
                        h.Password("admin");
                    });
                    cfg.AutoStart = true;
                    cfg.ConfigureEndpoints(context);
                });
            });
    }

    public static IServiceCollection AddAzureServiceBusMassTransit(this IServiceCollection services)
    {
        return services.AddMassTransit(
            x =>
            {
                x.SetKebabCaseEndpointNameFormatter();
                x.AddConsumer<HelloConsumer>(typeof(HelloConsumerDefinition));
                x.AddConsumer<HelloConsumer2>();
                x.AddConsumer<SubmitOrderConsumer>();

                x.UsingAzureServiceBus((context, cfg) =>
                {
                    //cfg.Host("Endpoint=sb://mss-dev-east-demo-sbn.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=FcS3S3pvO486i3E3rajPZEGUm1LEzxvanvyiswIcS9s=");

                    //function?
                    cfg.UseServiceBusMessageScheduler();

                    //send contract hello to topic
                    cfg.Message<Hello>(x => { x.SetEntityName("test-hello"); });


                    //setup the queue name for receive endpoint
                    cfg.ReceiveEndpoint("hello-consumer-queue", e =>
                    {
                        //add one more subscription
                        e.Subscribe("test-hello", "hello-consumer-subscription");
                        e.ConfigureConsumer<HelloConsumer>(context);
                    });

                    //add a subscription endpoint of contract with HelloConsumer2
                    cfg.SubscriptionEndpoint<Hello>("hello-sub2", e =>
                    {
                        e.ConfigureConsumer<HelloConsumer2>(context);
                    });
                    cfg.AutoStart = true;

                    cfg.ConfigureEndpoints(context);
                });
            });
    }
}