using Microsoft.AspNetCore.Mvc;

namespace WebUI.SessionAuthorization.Controllers;

[ActionAuthorizationFilterAttribute]
public class HomeController : Controller
{
    public IActionResult GetUserInfo()
    {
        //从cookie获取用户信息
        CurrentUser? user = HttpContext.GetCurrentUserBySession();
        return View(user);
    }
}
