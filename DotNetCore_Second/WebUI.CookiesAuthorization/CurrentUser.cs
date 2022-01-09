namespace WebUI.CookiesAuthorization
{
    /// <summary>
    /// 登录用户的信息
    /// </summary>
    public class CurrentUser
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string? Account { get; set; }
    }

    public static class CookieHelper
    {
        public static void SetCookies(this HttpContext httpContext, string key, string value, int minutes = 30)
        {
            httpContext.Response.Cookies.Append(key, value, new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(minutes)
            });
        }
        public static void DeleteCookies(this HttpContext httpContext, string key)
        {
            httpContext.Response.Cookies.Delete(key);
        }

        public static string? GetCookiesValue(this HttpContext httpContext, string key)
        {
            httpContext.Request.Cookies.TryGetValue(key, out string? value);
            return value;
        }
        public static CurrentUser? GetCurrentUserByCookie(this HttpContext httpContext)
        {
            httpContext.Request.Cookies.TryGetValue("CurrentUser", out string? sUser);
            if (sUser == null)
            {
                return null;
            }
            else
            {
                CurrentUser? currentUser = JsonSerializer.Deserialize<CurrentUser>(sUser);
                return currentUser;
            }
        }
    }
}
