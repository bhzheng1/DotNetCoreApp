using Microsoft.Extensions.Caching.Distributed;
using Second.DataAccess.ApplicationDb;
using Second.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;

namespace Second.DataAccess.Repositories
{
    interface ICacheDataSet
    {

    }
    public class CacheDataSet
    {
        private readonly ApplicationDbContext _context;
        private IDistributedCache _cache;
        public CacheDataSet(IDistributedCache cache, ApplicationDbContext context)
        {
            _cache = cache;
            _context = context;
        }
        public IList<CountryModel> CacheCountries()
        {
            string cacheKey = "countries";
            var bytes = _cache.Get(cacheKey);
            var countries = new List<CountryModel>();
            if (bytes != null)
            {
                using (MemoryStream memoryStream = new MemoryStream(bytes))
                {
                    XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(memoryStream, new XmlDictionaryReaderQuotas());
                    DataContractSerializer ser = new DataContractSerializer(typeof(IList<CountryModel>));
                    countries = (List<CountryModel>)ser.ReadObject(reader, true);
                }
            }
            else
            {
                countries = _context.Countries.Select(_ => new CountryModel { CountryId = _.CountryId, CountryName = _.CountryName }).ToList();
                using (MemoryStream memoryStream = new MemoryStream()) {
                    DataContractSerializer ser = new DataContractSerializer(typeof(IList<CountryModel>));
                    ser.WriteObject(memoryStream, countries);
                    bytes = memoryStream.ToArray();
                }
                _cache.Set(cacheKey, bytes, new DistributedCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(1)));
            }
            return countries;
        }

    }
}
