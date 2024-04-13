using Amazon.SimpleNotificationService;
using Amazon.SQS;
using MassTransit;
using MassTransit_WebApplication.Consumers;
using MassTransit_WebApplication.Contracts;
using MassTransit_WebApplication.Workers;
using Microsoft.Extensions.Options;

namespace MassTransit_WebApplication;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddInMemoryMassTransit<T>(this IServiceCollection services)
    {
        return services.AddMassTransit(
            x =>
            {
                x.SetKebabCaseEndpointNameFormatter();
                x.AddConsumers(typeof(T).Assembly);
                x.AddConsumersFromNamespaceContaining<T>();
                x.UsingInMemory((context, cfg) =>
                {
                    //cfg.AutoStart = true;
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

    //only use the amazon sqs service 
    public static IServiceCollection AddAwsSqsMassTransit(this IServiceCollection services)
    {
        //add host options
        services.AddOptions<MassTransitHostOptions>()
            .Configure(options =>
            {
                options.StartTimeout = TimeSpan.FromMinutes(5);
                options.StopTimeout = TimeSpan.FromMinutes(5);
                options.ConsumerStopTimeout = TimeSpan.FromMinutes(5);
            });
        //add Transport Options
        services.AddOptions<AmazonSqsTransportOptions>()
            .Configure(options =>
            {
                options.Region = "us-east-1";
                options.AccessKey = "test";
                options.SecretKey = "test";
                options.Scope = "Development";
            });
        services.AddMassTransit(
            x =>
            {
                x.SetKebabCaseEndpointNameFormatter();

                x.AddConsumer<HelloWorldMessageConsumer, HelloWorldMessageConsumerDefinition>();
                x.UsingAmazonSqs((context, cfg) =>
                {
                    var url = "http://localhost:4566";
                    var options = context.GetRequiredService<IOptions<AmazonSqsTransportOptions>>().Value;
                    
                    cfg.Host(new Uri("amazonsqs://localhost:4566"), h =>
                    {
                        h.AccessKey(options.AccessKey);
                        h.SecretKey(options.SecretKey);
                        h.Scope(options.Scope);
                        h.Config(new AmazonSQSConfig { ServiceURL = url });
                    });
                    
                    // Configure the message EndpointConvention to bus for sending service
                    var formator = context.GetRequiredService<IEndpointNameFormatter>();
                    EndpointConvention.Map<HelloWorldMessage>(new Uri($"queue:{formator.Message<HelloWorldMessage>()}"));
                    cfg.ConfigureEndpoints(context);
                });
            }); 
        services.AddHostedService<SendWorker>();
        return services;
    }

    public static IServiceCollection AddAwsSqsAndSnsMassTransit<T>(this IServiceCollection services)
    {
        services.AddMassTransit(
            x =>
            {
                x.SetKebabCaseEndpointNameFormatter();
                x.AddConsumers(typeof(T).Assembly);
                x.AddConsumersFromNamespaceContaining<T>();
                x.UsingAmazonSqs((context, cfg) =>
                {
                    var url = "http://localhost:4566";
                    cfg.Host(new Uri("amazonsqs://localhost:4566"), h =>
                    {
                        h.AccessKey("test");
                        h.SecretKey("test");
                        h.Config(new AmazonSimpleNotificationServiceConfig { ServiceURL = url });
                        h.Config(new AmazonSQSConfig { ServiceURL = url });
                    });
                    //cfg.AutoStart = true;
                    cfg.ConfigureEndpoints(context);
                });
            });
        services.AddHostedService<PublishWorker>();
        return services;
    }
    
   
    public static IServiceCollection AddAzureServiceBusMassTransit(this IServiceCollection services)
    {
        services.AddMassTransit(
            x =>
            {
                x.SetKebabCaseEndpointNameFormatter();
                x.AddConsumer<HelloConsumer>(typeof(HelloConsumerDefinition));

                x.UsingAzureServiceBus((context, cfg) =>
                {
                    //cfg.Host("Endpoint=sb://mss-dev-east-demo-sbn.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=FcS3S3pvO486i3E3rajPZEGUm1LEzxvanvyiswIcS9s=");

                    //function?
                    cfg.UseServiceBusMessageScheduler();

                    //send contract hello to topic
                    cfg.Message<HelloMessage>(x => { x.SetEntityName("test-hello"); });


                    //setup the queue name for receive endpoint
                    cfg.ReceiveEndpoint("hello-consumer-queue", e =>
                    {
                        //add one more subscription
                        e.Subscribe("test-hello", "hello-consumer-subscription");
                        e.ConfigureConsumer<HelloConsumer>(context);
                    });

                    //add a subscription endpoint of contract with HelloConsumer
                    cfg.SubscriptionEndpoint<HelloMessage>("hello-sub2", e =>
                    {
                        e.ConfigureConsumer<HelloConsumer>(context);
                    });
                    //cfg.AutoStart = true;
                    cfg.ConfigureEndpoints(context);
                });
            });
        return services;
    }
}