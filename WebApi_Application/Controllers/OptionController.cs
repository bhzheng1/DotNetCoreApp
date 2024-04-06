using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi_Application.Models;

namespace WebApi_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OptionController : ControllerBase
    {
        private readonly EmailOptions _options;
        public OptionController(IOptions<EmailOptions> options)
        {
            _options = options.Value;
        }


        [HttpGet(nameof(Options))]
        public IActionResult Options()
        {
            return Ok(_options);
        }
    }
}

