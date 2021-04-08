using System.Linq;
using System.Security.Claims;

namespace One.UI.HttpRequestHelper
{
    public static class ClaimsPrincipalExtension
    {
        public static string GetImpersonateUserName(this ClaimsPrincipal principal)
        {
            var impersonateUserName = principal.Claims.FirstOrDefault(c => c.ValueType == "SimulatedUserEmailId");
            return impersonateUserName?.Value;
        }

        public static string GetPageNameToRedirectUser(this ClaimsPrincipal principal)
        {
            var impersonateUserName = principal.Claims.FirstOrDefault(c => c.ValueType == "SimulatedUserPageName");
            return impersonateUserName?.Value;
        }

        public static string GetClientName(this ClaimsPrincipal principal)
        {
            var clientName = principal.Claims.FirstOrDefault(c => c.ValueType == "ClientName");
            return clientName?.Value;
        }

        //public static string Username(this ClaimsPrincipal principal) =>
        //    principal.Claims.FirstOrDefault(_ => _.Type == CoreAuth.CoreAuthClaimTypes.UserName)?.Value;

        public static string FullName(this ClaimsPrincipal principal) =>
            principal.Claims.FirstOrDefault(_ => _.Type == ClaimTypes.GivenName)?.Value;
    }
}
