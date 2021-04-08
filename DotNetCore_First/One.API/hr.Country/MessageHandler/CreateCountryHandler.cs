using System;
using System.Threading.Tasks;
using NSBusMessages.Country;
using NServiceBus;

namespace One.API.hr.Country.MessageHandler
{
    public class CreateCountryHandler:IHandleMessages<CreateCountry>
    {
        public Task Handle(CreateCountry message, IMessageHandlerContext context)
        {
            return null;
        }
    }
}
