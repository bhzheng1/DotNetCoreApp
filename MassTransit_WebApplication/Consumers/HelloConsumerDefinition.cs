using MassTransit;

namespace MassTransit_WebApplication.Consumers;

public class HelloConsumerDefinition :
    ConsumerDefinition<HelloConsumer>
{
    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<HelloConsumer> consumerConfigurator)
    {
        base.ConfigureConsumer(endpointConfigurator, consumerConfigurator);
        endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 1000));
    }
}