using MassTransit;
using Microsoft.Extensions.Logging;

namespace MassTransit_Contracts.Consumers;

public class HelloConsumer : IConsumer<Hello>
{
    readonly ILogger<HelloConsumer> _logger;
    public HelloConsumer(ILogger<HelloConsumer> logger)
    {
        _logger = logger;
    }
    public Task Consume(ConsumeContext<Hello> context)
    {
        _logger.LogInformation("Hello {Value} from contract consumer", context.Message.Value);
        return Task.CompletedTask;
    }
}
