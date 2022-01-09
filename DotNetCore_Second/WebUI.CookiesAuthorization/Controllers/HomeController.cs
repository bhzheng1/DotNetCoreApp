using Microsoft.AspNetCore.Mvc;

namespace WebUI.CookiesAuthorization.Controllers
{
    [ActionAuthorizationFilterAttribute]
    public class HomeController : Controller
    {
        public IActionResult GetUserInfo()
        {
            //从cookie获取用户信息
            CurrentUser? user = HttpContext.GetCurrentUserByCookie();
            return View(user);
        }
    }
}
