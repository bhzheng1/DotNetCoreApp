using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using One.UI.Models;
namespace One.UI.Controllers
{
    public class SessionSampleController : Controller
    {
        public IActionResult Index()
        {
            //get sessionSample from session
            var sessionSample = JsonConvert.DeserializeObject<SessionModel>(HttpContext.Session.GetString("SessionSample"));
            return View();
        }
    }
}