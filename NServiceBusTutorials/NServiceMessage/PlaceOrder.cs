using System;
using NServiceBus;


namespace NServiceMessage
{
    public class PlaceOrder:ICommand
    {
        public string OrderId { get; set; }
    }
}
