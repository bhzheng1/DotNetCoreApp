using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using NServiceMessage;

namespace Shipping
{
    public class OrderPlacedAndBilledHandler:IHandleMessages<OrderPlaced>,IHandleMessages<OrderBilled>
    {
        private static readonly ILog Log = LogManager.GetLogger<OrderPlacedAndBilledHandler>();
        public Task Handle(OrderPlaced message, IMessageHandlerContext context)
        {
            Log.Info($"Received OrderPlaced, OrderId = {message.OrderId} - shipping...");
            return Task.CompletedTask;
        }

        public Task Handle(OrderBilled message, IMessageHandlerContext context)
        {
            Log.Info($"Received BillPlaced, OrderId = {message.OrderId} -shipping...");
            return Task.CompletedTask;
        }
    }
}
