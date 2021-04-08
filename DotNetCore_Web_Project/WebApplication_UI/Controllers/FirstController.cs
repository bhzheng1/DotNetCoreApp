using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelClassLibrary;

namespace WebApplication_UI.Controllers
{
    public class FirstController : Controller
    {
        const string SessionName = "_Name";
        const string SessionAge = "_Age";
        private readonly IHttpContextAccessor _httpContextAccessor;
        public FirstController(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }
        //ViewData is a Dictionary of key and value.
        // ViewBag is a wrapper of ViewData.Is a dynamic object. 
        //Both are used to pass data from Controller to View. 
        //Both are available only for Current Request. They will be destroyed on redirection.
        public IActionResult Index() {
            ViewData["Title"] = "Foo";
            ViewBag.Title = "Bar";

            ViewBag.Name = HttpContext.Session.GetString(SessionName);
            ViewBag.Age = HttpContext.Session.GetInt32(SessionAge);
            return View();
        }

        //TempData is derived from the TempDataDictionary class and is basically a Dictionary object
        //Data is stored as Object in TempData
        //TempData can be used for passing value from Controller to View and also from Controller to Controller
        //TempData is available for Current and Subsequent Requests. It will not be destroyed on redirection.
        public IActionResult IndexRedirect()
        {
            TempData["Message"] = "Hello Message!";
            return new RedirectResult("~/Second");
        }

        //Session is a property of Controller class whose type is HttpSessionStateBase.
        //public HttpSessionStateBase Session { get; }
        //The Session is also used to pass data within the ASP.NET MVC application and Unlike TempData
        //it persists for its expiration time (by default session expiration time is 20 minutes but it can be increased).
        //The Session is valid for all requests, not for a single redirect.
        //It also requires typecasting for getting data and checks for null values to avoid error.

        public IActionResult IndexSession() {
            HttpContext.Session.SetString(SessionName, "Merrick");
            HttpContext.Session.SetInt32(SessionAge, 24);
            ViewData["Message"] = "Asp.Net Core !!!.";
            return View();
        }

        //Cookies are key-value pair collections where we can read, write and delete using key.
        //In ASP.NET, we can access cookies using httpcontext.current
        //in ASP.NET Core, we can access cookies using IHttpContextAccessor
        public IActionResult IndexCookie() {
            //read cookie from Request object (prefer)  
            string cookieValueFromReq = Request.Cookies["Key"];

            //read cookie from IHttpContextAccessor () 
            string cookieValueFromContext = _httpContextAccessor.HttpContext.Request.Cookies["key"];

            //set the key value in Cookie  
            Set("kay", "Hello from cookie", 10);
            //Delete the cookie object  
            Remove("Key");
            return View();
        }

        public IActionResult GameShow()
        {
            var player1 = new Player() {
                Name="aa",
                Rank=1
            };
            var player2 = new Player()
            {
                Name = "bb",
                Rank = 2
            };
            var game = new Game()
            {
                City = "sh",
                Player1=player1,
                Player2=player2,
            }; 
            return View(game);
        }

        [HttpGet]
        public IActionResult GameCreate()
        {
            return View();
        }
        //两种使用ModeBinder的方法:
        //一种是直接修改Game，添加[ModelBinder(BinderType = typeof(GameModelBinder))] attribute ---该方法意味着所有game都要从这个form创建，所有不推荐这种方法
        //第二种方法是添加[ModelBinder(BinderType = typeof(GameModelBinder))]到action 的parameter上 ---意味着只有调用该方法时才从这个form创建game
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GameCreate([ModelBinder(BinderType = typeof(GameModelBinder))] Game game)
        {
            //server side validation
            if (ModelState.IsValid == true)
            {
                var city = game.City;
                var p1 = game.Player1.Name;
                var r1 = game.Player1.Rank;
                var p2 = game.Player2.Name;
                var r2 = game.Player2.Rank;
                // business logic ...
                return View("GameShow", game);
            }
            else
            {
                // let user re-input the data
                return View();
            }
        }

        [HttpGet]
        public ActionResult CreateEmployee()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                RedirectToAction("Index");
            }

            return View();
        }

        //return different view result: JsonResult, Json, View
        [HttpGet]
        public IActionResult GetAllProducts(string r)
        {
            ViewData["Heading"] = "All Products";
            var products = new List<Product>();
            products.Add(new Product { ID = 101, Name = "Apple", Price = 1.1 });
            products.Add(new Product { ID = 202, Name = "Bike", Price = 2.2 });
            products.Add(new Product { ID = 303, Name = "Calculator", Price = 3.3 });

            return (r.ToLower()) switch
            {
                "jr" => new JsonResult(products),
                "j" => Json(products),
                _ => View("ShowProduct", products),
            };
        }

        /// <summary>  
        /// Get the cookie  
        /// </summary>  
        /// <param name="key">Key </param>  
        /// <returns>string value</returns>  
        public string Get(string key)
        {
            return Request.Cookies[key];
        }
        /// <summary>  
        /// set the cookie  
        /// </summary>  
        /// <param name="key">key (unique indentifier)</param>  
        /// <param name="value">value to store in cookie object</param>  
        /// <param name="expireTime">expiration time</param>  
        public void Set(string key, string value, int? expireTime)
        {
            CookieOptions option = new CookieOptions();
            if (expireTime.HasValue)
                option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
            else
                option.Expires = DateTime.Now.AddMilliseconds(10);
            Response.Cookies.Append(key, value, option);
        }
        /// <summary>  
        /// Delete the key  
        /// </summary>  
        /// <param name="key">Key</param>  
        public void Remove(string key)
        {
            Response.Cookies.Delete(key);
        }
    }
}
