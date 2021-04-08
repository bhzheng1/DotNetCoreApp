using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace One.UI.Controllers
{
    public class CookieController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult WriteCookie(string cookieName, string cookieValue, bool isPersistent)
        {
            var cookie = new CookieOptions();
            cookie.Expires = DateTime.Now.AddDays(1);

            if (isPersistent)
            {
                Response.Cookies.Append(cookieName, cookieValue, cookie);
            }
            else
            {
                Response.Cookies.Append(cookieName, cookieValue);
            }
            ViewBag.message = "Cookies added successfully";

            return View("Index");
        }

        public IActionResult ReadCookie()
        {
            ViewBag.cookieValue = Request.Cookies["username"];
            return View();
        }

    }
}