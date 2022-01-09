using Microsoft.AspNetCore.Mvc;
using Second.WebUI.Models;
using System.Text.Json;

namespace Second.WebUI.Controllers
{
    public class ConstantController : Controller
    {
        public ContentResult GetConstants()
        {
            var json = JsonSerializer.Serialize(new Constants(), new JsonSerializerOptions { IncludeFields = true, IgnoreReadOnlyFields = false });
            return Content("var constants = " + json + ";", "text/javascript");
        }
    }
}
