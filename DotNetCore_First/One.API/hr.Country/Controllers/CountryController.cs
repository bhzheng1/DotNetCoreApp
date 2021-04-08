using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSBusMessages;
using NSBusMessages.Country;
using One.Business.hr.Country;
using One.Domain.Models;
using One.Domain.ServiceRequest;
using One.Domain.ServiceResponse;

namespace One.API.hr.Country.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]")]
    public class CountryController : Controller
    {
        private readonly ICountryBusiness _countryBusiness;
        private readonly ILogger<CountryController> _logger;

        public CountryController(ICountryBusiness countryBusiness,ILogger<CountryController> logger)
        {
            _countryBusiness = countryBusiness;
            _logger = logger;
        }

        [HttpGet("GetAll")]
        public async Task<ServiceResponse<IEnumerable<CountryReadModel>>> GetAll(CountryRequest request)
        {
            return await _countryBusiness.GetAll(request);
        }
        

        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public Task<CommandResult<string>> CreateCountry([FromBody] CreateCountry value)
        {
            return null;
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
