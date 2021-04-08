using Microsoft.AspNetCore.Mvc;
using WebApplication_API.DbContexts;
using System.Linq;

namespace WebApplication_API.Controllers
{
    //MediatR Pipeline
    [Route("api/[controller]")]
    public class CountriesController : Controller
    {

        private WorldDbContextMySQL _dbContext;

        public CountriesController(WorldDbContextMySQL worldDbContext)
        {
            _dbContext = worldDbContext;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(_dbContext.Country.ToArray());
        }


    }
}