using Microsoft.AspNetCore.Mvc;

namespace WebUI.CookiesAuthorization.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        //登录提交
        [HttpPost]
        public IActionResult LoginSub(IFormCollection fromData)
        {
            string userName = fromData["userName"].ToString();
            string passWord = fromData["password"].ToString();
            //真正写法是读数据库验证
            if (userName == "test" && passWord == "123456")
            {
                #region 传统session/cookies
                //登录成功,记录用户登录信息
                CurrentUser currentUser = new CurrentUser()
                {
                    Id = 123,
                    Name = "测试账号",
                    Account = userName
                };

                //写cookies
                HttpContext.SetCookies("CurrentUser", JsonSerializer.Serialize(currentUser));
                #endregion

                //跳转到首页
                return RedirectToAction("GetUserInfo", "Home");

            }
            else
            {
                TempData["err"] = "账号或密码不正确";
                //账号密码不对,跳回登录页
                return RedirectToAction("Login", "Account");
            }
        }
        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public IActionResult LogOut()
        {
            HttpContext.DeleteCookies("CurrentUser");
            return RedirectToAction("Login", "Account");
        }
    }
}
