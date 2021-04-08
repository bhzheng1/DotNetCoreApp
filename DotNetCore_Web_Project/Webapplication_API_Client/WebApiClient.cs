using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using ModelClassLibrary.ServiceRequest;
using ModelClassLibrary.ResponseModel;
using ModelClassLibrary;
using One.API.Client;

namespace Webapplication_API_Client
{
    public interface IWebApiClient
    {
        Task<Stream> CountryDownloadAll(CountryDownloadRequest request);
        Task<ServiceResponse<IEnumerable<CountryModel>>> GetAllCountries(CountryRequest request);
    }
    public class WebApiClient : IWebApiClient
    {
        private readonly HttpClient _client;
        public WebApiClient(HttpClient client)
        {
            _client = client;
        }
        public async Task<ServiceResponse<IEnumerable<CountryModel>>> GetAllCountries(CountryRequest request)
        {
            return await _client.GetAsync<ServiceResponse<IEnumerable<CountryModel>>>(MakeRelativeUri("api/Country/GetAll", request));
        }

        public async Task<Stream> CountryDownloadAll(CountryDownloadRequest request)
        {
            return await _client.GetStreamAsync(MakeRelativeUri("api/Country/DownloadAllCountry", request));
        }

        private string MakeRelativeUri(string relative, object param = null) => relative + (param == null ? string.Empty : QueryString.Make(param));
    }
}