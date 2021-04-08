using NServiceBus;

namespace NServiceMessage
{
    public class OrderPlaced : IEvent
    {
        public string OrderId { get; set; }
    }
}