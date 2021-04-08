using System.Collections.Generic;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using One.API.Client;
using One.Domain.Models;
using One.Domain.ServiceRequest;
using One.Domain.ServiceResponse;

namespace One.UI.APICaller
{
    [Produces("application/json")]
    [Route("api/CountryService")]
    public class CountryServiceController:Controller
    {
        private readonly IOneApiClient _apiClient;

        public CountryServiceController(IOneApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        [HttpGet("GetAll")]
        public async Task<ServiceResponse<IEnumerable<CountryReadModel>>> GetAllCountries(CountryRequest request)
        {
            return await _apiClient.GetAllCountries(request);
        }

        [HttpGet("DownloadAllCountry")]
        public async Task<IActionResult> DownloadAll(CountryDownloadRequest request)
        {
            var stream = new CryptoStream(await _apiClient.CountryDownloadAll(request),new FromBase64Transform(),CryptoStreamMode.Read);
            return File(stream, MediaTypeNames.Text.Plain, "fileName.csv");
        }
    }
}
