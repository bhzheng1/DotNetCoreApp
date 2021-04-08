using One.Domain.Models;
using One.Domain.ServiceRequest;
using One.Domain.ServiceResponse;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;

namespace One.API.Client
{
    public interface IOneApiClient
    {
        Task<Stream> CountryDownloadAll(CountryDownloadRequest request);
        Task<ServiceResponse<IEnumerable<CountryReadModel>>> GetAllCountries(CountryRequest request);
    }
    public class OneApiClient : IOneApiClient
    {
        private readonly HttpClient _client;
        public OneApiClient(HttpClient client)
        {
            _client = client;
        }
        public async Task<ServiceResponse<IEnumerable<CountryReadModel>>> GetAllCountries(CountryRequest request)
        {
            return await _client.GetAsync<ServiceResponse<IEnumerable<CountryReadModel>>>(MakeRelativeUri("api/Country/GetAll", request));
        }

        public async Task<Stream> CountryDownloadAll(CountryDownloadRequest request)
        {
            return await _client.GetStreamAsync(MakeRelativeUri("api/Country/DownloadAllCountry", request));
        }

        private string MakeRelativeUri(string relative, object param = null) => relative + (param == null ? string.Empty : QueryString.Make(param));
    }
}