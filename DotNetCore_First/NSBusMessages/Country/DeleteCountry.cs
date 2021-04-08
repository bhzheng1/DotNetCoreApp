using NServiceBus;

namespace NSBusMessages.Country
{
    public class DeleteCountry : ICommand
    {
        public string CountryId { get; set; }
    }
}