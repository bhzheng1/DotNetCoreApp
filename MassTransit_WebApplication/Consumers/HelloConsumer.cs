using MassTransit;
using MassTransit_WebApplication.Contracts;

namespace MassTransit_WebApplication.Consumers;

public class HelloConsumer(ILogger<HelloConsumer> logger)
    : IConsumer<HelloMessage>
{
    public Task Consume(ConsumeContext<HelloMessage> context)
    {
        logger.LogInformation("Hello {Value}", context.Message.Value);
        return Task.CompletedTask;
    }
}