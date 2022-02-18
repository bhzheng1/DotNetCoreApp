using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Principal;

namespace WebApi.CertificateAuthentication.Controllers
{
    [Route("[controller]/[action]")]
    public class UserController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context) {
            
            var userLoginName = User.Identity.Name.ToString();
            HttpContext.Session.SetString("user_id", userLoginName);
        }

        [HttpGet(Name = "UserInfo")]
        public string GetUser() { 
            var user = HttpContext.Session.GetString("user_id");
            return user;
        }
    }
}
