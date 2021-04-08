using Microsoft.AspNetCore.Mvc;


namespace One.UI.Controllers
{
    [Route("FileDownload")]
    public class FileDownloadController:Controller
    {
        [HttpGet("Index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
