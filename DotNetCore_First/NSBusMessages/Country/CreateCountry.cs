using NServiceBus;

namespace NSBusMessages.Country
{
    public class CreateCountry:ICommand
    {
        public string CountryId { get; set; }
        public string CountryName { get; set; }
        public int RegionId { get; set; }
    }
}
