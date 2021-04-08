using CsvHelper.Configuration;
using One.Domain.Models;

namespace One.Domain.Mapping
{
    public class ExportCountryMapper:ClassMap<CountryReadModel>
    {
        public ExportCountryMapper()
        {
            Map(x => x.CountryId).Name("Id").Index(0);
            Map(x => x.CountryName).Name("Country Name").Index(1);
            Map(x => x.RegionId).Name("Region").Index(2);
        }
    }
}
