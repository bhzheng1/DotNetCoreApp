using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.MiddlewareAuthorization.Controllers;

[Authorize]
public class HomeController : Controller
{
    public IActionResult GetUserInfo()
    {
        var userInfo = HttpContext.User;
        CurrentUser user = new CurrentUser()
        {
            Id = Convert.ToInt32(userInfo?.FindFirst("id")?.Value),
            Name = userInfo?.Identity?.Name,
            Account = userInfo?.FindFirst("account")?.Value
        };
        return View(user);
    }
    /// <summary>
    /// 无需登录，匿名访问
    /// </summary>
    /// <returns></returns>
    [AllowAnonymousAttribute]
    public IActionResult About()
    {
        return Content("欢迎来到关于页面");
    }

    /// <summary>
    /// 角色为Admin能访问
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = "Admin")]
    public IActionResult roleData1()
    {
        return Content("Admin能访问");
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = "Admin,Test")]//多个角色用逗号隔开，角色包含有其中一个就能访问
    public IActionResult roleData2()
    {
        return Content("roleData2访问成功");
    }

    /// <summary>
    /// 同时拥有标记的全部角色才能访问
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = "Admin")]
    [Authorize(Roles = "Test")]
    public IActionResult roleData3()
    {
        return Content("roleData3访问成功");
    }
    /// <summary>
    /// 进入授权策略
    /// </summary>
    /// <returns></returns>
    [Authorize(policy: "customPolicy")]
    public IActionResult roleData4()
    {
        return Content("自定义授权策略");
    }
}
