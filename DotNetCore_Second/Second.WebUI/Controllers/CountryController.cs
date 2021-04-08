using Microsoft.AspNetCore.Mvc;
using Second.DataAccess.Repositories;

namespace Second.WebUI.Controllers
{
    public class CountryController : Controller
    {
        private readonly ICountryRepository _countryRepository;
        public CountryController(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }
        public IActionResult Index()
        {
            var countries = _countryRepository.GetCountries();
            return View(countries);
        }
    }
}
