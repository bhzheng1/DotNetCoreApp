using Microsoft.AspNetCore.Mvc;
using WebApplication_API.OtherModels;

namespace WebApplication_API.Controllers
{

    [Route("api/[controller]")]
    public class ExtractingValuesController : Controller
    {

        [HttpGet("add")]
        public double Add([FromQuery] double a, [FromQuery] double b)
        {
            return a + b;
        }

        [HttpGet("sub/a/{a}/b/{b}")]
        public double Sub([FromRoute] double a, [FromRoute] double b)
        {
            return a - b;
        }

        [HttpPost("mul")]
        public double Mul([FromBody] CalculationParameter p)
        {
            return p.A * p.B;
        }

        [HttpPost("div")]
        public double Div([FromForm] CalculationParameter p)
        {
            return p.A / p.B;
        }
    }
}