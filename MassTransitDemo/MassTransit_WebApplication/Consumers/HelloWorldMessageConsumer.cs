using System.Globalization;
using MassTransit;
using MassTransit_WebApplication.Contracts;

namespace MassTransit_WebApplication.Consumers;

public  class HelloWorldMessageConsumer(ILogger<HelloWorldMessageConsumer> logger) : IConsumer<HelloWorldMessage>
{
    public Task Consume(ConsumeContext<HelloWorldMessage> context)
    {
        logger.LogInformation("Received {value1} at {value2}",context.Message.Value, DateTime.Now.ToString(CultureInfo.InvariantCulture) );
        return Task.CompletedTask;
    }
}