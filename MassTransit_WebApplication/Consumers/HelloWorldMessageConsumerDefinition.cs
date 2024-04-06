using MassTransit;

namespace MassTransit_WebApplication.Consumers;

public class HelloWorldMessageConsumerDefinition : ConsumerDefinition<HelloWorldMessageConsumer>
{
    /// <summary>
    /// if the consumer's format is NOT like {messageName}Consumer, need to specify the Endpointname same as queue name in the sender
    /// </summary>
    /// <param name="endpointConfigurator"></param>
    /// <param name="consumerConfigurator"></param>
    // public HelloWorldMessageConsumerDefinition(IEndpointNameFormatter formatter)
    // {
    //     EndpointName = formatter.Message<HelloWorldMessage>();
    // }
    
    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<HelloWorldMessageConsumer> consumerConfigurator)
    {
        base.ConfigureConsumer(endpointConfigurator, consumerConfigurator);
        endpointConfigurator.ConfigureConsumeTopology = false; // don't create subscription
    }
}