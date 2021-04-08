using System.Threading.Tasks;
using DotNetCorePdfService.Models;
using DotNetCorePdfService.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCorePdfService.Controllers
{
    [Route("Home")]
    public class HomeController : Controller
    {
        private readonly IPdfService _pdfService;
        public HomeController(IPdfService pdfService)
        {
            _pdfService = pdfService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost("GeneratePdf")]
        public async Task<IActionResult> GeneratePdf(GeneratePdfRequest request)
        {
            return File(await _pdfService.GeneratePdf(request),"application/pdf");
        }
    }

}
