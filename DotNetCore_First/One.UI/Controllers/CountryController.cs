using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using One.API.Client;

namespace One.UI.Controllers
{
    [Route("Country")]
    public class CountryController:Controller
    {
        private readonly IOneApiClient _apiClient;
        private readonly ActivityTimeoutSettings _activityTimeoutSettings;

        public CountryController(IOneApiClient apiClient,IOptions<ActivityTimeoutSettings> activityTimeoutSettings)
        {
            _activityTimeoutSettings = activityTimeoutSettings.Value;
            _apiClient = apiClient;
        }

        [HttpGet("index")]
        public async Task<IActionResult> Index()
        {
            var result = await _apiClient.GetAllCountries(null);
            var model = result.Result;
            return View(model);
        }
    }
}
