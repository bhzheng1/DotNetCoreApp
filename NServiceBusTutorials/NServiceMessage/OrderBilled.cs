using NServiceBus;

namespace NServiceMessage
{
    public class OrderBilled : IEvent
    {
        public string OrderId { get; set; }
    }
}