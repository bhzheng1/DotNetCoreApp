using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using NServiceMessage;

namespace Billing
{
    public class OrderPlacedHandler:IHandleMessages<OrderPlaced>
    {
        private static readonly ILog Log = LogManager.GetLogger<OrderPlacedHandler>();
        public Task Handle(OrderPlaced message, IMessageHandlerContext context)
        {
            Log.Info($"Received OrderPlaced, OrderId = {message.OrderId} - Charging credit card...");
            var orderBilled = new OrderBilled(){OrderId = message.OrderId};
            return context.Publish(orderBilled);
        }
    }
}
