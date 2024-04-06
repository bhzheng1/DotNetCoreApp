using MassTransit;
using Microsoft.Extensions.Logging;
using MassTransit_Contracts;

namespace MassTransit_Consumer.Consumers
{
    public class HelloConsumer :
        IConsumer<Hello>
    {
        readonly ILogger<HelloConsumer> _logger;
        public HelloConsumer(ILogger<HelloConsumer> logger)
        {
            _logger = logger;
        }
        public Task Consume(ConsumeContext<Hello> context)
        {
            _logger.LogInformation("Hello {Value}", context.Message.Value);
            return Task.CompletedTask;
        }
    }
    public class HelloConsumer2 : IConsumer<Hello>
    {
        readonly ILogger<HelloConsumer2> _logger;
        public HelloConsumer2(ILogger<HelloConsumer2> logger)
        {
            _logger = logger;
        }
        public Task Consume(ConsumeContext<Hello> context)
        {
            _logger.LogInformation("Hello {Value} 2", context.Message.Value);
            return Task.CompletedTask;
        }
    }
}