using MassTransit_Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace MassTransit_Consumer.Consumers
{
    public class SubmitOrderConsumer :
    IConsumer<SubmitOrder>
    {
        readonly ILogger _logger;

        public SubmitOrderConsumer(ILogger<SubmitOrderConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<SubmitOrder> context)
        {
            _logger.LogInformation("Order Submission Received: {OrderId}", context.Message.OrderId);


            await context.RespondAsync(new OrderSubmissionAccepted
            {

                OrderId = context.Message.OrderId,
                OrderNumber = context.Message.OrderNumber + "accepted"
            });
        }
    }
}

