using Microsoft.AspNetCore.Mvc;

namespace Second.WebUI.Controllers
{
    public class FormSampleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
